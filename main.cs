// Welcome to ValeJS, the Vale-to-JS cross-compiler!
// See https://vale.dev/blog/replit-prototype for information
// about what this is and why it's so awesome!

// Modify the below VALE_CODE constant and then hit Run to
// compile your program!

// Also see ValeCompiler0.0.8.zip for the version of the
// compiler that has the Hybrid-Generational Memory prototype!

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;

namespace dev.vale.awesome {
  public class Compilifier {
    public static void Main() {
      // Read the Vale code from roguelike-small.vale.
      string valeCode = File.ReadAllText("roguelike-small.vale");

      Console.WriteLine("Compiling...");

      // This sends a request to the Vale compile server, and
      // results in some JSON representing VIR (Vale Intermediate Representation).
      string astJson = ValeHelper.Build(valeCode);
      // Uncomment this to see it!
      // Console.WriteLine(astJson);
      // Deserialize the VIR into AstModel instances.
      var ast = (IProgram)Json.Deserialize<AstModel>(astJson);

      Console.WriteLine("Cross-compiling to Javascript...");
      // Convert the VIR into Javascript, and write to output.js
      using (var outputFileStream = File.Create("output.js")) {
        using (var outputFileStreamWriter = new StreamWriter(outputFileStream, Encoding.ASCII)) {
          foreach (var code in JavaScriptGenerator.Generate(ast)) {
            outputFileStreamWriter.Write(code);
          }
        }
      }

      // We're done!
      Console.WriteLine("Compiling done! Now run with:");
      Console.WriteLine("  node output.js");
    }

  }
}
