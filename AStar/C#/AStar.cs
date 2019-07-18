using System;
using System.Collections.Generic;

namespace Pathfinding {

  /// <summary>
  /// The Main class for A* pathfinding.
  /// Contains all the logic required to generate the path.
  /// </summary>
  class AStar {

    private Board boardHandler {get; set;} //The Board on which the algorithm searches the path.

    /// <summary>
    /// Sets the reference Board to ensure that the functions can work properly.
    /// </summary>
    /// <param name="boardHandler">The Board on which the path is calculated.</param>
    public AStar(Board boardHandler){
      this.boardHandler = boardHandler;
    }

    /// <summary>
    /// Calculates the path to the target using the A* algorithm, then returns it. If there is not path, returns an empty stack.
    /// </summary>
    public Stack<Node> GetPath(){
      Stack<Node> path = new Stack<Node>();

      //Creating the open and closed node list.
      List<Node> openNodes = new List<Node>();
      List<Node> closedNodes = new List<Node>();

      //References for the start and destination node for easier use.
      Node startNode = boardHandler.startingNode;
      Node destinationNode = boardHandler.destinationNode;

      openNodes.Add(startNode);

      do {
        
        //Selects the node with the lowest F value from the open node list, then checks it's neighbours.
        Node currentTile = GetTileWithLowestFValue(openNodes);

        closedNodes.Add(currentTile);
        openNodes.Remove(currentTile);

        //If the closed node list contains the destination, then the path is completed.
        if (closedNodes.Contains(destinationNode)) {

          //Backtracking and adding the steps to the stack.
          while (currentTile.parent != null) {
            path.Push(currentTile);
            currentTile = currentTile.parent;
          }
          break;
        }

        //Checks each neighbour of the node.
        foreach (Node neighbour in currentTile.neighbours) {

          //Filtering out nodes that are not fit for path.
          if (!neighbour.type.Equals(Node.Types.Wall)) {

            if (closedNodes.Contains(neighbour)) {
              continue;
            }

            neighbour.G = GetGValue(currentTile)+ neighbour.cost;
            neighbour.H = GetHValue(neighbour);
            neighbour.F = GetFValue(neighbour);

            //If it's not in the open list, add it and set the parent.
            //Else check if the F value is better with the current node, if it is then set the it's parent to the current node.
            if (!openNodes.Contains(neighbour)) {
              neighbour.parent = currentTile;
              openNodes.Add(neighbour);
            }
            else { 
              if(GetFValue(neighbour) > GetFValueWithG(neighbour, currentTile.G)){
                neighbour.parent = currentTile;
              }
            }
          }
        }
      } while (openNodes.Count > 0);

      return path;
    }

    /// <summary>
    /// Searches for the node with the lowest total cost in the node list.
    /// </summary>
    /// <param name="nodes">List of nodes.</param>
    /// <returns>
    /// The node with the lowest total cost.
    /// </returns>
    private Node GetTileWithLowestFValue(List<Node> nodes){
      Node chosenNode = null;

      foreach (Node openNode in nodes) {
        if (chosenNode == null || GetFValue(openNode) < GetFValue(chosenNode)){
          chosenNode = openNode;
        }
      }

      return chosenNode;
    }

    /// <summary>
    /// This is used to calculate the F value of a node by using the G value of another node.
    /// </summary>
    /// <param name="node">The node that used as a basis for the G value.</param>
    /// <param name="gValue">The G value of another node</param>
    /// <returns></returns>
    private int GetFValueWithG(Node node, int gValue) {
      return node.H + gValue;
    }

    /// <summary>
    /// Calculates the total cost of a node.
    /// </summary>
    /// <param name="node"></param>
    /// <returns>
    /// The sum of the G and H value.
    /// </returns>
    private int GetFValue(Node node){
      return node.H + node.G;
    }

    /// <summary>
    /// Calculates the heuristic distance between the node and the starting node.
    /// </summary>
    /// <param name="node">The node which position is used for the calculation.</param>
    /// <seealso cref="AStar.GetHeuristicDistance(Node, Node)"/>
    /// <returns>
    /// The absolute distance between the starting and the given node.
    /// </returns>
    private int GetGValue(Node node) {
      return GetHeuristicDistance(boardHandler.startingNode, node);
    }

    /// <summary>
    /// Calculates the heuristic distance between the node and the destination node.
    /// </summary>
    /// <param name="node">The node which position is used for the calculation.</param>
    /// <seealso cref="AStar.GetHeuristicDistance(Node, Node)"/>
    /// <returns>
    /// The absolute distance between the destination and the given node.
    /// </returns>
    private int GetHValue(Node node) {
      return GetHeuristicDistance(boardHandler.destinationNode, node);
    }

    /// <summary>
    /// Calculates the heuristic distance between two nodes.
    /// </summary>
    /// <param name="node1"></param>
    /// <param name="node2"></param>
    /// <returns>
    /// The absolute distance between two nodes.
    /// </returns>
    private int GetHeuristicDistance(Node node1, Node node2){
      return Math.Abs(node1.x - node2.x) + Math.Abs(node1.y - node2.y);
    }
  }
}
