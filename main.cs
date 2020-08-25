using System;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;

namespace dev.vale.awesome {

  public class Compilifier {

    public static void Main() {
      //var valeCode = "fn main() { 42 }";
      //var valeCode = "fn main() { 1 + 2 }";
      //var valeCode = "fn main() { print(\"hello!\"); }";
      //var valeCode = "fn main() { println(\"hello!\"); }";
      /*
      var valeCode = @"
        struct Marine { hp int; }
        fn main() {
          m = Marine(9);
          mut m.hp = 4;
          = m.hp;
        }
      ";
      */

      /*
      var valeCode = @"
      struct MyStruct { a int; }
      struct OtherStruct { b MyStruct; }
      fn main() {
        ms = OtherStruct(MyStruct(11));
        = ms.b.a;
      }
      ";
      */

      /*
      var valeCode = @"
      interface Car { }
      fn doCivicDance(virtual this Car) int abstract;

      struct Civic {}
      impl Car for Civic;
      fn doCivicDance(civic Civic impl Car) int {
        = 4;
      }

      struct Toyota {}
      impl Car for Toyota;
      fn doCivicDance(toyota Toyota impl Car) int {
        = 7;
      }

      fn main() int {
        x Car = Toyota();
        = doCivicDance(x);
      }
      ";
      */
      var valeCode = @"
        fn main() {
          if (true) {
            a = 1;
          }

          = if (true) {
              42
            } else {
              73
            }

        }
      ";

      Console.WriteLine("Vale code:");
      Console.WriteLine(valeCode);

      Console.WriteLine("----------");

      // Get the AST for the vale code
      var astJson = ValeHelper.Build(valeCode);

      var ast = (IProgram)Json.Deserialize<AstModel>(astJson);

      //Console.WriteLine("----------");
      //var json = Json.Serialize(ast);
      //Console.WriteLine("Serialized again:");
      //Console.WriteLine(json);

      Console.WriteLine("AST:");
      Console.WriteLine(astJson);
      Console.WriteLine("----------");


      foreach (var code in JavaScriptGenerator.Generate(ast)) {
        Console.Write(code);
      }

      Console.WriteLine("");
      Console.WriteLine("----------");

      return;

      // Get the result from running (uses the test VM on the server)
      var result = ValeHelper.Run(valeCode);

      Console.WriteLine("Execution's stdout:");
      Console.WriteLine(result);
    }

  }
}
