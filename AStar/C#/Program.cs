using System;
using System.Collections.Generic;

namespace Pathfinding {

  /// <summary>
  /// The Main Program Loop
  /// </summary>
  class Program {
    private static Board boardHandler; //Board instance
    private static AStar pathHandler; //Pathfinding instance

    /// <summary>
    /// Instantiates the handlers and starts an infinite loop, which can be only escaped through user input.
    /// </summary>
    /// <param name="args">CLI Arguments</param>
    /// <seealso cref="Program.ProcessInput(string)"/>
    static void Main(string[] args) {
      boardHandler = new Board();
      pathHandler = new AStar(boardHandler);

      //Starts an infinite loop which always generates a new board.
      while(true){
        System.Console.Clear();

        ProcessInput("help");
        System.Console.WriteLine("Press any key to start.");
        ProcessInput(System.Console.ReadLine());

        boardHandler.GenerateBoard(10,10);
        
        MainLoop();
      }
    }

    /// <summary>
    /// Ask the user to set the start and destination node, the calculates the path.
    /// </summary>
    /// <seealso cref="Program.SetCoordinates(out int, out int, string)"/>
    /// <seealso cref="Program.ProcessInput(string)"/>
    private static void MainLoop(){
      int x, y;

      SetCoordinates(out x, out y, "starting");

      boardHandler.SetStartingNode(x,y);
      boardHandler.DrawBoard();

      SetCoordinates(out x, out y, "destination");

      boardHandler.SetDestinationNode(x,y);
      boardHandler.DrawBoard();

      System.Console.WriteLine("Press any key to start the pathfinding.");
      ProcessInput(System.Console.ReadLine());

      Stack<Node> path = pathHandler.GetPath();

      if(path.Count == 0){
        System.Console.WriteLine("No Path found.");
      } else {
        System.Console.WriteLine("Path found.");
      }

      boardHandler.MarkPath(path);
      boardHandler.DrawBoard();

      ProcessInput(System.Console.ReadLine());
    }

    /// <summary>
    /// Checks if the string is a number, if it is, sets the number via reference.
    /// </summary>
    /// <param name="input">The input string which needs to be evaluated.</param>
    /// <param name="number">The reference number which requires the value.</param>
    /// <returns>
    /// If the string is a number.
    /// </returns>
    private static bool IsItANumber(string input, out int number){
      return int.TryParse(input, out number);
    }

    /// <summary>
    /// Ask the user until a correct X and Y coordinate is provided.
    /// </summary>
    /// <param name="x">The X coordinate reference.</param>
    /// <param name="y">The Y coordinate reference.</param>
    /// <param name="name">The name of which node type requires the values.</param>
    /// <seealso cref="Program.SetCoordinate(string, out int)"/>
    private static void SetCoordinates(out int x, out int y, string name){
      do {
        boardHandler.DrawBoard();

        SetCoordinate(string.Format("Please type the {0} column number.", name), out x);
        SetCoordinate(string.Format("Please type the {0} row number.", name), out y);

      } while (!boardHandler.IsCoordinateAvailable(x,y));
    }

    /// <summary>
    /// Displays a guide text for the user and evaluates if the input fits the requirements. Repeats until a valid response is not given.
    /// </summary>
    /// <param name="text">The guide text, which ask the input from the user.</param>
    /// <param name="coordinate">The required coordinate.</param>
    private static void SetCoordinate(string text, out int coordinate){
      System.Console.WriteLine(text);

      while(!IsItANumber(ProcessInput(System.Console.ReadLine()), out coordinate)){
        System.Console.WriteLine("Please try again.");
      }
    }

    /// <summary>
    /// Checks the user input for commands.
    /// </summary>
    /// <param name="input">The input that the user has provided.</param>
    /// <returns>
    /// The input string.
    /// </returns>
    private static string ProcessInput(string input){

      if(input.ToLower().Equals("exit")){
        Environment.Exit(0);
      } 
      else if(input.ToLower().Equals("help")){
        System.Console.WriteLine("Symbol Description{0}- : floor node.{0}X : wall node.{0}S : starting node.{0}D : destination node.{0}* : path node.{0}Commands{0}Type exit to close the application.", Environment.NewLine);
      }

      return input;
    }
  }
}
