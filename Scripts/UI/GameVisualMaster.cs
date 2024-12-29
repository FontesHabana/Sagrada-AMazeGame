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
            table.AddColumn(new TableColumn("Options").Centered());
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
        /* public static void SwitchMenu()
         {
             var table = new Table();
             table.AddColumn("Options");
             foreach (var item in GameMaster.pswitch)
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


             layout["GameOption"].Update(
                 Align.Center(table)
             );
         }*/
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
            table.AddColumn(new TableColumn("MENU").Centered());
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


            string[] stringoption = [("Vision Of Light"), ("Creative Wind"), ("Vital Soul"), ("Idea Mimetist"), ("Natural Breaker"), ("Mirror Of Time")];
            string[] characterHistory = [
                                            ("Deep within Gaudí's mind, where ideas shine like stars, emerges Vision of Light. This ethereal being represents the overflowing creativity of the architect. With a single leap, it can surpass walls and barriers, symbolizing Gaudí's ability to transcend the limits of architecture. When the labyrinth becomes dark and oppressive, Vision of Light illuminates the path, guiding its companions toward new possibilities. "),
                                            ("Through the corridors of the labyrinth blows Creative Wind, an ethereal being that moves with the swiftness of the wind. Its ability to increase speed allows its companions to act with agility and ingenuity. Symbolizing the fluidity and dynamism of Gaudí's architectural design, Creative Wind guides its team through challenges with grace and skill, always one step ahead in their quest."),
                                            ("At the beating heart of the labyrinth lies Vital Soul, a being filled with energy and hope. With its power to increase the life of its allies, it infuses strength into those around it. It represents the love for life that Gaudí infused into each of his designs. When players feel discouraged, Vital Soul revives their spirits, reminding them that there is always light at the end of the tunnel."),
                                            ("In the shadows of the labyrinth dwells Idea Mimetist, the master of disguise and transformation. With its power to switch with another player, it can alter the course of the game in an instant. This character reflects the duality and adaptability present in Gaudí's creative mind. When strategies or roles need to change, Idea Mimetist becomes the perfect ally, ensuring that every player can shine in their moment."),
                                            ("From the depths of architectural design arises Natural Breaker, an unyielding warrior who challenges conventions. With its unwavering strength, it can break traps designed to ensnare the unwary. This character embodies Gaudí's tenacity, who was never intimidated by criticism. When dangers lurk in the labyrinth, Natural Breaker charges forward, dismantling obstacles and clearing the way to freedom."),
                                            ("In a hidden corner of Gaudí's mind resides Mirror of Time, a cunning manipulator of time and perception. This character has the power to take an extra turn, allowing it to plan its moves with precision. Reflecting Gaudí's holistic vision, Mirror of Time gazes into the future and acts wisely, ensuring that every step taken is decisive in the quest for the lost piece of the Sagrada Familia.")  ];



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
                            .Label("[blue bold underline]PLAYER Information[/]")
                            .CenterLabel()
                            .AddItem("Life", GameMaster.CharacterOption[i].Life, Color.Red)
                            .AddItem("Speed", GameMaster.CharacterOption[i].Speed, Color.Green)
                            .AddItem("Power", GameMaster.CharacterOption[i].Power, Color.Blue)
                            .AddItem("Attack", GameMaster.CharacterOption[i].Attack, Color.Purple), VerticalAlignment.Middle)
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
            LiveText.PrintHitory();
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

            AnsiConsole.Write(GameDisplay.VerticalMenuInit(Program.InitMenu).Centered().Expand());

            AnsiConsole.Write(sagrada);
        }
        public static void Victory(int winner)
        {
            AnsiConsole.Clear();
            Layout victory = new Layout("victory").SplitRows(
                new Layout("top"),
                new Layout("image")
            );
            victory["top"].Size(15);
            victory["top"].Update(new FigletText("Victory")
                .LeftJustified()
                .Color(Color.Blue)
                .Centered());

            GameDisplay.GenerateCharacter(35);
            victory["image"].Update(new Panel(Align.Center(GameMaster.players[winner - 1].Image)).Expand().NoBorder());



            AnsiConsole.Write(victory);
            Thread.Sleep(5000);

        }

    }


}




