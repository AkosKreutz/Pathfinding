# CSharp A* Implementation

## Introduction
The goal of this project was to create an easy to understand implementation of the A* pathfinding algorithm in C#.
To make the project more user friendly I created a simple CLI BoardGame in which you can try out the algorithm.

## Things that can be improved
As the main focus of this project was to implement the pathfinding algorithm (and not to create a game) the whole Demo part could be improved. 
Example: 
1. Baking the node data once, after the start and destination node is selected would be much better, but I stuck with a more common logic as it's more flexible.
2. Currently the Node cost variable is always 0, but it can be used to indicate different terrains, etc.

## Example output
```
  0 1 2 3 4 5 6 7 8 9
0 X X X X X X X X X X
1 X * * * - - * * * X
2 X * X * * - * X * X
3 X * S X * * * X D X
4 X - X X - - - X - X
5 X - - - - - - - - X
6 X - - - - - - - - X
7 X - - - X - - - - X
8 X - - - - - - - - X
9 X X X X X X X X X X

  0 1 2 3 4 5 6 7 8 9
0 X X X X X X X X X X
1 X S * * X X - * D X
2 X - - * * * X * - X
3 X - - - - * * * - X
4 X - - - - - - - X X
5 X - - - - - - - - X
6 X - X X - - - X - X
7 X - - - - - X - - X
8 X - - - - X - - - X
9 X X X X X X X X X X
```

## Example Scenario
```
Symbol Description
- : floor node.
X : wall node.
S : starting node.
D : destination node.
* : path node.
Commands
Type exit to close the application.
Press any key to start.

  0 1 2 3 4 5 6 7 8 9 
0 X X X X X X X X X X 
1 X - X - X - - - - X 
2 X - - - - - - - - X 
3 X - - - - - - - X X
4 X - - - X - - - - X
5 X - - - - - - - X X
6 X X - X - - - - - X
7 X X - - X - - - - X
8 X - - - - X - - X X
9 X X X X X X X X X X

Please type the starting column number.
4
Please type the starting row number.
8
  0 1 2 3 4 5 6 7 8 9 
0 X X X X X X X X X X
1 X - X - X - - - - X
2 X - - - - - - - - X
3 X - - - - - - - X X
4 X - - - X - - - - X
5 X - - - - - - - X X
6 X X - X - - - - - X
7 X X - - X - - - - X
8 X - - - S X - - X X
9 X X X X X X X X X X

  0 1 2 3 4 5 6 7 8 9
0 X X X X X X X X X X
1 X - X - X - - - - X
2 X - - - - - - - - X
3 X - - - - - - - X X
4 X - - - X - - - - X
5 X - - - - - - - X X
6 X X - X - - - - - X
7 X X - - X - - - - X
8 X - - - S X - - X X
9 X X X X X X X X X X

Please type the destination column number.
6
Please type the destination row number.
8
  0 1 2 3 4 5 6 7 8 9 
0 X X X X X X X X X X
1 X - X - X - - - - X
2 X - - - - - - - - X
3 X - - - - - - - X X
4 X - - - X - - - - X
5 X - - - - - - - X X
6 X X - X - - - - - X
7 X X - - X - - - - X
8 X - - - S X D - X X
9 X X X X X X X X X X

Press any key to start the pathfinding.

Path found.
  0 1 2 3 4 5 6 7 8 9
0 X X X X X X X X X X
1 X - X - X - - - - X
2 X - - - - - - - - X
3 X - - - - - - - X X
4 X - - - X - - - - X
5 X - * * * - - - X X
6 X X * X * * - - - X
7 X X * * X * * - - X
8 X - - * S X D - X X
9 X X X X X X X X X X
```