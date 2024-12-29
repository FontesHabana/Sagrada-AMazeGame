using System.ComponentModel;
using Microsoft.VisualBasic;
using NAudio.Wave;
namespace UserInterface
{
    class Audio
    {
        public static bool isPlaying = true;
        //Determina el archivo de audio que se está reproduciendo
        public static string currentFile;
        public static Dictionary<string, string> music = new Dictionary<string, string>(){
            {"presentation","Assets/Music/José_María_Vitier_Tema_del_Festival.mp3" },
            {"selectionMenu","Assets/Music/José María Vitier - Fresa y Chocolate.mp3"}
        };


        public static void PlayAudio()
        {
            // Cambia esto a la ruta de tu archivo
            while (isPlaying)
            {
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
                    outputDevice.Stop(); // Detener el dispositivo de salida si se debe parar


                }
            }


        }


    }
}