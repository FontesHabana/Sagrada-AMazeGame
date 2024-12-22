using UserInterface;
using Tiles;
using Spectre.Console;
namespace LogicGame
{

    class Menu
    {  //General 
        List<(bool, string)> MenuOption { get; set; }
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

                            GameMaster.Player.Power -= 4;
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


        public List<(bool, string)> GetList()
        {
            return MenuOption;
        }






    }
}