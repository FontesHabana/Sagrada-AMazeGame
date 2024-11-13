using Spectre.Console;
using MazeBuilder;
namespace UserInterface
{
    public class MazeDisplay
    {
        public static void PrintMaze()
        {
            Cell[,] mainMaze = Maze.mainMaze;
            var canvas = new Canvas(3 * Maze.mainWidth + 1, 3 * Maze.mainHeight + 1);


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
            for (int x = 0; x < Maze.mainWidth; x++)
            {
                for (int y = 0; y < Maze.mainHeight; y++)
                {
                    PrintCell(x, y, mainMaze, canvas);
                }
            }

            AnsiConsole.Write(canvas);
        }
        public static void PrintCell(int x, int y, Cell[,] maze, Canvas canvas)
        {
            //Print Cell
            if (Maze.mainMaze[x, y].Visited)
            {
                canvas.SetPixel(3 * x + 1, 3 * y + 1, Color.White);
                canvas.SetPixel(3 * x + 2, 3 * y + 1, Color.White);
                canvas.SetPixel(3 * x + 1, 3 * y + 2, Color.White);
                canvas.SetPixel(3 * x + 2, 3 * y + 2, Color.White);
            }
            //Eliminar este pedazo de codigo
            else
            {
                canvas.SetPixel(3 * x + 1, 3 * y + 1, Color.Red);
                canvas.SetPixel(3 * x + 2, 3 * y + 1, Color.Red);
                canvas.SetPixel(3 * x + 1, 3 * y + 2, Color.Red);
                canvas.SetPixel(3 * x + 2, 3 * y + 2, Color.Red);
            }//

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


    }
}
