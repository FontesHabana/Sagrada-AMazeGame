# pragma warning disable
namespace MazeBuilder
{
    public enum WallDir
    {
        N,
        E,
        S,
        W,
    };
    public class Maze
    {  //Tamaño del laberinto principal
        public static int mainWidth = 13;
        public static int mainHeight = 13;
        public static Cell[,] mainMaze;
        //Laberinto principal

        #region mainMaze
        public static void MainMaze()
        { //Los laberintos están enumerados en el sentido de las agujas del reloj comenzando en 0,0 y el central es el 0 ;
            mainMaze = new Cell[mainWidth, mainHeight];
            int xcenter = (mainWidth - 1) / 2;
            int ycenter = (mainHeight - 1) / 2;
            //Genera los laberintos auxiliares
            //0
            Cell[,] aux0 = GenerateMaze(5, 5);
            //1
            Cell[,] aux1 = GenerateMaze(xcenter - 2, ycenter + 3);
            //2
            Cell[,] aux2 = GenerateMaze(xcenter + 3, ycenter + -2);
            //3
            Cell[,] aux3 = GenerateMaze(xcenter - 2, ycenter + 3);
            //4
            Cell[,] aux4 = GenerateMaze(xcenter + 3, ycenter - 2);
            //Coloca los laberintos auxiliares en el laberinto principal
            //1
            for (int x = 0; x < xcenter - 2; x++)
            {
                for (int y = 0; y < ycenter + 3; y++)
                {
                    mainMaze[x, y] = aux1[x, y];
                }
            }
            //2
            for (int x = xcenter - 2; x < mainWidth; x++)
            {
                for (int y = 0; y < ycenter - 2; y++)
                {
                    mainMaze[x, y] = aux2[x - (xcenter - 2), y];
                }
            }
            //3
            for (int x = xcenter + 3; x < mainWidth; x++)
            {
                for (int y = ycenter - 2; y < mainHeight; y++)
                {
                    mainMaze[x, y] = aux3[x - (xcenter + 3), y - (ycenter - 2)];
                }
            }
            //4
            for (int x = 0; x < xcenter + 3; x++)
            {
                for (int y = ycenter + 3; y < mainHeight; y++)
                {
                    mainMaze[x, y] = aux4[x, y - (ycenter + 3)];
                }
            }
            //0 ok
            for (int x = xcenter - 2; x < xcenter + 3; x++)
            {
                for (int y = ycenter - 2; y < ycenter + 3; y++)
                {
                    mainMaze[x, y] = aux0[x - (xcenter - 2), y - (ycenter - 2)];
                }
            }

            //Fija celdas estaticas
            StaticCell();
            //Generar trampas en el laberinto con una probabilidad de 1/5
            Random rnd = new Random();
            for (int x = 1; x < mainWidth - 2; x++)
            {
                for (int y = 1; y < mainHeight - 2; y++)
                {
                    if (mainMaze[x - 1, y] is Trap)
                    {
                        y += 1;
                    }
                    if (x < 5 || x > 7)
                    {
                        if (rnd.Next(0, 3) == 0)
                        {
                            Trap trap = new Trap(x, y);
                            trap.Wall = mainMaze[x, y].Wall;
                            mainMaze[x, y] = trap;
                            y += 1;
                        }
                    }
                    else if (y < 5 || y > 7)
                    {
                        if (rnd.Next(0, 3) == 0)
                        {
                            Trap trap = new Trap(x, y);
                            trap.Wall = mainMaze[x, y].Wall;
                            mainMaze[x, y] = trap;
                            y += 1;
                        }
                    }

                }
            }




        }

        //Declarar celdas específicas. Esta disposición es personal con el fin de conectar los distintos espacios de una forma más equilibrada.
        //Es totalmente una decisión personal
        public static void StaticCell()
        {
            int xcenter = (mainWidth - 1) / 2;
            int ycenter = (mainHeight - 1) / 2;
            //Celdas fijas para cualquier laberinto cuadrado de lado 2k+1 con k>2
            //Center Cell
            mainMaze[xcenter, ycenter].Wall[(int)WallDir.N] = false;
            mainMaze[xcenter, ycenter - 1].Wall[(int)WallDir.S] = false;
            mainMaze[xcenter, ycenter].Wall[(int)WallDir.E] = false;
            mainMaze[xcenter + 1, ycenter].Wall[(int)WallDir.W] = false;
            mainMaze[xcenter, ycenter].Wall[(int)WallDir.S] = false;
            mainMaze[xcenter, ycenter + 1].Wall[(int)WallDir.N] = false;
            mainMaze[xcenter, ycenter].Wall[(int)WallDir.W] = false;
            mainMaze[xcenter - 1, ycenter].Wall[(int)WallDir.E] = false;

            //0-1-2
            mainMaze[xcenter - 2, ycenter - 2].Wall[(int)WallDir.N] = false;
            mainMaze[xcenter - 2, ycenter - 2].Wall[(int)WallDir.W] = false;

            mainMaze[xcenter - 2, ycenter - 3].Wall[(int)WallDir.S] = false;
            mainMaze[xcenter - 3, ycenter - 2].Wall[(int)WallDir.E] = false;
            //0-2-3
            mainMaze[xcenter + 2, ycenter - 2].Wall[(int)WallDir.N] = false;
            mainMaze[xcenter + 2, ycenter - 2].Wall[(int)WallDir.E] = false;

            mainMaze[xcenter + 2, ycenter - 3].Wall[(int)WallDir.S] = false;
            mainMaze[xcenter + 3, ycenter - 2].Wall[(int)WallDir.W] = false;
            //0-3-4
            mainMaze[xcenter + 2, ycenter + 2].Wall[(int)WallDir.S] = false;
            mainMaze[xcenter + 2, ycenter + 2].Wall[(int)WallDir.E] = false;

            mainMaze[xcenter + 2, ycenter + 3].Wall[(int)WallDir.N] = false;
            mainMaze[xcenter + 3, ycenter + 2].Wall[(int)WallDir.W] = false;
            //0-4-1
            mainMaze[xcenter - 2, ycenter + 2].Wall[(int)WallDir.S] = false;
            mainMaze[xcenter - 2, ycenter + 2].Wall[(int)WallDir.W] = false;

            mainMaze[xcenter - 2, ycenter + 3].Wall[(int)WallDir.N] = false;
            mainMaze[xcenter - 3, ycenter + 2].Wall[(int)WallDir.E] = false;

            #region 13x13
            //A partir de aquí es solo para 13x13       
            //1-4
            mainMaze[0, 8].Wall[(int)WallDir.S] = false;
            mainMaze[0, 9].Wall[(int)WallDir.N] = false;

            mainMaze[2, 8].Wall[(int)WallDir.S] = false;
            mainMaze[2, 9].Wall[(int)WallDir.N] = false;

            //2-3
            mainMaze[10, 3].Wall[(int)WallDir.S] = false;
            mainMaze[10, 4].Wall[(int)WallDir.N] = false;

            mainMaze[12, 3].Wall[(int)WallDir.S] = false;
            mainMaze[12, 4].Wall[(int)WallDir.N] = false;
            //1-2
            mainMaze[3, 0].Wall[(int)WallDir.E] = false;
            mainMaze[4, 0].Wall[(int)WallDir.W] = false;

            mainMaze[3, 2].Wall[(int)WallDir.E] = false;
            mainMaze[4, 2].Wall[(int)WallDir.W] = false;
            //4-3
            mainMaze[8, 10].Wall[(int)WallDir.E] = false;
            mainMaze[9, 10].Wall[(int)WallDir.W] = false;

            mainMaze[8, 12].Wall[(int)WallDir.E] = false;
            mainMaze[9, 12].Wall[(int)WallDir.W] = false;

            #endregion
        }





        #endregion
        //Maze algorithm
        #region mazealgorithm
        public static Cell[,] GenerateMaze(int MX, int MY)

        {   //Variables de tamaño del laberinto
            int heigth = MY;
            int width = MX;
            //Stack con las celdas visitadas
            Stack<Cell> Mystack = new Stack<Cell>();
            //Values of Initial Array
            Cell[,] MyMaze = new Cell[width, heigth];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < heigth; y++)
                {
                    MyMaze[x, y] = new Cell(x, y);
                }
            }

            //Declarate a initial cell
            Cell currentCell = MyMaze[0, 0];
            Mystack.Push(currentCell);


            //Do Maze Algorithm
            while (Mystack.Count > 0)
            {
                //Actualiza el valor de la celda actual
                currentCell = Mystack.Peek();
                //Step 1: Create a set of the unvisted neighbours
                List<int> neighbors = GetUnvisitedNeighbors(currentCell, MyMaze, width, heigth);
                //Step2: Peek one and removes the walls
                RemoveWall(neighbors, currentCell, MyMaze, Mystack);
            }
            return MyMaze;

        }





        private static List<int> GetUnvisitedNeighbors(Cell currentCell, Cell[,] maze, int width, int height)
        {

            List<int> neighbors = new List<int>();
            //North Neighbour
            if (currentCell.Coordenada.Item2 != 0)
            {
                if (!maze[currentCell.Coordenada.Item1, currentCell.Coordenada.Item2 - 1].Visited)
                {
                    neighbors.Add((int)WallDir.N);
                }
            }
            //East Neighbour
            if (currentCell.Coordenada.Item1 != width - 1)
            {
                if (!maze[currentCell.Coordenada.Item1 + 1, currentCell.Coordenada.Item2].Visited)
                {
                    neighbors.Add((int)WallDir.E);
                }
            }
            //South Neighbour
            if (currentCell.Coordenada.Item2 != height - 1)
            {
                if (!maze[currentCell.Coordenada.Item1, currentCell.Coordenada.Item2 + 1].Visited)
                {
                    neighbors.Add((int)WallDir.S);
                }
            }
            //West Neighbour
            if (currentCell.Coordenada.Item1 != 0)
            {
                if (!maze[currentCell.Coordenada.Item1 - 1, currentCell.Coordenada.Item2].Visited)
                {
                    neighbors.Add((int)WallDir.W);
                }
            }
            return neighbors;
        }


        private static void RemoveWall(List<int> neighbors, Cell currentCell, Cell[,] maze, Stack<Cell> Mystack)
        {
            if (neighbors.Count != 0)
            {
                Random rnd = new Random();
                int next = neighbors[rnd.Next(neighbors.Count)];

                // Eliminar la pared entre dos celdas vecinas
                switch (next)
                {   //North
                    case 0:
                        currentCell.Wall[(int)WallDir.N] = false;
                        maze[currentCell.Coordenada.Item1, currentCell.Coordenada.Item2 - 1].Wall[(int)WallDir.S] = false;
                        maze[currentCell.Coordenada.Item1, currentCell.Coordenada.Item2 - 1].Visited = true;
                        Mystack.Push(maze[currentCell.Coordenada.Item1, currentCell.Coordenada.Item2 - 1]);
                        break;
                    //East
                    case 1:
                        currentCell.Wall[(int)WallDir.E] = false;
                        maze[currentCell.Coordenada.Item1 + 1, currentCell.Coordenada.Item2].Wall[(int)WallDir.W] = false;
                        maze[currentCell.Coordenada.Item1 + 1, currentCell.Coordenada.Item2].Visited = true;
                        Mystack.Push(maze[currentCell.Coordenada.Item1 + 1, currentCell.Coordenada.Item2]);
                        break;
                    //South
                    case 2:
                        currentCell.Wall[(int)WallDir.S] = false;
                        maze[currentCell.Coordenada.Item1, currentCell.Coordenada.Item2 + 1].Wall[(int)WallDir.N] = false;
                        maze[currentCell.Coordenada.Item1, currentCell.Coordenada.Item2 + 1].Visited = true;
                        Mystack.Push(maze[currentCell.Coordenada.Item1, currentCell.Coordenada.Item2 + 1]);
                        break;
                    //West
                    case 3:
                        currentCell.Wall[(int)WallDir.W] = false;
                        maze[currentCell.Coordenada.Item1 - 1, currentCell.Coordenada.Item2].Wall[(int)WallDir.E] = false;
                        maze[currentCell.Coordenada.Item1 - 1, currentCell.Coordenada.Item2].Visited = true;
                        Mystack.Push(maze[currentCell.Coordenada.Item1 - 1, currentCell.Coordenada.Item2]);
                        break;

                    default:
                        break;
                }

            }
            else
            {
                Mystack.Pop();
            }
        }
        #endregion


    }
}