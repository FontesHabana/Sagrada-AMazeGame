namespace UserInterface
{
    class LiveText
    {
        static string mainStoryTitle = "The Search for the Lost Piece";
        static string mainStory = "In the heart of Barcelona, the majestic Sagrada Familia stands as an eternal testament to the vision of Antonio Gaudí. However, as the sun sets behind its towers, an ancient secret begins to awaken. The architect's masterpiece is not only a symbol of faith and creativity but also a labyrinth of thoughts and emotions trapped in time. +One day, a crucial piece disappears from Gaudí's workshop: an artifact that holds the harmony of his work. Without it, the Sagrada Familia risks collapsing into chaos of forms and colors. Echoes of the past resonate within the walls of the labyrinth, and Gaudí's thoughts and demons come to life, each with their own powers and desires. +You and your fellow players have been summoned as guardians of Gaudí's legacy. But in this labyrinth, there are no allies; each of you seeks the lost piece to claim it as your own. Equipped with unique abilities that reflect the deeper aspects of Gaudí's creative mind, you must use your wit and cunning to outsmart one another.+As you venture into winding corridors and hidden rooms, each player will face ingenious traps and challenges designed to test their ingenuity and strategy. The powers you possess will allow you to jump over walls, break traps, increase your speed, or even swap places with other competitors. +But beware: in this game, everyone is a rival. Every decision counts, and each move may be the key to advancing or falling into another player's trap. The search is not only physical; it is also a mental battle where you must confront your own fears and doubts while trying to outpace your opponents. +Who will be the first to find the lost piece? Who will emerge from the labyrinth with Gaudí's legacy in hand? The adventure begins now, and only one can claim victory. Good luck! ";

        //Crear un diccionario con los textos en inglés y en español








        public static void PrintHitory()
        {
            foreach (var x in mainStoryTitle)
            {
                System.Console.Write(x);
                Thread.Sleep(100);
            }
            Thread.Sleep(100);
            System.Console.WriteLine();
            foreach (char x in mainStory)
            {
                if (x == '+')
                {
                    System.Console.WriteLine();
                }
                else
                {
                    System.Console.Write(x);
                    Thread.Sleep(25);
                }

            }


        }


    }

}