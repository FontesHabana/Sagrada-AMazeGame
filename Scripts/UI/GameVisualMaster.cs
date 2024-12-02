using Spectre.Console;
using MazeBuilder;
using Tiles;
using LogicGame;
using System.ComponentModel;
namespace UserInterface
{
    class GameDisplay
    {

        public static Layout layout;


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

        public static void GameMenu()
        {
            var table = new Table();
            table.AddColumn("Options");
            foreach (var item in GameMaster.gameOption)
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
                table
            );



        }
    }
}

