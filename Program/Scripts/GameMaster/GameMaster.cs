using MazeBuilder;
using Tiles;
using Spectre.Console;
using UserInterface;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;

namespace LogicGame
{   // This class manages the overall game logic and flow
    class GameMaster
    {


        public static List<Character> players = new List<Character>();
        public static int turn;
        public static int playeramount;
        // Array of starting positions for each player
        public static (int, int)[] position = [(0, 0), (Maze.mainWidth - 1, Maze.mainHeight - 1), (Maze.mainWidth - 1, 0), (0, Maze.mainHeight - 1)];

        // Current active player
        public static Character Player { get; set; }
        // Speed of the current player
        public static int playerspeed = 0;
        public static Flag mainFlag = new Flag((Maze.mainHeight / 2, Maze.mainWidth / 2), Color.DarkMagenta);
        // Menus used in the game
        public static Menu GameMenu = new Menu(Menu.GameMenu(), Menu.action);
        public static Menu SwitchMenu = new Menu(Menu.PSwitch(), Menu.change);
        public static Menu CharacterSelection = new Menu(Menu.CharacterList(), Menu.charactersaction);
        public static Menu NumberOfPlayers = new Menu(Menu.NumberPlayer(), Menu.numberofplayeraction);
        public static Menu CopyPowerMenu = new Menu(Menu.PSwitch(), Menu.powercopy);

        // Available character options

        public static List<Character> CharacterOption = [ new Character(position[0], Color.Blue,new CanvasImage("Assets/pxjs3trcyyv71-01-removebg-preview.png"), "", 10, 3, PowerEnum.JumpWall, 8, 1, 2),
                                               new Character(position[0], Color.Red,new CanvasImage("Assets/pxjs3trcyyv71-06-removebg-preview.png"), "", 8, 3, PowerEnum.IncreaseSpeed, 6, 1, 3),
                                               new Character(position[0], Color.Yellow,new CanvasImage("Assets/pxjs3trcyyv71-03-removebg-preview.png"), "", 8, 3, PowerEnum.IncreaseLife, 5, 3, 4),
                                               new Character(position[0], Color.Green,new CanvasImage("Assets/pxjs3trcyyv71-05-removebg-preview.png"), "", 9, 4, PowerEnum.SwitchPlayer, 7, 2, 3),
                                               new Character(position[0], Color.Pink1,new CanvasImage("Assets/pxjs3trcyyv71-08-removebg-preview.png"), "", 10, 3, PowerEnum.DestroyTrap, 9, 2, 2),
                                               new Character(position[0], Color.Orange1,new CanvasImage("Assets/pxjs3trcyyv71-07-removebg-preview(1).png"), "", 10, 3, PowerEnum.NewTurn, 6, 1, 3),
                                               new Character(position[0], Color.Turquoise4,new CanvasImage("Assets/pxjs3trcyyv71-04-removebg-preview.png"), "", 6, 3, PowerEnum.CopyPower, 6, 1, 4)];



        // Main game method
        public static void Game()
        {
            GameDisplay.GameScreen();
            InitGame();
            Audio.Game = true;
            while (VictoryCondition() == 0)
            {

                Turn();
            }

        }
        // Initialization method
        public static bool InitGame()

        {
            // Reset the GameMenu
            GameMenu = new Menu(Menu.GameMenu(), Menu.action);
            // Reset the CharacterSelection menu
            CharacterSelection = new Menu(Menu.CharacterList(), Menu.charactersaction);
            // Set the CharacterSelection menu options
            CharacterSelection.MenuOption = Menu.CharacterList();
            GameDisplay.InitLayout();
            // Generate character images for the selection process
            GameDisplay.GenerateCharacter(30);
            // Clear the list of players
            players.Clear();

            // Loop to select the number of players
            while (true)
            {
                Console.Clear();
                // Display the number of players menu
                AnsiConsole.Write(GameDisplay.HorizontalMenu(NumberOfPlayers, MyText.text[MyText.language]["gameMaster"]["numberPlayers"]));

                ConsoleKeyInfo key = Console.ReadKey();
                NumberOfPlayers.ChangeOption(key);
                // Check if the selected option should trigger an action
                if (NumberOfPlayers.actionMenu(key))
                {
                    break;// Exit the loop if an action was triggered
                }

            }
            // Loop to initialize each player
            for (int i = 0; i < playeramount; i++)
            {
                // Players name input
                Console.Clear();
                AnsiConsole.Write(new Markup(MyText.text[MyText.language]["gameMaster"]["name"]).Centered());
                //Hacer que el nombre sea válido;
                // Input validation for player name
                string? name = Console.ReadLine();
                while (ValidateName(name))
                {
                    Console.Clear();
                    if (name.Length > 10)
                    {
                        AnsiConsole.Write(new Markup(MyText.text[MyText.language]["gameMaster"]["nameError"]).Centered());
                    }
                    AnsiConsole.Write(new Markup(MyText.text[MyText.language]["gameMaster"]["name"]).Centered());

                    name = Console.ReadLine();
                }

                // Character selection

                while (true)
                {
                    GameDisplay.PrintSelectionMenu(CharacterSelection, MyText.text[MyText.language]["gameMaster"]["character"]);
                    // Read a key press from the console
                    ConsoleKeyInfo key = Console.ReadKey();

                    // Change the selected option in the CharacterSelection menu
                    CharacterSelection.ChangeOption(key);

                    // Check if the selected option should trigger an action
                    if (CharacterSelection.actionMenu(key))
                    {
                        break;// Exit the loop if an action was triggered
                    }

                }
                // Set up the player with the chosen name and character
                players[i].Name = name;
                players[i].Position = position[i];
                players[i].haveFlag = false;


            }




            GameDisplay.GenerateCharacter(16);
            // Randomly determine the first player's turn
            Random rand = new Random();
            turn = rand.Next(0, playeramount);
            // Generate the main maze
            Maze.MainMaze();
            // Print the initial maze
            MazeCanvas.PrintMaze();
            // Place players on the maze
            for (int i = 0; i < playeramount; i++)
            {
                Maze.mainMaze[players[i].Position.Item1, players[i].Position.Item2].Occuped = true;
                MazeCanvas.AddTile(players[i]);
            }
            // Place the main flag on the maze
            mainFlag.Position = (Maze.mainHeight / 2, Maze.mainWidth / 2);
            MazeCanvas.AddTile(mainFlag);


            return true;
        }
        // Turn method
        public static void Turn()
        {
            // Set the current player as the active player
            Player = players[turn];

            // Set the player's speed
            playerspeed = Player.Speed;

            // Increase the player's power
            Player.Power += Player.PowerIncrease;

            // Clear the bottom panel of the game display
            GameDisplay.layoutGame["bottom"].Update(new Panel("").NoBorder());

            // Main loop for the player's turn
            while (true)
            {
                Normalize();
                //Print the display game
                GameDisplay.RefreshMaze();

                //Read a key for a action
                ConsoleKeyInfo key = Console.ReadKey();

                // Handle movement action
                if (playerspeed > 0)
                {
                    if (Player.Movement(key))
                    {
                        playerspeed--;
                        Player.HaveFlag();

                        Maze.mainMaze[Player.Position.Item1, Player.Position.Item2].ApplyEffect();
                    }
                }
                // Handle movement action

                GameMenu.ChangeOption(key);
                if (GameMenu.actionMenu(key))
                {// If the 'Action' key is pressed and it's not the last option
                    if (GameMenu.GetList()[3].Item1)
                    {
                        break;// End the turn
                    }
                }
                // Check for victory condition
                if (VictoryCondition() > 0)
                {
                    // Set game audio to false and play victory music
                    Audio.Game = false;
                    Audio.currentFile = Audio.music["victory"];
                    // Display victory screen
                    GameDisplay.Victory(VictoryCondition());
                    break; // Exit the turn loop
                }

            }
            // Move to the next player's turn
            NextTurn();
        }

        private static void NextTurn()
        {

            turn++;
            turn %= playeramount;
        }
        // Check victory condition
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


        // Normalize player stats
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
        // Validate player name
        static bool ValidateName(string s)
        {


            if (string.IsNullOrEmpty(s) || s.Length > 10)
            {
                return true;
            }
            return false;
        }

    }
}