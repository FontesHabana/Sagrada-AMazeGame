using MazeBuilder;
using Tiles;
using Spectre.Console;
using UserInterface;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;

namespace LogicGame
{
    class GameMaster
    {
        public static List<Character> players = new List<Character>();
        public static int turn;
        public static int playeramount;
        public static (int, int)[] position = [(0, 0), (Maze.mainWidth - 1, 0), (Maze.mainWidth - 1, Maze.mainHeight - 1), (0, Maze.mainHeight - 1)];
        private static Color[] appearance = [Color.Blue, Color.Red, Color.Green, Color.Yellow];
        private static int[] speed = [5, 5, 5, 5];
        public static Character Player { get; set; }
        public static int playerspeed = 0;
        public static Flag mainFlag = new Flag((6, 6), Color.DarkMagenta);


        //Variable speed modificar más adelante

        public static bool InitGame()
        {   //Declarar jugadores. Más adelante esto será elegible
            playeramount = 4;
            for (int i = 0; i < playeramount; i++)
            {
                System.Console.WriteLine("Inserte su nombre");
                Character Player = new Character(position[i], appearance[i], Console.ReadLine(), 10, speed[i], 10, 3);
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

            MazeCanvas.AddTile(mainFlag);

            //MazeCanvas.RefreshMaze();

            return true;
        }

        public static void Turn()
        {
            Player = players[turn];
            playerspeed = Player.Speed;
            MazeCanvas.RefreshMaze();
            while (playerspeed > 0)
            {

                ConsoleKeyInfo key = Console.ReadKey();
                if (Player.Movement(key) == true && playerspeed > 0)
                {
                    playerspeed--;
                    Player.HaveFlag();
                    Maze.mainMaze[Player.Position.Item1, Player.Position.Item2].ApplyEffect();


                }
                if (Player.AttackTo(key) == true)
                {
                    for (int i = 0; i < players.Count; i++)
                    {
                        if (players[i].Life <= 0)
                        {
                            players[i].Respawn(players[i]);
                        }
                    }
                }
                MazeCanvas.RefreshMaze();
                if (VictoryCondition() > 0)
                {
                    Victory(VictoryCondition());
                    break;
                }


            }
            NextTurn();
        }
        private static void NextTurn()
        {
            turn++;
            turn %= playeramount;
        }

        public static int VictoryCondition()
        {

            if (mainFlag.Position == position[0])
            {
                return 1;
            }
            if (mainFlag.Position == position[1])
            {
                return 2;
            }
            if (mainFlag.Position == position[2])
            {
                return 3;
            }
            if (mainFlag.Position == position[3])
            {
                return 4;
            }
            return 0;
        }

        public static void Victory(int winner)
        {
            //players[turn].Speed = 0;
            switch (winner)
            {
                case 1:
                    AnsiConsole.Clear();
                    System.Console.WriteLine($"{players[0].Name} eres el ganador");
                    break;
                case 2:
                    AnsiConsole.Clear();
                    System.Console.WriteLine($"{players[1].Name} eres el ganador");
                    break;
                case 3:
                    AnsiConsole.Clear();
                    System.Console.WriteLine($"{players[2].Name} eres el ganador");
                    break;
                case 4:
                    AnsiConsole.Clear();
                    System.Console.WriteLine($"{players[3].Name} eres el ganador");
                    break;
                default:
                    break;
            }




        }


    }
}