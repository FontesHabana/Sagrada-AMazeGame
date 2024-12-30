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
        public static Character Player { get; set; }
        public static int playerspeed = 0;
        public static Flag mainFlag = new Flag((Maze.mainHeight / 2, Maze.mainWidth / 2), Color.DarkMagenta);
        //Listas para ejecutar los menus
        public static Menu GameMenu = new Menu(Menu.GameMenu(), Menu.action);
        public static Menu SwitchMenu = new Menu(Menu.PSwitch(), Menu.change);
        public static Menu CharacterSelection = new Menu(Menu.CharacterList(), Menu.charactersaction);
        public static Menu NumberOfPlayers = new Menu(Menu.NumberPlayer(), Menu.numberofplayeraction);
        public static Menu CopyPowerMenu = new Menu(Menu.PSwitch(), Menu.powercopy);
        //Mis jugadores
        //Creo que la referencia no se usa
        public static List<Character> CharacterOption = [ new Character(position[0], Color.Blue,new CanvasImage("Assets/pxjs3trcyyv71-01-removebg-preview.png"), "", 10, 3, PowerEnum.JumpWall, 8, 1, 2),
                                               new Character(position[0], Color.Red,new CanvasImage("Assets/pxjs3trcyyv71-06-removebg-preview.png"), "", 8, 3, PowerEnum.IncreaseSpeed, 6, 1, 3),
                                               new Character(position[0], Color.Yellow,new CanvasImage("Assets/pxjs3trcyyv71-03-removebg-preview.png"), "", 12, 3, PowerEnum.IncreaseLife, 5, 3, 5),
                                               new Character(position[0], Color.Green,new CanvasImage("Assets/pxjs3trcyyv71-05-removebg-preview.png"), "", 9, 4, PowerEnum.SwitchPlayer, 7, 2, 3),
                                               new Character(position[0], Color.Pink1,new CanvasImage("Assets/pxjs3trcyyv71-08-removebg-preview.png"), "", 10, 3, PowerEnum.DestroyTrap, 9, 2, 2),
                                               new Character(position[0], Color.Orange1,new CanvasImage("Assets/pxjs3trcyyv71-07-removebg-preview(1).png"), "", 10, 3, PowerEnum.NewTurn, 6, 1, 3),
                                               new Character(position[0], Color.Turquoise4,new CanvasImage("Assets/pxjs3trcyyv71-04-removebg-preview.png"), "", 6, 3, PowerEnum.CopyPower, 6, 1, 4)];



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

        {
            GameMenu = new Menu(Menu.GameMenu(), Menu.action);

            CharacterSelection = new Menu(Menu.CharacterList(), Menu.charactersaction);

            CharacterSelection.MenuOption = Menu.CharacterList();
            GameDisplay.InitLayout();
            GameDisplay.GenerateCharacter(30);
            // playeramount = 4;
            players.Clear();
            //Cuantos jugadores menu

            while (true)
            {
                Console.Clear();
                AnsiConsole.Write(GameDisplay.HorizontalMenu(NumberOfPlayers, MyText.text[MyText.language]["gameMaster"]["numberPlayers"]));

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
                AnsiConsole.Write(new Markup(MyText.text[MyText.language]["gameMaster"]["name"]).Centered());
                //Hacer que el nombre sea válido;
                string? name = Console.ReadLine();
                while (ValidateName(name))
                {
                    Console.Clear();
                    AnsiConsole.Write(new Markup(MyText.text[MyText.language]["gameMaster"]["name"]).Centered());
                    name = Console.ReadLine();
                }

                //Selecciona tu personaje

                while (true)
                {
                    GameDisplay.PrintSelectionMenu(CharacterSelection, MyText.text[MyText.language]["gameMaster"]["character"]);

                    ConsoleKeyInfo key = Console.ReadKey();
                    CharacterSelection.ChangeOption(key);

                    if (CharacterSelection.actionMenu(key))
                    {
                        break;
                    }

                }
                players[i].Name = name;
                players[i].Position = position[i];
                players[i].haveFlag = false;


            }




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
            mainFlag.Position = (Maze.mainHeight / 2, Maze.mainWidth / 2);
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
                        if (Player.HaveFlag())
                        {
                            GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["gameMaster"]["flag"]).NoBorder());
                        }
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
                    GameDisplay.Victory(VictoryCondition());
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