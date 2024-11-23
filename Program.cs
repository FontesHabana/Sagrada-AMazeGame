using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using MazeBuilder;
using Tiles;
using Spectre.Console;
using UserInterface;
using System.Runtime.CompilerServices;
using LogicGame;

public class Program
{
    public static void Main(string[] args)
    {
        GameDisplay.GameScreen();
        GameMaster.InitGame();
        //MazeCanvas.RefreshMaze();
        while (GameMaster.VictoryCondition() == 0)
        {// La victoria se desata al finalizar el turno. Hay que implementar que el final se desate cuando llegues a la meta
            GameMaster.Turn();

        }

    }
}