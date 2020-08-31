using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

#pragma warning disable 0162
public static class JavaScriptGenerator {
  private const string TYPE_CONSECUTOR = "Consecutor";
  private const string TYPE_RETURN = "Result";
  private static readonly Regex _validFunctionCharsOnlyRegex = new Regex("^[0-9a-z_]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
  private static readonly Regex _functionNameRegex = new Regex("F\\(\"(?<Name>[a-z_]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
  private static readonly Regex _cNameRegex = new Regex("C\\(\"(?<Name>[a-z_]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
  private static readonly Regex _sIdNameRegex = new Regex("SId\\(\"C\\(\\\\\"(?<Name>[a-z_]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
  private static readonly Regex _codeVarNameRegex = new Regex("CodeVarName\\(\"(?<Name>.+)\"\\)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
  private static readonly Regex _templarBlockResultVarNameRegex = new Regex("(TemplarBlockResultVarName)\\((?<Number>\\d+)\\)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
  private static readonly Regex _codeLocationRegex = new Regex("CodeLocation\\((?<Position>[\\d\\-,]+)\\)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
  private static readonly Regex _tupRegex = new Regex("^Tup\\d+$", RegexOptions.Compiled);

  private static int _tempVarCounter = 0;
  private static readonly HashSet<string> _ignoredDiscardTypes = new HashSet<string>(){ "Unstackify", "Argument" };
  private static readonly HashSet<string> _noReturnConsecutorChild = new HashSet<string>(){ "Return", "Stackify", "Destroy" };
  private static readonly HashSet<string> _anonymousVarNames = new HashSet<string>(){ "AnonymousSubstructMemberName", "TemplarTemporaryVarName", "TemplarBlockResultVarName", };

  private static readonly HashSet<string> _functionLocalVarNames = new HashSet<string>();

  private static NamingHelper _namingHelper = null;
  private static Dictionary<string, string> _protoNameByStructName = new Dictionary<string, string>();

  private const bool VERBOSE = false;
  private const bool DEBUG = true;

  public static IEnumerable<string> Generate(IProgram program) {
    _namingHelper = new NamingHelper();

    var level = 1;
    yield return "'use strict';\r\n";
    yield return "(function(){\r\n";

    foreach(var headerCode in CreateHeader())
      yield return headerCode;

    InitPrototypes(program);

    foreach(var function in program.Functions) {
      foreach(var functionCode in GenerateFunction(function, null, level))
        yield return functionCode;
    }

    foreach(var createPrototypesCode in CreatePrototypes(program))
      yield return createPrototypesCode;
    //foreach(var implementationPointerCode in CreateImplementationPointers())
      //yield return implementationPointerCode;

    yield return LevelString(level);
    yield return "console.log(main());\r\n";
    yield return "})();";
  }

  private static string LevelString(int level) {
    return new String(' ', level * 2);
  }

  private static string GetUniqueVarName() {
    return $"__temp_{_tempVarCounter++}";
  }

  private static string ParameterName(string functionName, int argumentIndex) {
    // Make name camelcase-like, to please simple formatters
    var firstChar = char.ToLowerInvariant(functionName[0]);
    var restOfFunctionName = new string(functionName.Skip(1).ToArray());
    return $"{firstChar}{restOfFunctionName}_p_{argumentIndex}";
  }

  private static string ExtractCodeVarName(string str) {
    return str;

    var match = _codeVarNameRegex.Match(str);
    if (match.Success)
      return $"{match.Groups["Name"].Value}";

    match = _templarBlockResultVarNameRegex.Match(str);
    if (match.Success)
      return $"_blockResult_{match.Groups["Number"].Value}";

    if (_anonymousVarNames.Any(x => str.EndsWith(x))) {
      var matches = _codeLocationRegex.Matches(str);
      var lastMatch = matches.LastOrDefault();
      if (lastMatch != null) {
        var codeLocation = lastMatch.Groups["Position"].Value;
        return $"_templar_CL_{codeLocation.Replace(",", "_").Replace("-", "m")}";
      }
    }
      
    return null;
  }

  private static string ExtractCodeVarName(ILocal local) {
    var number = local.Id.Number;
    var value = local.Id.OptName.Value;
    //Console.WriteLine($"DEBUG: Could not extract code var name from '{number} - {value} - {value == null}'");

    if (value != null) {
      var codeVarName = ExtractCodeVarName(value);
      if (codeVarName != null)
        return $"{codeVarName}_{number}";
    }

    var numberName = $"__blockVar_{number}";
    _functionLocalVarNames.Add(numberName);
    return numberName;
  }

  private static string CName(string cName) {
    return _namingHelper.FixCName(cName);
    var match = _cNameRegex.Match(cName);
    if (match.Success)
      return match.Groups["Name"].Value;
    else
      return cName;
  }

  private static string SIdName(string sIdName) {
    var match = _sIdNameRegex.Match(sIdName);
    if (match.Success) {
      return match.Groups["Name"].Value;
    }
    else
      return null;
  }

  private static string SaneFunctionName(string insaneFunctionName) {
    return _namingHelper.FixFunction(insaneFunctionName);
    var match = _functionNameRegex.Match(insaneFunctionName);
    if (match.Success) {
      return match.Groups["Name"].Value;
    }

    var functionNameWithoutPrefix = insaneFunctionName.Substring(3, insaneFunctionName.Length - 5);
    if (_validFunctionCharsOnlyRegex.IsMatch(functionNameWithoutPrefix))
      return functionNameWithoutPrefix;
    
    var base64Name = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(functionNameWithoutPrefix)).Replace("=", "").Replace("+", "_plus_");
    return "__" + base64Name;
  }


  private static void InitPrototypes(IProgram program) {
    foreach (var _ in CreatePrototypes(program)) { }
  }

  private static IEnumerable<string> CreatePrototypes(IProgram program) {
    _protoNameByStructName.Clear();

    foreach (var @struct in program.Structs) {
      foreach (var edge in @struct.Edges) {
        var structName = CName(edge.StructName.Name);
        var interfaceName = CName(edge.InterfaceName.Name);

        string protoName = $"_proto_{interfaceName}_{structName}";
        _protoNameByStructName.Add(structName, protoName);
        yield return $"const {protoName} = {{\r\n";

        foreach (var entry in edge.Methods) {
          var @method = entry.Method;
          var interfaceFunctionName = _namingHelper.FixFunction(@method.Prototype.Name);
          var overrideName = _namingHelper.FixFunction(entry.Override.Name);
          yield return $" _ic_{interfaceName}_{interfaceFunctionName}: {overrideName},\r\n";
        }
        yield return "\r\n};\r\n";
      }
    }
    yield break;
  }

  private static IEnumerable<string> GenerateFunction(IFunction function, string contentOfFunctionName, int level) {
    _functionLocalVarNames.Clear();
    var functionName = function.Prototype.Name;
    //var sid = SIdName(functionName);
    var saneFunctionName = SaneFunctionName(functionName);
    //if (sid != null)
      //saneFunctionName = $"struct_{sid}_{saneFunctionName}";

    if (VERBOSE) {
      yield return LevelString(level);
    }
    //yield return $"// {functionName}\r\n";
    yield return LevelString(level);
    yield return $"function {saneFunctionName}(";

    bool firstParam = true;
    var functionParameters = function.Prototype.Params;
    for (var i = 0; i < functionParameters.Count; i++) {
      var functionParams = functionParameters[i];

      if (firstParam)
        firstParam = false;
      else
        yield return ", ";

      yield return ParameterName(saneFunctionName, i);
    }

    yield return $") {{\r\n";

    string cachedCode = "";
    foreach (var blockCode in GenerateExpression(function.Block, saneFunctionName, level + 1)) {
      cachedCode += blockCode; //TODO: Should be itterated, might require AST changes
    }

    foreach(var functionLocalVarName in _functionLocalVarNames) {
      yield return LevelString(level + 1);
      yield return $"let {functionLocalVarName};\r\n";
    }

    yield return "return ";
    yield return cachedCode;

    yield return LevelString(level);
    yield return "}\r\n\r\n";
  }

  private static IEnumerable<string> GenerateBlock(IBlock block, string contentOfFunctionName, int level) {
    if (block.InnerExpr != null) {
      foreach(var innerExpressionCode in GenerateExpression(block.InnerExpr, contentOfFunctionName, level))
        yield return innerExpressionCode;
    }
    if (block.SourceExpr != null) {
      foreach(var sourceExpressionCode in GenerateExpression(block.SourceExpr, contentOfFunctionName, level))
        yield return sourceExpressionCode;
    }
  }

  private static IEnumerable<string> GenerateReturn(IReturn @return, string contentOfFunctionName, int level) {
    //yield return LevelString(level);
    //yield return "return ";
    foreach(var expressionCode in GenerateExpression(@return.SourceExpr, contentOfFunctionName, level + 1, parent: (AstModel)@return)) {
        yield return expressionCode;
    }
    //yield return "\r\n";
  }

  private static IEnumerable<string> GenerateCall(ICall call, string contentOfFunctionName, int level, AstModel parent) {
    //yield return LevelString(level);
    var function = call.Function;
    var saneFunctionName = SaneFunctionName(function.Name);
    yield return saneFunctionName + "(";

    var first = call.ArgExprs.FirstOrDefault();
    foreach (var argExpr in call.ArgExprs) {
      if (first != argExpr)
        yield return ", ";

      foreach(var argExprCode in GenerateExpression(argExpr, contentOfFunctionName, level, null, true)) {
        yield return argExprCode;
      }
    }

    yield return ")";
    //if (parent?.__type == TYPE_CONSECUTOR)
      //yield return ";";
  }

  private static IEnumerable<string> GenerateInterfaceCall(IInterfaceCall interfaceCall, string contentOfFunctionName, int level, AstModel parent) {

    var methodName = _namingHelper.FixFunction(interfaceCall.FunctionType.Name);
    var interfaceName = CName(interfaceCall.InterfaceRef.Name);
    //var interfaceName = CName(interfaceCall.InterfaceRef.Name);
    var first = interfaceCall.ArgExprs.First();

    foreach(var firstCode in GenerateExpression(first, contentOfFunctionName, level))
        yield return firstCode;
    yield return $"._ic_{interfaceName}_{methodName}(";

    var implementationName = first.ResultType.Referend.Name;

    foreach (var argExpr in interfaceCall.ArgExprs) {
      if (first != argExpr)
        yield return ", ";

      foreach(var argExprCode in GenerateExpression(argExpr, contentOfFunctionName, level)) {
        yield return argExprCode;
      }
    }

    yield return ")";
    //if (parent?.__type == TYPE_CONSECUTOR)
      //yield return ";";
  }

  private static IEnumerable<string> GenerateExternCall(IExternCall externCall, string contentOfFunctionName, int level) {
    var functionName = externCall.Function.Name;
    var saneFunctionName = _namingHelper.FixExternalFunction(functionName);
    if (VERBOSE)
      yield return "\r\n//<externCall>\r\n";
    yield return $"__ext_{saneFunctionName}(";
    
    bool first = true;
    foreach (var argExpr in externCall.ArgExprs) {
      if (first)
        first = false;
      else
        yield return ", ";

      foreach(var argExprCode in GenerateExpression(argExpr, contentOfFunctionName, level)) {
        yield return argExprCode;
      }
    }

    yield return ")";
  }


  private static IEnumerable<string> GenerateConsecutor(IConsecutor consecutor, string contentOfFunctionName, int level, IExpression parent, bool inline) {
    if (VERBOSE)
      yield return "\r\n//<consecutor>\r\n";

    var lastExpression = consecutor.Exprs.Last();

    //if (_noReturnConsecutorChild.Contains(lastExpression.__type))
    //  lastExpression = null; // Don't treat any ast the last (avoid creating a return)

    yield return "(";
    foreach(var expr in consecutor.Exprs) {
      bool hasContent = false;
      foreach(var expressionCode in GenerateExpression(expr, contentOfFunctionName, level, parent: (AstModel)consecutor)) {
        if (!string.IsNullOrWhiteSpace(expressionCode))
          hasContent = true;
        yield return expressionCode;
      }
      if (expr != lastExpression && hasContent)
        yield return ", ";
    }
    yield return ")";

    if (VERBOSE)
      yield return "\r\n//</consecutor>\r\n";
  }


/*
  private static IEnumerable<string> GenerateConsecutor(IConsecutor consecutor, string contentOfFunctionName, int level, IExpression parent, bool inline) {
    if (VERBOSE)
      yield return "\r\n//<consecutor>\r\n";

    var lastExpression = consecutor.Exprs.Last();
    if (_noReturnConsecutorChild.Contains(lastExpression.__type))
      lastExpression = null; // Don't treat any ast the last (avoid creating a return)

    if (inline)
      yield return "(function(){";
    foreach(var expr in consecutor.Exprs) {
      yield return LevelString(level);
      if (expr == lastExpression) {
        if (lastExpression.__type == "NewStruct" && _tupRegex.IsMatch(lastExpression.ResultType.Referend.Name))
          break;
        yield return "return ";
      }

      foreach(var expressionCode in GenerateExpression(expr, contentOfFunctionName, level, parent: (AstModel)consecutor))
        yield return expressionCode;

      if (expr == lastExpression)
        yield return ";\r\n";
    }
    if (inline)
      yield return "})()";

    if (VERBOSE)
      yield return "\r\n//</consecutor>\r\n";
  }
  */

  private static IEnumerable<string> GenerateLocalLoad(ILocalLoad localLoad, string contentOfFunctionName, int level) {
    if (VERBOSE)
      yield return "\r\n// <LocalLoad>\r\n";

    var local = localLoad.Local;
    var codeVarName = ExtractCodeVarName(local);

    //Console.WriteLine("Doing localload! " + codeVarName);

    yield return codeVarName;
  }

  private static IEnumerable<string> GenerateLocalStore(ILocalStore localStore, string contentOfFunctionName, int level, AstModel parent) {
    if (VERBOSE)
      yield return "\r\n// <LocalStore>\r\n";

    var local = localStore.Local;
    var codeVarName = ExtractCodeVarName(local);

    //Console.WriteLine("Doing localstore, huzzah! " + codeVarName);

    yield return codeVarName;
    yield return " = ";
    foreach(var sourceCode in GenerateExpression(localStore.SourceExpr, contentOfFunctionName, level))
        yield return sourceCode;
    //if (parent?.__type == TYPE_CONSECUTOR || parent?.__type == "Discard")
      //yield return ";\r\n";
  }

  private static IEnumerable<string> GenerateStackify(IStackify stackify, string contentOfFunctionName, int level, AstModel parent) {
    
    var local = stackify.Local;
    if (local == null)
      throw new Exception("Unsupported stackify: Local not found");

    var codeVarName = ExtractCodeVarName(local);
    _functionLocalVarNames.Add(codeVarName);
    yield return LevelString(level);
    //yield return $"let {codeVarName}";
    yield return $"{codeVarName}";

    if (stackify.SourceExpr != null) {
      yield return $" = ";
      foreach(var expressionCode in GenerateExpression(stackify.SourceExpr, contentOfFunctionName, level))
        yield return expressionCode;
    }

    //if (parent?.__type == TYPE_CONSECUTOR)
      //yield return $";\r\n";

    if (VERBOSE)
      yield return "\r\n//<stackify />\r\n";
  }

  private static IEnumerable<string> GenerateUnstackify(IUnstackify unstackify, string contentOfFunctionName, int level) {
    
    var local = unstackify.Local;
    if (local == null)
      throw new Exception("Unsupported unstackify: Local not found");

    var codeVarName = ExtractCodeVarName(local);
    yield return codeVarName;

    if (VERBOSE)
      yield return "\r\n//<unstackify />\r\n";
  }

  private static IEnumerable<string> GenerateDiscard(IDiscard discard, string contentOfFunctionName, int level) {
    if (VERBOSE)
      yield return "\r\n//<discard>\r\n";
    
    if (_ignoredDiscardTypes.Contains(discard.SourceExpr.__type)) {
      yield return "void(0)";
      yield break;
    }

    if (discard.SourceExpr != null/* && !_ignoredDiscardTypes.Contains(discard.SourceExpr.__type)*/) {
      //yield return "(function(){\r\n";
      yield return LevelString(level);
      foreach (var expressionCode in GenerateExpression(discard.SourceExpr, contentOfFunctionName, level, parent: (AstModel)discard)) {
        yield return expressionCode;
      }
      //yield return "\r\n})();\r\n";
    }

    if (VERBOSE)
      yield return "\r\n//</discard>\r\n";
  }

  private static IEnumerable<string> GenerateArgument(IArgument argument, string contentOfFunctionName, int level) {
    yield return ParameterName(contentOfFunctionName, argument.ArgumentIndex);
    if (VERBOSE)
      yield return "\r\n//<argument />\r\n";
  }

  private static IEnumerable<string> GenerateDestroy(IArgument argument, string contentOfFunctionName, int level) {
    yield return ParameterName(contentOfFunctionName, argument.ArgumentIndex);
    if (VERBOSE)
      yield return "\r\n//<destroy />\r\n";
  }

  private static IEnumerable<string> GenerateConstant(IConstant constant, string contentOfFunctionName, int level) {
    if (VERBOSE)
      yield return $"\r\n//<{constant.__type} />\r\n";

    switch (constant.__type) {
      case "ConstantBool":
      case "ConstantI64":
        yield return constant.Value;
        break;
      case "ConstantStr":
        var jsString = JsHelper.GenerateString(constant.Value);
        yield return jsString;
        break;
    }
  }

  private static IEnumerable<string> GenerateIf(IIf @if, string contentOfFunctionName, int level, AstModel parent) {
    if (VERBOSE)
      yield return "\r\n//<if />\r\n";


    //var isParentConsecutor = parent != null && parent.__type == TYPE_CONSECUTOR;
    var conditionBlock = @if.ConditionBlock;
    var thenBlock = @if.ThenBlock;
    var elseBlock = @if.ElseBlock;

    yield return "((";
    foreach(var conditionCode in GenerateExpression(conditionBlock, contentOfFunctionName, level))
      yield return conditionCode;
    yield return ") ? function(){\r\n";
    //if (thenBlock.InnerExpr.__type != TYPE_CONSECUTOR)
    yield return "return ";
    foreach(var thenCode in GenerateExpression(thenBlock, contentOfFunctionName, level + 1))
      yield return thenCode;
    if (thenBlock.InnerExpr.__type != TYPE_CONSECUTOR)
      yield return ";";
    yield return "\r\n} : function(){\r\n";
    //if (elseBlock.InnerExpr.__type != TYPE_CONSECUTOR)
    yield return "return ";
    foreach(var elseCode in GenerateExpression(elseBlock, contentOfFunctionName, level + 1))
      yield return elseCode;
    if (elseBlock.InnerExpr.__type != TYPE_CONSECUTOR)
        yield return ";";
    yield return "\r\n})()\r\n";
  }

  private static IEnumerable<string> GenerateWhile(IWhile @while, string contentOfFunctionName, int level, AstModel parent) {
    if (parent?.__type != TYPE_CONSECUTOR)
      throw new Exception("Does not support while outside a consectutor");

    yield return "(function(){ while (";
    foreach(var ifCode in GenerateExpression((AstModel)@while.BodyBlock, contentOfFunctionName, level, parent: (AstModel)@while))
      yield return ifCode;
    yield return ") {} })()\r\n";
  }

  private static IEnumerable<string> GenerateNewStruct(INewStruct newStruct, string contentOfFunctionName, int level) {
    if (VERBOSE)
      yield return "\r\n//<NewStruct />\r\n";

    var fullStructName = newStruct.ResultType.Referend.Name;
    var structName = CName(fullStructName);
    if (_tupRegex.IsMatch(fullStructName)) {
      yield return "void(0)"; //undefined
      yield break;
    }

    string protoName;
    if (!_protoNameByStructName.TryGetValue(structName, out protoName))
      protoName = "{}";

    yield return $"Object.create({protoName}, {{";

    for (var i = 0; i < newStruct.MemberNames.Count; i++) {
      var memberName = newStruct.MemberNames[i];
      var codeVarName = ExtractCodeVarName(memberName);

      var sourceExpr = newStruct.SourceExprs[i];

/*
      if (sourceExpr.__type == "Argument") {
      var argumentIndex = sourceExpr.ArgumentIndex;
      }else{
        throw new Exception($"Unsupported source expression in NewStruct: {sourceExpr.__type}. fullStructName: {fullStructName}");
      }
      */


      yield return " ";
      yield return codeVarName;
      yield return ": { writable: true, value: ";
      //yield return ParameterName(contentOfFunctionName, argumentIndex);
      foreach (var expressionCode in GenerateExpression(sourceExpr, contentOfFunctionName, level, parent: null, inline: true))
        yield return expressionCode;
      yield return ", },";
    }

    yield return "})";
  }

  private static IEnumerable<string> GenerateStructToInterfaceUpcast(AstModel structToInterfaceUpcast, string contentOfFunctionName, int level, IExpression parent, bool inline) {
    if (VERBOSE)
      yield return $"\r\n//<StructToInterfaceUpcast />\r\n";
      foreach(var expressionCode in GenerateExpression(structToInterfaceUpcast.SourceExpr, contentOfFunctionName, level, parent: null, inline: inline))
        yield return expressionCode;
  }

  private static IEnumerable<string> GenerateMemberLoad(IMemberLoad memberLoad, string contentOfFunctionName, int level) {
    if (VERBOSE)
      yield return "\r\n//<MemberLoad />\r\n";

    var memberName = memberLoad.MemberName;
    var codeVarName = ExtractCodeVarName(memberName);

    foreach(var expressionCode in GenerateExpression(memberLoad.StructExpr, contentOfFunctionName, level))
      yield return expressionCode;
    yield return ".";
    if (codeVarName == null || codeVarName == "")
      yield return ($"\r\ncodeVarName: {codeVarName} - {memberName}\r\n");
    yield return codeVarName;
  }

  private static IEnumerable<string> GenerateMemberStore(IMemberStore memberStore, string contentOfFunctionName, int level) {
    if (VERBOSE)
      yield return "\r\n//<MemberStore />\r\n";

      var memberName = memberStore.MemberName;
      var codeVarName = ExtractCodeVarName(memberName);

      foreach(var expressionCode in GenerateExpression(memberStore.StructExpr, contentOfFunctionName, level))
        yield return expressionCode;
      yield return ".";
      yield return codeVarName;
      yield return " = ";
      foreach(var expressionCode in GenerateExpression(memberStore.SourceExpr, contentOfFunctionName, level))
        yield return expressionCode;
      //yield return ";";
  }

  private static IEnumerable<string> GenerateNewArrayFromValues(INewArrayFromValues newArrayFromValues, string contentOfFunctionName, int level) {
    yield return "[";

    var first = newArrayFromValues.SourceExprs.FirstOrDefault();
    foreach(var sourceExpr in newArrayFromValues.SourceExprs) {
      if (sourceExpr != first)
        yield return ", ";
      foreach(var expressionCode in GenerateExpression(sourceExpr, contentOfFunctionName, level))
        yield return expressionCode;
    }
    yield return "]";
  }

  private static IEnumerable<string> GenerateConstructUnknownSizeArrayCode(IConstructUnknownSizeArray constructUnknownSizeArray, string contentOfFunctionName, int level) {
    var methodName =
      _namingHelper.FixFunction(constructUnknownSizeArray.GeneratorMethod.Name);
    var interfaceName = CName(constructUnknownSizeArray.GeneratorReferend.Name);

    yield return "(function(){\r\n";

    yield return "const __size = ";
      foreach(var expressionCode in GenerateExpression(constructUnknownSizeArray.SizeExpr, contentOfFunctionName, level))
        yield return expressionCode;
    yield return ";\r\n";

    yield return "const __generator = ";
    foreach(var expressionCode in GenerateExpression(constructUnknownSizeArray.GeneratorExpr, contentOfFunctionName, level))
        yield return expressionCode;
    yield return ";\r\n";

    yield return "const __arr = new Array(__size);\r\n";
    yield return "for(let __i = 0; __i < __size; __i++) {\r\n";
    yield return "  __arr[__i] = __generator";
    yield return $"._ic_{interfaceName}_{methodName}" + "(__generator, __i);\r\n}\r\n";

    yield return "return __arr;";
    yield return "\r\n})()";
  }

  private static IEnumerable<string> GenerateArrayLength(IArrayLength arrayLength, string contentOfFunctionName, int level) {
    foreach(var expressionCode in GenerateExpression(arrayLength.SourceExpr, contentOfFunctionName, level))
      yield return expressionCode;
    
    yield return ".length";
  }

  private static IEnumerable<string> GenerateArrayLoad(IArrayLoad arrayLoad, string contentOfFunctionName, int level) {
    foreach (var arrayExprCode in GenerateExpression(arrayLoad.ArrayExpr, contentOfFunctionName, level))
      yield return arrayExprCode;
    yield return "[";
    foreach (var indexExprCode in GenerateExpression(arrayLoad.IndexExpr, contentOfFunctionName, level))
      yield return indexExprCode;
    yield return "]";
  }
  

  private static IEnumerable<string> GenerateExpression(AstModel astModel, string contentOfFunctionName, int level, AstModel parent = null, bool inline = false) {
    if (astModel == null)
      throw new Exception("No AST model");

    switch (astModel.__type) {
      case "Call":
        foreach (var callCode in GenerateCall(astModel, contentOfFunctionName, level, parent))
          yield return callCode;
        break;
      case "InterfaceCall":
        foreach (var interfaceCallCode in GenerateInterfaceCall(astModel, contentOfFunctionName, level, parent))
          yield return interfaceCallCode;
        break;
      case "ExternCall":
        foreach (var externCallCode in GenerateExternCall(astModel, contentOfFunctionName, level))
          yield return externCallCode;
        break;
      case "Return":
        foreach (var returnCode in GenerateReturn(astModel, contentOfFunctionName, level))
          yield return returnCode;
        break;
      case "Consecutor":
        foreach (var consecutorCode in GenerateConsecutor(astModel, contentOfFunctionName, level, parent, inline))
          yield return consecutorCode;
        break;
      case "Stackify":
        foreach (var stackifyCode in GenerateStackify(astModel, contentOfFunctionName, level, parent))
          yield return stackifyCode;
        break;
      case "Unstackify":
        foreach (var unstackifyCode in GenerateUnstackify(astModel, contentOfFunctionName, level))
          yield return unstackifyCode;
        break;
      case "LocalLoad":
        foreach (var localLoadCode in GenerateLocalLoad(astModel, contentOfFunctionName, level))
          yield return localLoadCode;
        break;
      case "LocalStore":
        foreach (var localStoreCode in GenerateLocalStore(astModel, contentOfFunctionName, level, parent))
          yield return localStoreCode;
        break;
      case "Discard":
        foreach (var discardCode in GenerateDiscard(astModel, contentOfFunctionName, level))
          yield return discardCode;
        break;
      case "Argument":
        foreach (var argumentCode in GenerateArgument(astModel, contentOfFunctionName, level))
          yield return argumentCode;
        break;
      case "Block":
        foreach (var blockCode in GenerateBlock(astModel, contentOfFunctionName, level))
          yield return blockCode;
        break;
      case "ConstantI64":
      case "ConstantStr":
      case "ConstantBool":
        foreach (var constantCode in GenerateConstant(astModel, contentOfFunctionName, level))
          yield return constantCode;
        break;
      case "NewStruct":
        foreach (var newStructCode in GenerateNewStruct(astModel, contentOfFunctionName, level))
          yield return newStructCode;
        break;
      case "MemberLoad":
        foreach (var memberLoadCode in GenerateMemberLoad(astModel, contentOfFunctionName, level))
          yield return memberLoadCode;
        break;
      case "MemberStore":
        foreach (var memberStoreCode in GenerateMemberStore(astModel, contentOfFunctionName, level))
          yield return memberStoreCode;
        break;
      case "Destroy":
        foreach (var destroyCode in GenerateDestroy(astModel, contentOfFunctionName, level))
          yield return destroyCode;
        break;
     case "If":
        foreach (var ifCode in GenerateIf(astModel, contentOfFunctionName, level, parent))
          yield return ifCode;
        break;
     case "While":
        foreach (var whileCode in GenerateWhile(astModel, contentOfFunctionName, level, parent))
          yield return whileCode;
        break;
      case "StructToInterfaceUpcast":
        foreach (var structToInterfaceUpcastCode in GenerateStructToInterfaceUpcast(astModel, contentOfFunctionName, level, null, inline))
          yield return structToInterfaceUpcastCode;
        break;
      case "NewArrayFromValues":
        foreach (var newArrayFromValuesCode in GenerateNewArrayFromValues(astModel, contentOfFunctionName, level))
          yield return newArrayFromValuesCode;
        break;
      case "KnownSizeArrayLoad":
      case "UnknownSizeArrayLoad":
        foreach (var arrayLoadCode in GenerateArrayLoad(astModel, contentOfFunctionName, level))
          yield return arrayLoadCode;
        break;
      case "DestroyKnownSizeArray":
      case "DestroyUnknownSizeArray":
        // Do nothing
        break;
      case "ConstructUnknownSizeArray":
        foreach (var constructUnknownSizeArrayCode in GenerateConstructUnknownSizeArrayCode(astModel, contentOfFunctionName, level))
          yield return constructUnknownSizeArrayCode;
        break;
      case "ArrayLength":
        foreach (var arrayLengthCode in GenerateArrayLength(astModel, contentOfFunctionName, level))
          yield return arrayLengthCode;
        break;
      default:
        yield return $"\r\n//<UNSUPPORTED {astModel.__type} />\r\n";
        //throw new Exception("Unsupported __type: " + astModel.__type);
        break;
    }
  }

  private static IEnumerable<string> CreateHeader() {
    // int
    yield return "  function __ext___addIntInt(a, b) { return ((a|0) + (b|0)) | 0; }\r\n";
    yield return "  function __ext___subtractIntInt(a, b) { return ((a|0) - (b|0)) | 0; }\r\n";
    yield return "  function __ext___multiplyIntInt(a, b) { return Math.imul(a|0, b|0) | 0; }\r\n";
    yield return "  function __ext___lessThanInt(a, b) { return (a|0) < (b|0); }\r\n";
    yield return "  function __ext___lessThanIntOrEqInt(a, b) { return (a|0) <= (b|0); }\r\n";
    yield return "  function __ext___greaterThanInt(a, b) { return (a|0) > (b|0); }\r\n";
    yield return "  function __ext___greaterThanOrEqInt(a, b) { return (a|0) >= (b|0); }\r\n";
    yield return "  function __ext___eqIntInt(a, b) { return (a|0) === (b|0); }\r\n";
    

    // string
    yield return "  function __ext___addStrStr(a, b) { return a + b; }\r\n";
    yield return "  function __ext___eqStrStr(a, b) { return a === b; }\r\n";

    // misc
    yield return "  function __ext___and(a, b) { return !!a && !!b; }\r\n";
    yield return "  function __ext___not(a) { return !a; }\r\n";

    yield return "  function __ext___print(p) { console.log(p); }\r\n";
    yield return "  function __ext___getch() { if (!window) { return 5; } while (true) { const result = window.prompt('Press a key'); if (typeof result === 'string' && result.length !== 0) { return result.charCodeAt(0); } } }";

    yield return "\r\n";
  }

}