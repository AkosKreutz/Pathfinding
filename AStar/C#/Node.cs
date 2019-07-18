using System;
using System.Collections.Generic;

namespace Pathfinding {

  /// <summary>
  /// The Main class for the node which is the building block of the board.
  /// </summary>
  class Node {
    public enum Types {Wall, Floor, Start, Destination, Path} //Node Types

    public Types type {get; set;} //The type of the node.
    public Node parent {get; set;} //Parent node, used for backtracking the path.
    public List<Node> neighbours {get; set;} //The neighbours of the node.

    public int x {get; private set;} //The X position of the node.
    public int y {get; private set;} //The Y position of the node.
    public int cost {get; private set;} //The cost of the node.

    public int G {get; set;} //The distance between the node and the starting node.
    public int H {get; set;} //The heuristic distance between the node and the destination node.
    public int F {get; set;} //The total cost of a node

    /// <summary>
    /// This constructor ensures that the node always has a valid position and type.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public Node(Types type, int x, int y){
      this.type = type;
      this.x = x;
      this.y = y;
    }

    /// <summary>
    /// The string representation of a node depends on it's type
    /// </summary>
    /// <returns>
    /// The string representation of the node.
    /// </returns>
    public override string ToString(){
      if(type.Equals(Types.Wall)){
        return "X";
      } 
      else if(type.Equals(Types.Start)){
        return "S";
      } 
      else if(type.Equals(Types.Destination)){
        return "D";
      } 
      else if(type.Equals(Types.Path)){
        return "*";
      } 
      else {
        return "-";
      }
    }
  }
}
