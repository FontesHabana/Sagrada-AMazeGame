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
        public static List<(bool, string)> gameOption = new List<(bool, string)> { (true, "Attack"), (false, "Show Traps"), (false, "Special Effect"), (false, "Next Turn") };





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
            while (true)
            {


                ConsoleKeyInfo key = Console.ReadKey();

                for (int x = 0; x < Maze.mainWidth; x++)
                {
                    for (int y = 0; y < Maze.mainHeight; y++)
                    {
                        if (Maze.mainMaze[x, y] is Trap)
                        {
                            MazeCanvas.AddCell(x, y, Maze.mainMaze, MazeCanvas.canvas);
                        }
                    }
                }


                if (playerspeed > 0)
                {
                    if (Player.Movement(key))
                    {
                        playerspeed--;
                        Player.HaveFlag();
                        Maze.mainMaze[Player.Position.Item1, Player.Position.Item2].ApplyEffect();


                    }
                }


                if (Menu(gameOption, key))
                {

                }
                if (key.Key == ConsoleKey.Enter)
                {
                    int option = 0;
                    foreach (var item in gameOption)
                    {
                        if (item.Item1)
                        {
                            option = gameOption.IndexOf(item);
                        }
                    }
                    switch (option)
                    {
                        case 0:
                            if (Player.AttackTo())
                            {
                                for (int i = 0; i < players.Count; i++)
                                {
                                    if (players[i].Life <= 0)
                                    {
                                        players[i].Respawn(players[i]);
                                    }
                                }
                            }

                            break;
                        case 1:
                            Player.ShowTrap();
                            break;
                        case 2:

                            Console.WriteLine("estoy viendo mi poder");
                            break;
                        default:
                            break;
                    }
                    if (gameOption[3].Item1)
                    {

                        break;
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

        public static bool Menu(List<(bool, string)> menu, ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.UpArrow)
            {
                for (var i = 0; i < menu.Count; i++)
                {
                    if (menu[i].Item1)
                    {
                        menu[i] = (false, menu[i].Item2);
                        if (i == 0)
                        {
                            menu[menu.Count - 1] = (true, menu[menu.Count - 1].Item2);
                            return true;
                        }
                        else
                        {
                            menu[i - 1] = (true, menu[i - 1].Item2);
                            return true;
                        }
                    }
                }



            }

            if (key.Key == ConsoleKey.DownArrow)
            {
                for (int i = 0; i < menu.Count; i++)
                {
                    if (menu[i].Item1)
                    {
                        menu[i] = (false, menu[i].Item2);
                        if (i == menu.Count - 1)
                        {
                            menu[0] = (true, menu[0].Item2);
                            return true;
                        }
                        else
                        {
                            menu[i + 1] = (true, menu[i + 1].Item2);
                            return true;
                        }
                    }

                }
            }


            return false;
        }

    }
}