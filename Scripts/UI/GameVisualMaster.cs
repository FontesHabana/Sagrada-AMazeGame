using Spectre.Console;
using MazeBuilder;
using Tiles;
using LogicGame;
using System.ComponentModel;
using NAudio.Wave.SampleProviders;
using System.CodeDom.Compiler;
using Spectre.Console.Extensions;
using System.Runtime;
namespace UserInterface
{
    public enum MenuGame
    {
        menuG,
        menuS,

    }
    //Asignar nombre de cada personaje
    //Creo que estas referencias no serna necesarias 

    class GameDisplay
    {
        static CanvasImage sagrada = new CanvasImage("Assets/images_2_-01-removebg-preview.png");
        public static Layout layoutGame;
        public static Layout layoutInit;

        public static MenuGame showmenu = MenuGame.menuG;


        public static void GameScreen()
        {
            layoutGame = new Layout("GameScreen")
                     .SplitColumns(
                         new Layout("MazeContainer"),
                         new Layout("Rigth")
                         .SplitRows(
                             new Layout("top")
                             .SplitColumns(
                                 new Layout("CharacterContainer")
                                 .SplitRows(new Layout("Character name"),
                                 new Layout("Character Image")),
                                 new Layout("TopRigth")
                                 .SplitRows(
                                    new Layout("Character Status"),
                                    new Layout("GameOption")
                                 )
                             ),
                             new Layout("bottom")
                         )
                     );

            layoutGame["MazeContainer"].MinimumSize(85);
            layoutGame["top"].Ratio(3);
            layoutGame["Character Image"].Ratio(5);

        }


        public static void PlayerStatus()
        {


            layoutGame["Character Image"].Update(
                    new Panel(
                Align.Center(GameMaster.Player.Image).MiddleAligned()).Border(BoxBorder.Double).BorderColor(GameMaster.Player.Appearance).Expand());
            layoutGame["Character name"].Update(
                new Panel(
            Align.Center(
            new Markup($"{GameMaster.Player.Name} [blue]Is your turn![/]"),
            VerticalAlignment.Middle))
            .Expand().BorderColor(GameMaster.Player.Appearance));



            layoutGame["Character Status"].Update(
                new Panel(
                    Align.Left(
                    new BarChart()
                    .Width(30)
                    .Label("[blue bold underline]PLAYER STATUS[/]")
                    .CenterLabel()
                    .AddItem("Life", GameMaster.Player.Life, Color.Red)
                    .AddItem("Speed", GameMaster.playerspeed, Color.Green)
                    .AddItem("Power", GameMaster.Player.Power, Color.Blue)
                    .AddItem("Attack", GameMaster.Player.Attack, Color.Purple), VerticalAlignment.Middle)
                      ).Expand().BorderColor(GameMaster.Player.Appearance));



        }

        public static void VerticalMenu(MenuGame showmenu)
        {
            var table = new Table();
            table.AddColumn(new TableColumn(MyText.text[MyText.language]["visualMaster"]["option"]).Centered());
            List<(bool, string)> m = new List<(bool, string)>();
            if (showmenu == MenuGame.menuG)
            {
                m = GameMaster.GameMenu.GetList();
            }
            if (showmenu == MenuGame.menuS)
            {
                m = GameMaster.SwitchMenu.GetList();
            }


            foreach (var item in m)
            {
                if (item.Item1)
                {
                    table.AddRow(new Markup($"[blue]> {item.Item2} [/]"));
                }
                else
                {
                    table.AddRow(new Markup($"[white]  {item.Item2} [/]"));
                }
            }
            table.Border = TableBorder.HeavyHead;
            table.BorderColor(GameMaster.Player.Appearance);
            table.Expand();




            layoutGame["GameOption"].Update(
                Align.Center(table).VerticalAlignment(VerticalAlignment.Middle)
            );

        }

        public static void RefreshMaze()
        {
            MazeCanvas.PrintMaze();
            for (int i = 0; i < GameMaster.players.Count; i++)
            {
                MazeCanvas.AddTile(GameMaster.players[i]);
            }
            MazeCanvas.AddTile(GameMaster.mainFlag);



            layoutGame["MazeContainer"].Update(
                new Panel(Align.Center(MazeCanvas.canvas))
            );
            PlayerStatus();
            VerticalMenu(showmenu);


            Console.WriteLine();
            AnsiConsole.Clear();

            AnsiConsole.Write(layoutGame);

            ///AnsiConsole.Clear();
            //AnsiConsole.Write(canvas);
        }


        public static void GenerateCharacter(int size)
        {
            for (int i = 0; i < GameMaster.CharacterOption.Count; i++)
            {
                GameMaster.CharacterOption[i].Image.MaxWidth(size);
            }
        }


        #region  InitGameMenus
        public static Table VerticalMenuInit(Menu menu)
        {
            var table = new Table();
            table.AddColumn(new TableColumn(MyText.text[MyText.language]["visualMaster"]["menu"]).Centered());
            List<(bool, string)> m = menu.GetList();



            foreach (var item in m)
            {
                if (item.Item1)
                {
                    table.AddRow(new Markup($"[blue]> {item.Item2} [/]"));
                }
                else
                {
                    table.AddRow(new Markup($"[white]  {item.Item2} [/]"));
                }
            }
            table.Border = TableBorder.Minimal;
            //table.Expand();

            return table;
        }



        public static void InitLayout()
        {
            layoutInit = new Layout("Init")
                     .SplitRows(
                         new Layout("Menu"),
                         new Layout("Visual")
                         .SplitColumns(
                                 new Layout("CharacterImage"),
                                 new Layout("CharacterSpecification")
                                 .SplitRows(
                                    new Layout("Items"),
                                    new Layout("Information")
                                 ))
                     );

            // layoutInit["CharacterSpecification"].Ratio(40);
            // layoutInit["CharacterImage"].Ratio(40);
            layoutInit["Menu"].Size(8);
            layoutInit["Items"].Size(12);
            //


        }


        public static Table HorizontalMenu(Menu menu, string title)
        {
            var table = new Table();
            table.AddColumn(new TableColumn(title).Centered());
            List<(bool, string)> m = menu.GetList();



            foreach (var item in m)
            {
                if (item.Item1)
                {
                    table.AddRow(new Markup($"[blue] < {item.Item2} > [/]"));
                }
            }
            table.Border = TableBorder.Minimal;
            table.Expand();

            return table;
        }

        public static void PrintSelectionMenu(Menu menu, string title)
        {
            Console.Clear();
            layoutInit["Menu"].Update(
               new Panel(Align.Center(HorizontalMenu(menu, title))).NoBorder()
           );
            string Sreference = "";




            foreach (var item in menu.MenuOption)
            {
                if (item.Item1)
                {
                    Sreference = item.Item2;
                }
            }


            string[] stringoption = [MyText.text[MyText.language]["menu"]["visionLight"],
                                    MyText.text[MyText.language]["menu"]["creativeWind"],
                                    MyText.text[MyText.language]["menu"]["vitalSoul"],
                                    MyText.text[MyText.language]["menu"]["ideaMimetist"],
                                    MyText.text[MyText.language]["menu"]["naturalBreaker"],
                                    MyText.text[MyText.language]["menu"]["mirrorTime"],
                                    MyText.text[MyText.language]["menu"]["chameleonMind"]];
            string[] characterHistory = [ MyText.text[MyText.language]["visualMaster"]["visionLight"],
                                         MyText.text[MyText.language]["visualMaster"]["creativeWind"],
                                         MyText.text[MyText.language]["visualMaster"]["vitalSoul"],
                                         MyText.text[MyText.language]["visualMaster"]["ideaMimetist"],
                                         MyText.text[MyText.language]["visualMaster"]["naturalBreaker"],
                                         MyText.text[MyText.language]["visualMaster"]["mirrorTime"],
                                         MyText.text[MyText.language]["visualMaster"]["chameleonMind"]];



            for (int i = 0; i < stringoption.Length; i++)
            {
                if (Sreference == stringoption[i])
                {
                    layoutInit["CharacterImage"].Update(
                   new Panel(Align.Center(GameMaster.CharacterOption[i].Image)).BorderColor(GameMaster.CharacterOption[i].Appearance).HeavyBorder()
                  );

                    layoutInit["Items"].Update(
                        new Panel(
                            Align.Left(
                            new BarChart()
                            .Width(30)
                            .Label(MyText.text[MyText.language]["visualMaster"]["information"])
                            .CenterLabel()
                            .AddItem(MyText.text[MyText.language]["visualMaster"]["life"], GameMaster.CharacterOption[i].Life, Color.Red)
                            .AddItem(MyText.text[MyText.language]["visualMaster"]["speed"], GameMaster.CharacterOption[i].Speed, Color.Green)
                            .AddItem(MyText.text[MyText.language]["visualMaster"]["power"], GameMaster.CharacterOption[i].Power, Color.Blue)
                            .AddItem(MyText.text[MyText.language]["visualMaster"]["attack"], GameMaster.CharacterOption[i].Attack, Color.Purple), VerticalAlignment.Middle)
                              ).NoBorder().Expand());

                    layoutInit["Information"].Update(
                        new Panel(characterHistory[i]).Expand().NoBorder()
                    );
                }

            }


            AnsiConsole.Write(layoutInit);

        }

        #endregion

        public static void Start()
        {
            Console.Clear();
            AnsiConsole.Clear();

            sagrada.PixelWidth(1);
            MyText.PrintHitory();
            Thread.Sleep(5000);
        }

        public static void mainPage()
        {

            AnsiConsole.Clear();

            //Presentación del juego

            //Menú de inicio
            // System.Console.WriteLine("\n \n \n");
            AnsiConsole.Write(
                new FigletText("Sagrada")
                .LeftJustified()
                .Color(Color.Blue)
                .Centered()
            );

            AnsiConsole.Write(VerticalMenuInit(Program.InitMenu).Centered().Expand());

            AnsiConsole.Write(sagrada);
        }
        public static void Victory(int winner)
        {
            AnsiConsole.Clear();
            Layout victory = new Layout("victory").SplitRows(
                new Layout("top"),
                new Layout("image")
            );
            victory["top"].Size(10);
            victory["top"].Update(new FigletText(MyText.text[MyText.language]["visualMaster"]["victory"])
                .LeftJustified()
                .Color(Color.Blue)
                .Centered());

            GameDisplay.GenerateCharacter(25);
            victory["image"].Update(new Panel(Align.Center(GameMaster.players[winner - 1].Image)).Expand().NoBorder());



            AnsiConsole.Write(victory);
            Thread.Sleep(5000);

        }

    }


}




