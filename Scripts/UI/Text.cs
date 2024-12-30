using System.Diagnostics.SymbolStore;

namespace UserInterface
{
    class MyText
    {
        public static string language = "es";
        public static int langIndex = 0;
        public static string[] allLanguage = ["es", "en", "fr"];
        static string mainStoryTitle = "The Search for the Lost Piece";
        static string mainStory = "In the heart of Barcelona, the majestic Sagrada Familia stands as an eternal testament to the vision of Antonio Gaudí. However, as the sun sets behind its towers, an ancient secret begins to awaken. The architect's masterpiece is not only a symbol of faith and creativity but also a labyrinth of thoughts and emotions trapped in time. +One day, a crucial piece disappears from Gaudí's workshop: an artifact that holds the harmony of his work. Without it, the Sagrada Familia risks collapsing into chaos of forms and colors. Echoes of the past resonate within the walls of the labyrinth, and Gaudí's thoughts and demons come to life, each with their own powers and desires. +You and your fellow players have been summoned as guardians of Gaudí's legacy. But in this labyrinth, there are no allies; each of you seeks the lost piece to claim it as your own. Equipped with unique abilities that reflect the deeper aspects of Gaudí's creative mind, you must use your wit and cunning to outsmart one another.+As you venture into winding corridors and hidden rooms, each player will face ingenious traps and challenges designed to test their ingenuity and strategy. The powers you possess will allow you to jump over walls, break traps, increase your speed, or even swap places with other competitors. +But beware: in this game, everyone is a rival. Every decision counts, and each move may be the key to advancing or falling into another player's trap. The search is not only physical; it is also a mental battle where you must confront your own fears and doubts while trying to outpace your opponents. +Who will be the first to find the lost piece? Who will emerge from the labyrinth with Gaudí's legacy in hand? The adventure begins now, and only one can claim victory. Good luck! ";

        //Crear un diccionario con los textos en inglés y en español
        //Estructura del diccionario
        //Lenguage
        //Archive
        public static Dictionary<string, Dictionary<string, Dictionary<string, string>>> text = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>(){
            {"es",new Dictionary<string, Dictionary<string, string>>(){
                  {"trap",new Dictionary<string, string>(){
                         {"newMaze","El laberinto ha cambiado"},
                         {"teletransportation","Este no es mi sitio. ¿Dónde estoy?"},
                            {"damage","Auch, eso me dolió"}
                  }},
                  {"menu",new Dictionary<string, string>(){
                    //InitMenu
                         {"newGame","Nuevo Juego"},
                         {"instruction","Controles"},
                         {"language","Idioma(Español)"},
                         {"history","Historia"},
                         {"exit","Salir"},
                    //Character List
                         {"visionLight","Visión de Luz"},
                         {"creativeWind","Viento Creativo"},
                         {"vitalSoul","Alma Vital"},
                         {"ideaMimetist","Mimetista de Ideas"},
                         {"naturalBreaker","Ruptura Natural"},
                         {"mirrorTime","Espejo del Tiempo"},
                         {"chameleonMind","Mente Camaleón"},
                    //Game Menu
                         {"attack","Atacar"},
                         {"showTrap","Mostrar Trampas"},
                         {"specialPower","Poder Especial"},
                         {"next","Siguiente turno"},



                    //MenuAction
                         {"kill","Mataste a un jugador"},
                         {"show","Hay un montón de trampas"},

                    //Powers
                         {"jump","Ese muro no era tan alto"},
                         {"increaseLife","Me siento mucho mejor"},
                         {"increaseSpeed","Ahora puedo caminar mucho más"},
                         {"switch","Me gusta el cambio"},
                         {"destroyTrap","He destruido una trampa"},
                         {"newTurn","Tengo un nuevo turno"},
                         {"noWall"," o no es una pared "},
                         {"noPower","No tienes suficiente poder para realizar esta acción"}
                  }    },
                  {"text",new Dictionary<string, string>{
                    {"title", "La Búsqueda de la Pieza Perdida"},
                    {"content","En el corazón de Barcelona, la majestuosa Sagrada Familia se alza como un testimonio eterno de la visión de Antonio Gaudí. Sin embargo, a medida que el sol se oculta tras las torres, un antiguo secreto comienza a despertar. La obra maestra del arquitecto no solo es un símbolo de fe y creatividad, sino también un laberinto de pensamientos y emociones que han quedado atrapados en el tiempo.+Un día, una pieza crucial desaparece del taller de Gaudí: un artefacto que sostiene la armonía de su obra. Sin ella, la Sagrada Familia corre el riesgo de desmoronarse en un caos de formas y colores. Los ecos del pasado resuenan en las paredes del laberinto, y los pensamientos y demonios de Gaudí cobran vida, cada uno con sus propios poderes y deseos.+¿Quién será el primero en encontrar la pieza perdida? ¿Quién logrará salir del laberinto con el legado de Gaudí en sus manos? La aventura comienza ahora, y solo uno podrá reclamar la victoria. ¡Buena suerte!"}
                  }},
                  {"visualMaster",new Dictionary<string, string>(){
                    {"option", "Opciones"},
                    {"menu","MENU"},
                    //Character History
                     {"visionLight","En lo más profundo de la mente de Gaudí, donde las ideas brillan como estrellas, surge Vision of Light. Este ser etéreo representa la creatividad desbordante del arquitecto. Con un solo salto, puede sobrepasar muros y barreras, simbolizando la capacidad de Gaudí para trascender los límites de la arquitectura. Cuando el laberinto se vuelve oscuro y opresivo, Vision of Light ilumina el camino, guiando a sus compañeros hacia nuevas posibilidades."},
                         {"creativeWind","A través de los pasillos del laberinto sopla Creative Wind, un ser etéreo que se mueve con la rapidez del viento. Su habilidad para aumentar la velocidad permite a sus compañeros actuar con agilidad e ingenio. Simbolizando la fluidez y dinamismo del diseño arquitectónico de Gaudí, Creative Wind guía a su equipo a través de los desafíos con gracia y destreza, siempre un paso adelante en la búsqueda."},
                         {"vitalSoul","En el corazón palpitante del laberinto se encuentra Vital Soul, un ser lleno de energía y esperanza. Con su poder para aumentar la vida de sus aliados, infunde fuerza en aquellos que lo rodean. Representa el amor por la vida que Gaudí plasmó en cada uno de sus diseños. Cuando los jugadores se sienten desalentados, Vital Soul revive su espíritu, recordándoles que siempre hay luz al final del túnel."},
                         {"ideaMimetist","En las sombras del laberinto habita Idea Mimetist, el maestro del disfraz y la transformación. Con su poder para cambiarse con otro jugador, puede alterar el curso del juego en un instante. Este personaje refleja la dualidad y adaptabilidad presentes en la mente creativa de Gaudí. Cuando es necesario cambiar estrategias o roles, Idea Mimetist se convierte en el aliado perfecto, asegurando que cada jugador pueda brillar en su momento."},
                         {"naturalBreaker","De las entrañas del diseño arquitectónico nace Natural Breaker, un guerrero implacable que desafía las convenciones. Con su fuerza inquebrantable, puede romper trampas diseñadas para atrapar a los desprevenidos. Este personaje personifica la tenacidad de Gaudí, quien nunca se dejó intimidar por las críticas. Cuando los peligros acechan en el laberinto, Natural Breaker se lanza al frente, desmantelando obstáculos y abriendo el camino hacia la libertad."},
                         {"mirrorTime","En un rincón oculto de la mente de Gaudí reside Mirror of Time, un astuto manipulador del tiempo y la percepción. Este personaje tiene el poder de tomar un turno extra, permitiéndole planificar sus movimientos con precisión. Reflejando la visión holística de Gaudí, Mirror of Time observa el futuro y actúa con sabiduría, asegurándose de que cada paso sea decisivo en su búsqueda por la pieza perdida de la Sagrada Familia."},
                         {"chameleonMind","En lo profundo del laberinto de la mente de Gaudí, donde las ideas fluyen y se transforman, emerge Mente Camaleón. Este ser intrigante representa la adaptabilidad y la versatilidad del arquitecto. Con un simple toque, puede imitar los poderes de cualquier jugador, convirtiéndose en un rival formidable en la búsqueda de la pieza perdida."},
                 //Player Information
                    {"information","Información del jugador"},
                    {"life","Vida"},
                    {"speed","Pasos"},
                    {"power","Poder"},
                    {"attack","Ataque"},

                //Victory
                   {"victory","VICTORIA"}
                  }},
                  {"gameMaster",new Dictionary<string, string>(){
                    {"numberPlayers","Cantidad de jugadores"},
                    {"name","Escriba su nombre"},
                    {"character","Seleccione su personaje"},
                    {"flag","Ahora tienes la bandera"},
                  }}
            }},
            {"en", new Dictionary<string, Dictionary<string, string>>(){
        {"trap", new Dictionary<string, string>(){
            {"newMaze", "The maze has changed"},
            {"teletransportation", "This is not my place. Where am I?"},
            {"damage", "Ouch, that hurt"}
        }},
        {"menu", new Dictionary<string, string>(){
            //InitMenu
            {"newGame", "New Game"},
            {"instruction", "Controls"},
            {"language", "Language (English)"},
            {"history", "History"},
            {"exit", "Exit"},
            //Character List
            {"visionLight", "Vision of Light"},
            {"creativeWind", "Creative Wind"},
            {"vitalSoul", "Vital Soul"},
            {"ideaMimetist", "Idea Mimic"},
            {"naturalBreaker", "Natural Breaker"},
            {"mirrorTime", "Mirror of Time"},
            {"chameleonMind","Chamaleon Mind"},
            //Game Menu
            {"attack", "Attack"},
            {"showTrap", "Show Traps"},
            {"specialPower", "Special Power"},
            {"next", "Next turn"},
            //MenuAction
            {"kill", "You killed a player"},
            {"show", "There are a lot of traps"},
            //Powers
            {"jump", "That wall wasn't so high"},
            {"increaseLife", "I feel much better"},
            {"increaseSpeed", "Now I can walk much faster"},
            {"switch", "I like the change"},
            {"destroyTrap", "I destroyed a trap"},
            {"newTurn", "I have a new turn"},
            {"noWall", " or it's not a wall "},
            {"noPower", "You don't have enough power to perform this action"}
        }},
        {"text", new Dictionary<string, string>(){
            {"title", "The Search for the Lost Piece"},
            {"content", "In the heart of Barcelona, the majestic Sagrada Familia stands as an eternal testament to Antonio Gaudí's vision. However, as the sun sets behind the towers, an ancient secret begins to awaken. The architect's masterpiece is not only a symbol of faith and creativity but also a labyrinth of thoughts and emotions trapped in time.+One day, a crucial piece disappears from Gaudí's workshop: an artifact that holds the harmony of his work. Without it, the Sagrada Familia risks collapsing into chaos of forms and colors. Echoes of the past resonate within the walls of the labyrinth, and Gaudí's thoughts and demons come to life, each with their own powers and desires.+Who will be the first to find the lost piece? Who will manage to escape the labyrinth with Gaudí's legacy in hand? The adventure begins now, and only one can claim victory. Good luck!"}
        }},
        {"visualMaster", new Dictionary<string, string>(){
            {"option", "Options"},
            {"menu", "MENU"},
            //Character History
            {"visionLight","Deep within Gaudí's mind, where ideas shine like stars, Vision of Light emerges. This ethereal being represents the architect's overflowing creativity. With a single leap, it can surpass walls and barriers, symbolizing Gaudí's ability to transcend architectural limits. When the labyrinth becomes dark and oppressive, Vision of Light illuminates the path, guiding its companions toward new possibilities."},
            {"creativeWind","Through the corridors of the labyrinth blows Creative Wind, an ethereal being that moves with the swiftness of the wind. Its ability to increase speed allows its companions to act with agility and ingenuity. Symbolizing the fluidity and dynamism of Gaudí's architectural design, Creative Wind guides its team through challenges with grace and skill, always one step ahead in the quest."},
            {"vitalSoul","At the pulsating heart of the labyrinth lies Vital Soul, a being full of energy and hope. With its power to increase its allies' life, it infuses strength in those around it. It represents Gaudí's love for life that he imbued in each of his designs. When players feel discouraged, Vital Soul revives their spirit, reminding them that there is always light at the end of the tunnel."},
            {"ideaMimetist","In the shadows of the labyrinth dwells Idea Mimic, master of disguise and transformation. With its power to switch places with another player, it can alter the course of the game in an instant. This character reflects the duality and adaptability present in Gaudí's creative mind. When strategies or roles need to change, Idea Mimic becomes the perfect ally, ensuring that every player can shine in their moment."},
            {"naturalBreaker","From the depths of architectural design emerges Natural Breaker, an unyielding warrior who defies conventions. With its unwavering strength, it can break traps designed to ensnare the unwary. This character embodies Gaudí's tenacity, who was never intimidated by criticism. When dangers lurk in the labyrinth, Natural Breaker charges forward, dismantling obstacles and opening pathways to freedom."},
            {"mirrorTime","In a hidden corner of Gaudí's mind resides Mirror of Time, a cunning manipulator of time and perception. This character has the power to take an extra turn, allowing it to plan its moves with precision. Reflecting Gaudí's holistic vision, Mirror of Time observes the future and acts wisely, ensuring that each step is decisive in its quest for the lost piece of the Sagrada Familia."},
            {"chameleonMind","Deep within the labyrinth of Gaudí's mind, where ideas flow and transform, emerges Chameleon Mind. This intriguing being represents the adaptability and versatility of the architect. With a simple touch, it can imitate the powers of any player, becoming a formidable rival in the quest for the lost piece."},
             //Player Information
             {"information","Player Information"},
             {"life","Life"},
             {"speed","Steps"},
             {"power","Power"},
             {"attack","Attack"},
             //Victory
             {"victory","VICTORY"}
        }},
        {"gameMaster", new Dictionary<string, string>(){
             {"numberPlayers","Number of players"},
             {"name","Enter your name"},
             {"character","Select your character"},
             {"flag","You now have the flag"}
        }}
    }
},

          { "fr", new Dictionary<string, Dictionary<string, string>>(){
        {"trap", new Dictionary<string, string>(){
            {"newMaze", "Le labyrinthe a changé"},
            {"teletransportation", "Ce n'est pas mon endroit. Où suis-je ?"},
            {"damage", "Aïe, ça m'a fait mal"}
        }},
        {"menu", new Dictionary<string, string>(){
            //InitMenu
            {"newGame", "Nouveau Jeu"},
            {"instruction", "Contrôles"},
            {"language", "Langue (Français)"},
            {"history", "Histoire"},
            {"exit", "Sortir"},
            //Character List
            {"visionLight", "Vision de Lumière"},
            {"creativeWind", "Vent Créatif"},
            {"vitalSoul", "Âme Vitale"},
            {"ideaMimetist", "Mimétiste d'Idées"},
            {"naturalBreaker", "Briseur Naturel"},
            {"mirrorTime", "Miroir du Temps"},
            {"chameleonMind","Esprit Caméléon"},
            //Game Menu
            {"attack", "Attaquer"},
            {"showTrap", "Montrer les Pièges"},
            {"specialPower", "Pouvoir Spécial"},
            {"next", "Prochain tour"},
            //MenuAction
            {"kill", "Vous avez tué un joueur"},
            {"show", "Il y a beaucoup de pièges"},
            //Powers
            {"jump", "Ce mur n'était pas si haut"},
            {"increaseLife", "Je me sens beaucoup mieux"},
            {"increaseSpeed", "Maintenant je peux marcher beaucoup plus vite"},
            {"switch", "J'aime le changement"},
            {"destroyTrap", "J'ai détruit un piège"},
            {"newTurn", "J'ai un nouveau tour"},
            {"noWall", " ou ce n'est pas un mur "},
            {"noPower", "Vous n'avez pas assez de pouvoir pour effectuer cette action"}
        }},
        {"text", new Dictionary<string, string>(){
            {"title", "La Quête de la Pièce Perdue"},
            {"content", "Au cœur de Barcelone, la majestueuse Sagrada Familia se dresse comme un témoignage éternel de la vision d'Antonio Gaudí. Cependant, alors que le soleil se couche derrière les tours, un ancien secret commence à s'éveiller. Le chef-d'œuvre de l'architecte n'est pas seulement un symbole de foi et de créativité, mais aussi un labyrinthe de pensées et d'émotions piégées dans le temps.+Un jour, une pièce cruciale disparaît de l'atelier de Gaudí : un artefact qui maintient l'harmonie de son œuvre. Sans elle, la Sagrada Familia risque de s'effondrer dans un chaos de formes et de couleurs. Les échos du passé résonnent dans les murs du labyrinthe, et les pensées et démons de Gaudí prennent vie, chacun avec ses propres pouvoirs et désirs.+Qui sera le premier à trouver la pièce perdue ? Qui réussira à sortir du labyrinthe avec l'héritage de Gaudí en main ? L'aventure commence maintenant, et seul un pourra revendiquer la victoire. Bonne chance !"}
        }},
        {"visualMaster", new Dictionary<string, string>(){
            {"option", "Options"},
            {"menu", "MENU"},
            //Character History
            {"visionLight","Au plus profond de l'esprit de Gaudí, où les idées brillent comme des étoiles, émerge Vision de Lumière. Cet être éthéré représente la créativité débordante de l'architecte. D'un seul saut, il peut dépasser murs et barrières, symbolisant la capacité de Gaudí à transcender les limites de l'architecture. Lorsque le labyrinthe devient sombre et oppressant, Vision de Lumière éclaire le chemin, guidant ses compagnons vers de nouvelles possibilités."},
            {"creativeWind","À travers les couloirs du labyrinthe souffle Vent Créatif, un être éthéré qui se déplace avec la rapidité du vent. Sa capacité à augmenter la vitesse permet à ses compagnons d'agir avec agilité et ingéniosité. Symbolisant la fluidité et le dynamisme du design architectural de Gaudí, Vent Créatif guide son équipe à travers les défis avec grâce et habileté, toujours une longueur d'avance dans la quête."},
            {"vitalSoul","Au cœur palpitant du labyrinthe se trouve Âme Vitale, un être plein d'énergie et d'espoir. Avec son pouvoir d'augmenter la vie de ses alliés, il infuse force à ceux qui l'entourent. Il représente l'amour pour la vie que Gaudí a inscrit dans chacun de ses designs. Lorsque les joueurs se sentent découragés, Âme Vitale ravive leur esprit, leur rappelant qu'il y a toujours une lumière au bout du tunnel."},
            {"ideaMimetist","Dans les ombres du labyrinthe réside Mimétiste d'Idées, maître du déguisement et de la transformation. Avec son pouvoir d'échanger sa place avec un autre joueur, il peut altérer le cours du jeu en un instant. Ce personnage reflète la dualité et l'adaptabilité présentes dans l'esprit créatif de Gaudí. Lorsque des stratégies ou des rôles doivent changer, Mimétiste d'Idées devient l'allié parfait, s'assurant que chaque joueur puisse briller en son moment."},
            {"naturalBreaker","Des entrailles du design architectural naît Briseur Naturel, un guerrier inflexible qui défie les conventions. Avec sa force inébranlable, il peut briser des pièges conçus pour attraper les imprudents. Ce personnage incarne la ténacité de Gaudí, qui ne s'est jamais laissé intimider par les critiques. Lorsque des dangers rôdent dans le labyrinthe, Briseur Naturel se lance en avant, démontant les obstacles et ouvrant le chemin vers la liberté."},
            {"mirrorTime","Dans un coin caché de l'esprit de Gaudí réside Miroir du Temps, un manipulateur astucieux du temps et de la perception. Ce personnage a le pouvoir de prendre un tour supplémentaire, lui permettant de planifier ses mouvements avec précision. Reflétant la vision holistique de Gaudí, Miroir du Temps observe l'avenir et agit avec sagesse, s'assurant que chaque pas soit décisif dans sa quête pour la pièce perdue de la Sagrada Familia."},
             {"chameleonMind","Au cœur du labyrinthe de l'esprit de Gaudí, où les idées coulent et se transforment, émerge Esprit Caméléon. Cet être intrigant représente l'adaptabilité et la polyvalence de l'architecte. D'un simple toucher, il peut imiter les pouvoirs de n'importe quel joueur, devenant un rival redoutable dans la quête de la pièce perdue."},
             //Player Information
             {"information","Informations sur le joueur"},
             {"life","Vie"},
             {"speed","Pas"},
             {"power","Pouvoir"},
             {"attack","Attaque"},
             //Victory
             {"victory","VICTOIRE"}
        }},
        {"gameMaster", new Dictionary<string, string>(){
             {"numberPlayers","Nombre de joueurs"},
             {"name","Entrez votre nom"},
             {"character","Sélectionnez votre personnage"},
             {"flag","Vous avez maintenant le drapeau"}
        }}
    }
}

 };












        public static void PrintHitory()
        {
            foreach (var x in text[language]["text"]["title"])
            {
                System.Console.Write(x);
                Thread.Sleep(100);
            }
            Thread.Sleep(100);
            System.Console.WriteLine();
            foreach (char x in text[language]["text"]["content"])
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