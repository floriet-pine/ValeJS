using System;
using System.IO;
using System.Linq;
using System.Reflection;

public static class Json {
  private static readonly MethodInfo _deserializeMethodInfo;
  private static readonly MethodInfo _serializeMethodInfo;

  static Json() {
      var filePath = new FileInfo("./Newtonsoft.Json.dll");
      var dll = Assembly.LoadFile(filePath.FullName);
      var jsonConvertType = dll.GetExportedTypes().Where(x => x.Name == "JsonConvert").FirstOrDefault();

      _deserializeMethodInfo = jsonConvertType.GetMethods().FirstOrDefault(x => x.Name == "DeserializeObject" && x.IsGenericMethod && x.GetParameters().Length == 1);

      _serializeMethodInfo = jsonConvertType.GetMethods().FirstOrDefault(x => x.Name == "SerializeObject" && x.GetParameters().Length == 1);
  }

  public static T Deserialize<T>(string json) {
      var generic = _deserializeMethodInfo.MakeGenericMethod(typeof(T));
      var result = (T)generic.Invoke(null, new object[]{ json });
      return result;
  }

  public static string Serialize<T>(T model) {
      var result = (string)_serializeMethodInfo.Invoke(null, new object[]{ model });
      return result;
  }


}