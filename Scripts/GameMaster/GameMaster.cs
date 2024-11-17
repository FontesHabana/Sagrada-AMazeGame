using MazeBuilder;
using Tiles;
using Spectre.Console;
using UserInterface;
using System.Security.Cryptography.X509Certificates;

namespace LogicGame
{
    class GameMaster
    {
        public static List<Character> players = new List<Character>();
        public static int turn;
        public static int playeramount;
        private static (int, int)[] position = [(0, 0), (Maze.mainWidth - 1, Maze.mainHeight - 1), (0, Maze.mainHeight - 1), (Maze.mainWidth - 1, 0)];
        private static Color[] appearance = [Color.Blue, Color.Red, Color.Green, Color.Yellow];
        private static int[] speed = [5, 4, 5, 3];
        public static Character Player { get; set; }


        //Variable speed modificar más adelante

        public static bool InitGame()
        {   //Declarar jugadores. Más adelante esto será elegible
            playeramount = 2;
            for (int i = 0; i < playeramount; i++)
            {
                System.Console.WriteLine("Inserte su nombre");
                Character Player = new Character(position[i], appearance[i], Console.ReadLine(), 5, speed[i], 3);
                players.Add(Player);
            }
            Random rand = new Random();
            turn = rand.Next(0, playeramount);

            Maze.MainMaze();
            MazeCanvas.PrintMaze();
            for (int i = 0; i < playeramount; i++)
            {
                Maze.mainMaze[players[i].Position.Item1, players[i].Position.Item2].Occuped = true;
                MazeCanvas.AddTile(players[i]);
            }
            return true;
        }

        public static void Turn()
        {
            Player = players[turn];
            int speed = Player.Speed;
            System.Console.WriteLine($"{Player.Name} es tu turno");
            while (speed > 0)
            {

                ConsoleKeyInfo key = Console.ReadKey();
                if (Player.Movement(key) == true)
                {
                    speed--;
                    if (Maze.mainMaze[Player.Position.Item1, Player.Position.Item2] is Trap)
                    {
                        Maze.mainMaze[Player.Position.Item1, Player.Position.Item2].ApplyEffect();
                    }
                }

            }
        }
        public static void NextTurn()
        {
            turn++;
            turn %= playeramount;
        }




    }
}