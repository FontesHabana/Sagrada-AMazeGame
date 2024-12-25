using UserInterface;
using Tiles;
using Spectre.Console;
using System.Security;
namespace LogicGame
{

    class Menu
    {  //General 
        public List<(bool, string)> MenuOption { get; set; }
        //Create a delegate
        public delegate bool ActionMenu(ConsoleKeyInfo key);
        //Variable delegate
        public ActionMenu actionMenu { get; set; }



        //Listas de todos los menus del juego
        //Menu del juego
        public static List<(bool, string)> gamemenu = new List<(bool, string)> { (false, "Attack"), (false, "Show Traps"), (false, "Special Effect"), (true, "Next Turn") };
        public static ActionMenu action = MenuAction;
        //Menu Principal
        public static List<(bool, string)> pswitch = new List<(bool, string)>();
        public static ActionMenu change = SwitchMenu;
        //Menu de inicio
        public static List<(bool, string)> initmenu = new List<(bool, string)> { (true, "New Game"), (false, "Instruction"), (false, "Credits"), (false, "Exit") };
        public static ActionMenu initaction = InitAction;
        //Number of players
        public static List<(bool, string)> numberofplayer = new List<(bool, string)> { (true, "2"), (false, "3"), (false, "4") };
        public static ActionMenu numberofplayeraction = NumberOfPlayerAction;


        //Select player list
        public static List<(bool, string)> characters = new List<(bool, string)> { (false, "Vision Of Light"), (false, "Creative Wind"), (true, "Vital Soul"), (false, "Idea Mimetist"), (false, "Natural Breaker"), (false, "Mirror Of Time") };
        public static ActionMenu charactersaction = CharacterSelection;

        public Menu(List<(bool, string)> menu, ActionMenu action)
        {
            MenuOption = menu;
            actionMenu = action;
        }

        public bool ChangeOption(ConsoleKeyInfo key)
        {

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
                            Thread.Sleep(1000);
                        }
                        GameDisplay.RefreshMaze();
                        break;
                    case 2:

                        switch (GameMaster.Player.SpecialPower)
                        {
                            case PowerEnum.JumpWall:
                                Power.JumpWall(Console.ReadKey());
                                break;
                            case PowerEnum.IncreaseLife:
                                Power.IncreaseLife(3);
                                break;
                            case PowerEnum.IncreaseSpeed:
                                Power.IncreaseSpeed(4);
                                break;
                            case PowerEnum.SwitchPlayer:

                                GameMaster.SwitchMenu.actionMenu(key);

                                break;
                            case PowerEnum.DestroyTrap:
                                Power.DestroyTrap();
                                break;
                            case PowerEnum.NewTurn:
                                Power.NewTurn();
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
            pswitch.Clear();


            //Crea la lista del menu y la lista del player
            for (int i = 0; i < GameMaster.players.Count; i++)
            {
                if (GameMaster.players[i] != GameMaster.Player)
                {
                    pswitch.Add((false, GameMaster.players[i].Name));
                    p.Add(GameMaster.players[i]);
                }
            }
            //Actualiza el menú para que la primera opción sea 1
            pswitch[0] = (true, pswitch[0].Item2);

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

                    foreach (var item in pswitch)
                    {
                        if (item.Item1)
                        {
                            playerselected = pswitch.IndexOf(item);
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
                        return true;
                    case 1:
                        return true;
                    case 2:
                        return true;
                    case 3:
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
                string[] stringoption = [("Vision Of Light"), ("Creative Wind"), ("Vital Soul"), ("Idea Mimetist"), ("Natural Breaker"), ("Mirror Of Time")];
                for (int i = 0; i < stringoption.Length; i++)
                {
                    if (option == stringoption[i])
                    {
                        GameMaster.players.Add(GameMaster.CharacterOption[i]);
                        GameMaster.CharacterSelection.MenuOption.Remove((true, stringoption[i]));
                        GameMaster.CharacterSelection.MenuOption[0] = (true, GameMaster.CharacterSelection.MenuOption[0].Item2);
                    }
                }
                //El switch no funciona porque despues de eliminar el primer elemento se corre y no coincide, hacerlo con 6if
                /*   switch (option)
                   {
                       case 0:
                           GameMaster.players.Add(GameMaster.CharacterOption[0]);
                           GameMaster.CharacterSelection.MenuOption.Remove((true, "Vision Of Light"));
                           System.Console.WriteLine("vision of Light");
                           GameMaster.CharacterSelection.MenuOption[0] = (true, GameMaster.CharacterSelection.MenuOption[0].Item2);
                           break;
                       case 1:
                           GameMaster.players.Add(GameMaster.CharacterOption[1]);
                           GameMaster.CharacterSelection.MenuOption.Remove((true, "Creative Wind"));
                           GameMaster.CharacterSelection.MenuOption[0] = (true, GameMaster.CharacterSelection.MenuOption[0].Item2);
                           break;
                       case 2:
                           GameMaster.players.Add(GameMaster.CharacterOption[2]);
                           GameMaster.CharacterSelection.MenuOption.Remove((true, "Vita Soul"));
                           GameMaster.CharacterSelection.MenuOption[0] = (true, GameMaster.CharacterSelection.MenuOption[0].Item2);
                           break;
                       case 3:
                           GameMaster.players.Add(GameMaster.CharacterOption[3]);
                           GameMaster.CharacterSelection.MenuOption.Remove((true, "Idea Mimetist"));
                           GameMaster.CharacterSelection.MenuOption[0] = (true, GameMaster.CharacterSelection.MenuOption[0].Item2);
                           break;
                       case 4:
                           GameMaster.players.Add(GameMaster.CharacterOption[4]);
                           GameMaster.CharacterSelection.MenuOption.Remove((true, "Natural Breaker"));
                           GameMaster.CharacterSelection.MenuOption[0] = (true, GameMaster.CharacterSelection.MenuOption[0].Item2);
                           break;
                       case 5:
                           GameMaster.players.Add(GameMaster.CharacterOption[5]);
                           GameMaster.CharacterSelection.MenuOption.Remove((true, "Mirror Of Time"));
                           GameMaster.CharacterSelection.MenuOption[0] = (true, GameMaster.CharacterSelection.MenuOption[0].Item2);
                           break;

                       default:
                           break;
                   }*/
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






        public List<(bool, string)> GetList()
        {
            return MenuOption;
        }






    }
}