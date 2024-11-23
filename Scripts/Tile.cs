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
            if (keyInput.Key == ConsoleKey.W && Position.Item2 != 0
                             && Maze.mainMaze[Position.Item1, Position.Item2].Wall[(int)WallDir.N] == false
                             && Maze.mainMaze[Position.Item1, Position.Item2 - 1].Occuped == false)
            {
                MazeCanvas.RemoveTile(tile);
                Maze.mainMaze[Position.Item1, Position.Item2].Occuped = false;
                Position = (Position.Item1, Position.Item2 - 1);
                tile.Position = Position;
                Maze.mainMaze[Position.Item1, Position.Item2].Occuped = true;
                MazeCanvas.AddTile(tile);
                return true;
            }
            //Hacia derecha
            if (keyInput.Key == ConsoleKey.D && Position.Item1 != (Maze.mainWidth + 1)
                                                      && Maze.mainMaze[Position.Item1, Position.Item2].Wall[(int)WallDir.E] == false
                                                      && Maze.mainMaze[Position.Item1 + 1, Position.Item2].Occuped == false)
            {
                MazeCanvas.RemoveTile(tile);
                Maze.mainMaze[Position.Item1, Position.Item2].Occuped = false;
                Position = (Position.Item1 + 1, Position.Item2);
                tile.Position = Position;
                Maze.mainMaze[Position.Item1, Position.Item2].Occuped = true;
                MazeCanvas.AddTile(tile);
                return true;
            }
            //Hacia izquierda
            if (keyInput.Key == ConsoleKey.A && Position.Item1 != 0
                                                     && Maze.mainMaze[Position.Item1, Position.Item2].Wall[(int)WallDir.W] == false
                                                     && Maze.mainMaze[Position.Item1 - 1, Position.Item2].Occuped == false)
            {
                MazeCanvas.RemoveTile(tile);
                Maze.mainMaze[Position.Item1, Position.Item2].Occuped = false;
                Position = (Position.Item1 - 1, Position.Item2);
                tile.Position = Position;
                Maze.mainMaze[Position.Item1, Position.Item2].Occuped = true;
                MazeCanvas.AddTile(tile);
                return true;
            }
            //Hacia abajo
            if (keyInput.Key == ConsoleKey.S && Position.Item2 != (Maze.mainHeight - 1)
                                                     && Maze.mainMaze[Position.Item1, Position.Item2].Wall[(int)WallDir.S] == false
                                                     && Maze.mainMaze[Position.Item1, Position.Item2 + 1].Occuped == false)
            {
                MazeCanvas.RemoveTile(tile);
                Maze.mainMaze[Position.Item1, Position.Item2].Occuped = false;
                Position = (Position.Item1, Position.Item2 + 1);
                tile.Position = Position;
                Maze.mainMaze[Position.Item1, Position.Item2].Occuped = true;
                MazeCanvas.AddTile(tile);
                return true;
            }

            return false;
        }




    }
}