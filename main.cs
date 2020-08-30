using System;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;

namespace dev.vale.awesome {

  public class Compilifier {
    private const bool GENERATE_AST = false;

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

      /*
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
      */

/*
      var valeCode = @"
      fn main() {
        a! int = 1;
        while (a < 42) {
          mut a = a + 1;
        }
        = a;
      }
      ";
  */
/*
      var valeCode = @"
        fn main() {
          a = [23, 31, 37, 42, 49];
          = a.3;
        }
      ";
*/

      var valeCode = @"
        // immutable unknown-size-array

        fn Arr<M, F>(n int, generator &F) Array<M, T>
        rules(M Mutability, T Ref, Prot(""__call"", (&F, int), T))
        {
          Array<M>(n, &IFunction1<mut, int, T>(generator))
        }

        fn main() {
          a = Arr<imm>(5, {_});
          = a[3];
        }
      ";


      Console.WriteLine("Vale code:");
      Console.WriteLine(valeCode);

      Console.WriteLine("----------");

      string astJson = null;
      if (GENERATE_AST) {
        astJson = ValeHelper.Build(valeCode);

        Console.WriteLine("AST:");
        Console.WriteLine(astJson);
        Console.WriteLine("----------");
        File.WriteAllText("ast.json", astJson);
      }
      if (astJson == null)
        astJson = File.ReadAllText("ast.json");

      var ast = (IProgram)Json.Deserialize<AstModel>(astJson);

      using (var outputFileStream = File.Create("output.js"))
      using (var outputFileStreamWriter = new StreamWriter(outputFileStream, Encoding.UTF8)) {
        foreach (var code in JavaScriptGenerator.Generate(ast)) {
          outputFileStreamWriter.Write(code);
          Console.Write(code);
        }
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
