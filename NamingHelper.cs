using System.Collections.Generic;
using System.Text.RegularExpressions;

public class NamingHelper {
  private static readonly Regex _simpleFunctionNameRegex = new Regex("F\\(\"(?<Name>[a-z_]+)\"\\)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
  private static readonly Regex _externalFunctionNameRegex = new Regex("F\\(\"(?<Name>[a-z_]+)\"", RegexOptions.Compiled | RegexOptions.IgnoreCase);
  private static readonly Regex _cNameRegex = new Regex("C\\(\"(?<Name>[a-z_]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

  private static readonly Dictionary<string, string> collisionAvoidanceRename = new Dictionary<string, string>() {
    { "Array", "ValeArray" },
  };

  // Manual two-way dictionary
  private Dictionary<string, string> _prettyFunctionNameByAstFunctionName = new Dictionary<string, string>();
  private Dictionary<string, string> _astFunctionNameByPrettyFunctionName = new Dictionary<string, string>();

  public NamingHelper() {
  }

  public string FixFunction(string astFunctionName) {
    if (_prettyFunctionNameByAstFunctionName.TryGetValue(astFunctionName, out var result))
      return result;

    var match = _simpleFunctionNameRegex.Match(astFunctionName);
    if (match.Success)
      return GetUniqueName(match.Groups["Name"].Value, astFunctionName);

    var fallbackName = OnlyLettersAndNumbers(astFunctionName);
    return GetUniqueName(fallbackName, astFunctionName);
  }

  public string FixExternalFunction(string functionName) {
    var match = _externalFunctionNameRegex.Match(functionName);
    if (match.Success)
      return match.Groups["Name"].Value;

    return OnlyLettersAndNumbers(functionName);
  }

  public string FixCName(string cName) {
    var leftOfColon = cName.Split(':', 2)[0];
    var originalCNameType = leftOfColon;
    var match = _cNameRegex.Match(originalCNameType);
    if (match.Success)
      return GetUniqueName(match.Groups["Name"].Value, originalCNameType);

    // Fallback
    var onlyLettersAndNumbers = OnlyLettersAndNumbers(leftOfColon);
    var uniqueName = GetUniqueName(onlyLettersAndNumbers, originalCNameType);
    return uniqueName;
  }

  private static string OnlyLettersAndNumbers(string str) {
    bool lastValid = true;
    var result = "";
    for (var i = 0; i < str.Length; i++) {
      var c = str[i];
      if (char.IsLetterOrDigit(c)) {
        result += c;
        lastValid = true;
      }
      else if (lastValid) {
        result += '_';
        lastValid = false;
      }
    }
    return result;
  }

  private string GetUniqueName(string prettyFunctionName, string astFunctionName) {
    // Prettify at bit more
    // Waiting for AST generator to output more sane names, before improving this method
    prettyFunctionName = prettyFunctionName.Replace("_F_", "_");
    prettyFunctionName = prettyFunctionName.Replace("_R_", "_");
    prettyFunctionName = prettyFunctionName.Replace("_C_", "_");
    prettyFunctionName = prettyFunctionName.Replace("_TR_", "_");
    prettyFunctionName = prettyFunctionName.Replace("_TK_", "_");
    prettyFunctionName = prettyFunctionName.Replace("_TM_", "_");
    prettyFunctionName = prettyFunctionName.Replace("_SId_", "_");
    prettyFunctionName = prettyFunctionName.Replace("_IId_", "_");
    prettyFunctionName = prettyFunctionName.Replace("_i_", "_");
    prettyFunctionName = prettyFunctionName.Replace("_imm_", "_");

    prettyFunctionName = prettyFunctionName.Replace("_LC_", "_");
    prettyFunctionName = prettyFunctionName.Replace("_CodeLocation_", "_");
    prettyFunctionName = prettyFunctionName.Replace("_CodeLocat_", "_");

    if (prettyFunctionName.Length > 50)
      prettyFunctionName = prettyFunctionName.Substring(0, 50);
    //

    if (_prettyFunctionNameByAstFunctionName.TryGetValue(astFunctionName, out var result))
      return result;

    var orgPrettyName = prettyFunctionName;
    var i = 2;
    while (_astFunctionNameByPrettyFunctionName.ContainsKey(prettyFunctionName)) {
      prettyFunctionName = $"{orgPrettyName}_{i}";
    }

    if (collisionAvoidanceRename.TryGetValue(astFunctionName, out var replacedValue)) {
      prettyFunctionName = replacedValue;
    }

    _prettyFunctionNameByAstFunctionName.Add(astFunctionName, prettyFunctionName);
    _astFunctionNameByPrettyFunctionName.Add(prettyFunctionName, astFunctionName);
    return prettyFunctionName;
  }

}