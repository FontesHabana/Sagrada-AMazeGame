using System.ComponentModel.DataAnnotations;
using MazeBuilder;
using Spectre.Console;
using UserInterface;
public class Program
{
    public static void Main(string[] args)
    {
        Maze.MainMaze();
        MazeDisplay.PrintMaze();


    }
}