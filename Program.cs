using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using MazeBuilder;
using Tiles;
using Spectre.Console;
using UserInterface;
using System.Runtime.CompilerServices;
using LogicGame;
using NAudio.Wave;
using System.Text.Json;
using System.Text.Json.Nodes;

class Program
{
    public static Menu InitMenu = new Menu(Menu.InitMenu(), Menu.initaction);

    public static void Main(string[] args)
    {
        AnsiConsole.Background = Color.Black;
        // Iniciar la reproducción de audio en un hilo separado
        Audio.currentFile = Audio.music["history"];
        // Thread audioThread = new Thread(() => Audio.PlayAudio(Audio.currentFile));
        Thread audioThread = new Thread(Audio.PlayAudio);
        audioThread.Start();
        GameDisplay.Start();

        while (true)
        {
            Audio.currentFile = Audio.music["selectionMenu"];
            GameDisplay.mainPage();


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
