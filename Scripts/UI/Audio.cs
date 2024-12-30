using System.ComponentModel;
using Microsoft.VisualBasic;
using NAudio.Wave;
namespace UserInterface
{
    class Audio
    {
        public static bool isPlaying = true;
        public static bool changesong;
        //Determina el archivo de audio que se está reproduciendo
        public static string currentFile;
        public static Dictionary<string, string> music = new Dictionary<string, string>(){
            {"history","Assets/Music/Undertale OST - His Theme(MP3_160K).mp3" },
            {"selectionMenu","Assets/Music/Undertale OST - Home (Music Box)(MP3_160K).mp3"},
            {"game1","Assets/Music/Undertale OST - An Ending(MP3_160K).mp3"},
            {"game2","Assets/Music/Undertale OST - For the Fans(MP3_160K).mp3"},
            {"game3","Assets/Music/Undertale OST - Oh_ One True Love(MP3_160K).mp3"},
            {"victory","Assets/Music/Undertale OST - Menu (Full)(MP3_160K).mp3"}
        };


        public static void PlayAudio()
        {
            // Cambia esto a la ruta de tu archivo
            while (isPlaying)
            {
                changesong = false;
                using (var audioFile = new AudioFileReader(currentFile))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                    string aux = currentFile;

                    while (isPlaying && outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(100); // Espera mientras se reproduce el audio
                        if (aux != currentFile)
                        {
                            break;
                        }
                    }
                    changesong = true;
                    outputDevice.Stop(); // Detener el dispositivo de salida si se debe parar


                }
            }


        }


    }
}