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
        public static (int, int)[] position = [(0, 0), (Maze.mainWidth - 1, Maze.mainHeight - 1), (Maze.mainWidth - 1, 0), (0, Maze.mainHeight - 1)];
        //private static List<Color> appearance = [Color.Blue, Color.Red, Color.Green, Color.Yellow, Color.Orange1, Color.Black, Color.DeepPink1];
        private static int[] speed = [5, 5, 5, 5];
        public static Character Player { get; set; }
        public static int playerspeed = 0;
        public static Flag mainFlag = new Flag((6, 6), Color.DarkMagenta);
        //Listas para ejecutar los menus
        public static Menu GameMenu = new Menu(Menu.gamemenu, Menu.action);
        public static Menu SwitchMenu = new Menu(Menu.pswitch, Menu.change);
        public static Menu CharacterSelection = new Menu(Menu.characters, Menu.charactersaction);
        public static Menu NumberOfPlayers = new Menu(Menu.numberofplayer, Menu.numberofplayeraction);
        //Mis jugadores
        //Creo que la referencia no se usa
        public static List<Character> CharacterOption = [ new Character(CharacterReference.VisionOfLight,position[0], Color.Blue,new CanvasImage("Assets/pxjs3trcyyv71-01-removebg-preview.png"), "", 10, 3, PowerEnum.JumpWall, 8, 1, 2),
                                               new Character(CharacterReference.CreativeWind,position[0], Color.Red,new CanvasImage("Assets/pxjs3trcyyv71-06-removebg-preview.png"), "", 8, 5, PowerEnum.IncreaseSpeed, 6, 2, 3),
                                               new Character(CharacterReference.VitalSoul,position[0], Color.Yellow,new CanvasImage("Assets/pxjs3trcyyv71-03-removebg-preview.png"), "", 12, 2, PowerEnum.IncreaseLife, 10, 3, 5),
                                               new Character(CharacterReference.IdeaMimetist,position[0], Color.Green,new CanvasImage("Assets/pxjs3trcyyv71-05-removebg-preview.png"), "", 9, 4, PowerEnum.SwitchPlayer, 7, 2, 3),
                                               new Character(CharacterReference.NaturalBreaker,position[0], Color.Pink1,new CanvasImage("Assets/pxjs3trcyyv71-08-removebg-preview.png"), "", 10, 3, PowerEnum.DestroyTrap, 9, 2, 2),
                                               new Character(CharacterReference.MirrorOfTime,position[0], Color.Orange1,new CanvasImage("Assets/pxjs3trcyyv71-07-removebg-preview(1).png"), "", 10, 3, PowerEnum.NewTurn, 6, 1, 3)];



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
            //List<Color> gamecolor = appearance;
            List<Character> characters = CharacterOption;
            Menu charactersmenu = CharacterSelection;
            GameDisplay.InitLayout();
            GameDisplay.GenerateCharacter(30);
            // playeramount = 4;
            players.Clear();
            //Cuantos jugadores menu

            while (true)
            {
                Console.Clear();
                AnsiConsole.Write(GameDisplay.HorizontalMenu(NumberOfPlayers, "Number of Players"));

                ConsoleKeyInfo key = Console.ReadKey();
                NumberOfPlayers.ChangeOption(key);

                if (NumberOfPlayers.actionMenu(key))
                {
                    break;
                }

            }
            for (int i = 0; i < playeramount; i++)
            {
                //Escribe tu nombre
                Console.Clear();
                AnsiConsole.Write(new Markup("Insert your name").Centered());
                //Hacer que el nombre sea válido;
                string? name = Console.ReadLine();
                while (ValidateName(name))
                {
                    Console.Clear();
                    AnsiConsole.Write(new Markup("Insert your name").Centered());
                    name = Console.ReadLine();
                }



                //Selecciona tu personaje

                while (true)
                {
                    GameDisplay.PrintSelectionMenu(CharacterSelection, "Select your character");

                    ConsoleKeyInfo key = Console.ReadKey();
                    CharacterSelection.ChangeOption(key);

                    if (CharacterSelection.actionMenu(key))
                    {
                        break;
                    }

                }
                players[i].Name = name;
                players[i].Position = position[i];

            }
            //Falta la asignación de colores
            CharacterOption = characters;
            CharacterSelection = charactersmenu;
            GameDisplay.GenerateCharacter(16);
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


            while (true)
            {
                Normalize();
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
        static bool ValidateName(string s)
        {


            if (string.IsNullOrEmpty(s))
            {
                return true;
            }
            return false;
        }

    }
}