using Spectre.Console;
using MazeBuilder;
using Tiles;
using LogicGame;
using System.ComponentModel;
namespace UserInterface
{
    public enum MenuGame
    {
        menuG,
        menuS,
    }
    class GameDisplay
    {

        public static Layout layout;
        public static MenuGame showmenu = MenuGame.menuG;




        public static void GameScreen()
        {
            layout = new Layout("GameScreen")
                     .SplitColumns(
                         new Layout("MazeContainer"),
                         new Layout("Rigth")
                         .SplitRows(
                             new Layout("top")
                             .SplitColumns(
                                 new Layout("CharacterContainer"),
                                 new Layout("TopRigth")
                                 .SplitRows(
                                    new Layout("Character Status"),
                                    new Layout("GameOption")
                                 )
                             ),
                             new Layout("bottom")
                         )
                     );

            layout["MazeContainer"].MinimumSize(85);
            layout["top"].Ratio(3);

        }

        public static void PlayerStatus()
        {
            layout["CharacterContainer"].Update(
                new Panel(
            Align.Center(
            new Markup($"{GameMaster.Player.Name} [blue]Is your turn![/]"),
            VerticalAlignment.Middle))
            .Expand());



            layout["Character Status"].Update(
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
                      ).Expand()
                  );



        }

        public static void VerticalMenu(MenuGame showmenu)
        {
            var table = new Table();
            table.AddColumn("Options");
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


            layout["GameOption"].Update(
                Align.Center(table)
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



            layout["MazeContainer"].Update(
                new Panel(Align.Center(MazeCanvas.canvas))
            );
            PlayerStatus();
            VerticalMenu(showmenu);


            Console.WriteLine();
            AnsiConsole.Clear();

            AnsiConsole.Write(layout);

            ///AnsiConsole.Clear();
            //AnsiConsole.Write(canvas);
        }
    }
}
