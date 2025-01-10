using Spectre.Console;
using MazeBuilder;
using UserInterface;
namespace Tiles
{
    class Tile
    {  // Properties of the class
        public (int, int) Position { get; set; }
        public Color Appearance { get; set; }
        public (int, int)[] direction = { (0, -1), (1, 0), (0, 1), (-1, 0) };

        // Constructor
        public Tile((int, int) position, Color appearance)
        {
            Position = position;
            Appearance = appearance;

        }

        // Method for tile movement
        public bool Movement(ConsoleKeyInfo keyInput)
        {
            ConsoleKey[] key = { ConsoleKey.W, ConsoleKey.D, ConsoleKey.S, ConsoleKey.A };
            for (int i = 0; i < 4; i++)
            {
                if (keyInput.Key == key[i]
                            && Position.Item1 + direction[i].Item1 >= 0 && Position.Item1 + direction[i].Item1 < Maze.mainWidth
                            && Position.Item2 + direction[i].Item2 >= 0 && Position.Item2 + direction[i].Item2 < Maze.mainHeight
                            && !Maze.mainMaze[Position.Item1, Position.Item2].Wall[i]
                            && !Maze.mainMaze[Position.Item1 + direction[i].Item1, Position.Item2 + direction[i].Item2].Occuped)
                {

                    Maze.mainMaze[Position.Item1, Position.Item2].Occuped = false;
                    Position = (Position.Item1 + direction[i].Item1, Position.Item2 + direction[i].Item2);
                    Maze.mainMaze[Position.Item1, Position.Item2].Occuped = true;

                    return true;
                }
            }
            return false;
        }




    }
}