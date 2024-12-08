using Spectre.Console;
using MazeBuilder;
using Tiles;
using LogicGame;
using System.ComponentModel;
namespace UserInterface
{
    class MazeCanvas
    {
        public static Canvas canvas = new Canvas(3 * Maze.mainWidth + 1, 3 * Maze.mainHeight + 1);


        public static void PrintMaze()
        {
            Cell[,] mainMaze = Maze.mainMaze;


            //Print Borders
            for (int x = 0; x < 3 * Maze.mainWidth + 1; x++)
            {
                for (int y = 0; y < 3 * Maze.mainHeight + 1; y++)
                {
                    if (x == 0 || x == 3 * Maze.mainWidth)
                    {
                        canvas.SetPixel(x, y, Color.Black);
                    }
                    else if (y == 0 || y == 3 * Maze.mainHeight)
                    {
                        canvas.SetPixel(x, y, Color.Black);
                    }
                }
            }
            //Imprimir Celdas
            for (int x = 0; x < Maze.mainWidth; x++)
            {
                for (int y = 0; y < Maze.mainHeight; y++)
                {

                    AddCell(x, y, Maze.mainMaze, canvas);

                }
            }

        }
        public static void AddCell(int x, int y, Cell[,] maze, Canvas canvas)
        {
            //Print Cell
            if (Maze.mainMaze[x, y].Visited)
            {
                canvas.SetPixel(3 * x + 1, 3 * y + 1, Color.White);
                canvas.SetPixel(3 * x + 2, 3 * y + 1, Color.White);
                canvas.SetPixel(3 * x + 1, 3 * y + 2, Color.White);
                canvas.SetPixel(3 * x + 2, 3 * y + 2, Color.White);
            }

            //Print East Wall
            if (!Maze.mainMaze[x, y].Wall[(int)WallDir.E] && x != Maze.mainWidth - 1)
            {
                canvas.SetPixel(3 * x + 3, 3 * y + 1, Color.White);
                canvas.SetPixel(3 * x + 3, 3 * y + 2, Color.White);
                canvas.SetPixel(3 * x + 3, 3 * y + 3, Color.Black);
            }
            else if (x != Maze.mainWidth - 1)
            {
                canvas.SetPixel(3 * x + 3, 3 * y + 1, Color.Black);
                canvas.SetPixel(3 * x + 3, 3 * y + 2, Color.Black);
                canvas.SetPixel(3 * x + 3, 3 * y + 3, Color.Black);
            }
            //Print South Wall

            if (!Maze.mainMaze[x, y].Wall[(int)WallDir.S] && y != Maze.mainHeight - 1)
            {
                canvas.SetPixel(3 * x + 1, 3 * y + 3, Color.White);
                canvas.SetPixel(3 * x + 2, 3 * y + 3, Color.White);
            }
            else if (y != Maze.mainHeight - 1)
            {
                canvas.SetPixel(3 * x + 1, 3 * y + 3, Color.Black);
                canvas.SetPixel(3 * x + 2, 3 * y + 3, Color.Black);
            }
        }

        public static void AddTrap(int x, int y, Cell[,] maze, Canvas canvas)
        {
            //Print Trap

            canvas.SetPixel(3 * x + 1, 3 * y + 1, Color.Aqua);
            canvas.SetPixel(3 * x + 2, 3 * y + 1, Color.Blue);
            canvas.SetPixel(3 * x + 1, 3 * y + 2, Color.Blue);
            canvas.SetPixel(3 * x + 2, 3 * y + 2, Color.Aqua);


            //Print East Wall
            if (!Maze.mainMaze[x, y].Wall[(int)WallDir.E] && x != Maze.mainWidth - 1)
            {
                canvas.SetPixel(3 * x + 3, 3 * y + 1, Color.White);
                canvas.SetPixel(3 * x + 3, 3 * y + 2, Color.White);
                canvas.SetPixel(3 * x + 3, 3 * y + 3, Color.Black);
            }
            else if (x != Maze.mainWidth - 1)
            {
                canvas.SetPixel(3 * x + 3, 3 * y + 1, Color.Black);
                canvas.SetPixel(3 * x + 3, 3 * y + 2, Color.Black);
                canvas.SetPixel(3 * x + 3, 3 * y + 3, Color.Black);
            }
            //Print South Wall

            if (!Maze.mainMaze[x, y].Wall[(int)WallDir.S] && y != Maze.mainHeight - 1)
            {
                canvas.SetPixel(3 * x + 1, 3 * y + 3, Color.White);
                canvas.SetPixel(3 * x + 2, 3 * y + 3, Color.White);
            }
            else if (y != Maze.mainHeight - 1)
            {
                canvas.SetPixel(3 * x + 1, 3 * y + 3, Color.Black);
                canvas.SetPixel(3 * x + 2, 3 * y + 3, Color.Black);
            }
        }


        //Elimina una pieza del tablero
        public static void RemoveTile(Tile tile)
        {
            canvas.SetPixel(3 * tile.Position.Item1 + 1, 3 * tile.Position.Item2 + 1, Color.White);
            canvas.SetPixel(3 * tile.Position.Item1 + 2, 3 * tile.Position.Item2 + 1, Color.White);
            canvas.SetPixel(3 * tile.Position.Item1 + 1, 3 * tile.Position.Item2 + 2, Color.White);
            canvas.SetPixel(3 * tile.Position.Item1 + 2, 3 * tile.Position.Item2 + 2, Color.White);

        }
        //Añade una pieza en el tablero
        public static void AddTile(Tile tile)
        {

            canvas.SetPixel(3 * tile.Position.Item1 + 1, 3 * tile.Position.Item2 + 1, tile.Appearance);
            canvas.SetPixel(3 * tile.Position.Item1 + 2, 3 * tile.Position.Item2 + 1, tile.Appearance);
            canvas.SetPixel(3 * tile.Position.Item1 + 1, 3 * tile.Position.Item2 + 2, tile.Appearance);
            canvas.SetPixel(3 * tile.Position.Item1 + 2, 3 * tile.Position.Item2 + 2, tile.Appearance);
            ;
        }
        //Esto no va aquí esto va en un generico de la pantalla
        public static void RefreshMaze()
        {
            /* for (int x = 0; x < Maze.mainWidth; x++)
             {
                 for (int y = 0; y < Maze.mainHeight; y++)
                 {
                     if (Maze.mainMaze[x, y] is Trap)
                     {
                         AddCell(x, y, Maze.mainMaze, canvas);
                     }
                 }
             }*/
            PrintMaze();
            for (int i = 0; i < GameMaster.players.Count; i++)
            {
                AddTile(GameMaster.players[i]);
            }
            AddTile(GameMaster.mainFlag);



            GameDisplay.layout["MazeContainer"].Update(
                new Panel(Align.Center(canvas))
            );
            GameDisplay.PlayerStatus();
            GameDisplay.GameMenu();


            Console.WriteLine();
            AnsiConsole.Clear();

            AnsiConsole.Write(GameDisplay.layout);

            ///AnsiConsole.Clear();
            //AnsiConsole.Write(canvas);
        }
        public static void ShowTrap()
        {
            for (int x = 0; x < Maze.mainWidth; x++)
            {
                for (int y = 0; y < Maze.mainHeight; y++)
                {
                    if (Maze.mainMaze[x, y] is Trap)
                    {
                        AddTrap(x, y, Maze.mainMaze, canvas);
                    }
                }
            }

            GameDisplay.layout["MazeContainer"].Update(
                new Panel(Align.Center(canvas))
            );

            AnsiConsole.Write(GameDisplay.layout);

            ///AnsiConsole.Clear();
            //AnsiConsole.Write(canvas);
        }

    }
}
