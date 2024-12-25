using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using MazeBuilder;
using Tiles;
using Spectre.Console;
using UserInterface;
using System.Runtime.CompilerServices;
using LogicGame;
using NAudio.Wave;

class Program
{
    private static bool isPlaying = true; // Variable para controlar la reproducción
    public static Menu InitMenu = new Menu(Menu.initmenu, Menu.initaction);

    public static void Main(string[] args)
    {


        // Iniciar la reproducción de audio en un hilo separado
        Thread audioThread = new Thread(Audio.PlayAudio);
        audioThread.Start();

        CanvasImage sagrada = new CanvasImage("Assets/images_2_-01-removebg-preview.png");

        AnsiConsole.Clear();

        while (true)
        {
            Console.Clear();

            //Presentación del juego

            //Menú de inicio
            System.Console.WriteLine("\n \n \n");
            AnsiConsole.Write(
                new FigletText("Sagrada")
                .LeftJustified()
                .Color(Color.Blue)
                .Centered()
            );

            AnsiConsole.Write(GameDisplay.VerticalMenuInit(InitMenu).Centered().Expand());

            AnsiConsole.Write(sagrada);


            ConsoleKeyInfo key = Console.ReadKey();
            InitMenu.ChangeOption(key);
            if (!InitMenu.actionMenu(key))
            {
                break;
            }

        }
        Audio.isPlaying = false;
        audioThread.Join();
    }


}
