using NAudio.Wave;
namespace UserInterface
{
    class Audio
    {
        public static bool isPlaying = true;

        public static void PlayAudio()
        {
            string audioFilePath = "C:/Users/javie/Downloads/Telegram Desktop/Cínikos - Detenido Ante la Ráfaga.mp3";
            // Cambia esto a la ruta de tu archivo
            using (var audioFile = new AudioFileReader(audioFilePath))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();

                while (isPlaying && outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(100); // Espera mientras se reproduce el audio
                }
                outputDevice.Stop(); // Detener el dispositivo de salida si se debe parar
            }
        }


    }
}