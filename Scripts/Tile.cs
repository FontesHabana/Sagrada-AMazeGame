using Spectre.Console;
using MazeBuilder;
using UserInterface;
namespace Tiles
{
    class Tile
    { //Propiedades de la clase
        public (int, int) Position { get; set; }
        public Color Appearance { get; set; }


        public Tile((int, int) position, Color appearance)
        {
            Position = position;
            Appearance = appearance;
        }
        //Definimos el movimiento de una ficha en el tablero
        public bool Movement(ConsoleKeyInfo keyInput)
        {
            Tile tile = new Tile(Position, Appearance);
            //Hacia arriba
            if (keyInput.Key == ConsoleKey.UpArrow && Position.Item2 != 0 && Maze.mainMaze[Position.Item1, Position.Item2].Wall[(int)WallDir.N] == false)
            {
                MazeCanvas.RemoveTile(tile);
                Position = (Position.Item1, Position.Item2 - 1);
                tile.Position = Position;
                MazeCanvas.AddTile(tile);
                return true;
            }
            //Hacia derecha
            if (keyInput.Key == ConsoleKey.RightArrow && Position.Item1 != (Maze.mainWidth + 1) && Maze.mainMaze[Position.Item1, Position.Item2].Wall[(int)WallDir.E] == false)
            {
                MazeCanvas.RemoveTile(tile);
                Position = (Position.Item1 + 1, Position.Item2);
                tile.Position = Position;
                MazeCanvas.AddTile(tile);
                return true;
            }
            //Hacia izquierda
            if (keyInput.Key == ConsoleKey.LeftArrow && Position.Item1 != 0 && Maze.mainMaze[Position.Item1, Position.Item2].Wall[(int)WallDir.W] == false)
            {
                MazeCanvas.RemoveTile(tile);
                Position = (Position.Item1 - 1, Position.Item2);
                tile.Position = Position;
                MazeCanvas.AddTile(tile);
                return true;
            }
            //Hacia abajo
            if (keyInput.Key == ConsoleKey.DownArrow && Position.Item2 != (Maze.mainHeight - 1) && Maze.mainMaze[Position.Item1, Position.Item2].Wall[(int)WallDir.S] == false)
            {
                MazeCanvas.RemoveTile(tile);
                Position = (Position.Item1, Position.Item2 + 1);
                tile.Position = Position;
                MazeCanvas.AddTile(tile);
                return true;
            }

            return false;
        }




    }
}