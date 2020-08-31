using System;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;

namespace dev.vale.awesome {

  public class Compilifier {
    private const bool GENERATE_AST = true;

    public static void Main() {
      var valeCode = @"

fn eachI<F>(arr &Array<_, _>, func F) void {
  i! = 0;
  l = len(&arr);
  while (i < l) {
    func(i, arr[i]);
    mut i = i + 1;
  }
}


fn Arr<M, F>(n int, generator &F) Array<M, T>
rules(M Mutability, T Ref, Prot(""__call"", (&F, int), T))
{
  Array<M>(n, &IFunction1<mut, int, T>(generator))
}


struct Vec<N, T> rules(N int)
{
  values [<imm> N * T];
}

// struct Goblin {
//   location Vec<2, int>;
// }

fn makeBoard() Array<mut, Array<mut, str>> {
  ret
    Arr<mut>(10, (row){
      Arr<mut>(10, (col){
        = if (row == 0) { ""#"" }
          else if (col == 0) { ""#"" }
          else if (row == 9) { ""#"" }
          else if (col == 9) { ""#"" }
          else { ""."" }
      })
    });
}

fn display(
    board &Array<mut, Array<mut, str>>,
    //goblins &HashMap<int, Goblin, IntHasher, IntEquator>,
    playerRow int,
    playerCol int)
{
  toPrint! = """";
  eachI (&board) (rowI, row){
    eachI (&row) (cellI, cell){
      charToPrint! = cell;

      if (and(rowI == playerRow, cellI == playerCol)) {
        mut charToPrint = ""@"";
      }
      // else {
      //   goblins.keys().each((key){
      //     goblin? = goblins.get(key);
      //     goblin = goblin?^.get(); // TODO try getting rid of this ^, doesnt wanna find the get function

      //     if (and(rowI == goblin.location.values.1, cellI == goblin.location.values.0)) {
      //       mut charToPrint = ""g"";
      //     }
      //   });
      // }

      mut toPrint = toPrint + charToPrint;
    }
    mut toPrint = toPrint + ""\n"";
  }
  print(toPrint);
}

fn inputKey() int {
  key! = 0;
  done! = false;
  while (not done) {
    mut key = __getch();
    if (key == 81) {
      mut done = true;
    } else if (key == 119) {
      mut done = true;
    } else if (key == 115) {
      mut done = true;
    } else if (key == 97) {
      mut done = true;
    } else if (key == 100) {
      mut done = true;
    } else if (key == 10) {
      // Enter key, do nothing
    }
    // Continue
  }
  ret key;
}


// fn goblinAt?(
//   goblins &HashMap<int, Goblin, IntHasher, IntEquator>,
//   goblinKey int,
//   row int,
//   col int)
// bool {
//   a = goblins.get(goblinKey);
//   b = a^.get();
//   (goblinCol, goblinRow) = b.location.values;
//   ret and(row == goblinRow, col == goblinCol);
// }


fn main() int {
  board = makeBoard();

  playerRow! = 4;
  playerCol! = 3;

  // goblins = HashMap<int, Goblin, IntHasher, IntEquator>(IntHasher(), IntEquator());
  // goblins.add(13, Goblin(Vec([6, 3])));
  // goblins.add(14, Goblin(Vec([2, 6])));
  // goblins.add(15, Goblin(Vec([5, 7])));
  // goblins.add(17, Goblin(Vec([2, 5])));
  // goblins.add(19, Goblin(Vec([7, 1])));
  // goblins.add(23, Goblin(Vec([3, 3])));
  // goblins.add(24, Goblin(Vec([1, 7])));
  // goblins.add(25, Goblin(Vec([7, 6])));
  // goblins.add(27, Goblin(Vec([4, 6])));
  // goblins.add(29, Goblin(Vec([7, 4])));

  running! = true;
  while (running) {
    display(
        &board,
        // &goblins,
        playerRow,
        playerCol);

    key = inputKey();
    newPlayerRow! = playerRow;
    newPlayerCol! = playerCol;
    if (key == 81) {
      mut running = false;
    } else if (key == 119) {
      mut newPlayerRow = newPlayerRow - 1;
    } else if (key == 115) {
      mut newPlayerRow = newPlayerRow + 1;
    } else if (key == 97) {
      mut newPlayerCol = newPlayerCol - 1;
    } else if (key == 100) {
      mut newPlayerCol = newPlayerCol + 1;
    }

    killedGoblin! = false;
    // goblins.keys().each((key){
    //   if (goblinAt?(&goblins, key, newPlayerRow, newPlayerCol)) {
    //     goblins.remove(key);
    //     mut killedGoblin = true;
    //   }
    // });
    // if (not killedGoblin) {
      if (board[newPlayerRow][newPlayerCol] == ""."") {
        mut playerRow = newPlayerRow;
        mut playerCol = newPlayerCol;
      }
    // }
  }
  = 0;
}


      ";
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


      Console.WriteLine("Vale code:");
      Console.WriteLine(valeCode);

      Console.WriteLine("----------");

      string astJson = null;
      if (GENERATE_AST) {
        astJson = ValeHelper.Build(valeCode);

        Console.WriteLine("AST:");
        //Console.WriteLine(astJson);
        Console.WriteLine("----------");
        File.WriteAllText("ast.json", astJson);
      }
      if (astJson == null)
        astJson = File.ReadAllText("ast.json");

      var ast = (IProgram)Json.Deserialize<AstModel>(astJson);

      using (var outputFileStream = File.Create("output.js"))
      using (var outputFileStreamWriter = new StreamWriter(outputFileStream, Encoding.ASCII)) {
        foreach (var code in JavaScriptGenerator.Generate(ast)) {
          outputFileStreamWriter.Write(code);
          // Console.Write(code);
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
