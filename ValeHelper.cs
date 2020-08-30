using System;
using System.IO;
using System.Net;
using System.Text;

public static class ValeHelper {

  private static readonly String BUILD_URL =
    "https://us-central1-valesite.cloudfunctions.net/vale-build";
  private static readonly String RUN_URL =
    "https://us-central1-valesite.cloudfunctions.net/vale-run";


  public static string Build(string valeCode) {
    var astJson = Request(BUILD_URL, valeCode);

    // Change empty property names to something that can be auto mapped
    astJson = astJson.Replace("{\"\"", "{\"expressionType\"");

    return astJson;
  }

  public static string Run(string valeCode) {
    return Request(RUN_URL, valeCode);
  }

  // Makes a simple POST HTTP request and returns the response.
  private static string Request(String url, String postData) {
    WebRequest request = WebRequest.Create(url);
    request.Method = "POST";
    byte[] byteArray = Encoding.UTF8.GetBytes(postData);
    request.ContentLength = byteArray.Length;
    Stream dataStream = request.GetRequestStream();
    dataStream.Write(byteArray, 0, byteArray.Length);
    dataStream.Close();
    WebResponse response = request.GetResponse();
    // To print the status code:
    // Console.WriteLine(((HttpWebResponse)response).StatusDescription);
    dataStream = response.GetResponseStream();
    StreamReader reader = new StreamReader(dataStream);
    string responseFromServer = reader.ReadToEnd();
    reader.Close();
    dataStream.Close();
    response.Close();
    return responseFromServer;
  }

}
