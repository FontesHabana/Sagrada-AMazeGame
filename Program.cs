using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using MazeBuilder;
using Tiles;
using Spectre.Console;
using UserInterface;
using System.Runtime.CompilerServices;
using LogicGame;
using NAudio.Wave;

public class Program
{
    private static bool isPlaying = true; // Variable para controlar la reproducción
    public static void Main(string[] args)
    {


        // Iniciar la reproducción de audio en un hilo separado
        Thread audioThread = new Thread(Audio.PlayAudio);
        audioThread.Start();

        while (true)
        {
            Console.Clear();
            System.Console.WriteLine(Menu.gamemenu[0]);
            System.Console.WriteLine(GameMaster.GameMenu.GetList()[0].Item2);
            //Presentación del juego

            //Menú de inicio
            AnsiConsole.Write(
                new FigletText("Picasso`s Dream")
                .LeftJustified()
                .Color(Color.Blue)
                .Centered()
            );
            //Switch con opciones del menú de inicio?

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .PageSize(10)
                .AddChoices(new[] { "New Game", "Option", "Credits", "Exit" })
            );

            if (option == "New Game")
            {
                GameMaster.Game();
                Thread.Sleep(1000);
            }
            if (option == "Option")
            {

            }
            if (option == "Exit")
            {
                break;
            }
        }

        Audio.isPlaying = false;
        audioThread.Join();
    }

}