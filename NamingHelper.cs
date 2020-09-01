using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

public class NamingHelper {
  private static readonly Dictionary<string, string> collisionAvoidanceRename = new Dictionary<string, string>() {
    { "Array", "ValeArray" },
  };

  // Manual two-way dictionary
  //private Dictionary<string, string> _prettyFunctionNameByAstFunctionName = new Dictionary<string, string>();
  //private Dictionary<string, string> _astFunctionNameByPrettyFunctionName = new Dictionary<string, string>();

  public NamingHelper() {
  }

  public string FixFunction(string astFunctionName) {
    if (collisionAvoidanceRename.TryGetValue(astFunctionName, out var replacedValue)) {
      return replacedValue;
    }

    return JavascriptifyName(astFunctionName);
  }

  public string FixExternalFunction(string functionName) {
    if (collisionAvoidanceRename.TryGetValue(functionName, out var replacedValue)) {
      return replacedValue;
    }

    return JavascriptifyName(functionName);
  }

  public string FixCName(string cName) {
    if (collisionAvoidanceRename.TryGetValue(cName, out var replacedValue)) {
      return replacedValue;
    }

    return JavascriptifyName(cName);
  }

  private static string JavascriptifyName(string str) {
    var result = "";
    for (var i = 0; i < str.Length; i++) {
      var c = str[i];
      if (char.IsLetterOrDigit(c) || c == '_') {
        result += c;
      } else {
        result += "$" + (int)c + "_";
      }
    }
    return result;
  }
}