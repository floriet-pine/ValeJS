public static class JsHelper {
  public static string GenerateString(string str) {
      var result = "'";
      for (var i = 0; i < str.Length; i++) {
        var c = str[i];
        switch (c)
        {
          case '\"': result += "\\\""; break;
          case '\\': result += "\\\\"; break;
          case '\b': result += "\\b"; break;
          case '\f': result += "\\f"; break;
          case '\n': result += "\\n"; break;
          case '\r': result += "\\r"; break;
          case '\t': result += "\\t"; break;

          default: result += (c < 32 || c > 127) && !char.IsLetterOrDigit(c) ? $"\\u{(int)c:X04}" : c.ToString();
          break;
        }
      }
      result += "'";
      return result;
  }
}
