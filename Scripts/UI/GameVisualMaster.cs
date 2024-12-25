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


            #region referencia según string
            if (Sreference == "Vision Of Light")
            {
                layoutInit["CharacterImage"].Update(
           new Panel(Align.Center(GameMaster.CharacterOption[0].Image)).BorderColor(GameMaster.CharacterOption[0].Appearance).HeavyBorder()
       );

                layoutInit["Items"].Update(
                    new Panel(
                        Align.Left(
                        new BarChart()
                        .Width(30)
                        .Label("[blue bold underline]PLAYER Information[/]")
                        .CenterLabel()
                        .AddItem("Life", GameMaster.CharacterOption[0].Life, Color.Red)
                        .AddItem("Speed", GameMaster.CharacterOption[0].Speed, Color.Green)
                        .AddItem("Power", GameMaster.CharacterOption[0].Power, Color.Blue)
                        .AddItem("Attack", GameMaster.CharacterOption[0].Attack, Color.Purple), VerticalAlignment.Middle)
                          ).NoBorder().Expand());
            }

            if (Sreference == "Creative Wind")
            {
                layoutInit["CharacterImage"].Update(
         new Panel(Align.Center(GameMaster.CharacterOption[1].Image)).BorderColor(GameMaster.CharacterOption[1].Appearance).HeavyBorder()
     );
                layoutInit["Items"].Update(
                                        new Panel(
                                            Align.Left(
                                            new BarChart()
                                            .Width(30)
                                            .Label("[blue bold underline]PLAYER Information[/]")
                                            .CenterLabel()
                                            .AddItem("Life", GameMaster.CharacterOption[1].Life, Color.Red)
                                            .AddItem("Speed", GameMaster.CharacterOption[1].Speed, Color.Green)
                                            .AddItem("Power", GameMaster.CharacterOption[1].Power, Color.Blue)
                                            .AddItem("Attack", GameMaster.CharacterOption[1].Attack, Color.Purple), VerticalAlignment.Middle)
                                              ).NoBorder().Expand());
            }

            if (Sreference == "Idea Mimetist")
            {
                layoutInit["CharacterImage"].Update(
           new Panel(Align.Center(GameMaster.CharacterOption[3].Image)).BorderColor(GameMaster.CharacterOption[3].Appearance).HeavyBorder()
       );
                layoutInit["Items"].Update(
                                        new Panel(
                                            Align.Left(
                                            new BarChart()
                                            .Width(30)
                                            .Label("[blue bold underline]PLAYER Information[/]")
                                            .CenterLabel()
                                            .AddItem("Life", GameMaster.CharacterOption[3].Life, Color.Red)
                                            .AddItem("Speed", GameMaster.CharacterOption[3].Speed, Color.Green)
                                            .AddItem("Power", GameMaster.CharacterOption[3].Power, Color.Blue)
                                            .AddItem("Attack", GameMaster.CharacterOption[3].Attack, Color.Purple), VerticalAlignment.Middle)
                                              ).NoBorder().Expand());
            }

            if (Sreference == "Mirror Of Time")
            {
                layoutInit["CharacterImage"].Update(
           new Panel(Align.Center(GameMaster.CharacterOption[5].Image)).BorderColor(GameMaster.CharacterOption[5].Appearance).HeavyBorder()
       );
                layoutInit["Items"].Update(
                             new Panel(
                                 Align.Left(
                                 new BarChart()
                                 .Width(30)
                                 .Label("[blue bold underline]PLAYER Information[/]")
                                 .CenterLabel()
                                 .AddItem("Life", GameMaster.CharacterOption[5].Life, Color.Red)
                                 .AddItem("Speed", GameMaster.CharacterOption[5].Speed, Color.Green)
                                 .AddItem("Power", GameMaster.CharacterOption[5].Power, Color.Blue)
                                 .AddItem("Attack", GameMaster.CharacterOption[5].Attack, Color.Purple), VerticalAlignment.Middle)
                                   ).NoBorder().Expand());

            }

            if (Sreference == "Natural Breaker")
            {
                layoutInit["CharacterImage"].Update(
           new Panel(Align.Center(GameMaster.CharacterOption[4].Image)).BorderColor(GameMaster.CharacterOption[4].Appearance).HeavyBorder()
       );
                layoutInit["Items"].Update(
                             new Panel(
                                 Align.Left(
                                 new BarChart()
                                 .Width(30)
                                 .Label("[blue bold underline]PLAYER Information[/]")
                                 .CenterLabel()
                                 .AddItem("Life", GameMaster.CharacterOption[4].Life, Color.Red)
                                 .AddItem("Speed", GameMaster.CharacterOption[4].Speed, Color.Green)
                                 .AddItem("Power", GameMaster.CharacterOption[4].Power, Color.Blue)
                                 .AddItem("Attack", GameMaster.CharacterOption[4].Attack, Color.Purple), VerticalAlignment.Middle)
                                   ).NoBorder().Expand());

            }

            if (Sreference == "Vital Soul")
            {
                layoutInit["CharacterImage"].Update(
           new Panel(Align.Center(GameMaster.CharacterOption[2].Image)).BorderColor(GameMaster.CharacterOption[2].Appearance).HeavyBorder()
       );
                layoutInit["Items"].Update(
                             new Panel(
                                 Align.Left(
                                 new BarChart()
                                 .Width(30)
                                 .Label("[blue bold underline]PLAYER Information[/]")
                                 .CenterLabel()
                                 .AddItem("Life", GameMaster.CharacterOption[2].Life, Color.Red)
                                 .AddItem("Speed", GameMaster.CharacterOption[2].Speed, Color.Green)
                                 .AddItem("Power", GameMaster.CharacterOption[2].Power, Color.Blue)
                                 .AddItem("Attack", GameMaster.CharacterOption[2].Attack, Color.Purple), VerticalAlignment.Middle)
                                   ).NoBorder().Expand());

            }
            #endregion


            AnsiConsole.Write(layoutInit);

        }

    }

    #endregion
}




