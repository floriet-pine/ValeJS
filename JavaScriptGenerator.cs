using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class JavaScriptGenerator {
  private static readonly Regex _validFunctionCharsOnlyRegex = new Regex("^[0-9a-z_]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
  private static readonly Regex _functionNameRegex = new Regex("F\\(\"(?<Name>[a-z_]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
  private static readonly Regex _cNameRegex = new Regex("C\\(\"(?<Name>[a-z_]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
  private static readonly Regex _sIdNameRegex = new Regex("SId\\(\"C\\(\\\\\"(?<Name>[a-z_]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
  private static readonly Regex _codeVarNameRegex = new Regex("CodeVarName\\(\"(?<Name>.+)\"\\)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

  private static readonly HashSet<string> _ignoredDiscardTypes = new HashSet<string>(){ "Unstackify" };

  private static Dictionary<string, Dictionary<string, int>> _indexByImplementaionByInterface = new Dictionary<string, Dictionary<string, int>>();

  private const bool VERBOSE = false;
  private const bool DEBUG = true;

  public static IEnumerable<string> Generate(IProgram program) {
    var level = 1;
    yield return "'use strict';\r\n";
    yield return "(function(){\r\n";

    foreach(var headerCode in CreateHeader())
      yield return headerCode;

    InitImplementationPointers(program);

    foreach(var function in program.Functions) {
      foreach(var functionCode in GenerateFunction(function, null, level))
        yield return functionCode;
    }

    foreach(var implementationPointerCode in CreateImplementationPointers())
      yield return implementationPointerCode;

    yield return LevelString(level);
    yield return "console.log(main());\r\n";
    yield return "})();";
  }

  private static string LevelString(int level) {
    return new String(' ', level * 2);
  }

  private static string ParameterName(string functionName, int argumentIndex) {
    return $"{functionName}_p_{argumentIndex}";
  }

  private static string ExtractCodeVarName(string str) {
    var match = _codeVarNameRegex.Match(str);
    if (match.Success)
      return $"{match.Groups["Name"].Value}";
    return null;
  }

  private static string ExtractCodeVarName(ILocal local) {
    var number = local.Id.Number;
    var value = local.Id.OptName.Value;

    var codeVarName = ExtractCodeVarName(value);
    if (codeVarName != null)
      return $"{codeVarName}_{number}";

    var numberName = $"__blockVar_{number}";
    return numberName;
  }

  private static string CName(string cName) {
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
    return NamingHelper.FixFunction(insaneFunctionName);
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

  private static IEnumerable<string> CreateImplementationPointers() {
    foreach(var kvp in _indexByImplementaionByInterface) {
      var interfaceName = kvp.Key;
      var indexByImplementaion = kvp.Value;
      yield return $"__interfaceImpl_{interfaceName} = {{";
      foreach (var kvp2 in indexByImplementaion) {
        var implementationName = kvp2.Key;
        var index = kvp2.Value;
        yield return $"{index}: {implementationName}, ";
      }
      yield return "};\r\n";
    }
  }

  private static void InitImplementationPointers(IProgram program) {
    _indexByImplementaionByInterface.Clear();

    foreach (var @struct in program.Structs) {
      foreach (var edge in @struct.Edges) {
        var structName = CName(edge.StructName.Name);
        var interfaceName = CName(edge.InterfaceName.Name);
        if (!_indexByImplementaionByInterface.TryGetValue(interfaceName, out var indexByImplementation)) {
          indexByImplementation = new Dictionary<string, int>();
          _indexByImplementaionByInterface.Add(interfaceName, indexByImplementation);
        }
        if (!indexByImplementation.ContainsKey(structName))
          indexByImplementation.Add(structName, indexByImplementation.Count);
      }
    }
  }

  private static IEnumerable<string> GenerateFunction(IFunction function, string contentOfFunctionName, int level) {
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

    yield return $") {{";
    yield return "\r\n";

    foreach (var blockCode in GenerateExpression(function.Block, saneFunctionName, level + 1)) {
      yield return blockCode;
    }

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
    yield return LevelString(level);
    yield return "return ";
    foreach(var expressionCode in GenerateExpression(@return.SourceExpr, contentOfFunctionName, level + 1)) {
        yield return expressionCode;
    }
    yield return ";\r\n";
  }

  private static IEnumerable<string> GenerateCall(ICall call, string contentOfFunctionName, int level) {
    //yield return LevelString(level);
    var function = call.Function;
    var saneFunctionName = SaneFunctionName(function.Name);
    yield return saneFunctionName + "(";

    bool first = true;
    foreach (var argExpr in call.ArgExprs) {
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

  private static IEnumerable<string> GenerateExternCall(IExternCall externCall, string contentOfFunctionName, int level) {
    var functionName = externCall.Function.Name;
    var saneFunctionName = SaneFunctionName(functionName);
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

  private static IEnumerable<string> GenerateConsecutor(IConsecutor consecutor, string contentOfFunctionName, int level) {
    if (VERBOSE)
      yield return "\r\n//<consecutor>\r\n";

    foreach(var expr in consecutor.Exprs) {
      bool addedCode = false;
      foreach(var expressionCode in GenerateExpression(expr, contentOfFunctionName, level, parent: consecutor)) {
        addedCode = true;
        yield return expressionCode;
      }
      if (addedCode)
        yield return "\r\n";

    }

    if (VERBOSE)
      yield return "\r\n//</consecutor>\r\n";
  }

  private static IEnumerable<string> GenerateLocalLoad(ILocalLoad localLoad, string contentOfFunctionName, int level) {
    if (VERBOSE)
      yield return "\r\n// <LocalLoad>\r\n";

    var local = localLoad.Local;
    var codeVarName = ExtractCodeVarName(local);
    yield return codeVarName;
  }

  private static IEnumerable<string> GenerateStackify(IStackify stackify, string contentOfFunctionName, int level) {
    
    var local = stackify.Local;
    if (local == null)
      throw new Exception("Unsupported stackify: Local not found");

    var codeVarName = ExtractCodeVarName(local);

    yield return LevelString(level);
    yield return $"let {codeVarName}";

    if (stackify.SourceExpr != null) {
      yield return $" = ";
      foreach(var expressionCode in GenerateExpression(stackify.SourceExpr, contentOfFunctionName, level))
        yield return expressionCode;
    }

    yield return $";";

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
    
    yield return LevelString(level);
    if (discard.SourceExpr != null && !_ignoredDiscardTypes.Contains(discard.SourceExpr.__type)) {
      foreach (var expressionCode in GenerateExpression(discard.SourceExpr, contentOfFunctionName, level)) {
        yield return expressionCode;
      }
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
      case "ContantStr":
        var jsString = JsHelper.GenerateString(constant.Value);
        yield return jsString;
        break;
    }
  }

  private static IEnumerable<string> GenerateIf(IIf @if, string contentOfFunctionName, int level, AstModel parent) {
    if (VERBOSE)
      yield return "\r\n//<if />\r\n";


    var isConsecutor = parent != null && parent.__type == "Consecutor";
    var conditionBlock = @if.ConditionBlock;
    var thenBlock = @if.ThenBlock;
    var elseBlock = @if.ElseBlock;

    yield return isConsecutor ? "if (" : "(";
    foreach(var conditionCode in GenerateExpression(conditionBlock, contentOfFunctionName, level))
      yield return conditionCode;
    yield return isConsecutor ? ") {" : ") ? (";

    foreach(var thenCode in GenerateExpression(thenBlock, contentOfFunctionName, level))
      yield return thenCode;

    if (elseBlock != null) {
      yield return ") : (";
      foreach(var elseCode in GenerateExpression(elseBlock, contentOfFunctionName, level))
        yield return elseCode;
    }
    yield return ")";
  }

  private static IEnumerable<string> GenerateNewStruct(INewStruct newStruct, string contentOfFunctionName, int level) {
    if (VERBOSE)
      yield return "\r\n//<NewStruct />\r\n";

    yield return "{";

    for (var i = 0; i < newStruct.MemberNames.Count; i++) {
      var memberName = newStruct.MemberNames[i];
      var sourceExpr = newStruct.SourceExprs[i];
      if (sourceExpr.__type != "Argument")
        throw new Exception($"Unsupported source expression in NewStruct: {sourceExpr.__type}");

      var argumentIndex = sourceExpr.ArgumentIndex;
      var codeVarName = ExtractCodeVarName(memberName);

      yield return " ";
      yield return codeVarName;
      yield return ": ";
      yield return ParameterName(contentOfFunctionName, argumentIndex);
      yield return ",";
    }

    yield return "}";
  }

  private static IEnumerable<string> GenerateMemberLoad(IMemberLoad memberLoad, string contentOfFunctionName, int level) {
    if (VERBOSE)
      yield return "\r\n//<MemberLoad />\r\n";

      var memberName = memberLoad.MemberName;
      var codeVarName = ExtractCodeVarName(memberName);

      foreach(var expressionCode in GenerateExpression(memberLoad.StructExpr, contentOfFunctionName, level))
        yield return expressionCode;
      yield return ".";
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
      yield return ";";

  }

  private static IEnumerable<string> GenerateExpression(AstModel astModel, string contentOfFunctionName, int level, AstModel parent) {
    if (astModel == null)
      throw new Exception("No AST model");

    switch (astModel.__type) {
      case "Call":
        foreach (var callCode in GenerateCall(astModel, contentOfFunctionName, level))
          yield return callCode;
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
        foreach (var consecutorCode in GenerateConsecutor(astModel, contentOfFunctionName, level))
          yield return consecutorCode;
        break;
      case "Stackify":
        foreach (var stackifyCode in GenerateStackify(astModel, contentOfFunctionName, level))
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

    // string
    yield return "  function __ext___addStrStr(a, b) { return a + b; }\r\n";

    // misc
    yield return "  function __ext___print(p) { console.log(p); }\r\n";

    yield return "\r\n";
  }

}