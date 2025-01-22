using UserInterface;
using Tiles;
using Spectre.Console;
using System.Security;
namespace LogicGame
{

    class Menu
    {  // Properties
        public List<(bool, string)> MenuOption { get; set; }
        // Delegate for menu actions
        public delegate bool ActionMenu(ConsoleKeyInfo key);
        // Variable to hold the current action
        public ActionMenu actionMenu { get; set; }



        //List 
        //Game menu
        public static ActionMenu action = MenuAction;
        //Main Menu
        public static ActionMenu change = SwitchMenu;
        //Start Menu
        public static ActionMenu initaction = InitAction;
        //Number of players
        public static ActionMenu numberofplayeraction = NumberOfPlayerAction;

        //Select player list
        public static ActionMenu charactersaction = CharacterSelection;
        //CopyPower
        public static ActionMenu powercopy = CopyPowerAction;

        //Constructor
        public Menu(List<(bool, string)> menu, ActionMenu action)
        {
            MenuOption = menu;
            actionMenu = action;
        }



        // Static methods for creating different types of menus

        public static List<(bool, string)> InitMenu()
        {

            return new List<(bool, string)> { (true, MyText.text[MyText.language]["menu"]["newGame"]), (false, MyText.text[MyText.language]["menu"]["instruction"]), (false, MyText.text[MyText.language]["menu"]["language"]), (false, MyText.text[MyText.language]["menu"]["history"]), (false, MyText.text[MyText.language]["menu"]["exit"]) };
        }

        public static List<(bool, string)> NumberPlayer()
        {

            return new List<(bool, string)> { (true, "2"), (false, "3"), (false, "4") };
        }
        public static List<(bool, string)> CharacterList()
        {

            return new List<(bool, string)> { (true, MyText.text[MyText.language]["menu"]["visionLight"]),
                                              (false, MyText.text[MyText.language]["menu"]["creativeWind"]),
                                              (false, MyText.text[MyText.language]["menu"]["vitalSoul"]),
                                              (false, MyText.text[MyText.language]["menu"]["ideaMimetist"]),
                                              (false, MyText.text[MyText.language]["menu"]["naturalBreaker"]),
                                             (false, MyText.text[MyText.language]["menu"]["mirrorTime"]),
                                             (false, MyText.text[MyText.language]["menu"]["chameleonMind"]) };
        }
        public static List<(bool, string)> GameMenu()
        {

            return new List<(bool, string)> { (false, MyText.text[MyText.language]["menu"]["attack"]),
                                              (false, MyText.text[MyText.language]["menu"]["showTrap"]),
                                              (false, MyText.text[MyText.language]["menu"]["specialPower"]),
                                              (true, MyText.text[MyText.language]["menu"]["next"]) };
        }
        public static List<(bool, string)> PSwitch()
        {

            return new List<(bool, string)>();
        }


        // Method for changing selected menu option
        public bool ChangeOption(ConsoleKeyInfo key)
        {
            // Handles arrow key presses to navigate menu options
            if (key.Key == ConsoleKey.UpArrow)
            {
                for (var i = 0; i < MenuOption.Count; i++)
                {
                    if (MenuOption[i].Item1)
                    {
                        MenuOption[i] = (false, MenuOption[i].Item2);
                        if (i == 0)
                        {
                            MenuOption[MenuOption.Count - 1] = (true, MenuOption[MenuOption.Count - 1].Item2);
                            return true;
                        }
                        else
                        {
                            MenuOption[i - 1] = (true, MenuOption[i - 1].Item2);
                            return true;
                        }
                    }
                }



            }

            if (key.Key == ConsoleKey.DownArrow)
            {
                for (int i = 0; i < MenuOption.Count; i++)
                {
                    if (MenuOption[i].Item1)
                    {
                        MenuOption[i] = (false, MenuOption[i].Item2);
                        if (i == MenuOption.Count - 1)
                        {
                            MenuOption[0] = (true, MenuOption[0].Item2);
                            return true;
                        }
                        else
                        {
                            MenuOption[i + 1] = (true, MenuOption[i + 1].Item2);
                            return true;
                        }
                    }

                }
            }


            return false;

        }

        // Static methods for handling different menu actions
        static bool MenuAction(ConsoleKeyInfo key)
        {

            if (key.Key == ConsoleKey.Enter)
            {
                int option = 0;

                foreach (var item in GameMaster.GameMenu.MenuOption)
                {
                    if (item.Item1)
                    {
                        option = GameMaster.GameMenu.MenuOption.IndexOf(item);
                    }
                }
                switch (option)
                {
                    case 0:
                        if (GameMaster.Player.AttackTo())
                        {
                            for (int i = 0; i < GameMaster.players.Count; i++)
                            {
                                if (GameMaster.players[i].Life <= 0)
                                {
                                    GameMaster.players[i].Respawn(GameMaster.players[i]);
                                    GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["kill"]).NoBorder());
                                }
                            }
                            if (GameMaster.Player.Power < 0)
                            {
                                GameMaster.Player.Power = 0;
                            }
                        }

                        break;
                    case 1:
                        if (GameMaster.Player.ShowTrap())
                        {
                            MazeCanvas.ShowTrap();
                            GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["show"]).NoBorder());
                            Thread.Sleep(1000);
                        }
                        GameDisplay.RefreshMaze();
                        break;
                    case 2:

                        switch (GameMaster.Player.SpecialPower)
                        {
                            case PowerEnum.JumpWall:

                                if (Power.JumpWall(Console.ReadKey()))
                                {
                                    GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["jump"]).NoBorder());
                                }
                                else
                                {
                                    GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["noPower"] + MyText.text[MyText.language]["menu"]["noWall"]).NoBorder());
                                }
                                break;
                            case PowerEnum.IncreaseLife:
                                if (Power.IncreaseLife(3))
                                {
                                    GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["increaseLife"]).NoBorder());
                                }
                                else
                                {
                                    GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["noPower"]).NoBorder());
                                }
                                break;
                            case PowerEnum.IncreaseSpeed:
                                if (Power.IncreaseSpeed(4))
                                {
                                    GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["increaseSpeed"]).NoBorder());
                                }
                                else
                                {
                                    GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["noPower"]).NoBorder());
                                }
                                break;
                            case PowerEnum.SwitchPlayer:
                                if (GameMaster.Player.Power >= 5)
                                {
                                    GameMaster.SwitchMenu.actionMenu(key);
                                    GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["switch"]).NoBorder());
                                }
                                else
                                {
                                    GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["noPower"]).NoBorder());
                                }


                                break;
                            case PowerEnum.DestroyTrap:
                                if (Power.DestroyTrap())
                                {
                                    GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["destroyTrap"]).NoBorder());
                                }
                                else
                                {
                                    GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["noPower"]).NoBorder());
                                }
                                break;
                            case PowerEnum.NewTurn:
                                if (Power.NewTurn())
                                {
                                    GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["newTurn"]).NoBorder());
                                }
                                else
                                {
                                    GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["noPower"]).NoBorder());
                                }
                                break;
                            case PowerEnum.CopyPower:
                                if (GameMaster.Player.Power >= 1)
                                {
                                    GameMaster.CopyPowerMenu.actionMenu(key);
                                }
                                else
                                {
                                    GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["noPower"]).NoBorder());
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                return true;
            }
            return false;
        }

        static bool SwitchMenu(ConsoleKeyInfo key)
        {
            List<Character> p = new List<Character>();
            GameMaster.SwitchMenu.MenuOption.Clear();


            //Crea la lista del menu y la lista del player
            for (int i = 0; i < GameMaster.players.Count; i++)
            {
                if (GameMaster.players[i] != GameMaster.Player)
                {
                    GameMaster.SwitchMenu.MenuOption.Add((false, GameMaster.players[i].Name));
                    p.Add(GameMaster.players[i]);
                }
            }
            //Actualiza el menú para que la primera opción sea 1
            GameMaster.SwitchMenu.MenuOption[0] = (true, GameMaster.SwitchMenu.MenuOption[0].Item2);

            bool a = true;
            //Hacer el cambio de variable que modifica el menu desde afuera
            GameDisplay.showmenu = MenuGame.menuS;
            while (a)
            {

                GameDisplay.RefreshMaze();
                ConsoleKeyInfo k = Console.ReadKey();
                GameMaster.SwitchMenu.ChangeOption(k);

                int playerselected = 0;
                if (k.Key == ConsoleKey.Enter)
                {

                    foreach (var item in GameMaster.SwitchMenu.MenuOption)
                    {
                        if (item.Item1)
                        {
                            playerselected = GameMaster.SwitchMenu.MenuOption.IndexOf(item);
                        }
                    }

                    Power.SwitchPlayer(p[playerselected]);

                    a = false;
                }



            }
            GameDisplay.showmenu = MenuGame.menuG;
            return true;
        }
        static bool InitAction(ConsoleKeyInfo key)
        {

            if (key.Key == ConsoleKey.Enter)
            {
                int option = 0;

                foreach (var item in Program.InitMenu.MenuOption)
                {
                    if (item.Item1)
                    {
                        option = Program.InitMenu.MenuOption.IndexOf(item);
                    }
                }
                switch (option)
                {
                    case 0:
                        GameMaster.Game();
                        Thread.Sleep(1000);
                        //El threadSleep debe ser igual al tiempo de la animación de victoria
                        return true;
                    case 1:
                        //Escribe el texto
                        AnsiConsole.Clear();
                        System.Console.WriteLine(MyText.text[MyText.language]["menu"]["control"]);
                        Console.ReadKey();
                        return true;
                    case 2:
                        MyText.langIndex += 1;
                        MyText.language = MyText.allLanguage[MyText.langIndex % MyText.allLanguage.Length];
                        Program.InitMenu = new Menu(Menu.InitMenu(), Menu.initaction);
                        Program.InitMenu.MenuOption[0] = (false, Program.InitMenu.MenuOption[0].Item2);
                        Program.InitMenu.MenuOption[2] = (true, Program.InitMenu.MenuOption[2].Item2);
                        return true;
                    case 3:
                        Audio.currentFile = Audio.music["history"];
                        GameDisplay.History();
                        Console.ReadKey();
                        return true;
                    case 4:
                        return false;


                    default:
                        break;
                }
                return true;
            }
            return true;
        }
        //Crear lista auxiliar y eliminar el elmento de la lista del menu cuando se haya seleccionado
        static bool CharacterSelection(ConsoleKeyInfo key)
        {

            if (key.Key == ConsoleKey.Enter)
            {
                string option = "";

                foreach (var item in GameMaster.CharacterSelection.MenuOption)
                {
                    if (item.Item1)
                    {
                        option = item.Item2;
                    }
                }
                string[] stringoption = [MyText.text[MyText.language]["menu"]["visionLight"],
                                         MyText.text[MyText.language]["menu"]["creativeWind"],
                                         MyText.text[MyText.language]["menu"]["vitalSoul"],
                                         MyText.text[MyText.language]["menu"]["ideaMimetist"],
                                          MyText.text[MyText.language]["menu"]["naturalBreaker"],
                                          MyText.text[MyText.language]["menu"]["mirrorTime"],
                                          MyText.text[MyText.language]["menu"]["chameleonMind"]];
                for (int i = 0; i < stringoption.Length; i++)
                {
                    if (option == stringoption[i])
                    {
                        GameMaster.players.Add(GameMaster.CharacterOption[i]);
                        GameMaster.CharacterSelection.MenuOption.Remove((true, stringoption[i]));
                        GameMaster.CharacterSelection.MenuOption[0] = (true, GameMaster.CharacterSelection.MenuOption[0].Item2);
                    }
                }
                return true;
            }
            return false;
        }

        static bool NumberOfPlayerAction(ConsoleKeyInfo key)
        {

            if (key.Key == ConsoleKey.Enter)
            {
                int option = 0;

                foreach (var item in GameMaster.NumberOfPlayers.MenuOption)
                {
                    if (item.Item1)
                    {
                        option = GameMaster.NumberOfPlayers.MenuOption.IndexOf(item);
                    }
                }
                switch (option)
                {
                    case 0:
                        GameMaster.playeramount = 2;
                        break;
                    case 1:
                        GameMaster.playeramount = 3;
                        break;
                    case 2:
                        GameMaster.playeramount = 4;
                        break;

                    default:
                        break;
                }
                return true;
            }
            return false;
        }

        static bool CopyPowerAction(ConsoleKeyInfo key)
        {
            List<Character> p = new List<Character>();
            GameMaster.SwitchMenu.MenuOption.Clear();


            //Crea la lista del menu y la lista del player
            for (int i = 0; i < GameMaster.players.Count; i++)
            {
                if (GameMaster.players[i] != GameMaster.Player)
                {
                    GameMaster.SwitchMenu.MenuOption.Add((false, GameMaster.players[i].Name));
                    p.Add(GameMaster.players[i]);
                }
            }
            //Actualiza el menú para que la primera opción sea 1
            GameMaster.SwitchMenu.MenuOption[0] = (true, GameMaster.SwitchMenu.MenuOption[0].Item2);



            GameDisplay.showmenu = MenuGame.menuS;
            bool a = true;
            while (a)
            {
                GameDisplay.RefreshMaze();
                ConsoleKeyInfo k = Console.ReadKey();
                GameMaster.SwitchMenu.ChangeOption(k);

                int playerselected = 0;
                if (k.Key == ConsoleKey.Enter)
                {

                    foreach (var item in GameMaster.SwitchMenu.MenuOption)
                    {
                        if (item.Item1)
                        {
                            playerselected = GameMaster.SwitchMenu.MenuOption.IndexOf(item);
                        }
                    }

                    if (Power.CopyPower(p[playerselected], k))
                    {
                        GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["increaseLife"]).NoBorder());
                    }
                    else
                    {
                        GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["noPower"]).NoBorder());
                    }

                    a = false;
                }
            }
            GameDisplay.showmenu = MenuGame.menuG;
            return true;
        }
        public List<(bool, string)> GetList()
        {
            return MenuOption;
        }






    }
}