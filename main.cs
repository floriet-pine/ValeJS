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
    private static readonly string VALE_CODE = @"

fn Arr<M, F>(n int, generator &F) Array<M, T>
rules(M Mutability, T Ref, Prot(""__call"", (&F, int), T))
{
  Array<M>(n, &IFunction1<mut, int, T>(generator))
}

fn each<M, N, T, F>(arr &[<M> N * T], func F) void {
  i! = 0;
  l = len(&arr);
  while (i < l) {
    func(arr[i]);
    mut i = i + 1;
  }
}

fn eachI<F>(arr &Array<_, _>, func F) void {
  i! = 0;
  l = len(&arr);
  while (i < l) {
    func(i, arr[i]);
    mut i = i + 1;
  }
}


struct Vec<N, T> rules(N int)
{
  values [<imm> N * T];
}

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
  toPrint! = ""\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n"";
  eachI (&board) (rowI, row){
    eachI (&row) (cellI, cell){
      charToPrint! = cell;

      if (and(rowI == playerRow, cellI == playerCol)) {
        mut charToPrint = ""@"";
      }

      mut toPrint = toPrint + charToPrint;
    }
    mut toPrint = toPrint + ""\n"";
  }
  print(toPrint);
}

// Make this a real pause when Node gets that functionality.
fn pause() {
  i! = 0;
  while (i < 4000000) {
    mut i = i + 1;
  }
}

fn main() int {
  board = makeBoard();

  playerRow! = 4;
  playerCol! = 3;

  display(&board, playerRow, playerCol);

  directions = [
    ""up"",
    ""right"",
    ""up"",
    ""right"",
    ""down"",
    ""left"",
    ""up"",
    ""right"",
    ""down"",
    ""left"",
    ""up"",
    ""right"",
    ""down"",
    ""right"",
    ""down"",
    ""left"",
    ""up"",
    ""right"",
    ""down"",
    ""left"",
    ""up"",
    ""left"",
    ""up"",
    ""right"",
    ""down"",
    ""left"",
    ""up""];

  each (directions) (direction){
    pause();

    newPlayerRow = playerRow;
    newPlayerCol = playerCol;
    if (direction == ""up"") {
      mut newPlayerRow = newPlayerRow - 1;
    } else if (direction == ""down"") {
      mut newPlayerRow = newPlayerRow + 1;
    } else if (direction == ""left"") {
      mut newPlayerCol = newPlayerCol - 1;
    } else if (direction == ""right"") {
      mut newPlayerCol = newPlayerCol + 1;
    }

    if (board[newPlayerRow][newPlayerCol] == ""."") {
      mut playerRow = newPlayerRow;
      mut playerCol = newPlayerCol;
      display(&board, playerRow, playerCol);
    }
  }

  = 0;
}
";

    public static void Main() {
      Console.WriteLine("Compiling...");

      // This sends a request to the Vale compile server, and
      // results in some JSON representing VIR (Vale Intermediate Representation).
      string astJson = ValeHelper.Build(VALE_CODE);
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
