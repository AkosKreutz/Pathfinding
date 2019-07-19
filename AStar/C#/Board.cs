using System;
using System.Collections.Generic;
using System.Text;

namespace Pathfinding {

  /// <summary>
  /// The Main class for the Board, which contains all the board related functions.
  /// </summary>
  class Board {
    public int width {get; private set;} //The width of the board.
    public int height {get; private set;} //The height of the board.
    public Node[,] nodes {get; private set;} //Matrix that holds all the nodes, which is essentially the board itself.

    public Node startingNode {get; private set;} //Reference for the starting node
    public Node  destinationNode {get; private set;} //Reference for the destination node
    private Random rng = new Random();

    /// <summary>
    /// Instantiates the node matrix with the given dimensions, then fills it up with wall and floor nodes.
    /// </summary>
    /// <param name="width">The width of the board</param>
    /// <param name="height">The height of the board</param>
    /// <seealso cref="Board.nodes"/>
    /// <seealso cref="Board.GetNeighbours(Node)"/>
    public void GenerateBoard(int width, int height){
      this.width = width;
      this.height = height;

      this.nodes = new Node[height,width];

      //Iterates through the matrix
      for(int i = 0; i < height; i++){
        for(int j = 0; j < width; j++){
          Node.Types type;

          //If the node is on the edge, then it becomes a wall.
          //Else there is a 10% chache for it to become a wall, otherwise it will be a floor node.
          if(i == 0 || j == 0 || i == height - 1|| j == width - 1){
            type = Node.Types.Wall;
          } else {
            if(rng.Next(0,101) < 15){
              type = Node.Types.Wall;
            } else {
              type = Node.Types.Floor;
            }
          }

          nodes[i,j] = new Node(type,j,i);
        }
      }

      //Iterates through the nodes and set their neighbours.
      //This needs to be done after the previous iteration to ensure there is no null node.
      for(int i = 0; i < height; i++){
        for(int j = 0; j < width; j++){
          nodes[i,j].neighbours = GetNeighbours(nodes[i,j]);
        }
      }
    }

    /// <summary>
    /// Generates a string from the node matrix and adds marking numbers to it, then writes it to the console.
    /// </summary>
    public void DrawBoard(){
      //Using StringBuilder for better performance as the board size can change.
      StringBuilder board = new StringBuilder();
      StringBuilder horizontalNumberRow = new StringBuilder(" ", (width * 2) + 2);

      //Iterates through the board and appends their values to the StringBuilder.
      for(int i = 0; i < height; i++){
        for(int j = 0; j < width; j++){

          if(i == 0){
            horizontalNumberRow.Append(j.ToString() + " ");
          } 
          if(j == 0){
            board.Append(i + " ");
          }

          board.Append(nodes[i,j]);

          if(i != width - 1 || j != height - 1){
            board.Append(" ");
          }
        }
        board.Append(Environment.NewLine);
      }

      string horizontalNumberString = horizontalNumberRow.Insert(0, " ").ToString();
      Console.WriteLine(horizontalNumberRow.ToString() + Environment.NewLine + board.ToString());
    }

    /// <summary>
    /// Checks if is there a node at the coordinates and if it's a floor.
    /// </summary>
    /// <param name="x">The X coordinate of the node.</param>
    /// <param name="y">The Y coordinate of the node.</param>
    /// <seealso cref="Board.IsCoordinateWithinBounds(int, int)"/>
    /// <returns>
    /// If the coordinates are within the bound of the board size and if the node is a floor
    /// </returns>
    public bool IsCoordinateAvailable(int x, int y){

      if (IsCoordinateWithinBounds(x,y)){
        return nodes[y,x].type.Equals(Node.Types.Floor);
      }
      
      return false;
    }

    /// <summary>
    /// Checks if the given coordinates are within bounds of the Board size.
    /// </summary>
    /// <param name="x">The X coordinate of the node.</param>
    /// <param name="y">The Y coordinate of the node.</param>
    /// <returns>
    /// If the coordinates are within the bound of the board size.
    /// </returns>
    private bool IsCoordinateWithinBounds(int x, int y){
      return x < this.width && x >= 0 && y < this.height && y >= 0;
    }

    /// <summary>
    /// Sets the node to the given type.
    /// </summary>
    /// <param name="x">The X coordinate of the node.</param>
    /// <param name="y">The Y coordinate of the node.</param>
    /// <param name="type">The type to which the node should be set.</param>
    private void SetNode(int x, int y, Node.Types type){
      nodes[y,x].type = type;
    }

    /// <summary>
    /// Set the node type to Start and set the startingNode reference to node.
    /// </summary>
    /// <param name="x">The X coordinate of the node.</param>
    /// <param name="y">The Y coordinate of the node.</param>
    /// <seealso cref="Board.SetNode(int, int, Node.Types)"/>
    public void SetStartingNode(int x, int y){
      SetNode(x,y, Node.Types.Start);
      startingNode = nodes[y,x];
    }

    /// <summary>
    /// Set the node type to Destination and set the destinationNode reference to node.
    /// </summary>
    /// <param name="x">The X coordinate of the node.</param>
    /// <param name="y">The Y coordinate of the node.</param>
    /// <seealso cref="Board.SetNode(int, int, Node.Types)"/>
    public void SetDestinationNode(int x, int y){
      SetNode(x,y, Node.Types.Destination);
      destinationNode = nodes[y,x];
    }

    /// <summary>
    /// Set the node type to Path.
    /// </summary>
    /// <param name="x">The X coordinate of the node.</param>
    /// <param name="y">The Y coordinate of the node.</param>
    /// <seealso cref="Board.SetNode(int, int, Node.Types)"/>
    public void SetPathNode(int x, int y){
      SetNode(x,y, Node.Types.Path);
    }

    /// <summary>
    /// Iterates through the path and if the node type is floor marks it.
    /// </summary>
    /// <param name="path"></param>
    /// <seealso cref="Board.SetPathNode(int, int)"/>
    public void MarkPath(Stack<Node> path){
      while(path.Count > 0){

        Node currentNode = path.Pop();

        if(currentNode.type.Equals(Node.Types.Floor)){
          SetPathNode(currentNode.x, currentNode.y);
        }
      }
    }

    /// <summary>
    /// Runs a 4 connected node search for the node and saves the neighbours to the list.
    /// </summary>
    /// <param name="node"></param>
    /// <returns>
    /// A list of neighbours.
    /// </returns>
    private List<Node> GetNeighbours(Node node){
      List<Node> neighbours = new List<Node>();

      for (int i = node.x - 1; i <= node.x + 1; i++) {
        for (int j = node.y - 1; j <= node.y + 1; j++) {
          
          //This check ensures that the list will not contains the node and that there will be no null node.
          if((i == node.x || j == node.y) && !(i == node.x && j == node.y) && IsCoordinateWithinBounds(i,j)){
            neighbours.Add(nodes[j,i]);
          }
        }
      }

      return neighbours;
    }
  }
}
