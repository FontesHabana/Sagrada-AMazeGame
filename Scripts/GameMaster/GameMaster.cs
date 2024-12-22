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
        //Listas para ejecutar los menus
        public static Menu GameMenu = new Menu(Menu.gamemenu, Menu.action);
        public static Menu SwitchMenu = new Menu(Menu.pswitch, Menu.change);





        //Variable speed modificar más adelante
        public static void Game()
        {
            GameDisplay.GameScreen();
            InitGame();
            //MazeCanvas.RefreshMaze();
            while (VictoryCondition() == 0)
            {
                Turn();
            }
        }
        public static bool InitGame()
        {   //Declarar jugadores. Más adelante esto será elegible
            playeramount = 4;
            players.Clear();

            //------------------
            //Estos son personajes temporales 
            System.Console.WriteLine("Inserte su nombre");
            players.Add(new Character(position[0], appearance[0], Console.ReadLine(), 10, speed[0], PowerEnum.NewTurn, 10, 3, 3));
            System.Console.WriteLine("Inserte su nombre");
            players.Add(new Character(position[1], appearance[1], Console.ReadLine(), 10, speed[1], PowerEnum.DestroyTrap, 10, 3, 3));
            System.Console.WriteLine("Inserte su nombre");
            players.Add(new Character(position[2], appearance[2], Console.ReadLine(), 10, speed[2], PowerEnum.JumpWall, 10, 3, 3));
            System.Console.WriteLine("Inserte su nombre");
            players.Add(new Character(position[3], appearance[3], Console.ReadLine(), 10, speed[3], PowerEnum.SwitchPlayer, 10, 3, 3));




            //
            /* for (int i = 0; i < playeramount; i++)
             {
                 System.Console.WriteLine("Inserte su nombre");
                 Character Player = new Character(position[i], appearance[i], Console.ReadLine(), 10, speed[i], PowerEnum.NewTurn, 10, 3, 3);
                 players.Add(Player);
             }*/
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
            Player.Power += Player.PowerIncrease;
            Normalize();

            while (true)
            {

                //Print the display game
                GameDisplay.RefreshMaze();

                //Read a key for a action
                ConsoleKeyInfo key = Console.ReadKey();

                //Move action
                if (playerspeed > 0)
                {
                    if (Player.Movement(key))
                    {
                        playerspeed--;
                        Player.HaveFlag();
                        Maze.mainMaze[Player.Position.Item1, Player.Position.Item2].ApplyEffect();
                    }
                }
                //Change menu option

                GameMenu.ChangeOption(key);
                if (GameMenu.actionMenu(key))
                {
                    if (GameMenu.GetList()[3].Item1)
                    {
                        break;
                    }
                }
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

            if (Player.Power > 10)
            {
                Player.Power = 10;
            }
            turn++;
            turn %= playeramount;
        }

        public static int VictoryCondition()
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (mainFlag.Position == position[i] && players[i].HaveFlag())
                {
                    return i + 1;
                }
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

        /*  public static bool Menu(List<(bool, string)> menu, ConsoleKeyInfo key)
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
          }*/

        public static void Normalize()
        {
            if (Player.Power > Player.MaxPower)
            {
                Player.Power = Player.MaxPower;
            }

            if (Player.Life > Player.MaxLife)
            {
                Player.Life = Player.MaxLife;
            }
        }
    }
}