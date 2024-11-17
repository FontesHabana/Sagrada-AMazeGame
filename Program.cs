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
        int i = 10;
        GameMaster.InitGame();
        while (i > 0)
        {
            GameMaster.Turn();
            GameMaster.NextTurn();
            i--;
        }



    }
}