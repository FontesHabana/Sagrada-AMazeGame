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
    // Class responsible for managing game visual elements
    class GameDisplay
    {



        // Static canvas representing the sagrada logo
        static CanvasImage sagrada = new CanvasImage("Assets/images_2_-01-removebg-preview.png");
        // Layout objects for game screen and initialization
        public static Layout layoutGame;
        public static Layout layoutInit;
        // Current menu state
        public static MenuGame showmenu = MenuGame.menuG;

        // Method to set up the game screen layout
        public static void GameScreen()
        {
            // Create the main game screen layout
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
            // Set minimum size for maze container
            layoutGame["MazeContainer"].MinimumSize(85);
            // Set ratio for top section
            layoutGame["top"].Ratio(3);
            // Set ratio for character image
            layoutGame["Character Image"].Ratio(5);

        }

        // Method to display player statu
        public static void PlayerStatus()
        {

            // Update character image panel
            layoutGame["Character Image"].Update(
                    new Panel(
                Align.Center(GameMaster.Player.Image).MiddleAligned()).Border(BoxBorder.Double).BorderColor(GameMaster.Player.Appearance).Expand());

            // Update character name panel
            layoutGame["Character name"].Update(
                new Panel(
            Align.Center(
            new Markup(GameMaster.Player.Name + MyText.text[MyText.language]["visualMaster"]["yourturn"]),
            VerticalAlignment.Middle))
            .Expand().BorderColor(GameMaster.Player.Appearance));


            // Update character status panel
            layoutGame["Character Status"].Update(
                new Panel(
                    Align.Left(
                    new BarChart()
                    .Width(30)
                    .Label("[blue bold underline]PLAYER STATUS[/]")
                    .CenterLabel()
                     .AddItem(MyText.text[MyText.language]["visualMaster"]["life"], GameMaster.Player.Life, Color.Red)
                            .AddItem(MyText.text[MyText.language]["visualMaster"]["speed"], GameMaster.playerspeed, Color.Green)
                            .AddItem(MyText.text[MyText.language]["visualMaster"]["power"], GameMaster.Player.Power, Color.Blue)
                            .AddItem(MyText.text[MyText.language]["visualMaster"]["attack"], GameMaster.Player.Attack, Color.Purple), VerticalAlignment.Middle)
                      ).Expand().BorderColor(GameMaster.Player.Appearance));



        }


        // Method to display vertical menu
        public static void VerticalMenu(MenuGame showmenu)
        {  // Create table for menu options
            var table = new Table();
            table.AddColumn(new TableColumn(MyText.text[MyText.language]["visualMaster"]["option"]).Centered());
            // List to store menu items
            List<(bool, string)> m = new List<(bool, string)>();
            // Populate menu items based on current menu state
            if (showmenu == MenuGame.menuG)
            {
                m = GameMaster.GameMenu.GetList();
            }
            if (showmenu == MenuGame.menuS)
            {
                m = GameMaster.SwitchMenu.GetList();
            }

            // Add rows to the table based on menu items
            foreach (var item in m)
            {
                if (item.Item1)
                {// Highlight selected option
                    table.AddRow(new Markup($"[blue]> {item.Item2} [/]"));
                }
                else
                { // Normal unselected option
                    table.AddRow(new Markup($"[white]  {item.Item2} [/]"));
                }
            }
            // Set table properties
            table.Border = TableBorder.HeavyHead;
            table.BorderColor(GameMaster.Player.Appearance);
            table.Expand();


            // Update game option panel with the menu table

            layoutGame["GameOption"].Update(
                Align.Center(table).VerticalAlignment(VerticalAlignment.Middle)
            );

        }
        // Method to refresh maze visualization
        public static void RefreshMaze()
        {// Print maze to canvas
            MazeCanvas.PrintMaze();
            // Add player tiles to the canvas
            for (int i = 0; i < GameMaster.players.Count; i++)
            {
                MazeCanvas.AddTile(GameMaster.players[i]);
            }
            // Add main flag tile to the canvas
            MazeCanvas.AddTile(GameMaster.mainFlag);


            // Update maze container in the game layout
            layoutGame["MazeContainer"].Update(
                new Panel(Align.Center(MazeCanvas.canvas))
            );

            PlayerStatus();
            VerticalMenu(showmenu);

            // Clear console and redraw game layout
            Console.WriteLine();
            AnsiConsole.Clear();

            AnsiConsole.Write(layoutGame);

            ///AnsiConsole.Clear();
            //AnsiConsole.Write(canvas);
        }

        // Method to generate character images
        public static void GenerateCharacter(int size)
        {// Resize character images
            for (int i = 0; i < GameMaster.CharacterOption.Count; i++)
            {
                GameMaster.CharacterOption[i].Image.MaxWidth(size);
            }
        }


        #region  InitGameMenus
        // Method to create vertical menu for initialization
        public static Table VerticalMenuInit(Menu menu)
        {
            var table = new Table();
            table.AddColumn(new TableColumn(MyText.text[MyText.language]["visualMaster"]["menu"]).Centered());
            List<(bool, string)> m = menu.GetList();


            // Add rows to the table based on menu items
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


            layoutInit["Menu"].Size(8);
            layoutInit["Items"].Size(12);


        }


        public static Table HorizontalMenu(Menu menu, string title)
        {
            var table = new Table();
            table.AddColumn(new TableColumn(title).Centered());
            List<(bool, string)> m = menu.GetList();


            // Add rows to the table based on menu items
            foreach (var item in m)
            {
                if (item.Item1)
                {
                    // Highlight selected option
                    table.AddRow(new Markup($"[blue] ▼ {item.Item2} ▼ [/]"));
                }
            }
            table.Border = TableBorder.Minimal;
            table.Expand();

            return table;
        }


        // Method to print character selection menu
        public static void PrintSelectionMenu(Menu menu, string title)
        {
            Console.Clear();
            layoutInit["Menu"].Update(
               new Panel(Align.Center(HorizontalMenu(menu, title))).NoBorder()
           );
            // Reference variable for selected option
            string Sreference = "";



            // Arrays of strings for character options and histories
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


            // Iterate through menu options
            for (int i = 0; i < stringoption.Length; i++)
            {
                if (Sreference == stringoption[i])
                { // Update character image panel
                    layoutInit["CharacterImage"].Update(
                   new Panel(Align.Center(GameMaster.CharacterOption[i].Image)).BorderColor(GameMaster.CharacterOption[i].Appearance).HeavyBorder()
                  );
                    // Update character specification panel
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
        // Method to start the game
        public static void Start()
        {

            Console.Clear();
            AnsiConsole.Clear();


            sagrada.PixelWidth(1);
            History();

            Console.ReadKey();

        }
        // Method to display main page
        public static void mainPage()
        {

            AnsiConsole.Clear();

            //Game presentation

            AnsiConsole.Write(
                new FigletText("Sagrada")
                .LeftJustified()
                .Color(Color.Blue)
                .Centered()
            );
            // Display initial menu
            AnsiConsole.Write(VerticalMenuInit(Program.InitMenu).Centered().Expand());
            // Display sagrada logo
            AnsiConsole.Write(sagrada);
        }


        //Victory visualisation
        public static void Victory(int winner)
        {
            AnsiConsole.Clear();
            //victory text
            Layout victory = new Layout("victory").SplitRows(
                new Layout("top"),
                new Layout("image")
            );
            victory["top"].Size(10);
            victory["top"].Update(new FigletText(MyText.text[MyText.language]["visualMaster"]["victory"])
                .LeftJustified()
                .Color(Color.Blue)
                .Centered());

            //player image
            GameDisplay.GenerateCharacter(25);
            victory["image"].Update(new Panel(Align.Center(GameMaster.players[winner - 1].Image)).Expand().NoBorder());



            AnsiConsole.Write(victory);
            Thread.Sleep(5000);

        }


        public static void History()
        {
            Console.Clear();
            AnsiConsole.Clear();

            AnsiConsole.Write(
                          new FigletText("Sagrada")
                          .LeftJustified()
                          .Color(Color.Blue)
                          .Centered()
                      );
            System.Console.WriteLine("\n\n\n");
            System.Console.WriteLine(MyText.text[MyText.language]["text"]["title"]);
            Thread.Sleep(25);


            foreach (char x in MyText.text[MyText.language]["text"]["content"])
            {
                if (x == '+')
                {
                    System.Console.WriteLine();
                }
                else
                {
                    System.Console.Write(x);
                }


            }
        }
    }


}




