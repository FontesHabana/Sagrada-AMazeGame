using Spectre.Console;
using MazeBuilder;
using UserInterface;
namespace Tiles
{
    class Tile
    { //Propiedades de la clase
        public (int, int) Position { get; set; }
        public Color Appearance { get; set; }
        public (int, int)[] direction = { (0, -1), (1, 0), (0, 1), (-1, 0) };


        public Tile((int, int) position, Color appearance)
        {
            Position = position;
            Appearance = appearance;

        }
        //Definimos el movimiento de una ficha en el tablero
        //
        public bool Movement(ConsoleKeyInfo keyInput)
        {
            ConsoleKey[] key = { ConsoleKey.W, ConsoleKey.D, ConsoleKey.S, ConsoleKey.A };
            for (int i = 0; i < 4; i++)
            {
                if (keyInput.Key == ConsoleKey.W && Position.Item2 != 0
                            && Maze.mainMaze[Position.Item1, Position.Item2].Wall[(int)WallDir.N] == false
                            && Maze.mainMaze[Position.Item1, Position.Item2 - 1].Occuped == false)
                {

                    Maze.mainMaze[Position.Item1, Position.Item2].Occuped = false;
                    Position = (Position.Item1, Position.Item2 - 1);
                    Position = Position;
                    Maze.mainMaze[Position.Item1, Position.Item2].Occuped = true;

                    return true;
                }
            }

            //Hacia arriba
            if (keyInput.Key == ConsoleKey.W && Position.Item2 != 0
                             && Maze.mainMaze[Position.Item1, Position.Item2].Wall[(int)WallDir.N] == false
                             && Maze.mainMaze[Position.Item1, Position.Item2 - 1].Occuped == false)
            {

                Maze.mainMaze[Position.Item1, Position.Item2].Occuped = false;
                Position = (Position.Item1, Position.Item2 - 1);
                Position = Position;
                Maze.mainMaze[Position.Item1, Position.Item2].Occuped = true;

                return true;
            }
            //Hacia derecha
            if (keyInput.Key == ConsoleKey.D && Position.Item1 != (Maze.mainWidth + 1)
                                                      && Maze.mainMaze[Position.Item1, Position.Item2].Wall[(int)WallDir.E] == false
                                                      && Maze.mainMaze[Position.Item1 + 1, Position.Item2].Occuped == false)
            {

                Maze.mainMaze[Position.Item1, Position.Item2].Occuped = false;
                Position = (Position.Item1 + 1, Position.Item2);
                Position = Position;
                Maze.mainMaze[Position.Item1, Position.Item2].Occuped = true;

                return true;
            }
            //Hacia izquierda
            if (keyInput.Key == ConsoleKey.A && Position.Item1 != 0
                                                     && Maze.mainMaze[Position.Item1, Position.Item2].Wall[(int)WallDir.W] == false
                                                     && Maze.mainMaze[Position.Item1 - 1, Position.Item2].Occuped == false)
            {

                Maze.mainMaze[Position.Item1, Position.Item2].Occuped = false;
                Position = (Position.Item1 - 1, Position.Item2);
                Position = Position;
                Maze.mainMaze[Position.Item1, Position.Item2].Occuped = true;

                return true;
            }
            //Hacia abajo
            if (keyInput.Key == ConsoleKey.S && Position.Item2 != (Maze.mainHeight - 1)
                                                     && Maze.mainMaze[Position.Item1, Position.Item2].Wall[(int)WallDir.S] == false
                                                     && Maze.mainMaze[Position.Item1, Position.Item2 + 1].Occuped == false)
            {

                Maze.mainMaze[Position.Item1, Position.Item2].Occuped = false;
                Position = (Position.Item1, Position.Item2 + 1);
                Position = Position;
                Maze.mainMaze[Position.Item1, Position.Item2].Occuped = true;

                return true;
            }

            return false;
        }




    }
}