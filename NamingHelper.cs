public static class NamingHelper {

  public static string FixFunction(string functionName) {
    return OnlyLettersAndNumbers(functionName);
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

}