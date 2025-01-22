using System.Diagnostics.SymbolStore;

namespace UserInterface
{
    class MyText
    {
        public static string language = "es";
        public static int langIndex = 0;
        public static string[] allLanguage = ["es", "en", "fr"];



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
                         {"noPower","No tienes suficiente poder para realizar esta acción"},


                    //Control
                     {"control","🕹️ Manual de Usuario: La Búsqueda de la Pieza Perdida \n\n ¡Bienvenido a La Búsqueda de la Pieza Perdida! Prepárate para adentrarte en el laberinto de la mente de Antonio Gaudí, donde competirás contra otros jugadores para encontrar la pieza perdida de la Sagrada Familia. Aquí tienes todo lo que necesitas saber para comenzar tu aventura.\n\n🎮 Controles del Juego\nMovimiento\nUsa las siguientes teclas para moverte por el laberinto:\nW: Avanzar hacia adelante ↑\nA: Moverse a la izquierda ←\nS: Retroceder hacia atrás ↓\nD: Moverse a la derecha →\n\nMenús\nPara navegar por los menús, utiliza las flechas:\nFlecha Arriba: ↑ Moverse hacia arriba en el menú\nFlecha Abajo: ↓ Moverse hacia abajo en el menú\nPara seleccionar una opción, presiona:\nEnter: ✅ Aceptar selección\n\n🧩 Objetivo del Juego\nTu misión es encontrar la pieza perdida antes que los demás jugadores. Utiliza tus habilidades y estrategias para superar obstáculos y competir contra tus oponentes.\n\n\n Presiona cualquier tecla para salir"},
                  }    },
                  {"text",new Dictionary<string, string>{
                    {"title", "La Búsqueda de la Pieza Perdida"},
                    {"content","En el corazón de Barcelona, la majestuosa Sagrada Familia se alza como un testimonio eterno de la visión de Antonio Gaudí. Sin embargo, a medida que el sol se oculta tras las torres, un antiguo secreto comienza a despertar. La obra maestra del arquitecto no solo es un símbolo de fe y creatividad, sino también un laberinto de pensamientos y emociones que han quedado atrapados en el tiempo.+Un día, una pieza crucial desaparece del taller de Gaudí: un artefacto que sostiene la armonía de su obra. Sin ella, la Sagrada Familia corre el riesgo de desmoronarse en un caos de formas y colores. Los ecos del pasado resuenan en las paredes del laberinto, y los pensamientos y demonios de Gaudí cobran vida, cada uno con sus propios poderes y deseos.+¿Quién será el primero en encontrar la pieza perdida? ¿Quién logrará salir del laberinto con el legado de Gaudí en sus manos? La aventura comienza ahora, y solo uno podrá reclamar la victoria. ¡Buena suerte!"}
                  }},
                  {"visualMaster",new Dictionary<string, string>(){
                    {"option", "Opciones"},
                    {"menu","MENU"},
                    //Character History
                     {"visionLight","En lo más profundo de la mente de Gaudí, donde las ideas brillan como estrellas, surge Vision of Light. Este ser etéreo representa la creatividad desbordante del arquitecto. Con un solo salto, puede sobrepasar muros y barreras, simbolizando la capacidad de Gaudí para trascender los límites de la arquitectura. Cuando el laberinto se vuelve oscuro y opresivo, Vision of Light ilumina el camino, guiando a sus compañeros hacia nuevas posibilidades.\n Poder especial(-5): Saltar Paredes \n Ataque(-2)\n Mostrar trampa(-3)"},
                         {"creativeWind","A través de los pasillos del laberinto sopla Creative Wind, un ser etéreo que se mueve con la rapidez del viento. Su habilidad para aumentar la velocidad permite a sus compañeros actuar con agilidad e ingenio. Simbolizando la fluidez y dinamismo del diseño arquitectónico de Gaudí, Creative Wind guía a su equipo a través de los desafíos con gracia y destreza, siempre un paso adelante en la búsqueda.\n Poder especial(-3): Aumentar pasos \n Ataque(-2)\n Mostrar trampa(-3)"},
                         {"vitalSoul","En el corazón palpitante del laberinto se encuentra Vital Soul, un ser lleno de energía y esperanza. Con su poder para aumentar la vida de sus aliados, infunde fuerza en aquellos que lo rodean. Representa el amor por la vida que Gaudí plasmó en cada uno de sus diseños. Cuando los jugadores se sienten desalentados, Vital Soul revive su espíritu, recordándoles que siempre hay luz al final del túnel.\n Poder especial(-4): Aumentar vida \n Ataque(-2)\n Mostrar trampa(-3)"},
                         {"ideaMimetist","En las sombras del laberinto habita Idea Mimetist, el maestro del disfraz y la transformación. Con su poder para cambiarse con otro jugador, puede alterar el curso del juego en un instante. Este personaje refleja la dualidad y adaptabilidad presentes en la mente creativa de Gaudí. Cuando es necesario cambiar estrategias o roles, Idea Mimetist se convierte en el aliado perfecto, asegurando que cada jugador pueda brillar en su momento.\n Poder especial(-5): Intercambiarse con otro jugador \n Ataque(-2)\n Mostrar trampa(-3)"},
                         {"naturalBreaker","De las entrañas del diseño arquitectónico nace Natural Breaker, un guerrero implacable que desafía las convenciones. Con su fuerza inquebrantable, puede romper trampas diseñadas para atrapar a los desprevenidos. Este personaje personifica la tenacidad de Gaudí, quien nunca se dejó intimidar por las críticas. Cuando los peligros acechan en el laberinto, Natural Breaker se lanza al frente, desmantelando obstáculos y abriendo el camino hacia la libertad.\n Poder especial(-4): Romper trampas\n Ataque(-2)\n Mostrar trampa(-3)"},
                         {"mirrorTime","En un rincón oculto de la mente de Gaudí reside Mirror of Time, un astuto manipulador del tiempo y la percepción. Este personaje tiene el poder de tomar un turno extra, permitiéndole planificar sus movimientos con precisión. Reflejando la visión holística de Gaudí, Mirror of Time observa el futuro y actúa con sabiduría, asegurándose de que cada paso sea decisivo en su búsqueda por la pieza perdida de la Sagrada Familia.\n Poder especial(-5): Un turno extra\n Ataque(-2)\n Mostrar trampa(-3)"},
                         {"chameleonMind","En lo profundo del laberinto de la mente de Gaudí, donde las ideas fluyen y se transforman, emerge Mente Camaleón. Este ser intrigante representa la adaptabilidad y la versatilidad del arquitecto. Con un simple toque, puede imitar los poderes de cualquier jugador, convirtiéndose en un rival formidable en la búsqueda de la pieza perdida.\n Poder especial(?): Copia un poder de otro jugador\n Ataque(-2)\n Mostrar trampa(-3)"},
                 //Player Information
                    {"information","Información del jugador"},
                    {"life","Vida"},
                    {"speed","Pasos"},
                    {"power","Poder"},
                    {"attack","Ataque"},
                    {"yourturn"," es tu turno"},

                //Victory
                   {"victory","VICTORIA"}
                  }},
                  {"gameMaster",new Dictionary<string, string>(){
                    {"numberPlayers","Cantidad de jugadores"},
                    {"name","Escriba su nombre"},
                    {"nameError","Su nombre es demasiado largo"},
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
            {"noPower", "You don't have enough power to perform this action"},
             //Control
                     {"control","🕹️ User Manual: The Search for the Lost Piece\nWelcome to The Search for the Lost Piece! Prepare to dive into the labyrinth of Antonio Gaudí's mind, where you will compete against other players to find the lost piece of the Sagrada Familia. Here’s everything you need to know to start your adventure.\n\n🎮 Game Controls\nMovement\nUse the following keys to move through the labyrinth:\nW: Move forward ↑\nA: Move left ←\nS: Move backward ↓\nD: Move right →\n\nMenus\nTo navigate through menus, use the arrow keys:\nUp Arrow: ↑ Move up in the menu\nDown Arrow: ↓ Move down in the menu\nTo select an option, press:\nEnter: ✅ Confirm selection\n\n🧩 Game Objective\nYour mission is to find the lost piece before the other players. Use your skills and strategies to overcome obstacles and compete against your opponents.\n\n\nPress any key to exit."},
        }},
        {"text", new Dictionary<string, string>(){
            {"title", "The Search for the Lost Piece"},
            {"content", "In the heart of Barcelona, the majestic Sagrada Familia stands as an eternal testament to Antonio Gaudí's vision. However, as the sun sets behind the towers, an ancient secret begins to awaken. The architect's masterpiece is not only a symbol of faith and creativity but also a labyrinth of thoughts and emotions trapped in time.+One day, a crucial piece disappears from Gaudí's workshop: an artifact that holds the harmony of his work. Without it, the Sagrada Familia risks collapsing into chaos of forms and colors. Echoes of the past resonate within the walls of the labyrinth, and Gaudí's thoughts and demons come to life, each with their own powers and desires.+Who will be the first to find the lost piece? Who will manage to escape the labyrinth with Gaudí's legacy in hand? The adventure begins now, and only one can claim victory. Good luck!"}
        }},
        {"visualMaster", new Dictionary<string, string>(){
            {"option", "Options"},
            {"menu", "MENU"},
            //Character History
             { "visionLight","In the depths of Gaudí's mind, where ideas shine like stars, emerges Vision of Light. This ethereal being represents the overflowing creativity of the architect. With a single leap, it can surpass walls and barriers, symbolizing Gaudí's ability to transcend the limits of architecture. When the labyrinth becomes dark and oppressive, Vision of Light illuminates the path, guiding its companions toward new possibilities.\n Special Power (-5): Jump Walls \n Attack (-2)\n Show Trap (-3)" },
             {"creativeWind","Through the corridors of the labyrinth blows Creative Wind, an ethereal being that moves with the swiftness of the wind. Its ability to increase speed allows its companions to act with agility and ingenuity. Symbolizing the fluidity and dynamism of Gaudí's architectural design, Creative Wind guides its team through challenges with grace and skill, always one step ahead in the quest.\n Special Power (-3): Increase Steps \n Attack (-2)\n Show Trap (-3)" },
             {"vitalSoul","At the beating heart of the labyrinth lies Vital Soul, a being filled with energy and hope. With its power to increase the life of its allies, it infuses strength into those around it. It represents the love for life that Gaudí infused into each of his designs. When players feel discouraged, Vital Soul revives their spirits, reminding them that there is always light at the end of the tunnel.\n Special Power (-4): Increase Life \n Attack (-2)\n Show Trap (-3)" },
             {"ideaMimetist","In the shadows of the labyrinth dwells Idea Mimetist, the master of disguise and transformation. With its power to switch with another player, it can alter the course of the game in an instant. This character reflects the duality and adaptability present in Gaudí's creative mind. When strategies or roles need to change, Idea Mimetist becomes the perfect ally, ensuring that every player can shine in their moment.\n Special Power (-5): Switch with Another Player \n Attack (-2)\n Show Trap (-3)" },
             {"naturalBreaker","From the depths of architectural design arises Natural Breaker, an unyielding warrior who challenges conventions. With its unwavering strength, it can break traps designed to ensnare the unwary. This character embodies Gaudí's tenacity; he was never intimidated by criticism. When dangers lurk in the labyrinth, Natural Breaker charges forward dismantling obstacles and clearing the way to freedom.\n Special Power (-4): Break Traps\n Attack (-2)\n Show Trap (-3)" },
             {"mirrorTime","In a hidden corner of Gaudí's mind resides Mirror of Time, a cunning manipulator of time and perception. This character has the power to take an extra turn allowing it to plan its moves with precision. Reflecting Gaudí's holistic vision, Mirror of Time observes the future and acts wisely ensuring that each step is decisive in its quest for the lost piece of Sagrada Familia.\n Special Power (-5): Extra Turn\n Attack (-2)\n Show Trap (-3)" },
             {"chameleonMind","Deep within the labyrinth of Gaudí's mind, where ideas flow and transform, emerges Chameleon Mind. This intriguing being represents the adaptability and versatility of the architect. With a simple touch, it can imitate the powers of any player, becoming a formidable rival in the quest for the lost piece.\n Special Power (?): Copy a Power from Another Player\n Attack (-2)\n Show Trap (-3)" },
             //Player Information
             {"information","Player Information"},
             {"life","Life"},
             {"speed","Steps"},
             {"power","Power"},
             {"attack","Attack"},
             {"yourturn"," is your turn"},
             //Victory
             {"victory","VICTORY"}
        }},
        {"gameMaster", new Dictionary<string, string>(){
             {"numberPlayers","Number of players"},
             {"name","Enter your name"},
             {"nameError","Your name is too long"},
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
            {"noPower", "Vous n'avez pas assez de pouvoir pour effectuer cette action"},

             //Control
                     {"control","🕹️ Manuel de l'utilisateur : La recherche de la pièce perdue\nBienvenue dans La recherche de la pièce perdue ! Préparez-vous à plonger dans le labyrinthe de l'esprit d'Antonio Gaudí, où vous allez rivaliser avec d'autres joueurs pour trouver la pièce perdue de la Sagrada Familia. Voici tout ce que vous devez savoir pour commencer votre aventure.\n\n🎮 Contrôles du jeu\nMouvement\nUtilisez les touches suivantes pour vous déplacer dans le labyrinthe :\nW : Avancer ↑\nA : Aller à gauche ←\nS : Reculer ↓\nD : Aller à droite →\n\nMenus\nPour naviguer dans les menus, utilisez les flèches :\nFlèche Haut : ↑ Aller vers le haut dans le menu\nFlèche Bas : ↓ Aller vers le bas dans le menu\nPour sélectionner une option, appuyez sur :\nEntrée : ✅ Confirmer la sélection\n\n🧩 Objectif du jeu\nVotre mission est de trouver la pièce perdue avant les autres joueurs. Utilisez vos compétences et vos stratégies pour surmonter les obstacles et rivaliser avec vos adversaires. \n\n\nAppuyez sur n'importe quelle touche pour quitter."},
        }},
        {"text", new Dictionary<string, string>(){
            {"title", "La Quête de la Pièce Perdue"},
            {"content", "Au cœur de Barcelone, la majestueuse Sagrada Familia se dresse comme un témoignage éternel de la vision d'Antonio Gaudí. Cependant, alors que le soleil se couche derrière les tours, un ancien secret commence à s'éveiller. Le chef-d'œuvre de l'architecte n'est pas seulement un symbole de foi et de créativité, mais aussi un labyrinthe de pensées et d'émotions piégées dans le temps.+Un jour, une pièce cruciale disparaît de l'atelier de Gaudí : un artefact qui maintient l'harmonie de son œuvre. Sans elle, la Sagrada Familia risque de s'effondrer dans un chaos de formes et de couleurs. Les échos du passé résonnent dans les murs du labyrinthe, et les pensées et démons de Gaudí prennent vie, chacun avec ses propres pouvoirs et désirs.+Qui sera le premier à trouver la pièce perdue ? Qui réussira à sortir du labyrinthe avec l'héritage de Gaudí en main ? L'aventure commence maintenant, et seul un pourra revendiquer la victoire. Bonne chance !"}
        }},
        {"visualMaster", new Dictionary<string, string>(){
            {"option", "Options"},
            {"menu", "MENU"},
            //Character History
           { "visionLight", "Danes profondeurs de l'esprit de Gaudí, où les idées brillent comme des étoiles, émerge Vision of Light. Cet être éthéré représente la créativité débordante de l'architecte. D'un seul saut, il peut franchir des murs et des barrières, symbolisant la capacité de Gaudí à transcender les limites de l'architecture. Lorsque le labyrinthe devient sombre et oppressant, Vision of Light éclaire le chemin, guidant ses compagnons vers de nouvelles possibilités.\n Pouvoir spécial (-5) : Sauter les murs \n Attaque (-2)\n Montrer le piège (-3)"},
              {"creativeWind","À travers les couloirs du labyrinthe souffle Creative Wind, un être éthéré qui se déplace avec la rapidité du vent. Sa capacité à augmenter la vitesse permet à ses compagnons d'agir avec agilité et ingéniosité. Symbolisant la fluidité et le dynamisme du design architectural de Gaudí, Creative Wind guide son équipe à travers les défis avec grâce et habileté, toujours une étape en avance dans la quête.\n Pouvoir spécial (-3) : Augmenter les pas \n Attaque (-2)\n Montrer le piège (-3)"},
           {"vitalSoul","Au cœur palpitant du labyrinthe se trouve Vital Soul, un être rempli d'énergie et d'espoir. Avec son pouvoir d'augmenter la vie de ses alliés, il insuffle de la force à ceux qui l'entourent. Il représente l'amour pour la vie que Gaudí a inscrit dans chacun de ses designs. Lorsque les joueurs se sentent découragés, Vital Soul ravive leur esprit en leur rappelant qu'il y a toujours une lumière au bout du tunnel.\n Pouvoir spécial (-4) : Augmenter la vie \n Attaque (-2)\n Montrer le piège (-3)"},
           {"ideaMimetist","Dans les ombres du labyrinthe habite Idea Mimetist, le maître du déguisement et de la transformation. Avec son pouvoir d'échanger avec un autre joueur, il peut modifier le cours du jeu en un instant. Ce personnage reflète la dualité et l'adaptabilité présentes dans l'esprit créatif de Gaudí. Lorsqu'il est nécessaire de changer de stratégie ou de rôle, Idea Mimetist devient l'allié parfait, garantissant que chaque joueur puisse briller à son tour.\n Pouvoir spécial (-5) : Échanger avec un autre joueur \n Attaque (-2)\n Montrer le piège (-3)"},
           {"naturalBreaker","Des entrailles du design architectural naît Natural Breaker , un guerrier implacable qui défie les conventions . Avec sa force inébranlable , il peut briser des pièges conçus pour attraper les imprudents . Ce personnage incarne la ténacité de Gaudí ; il n'a jamais été intimidé par les critiques . Lorsque les dangers rôdent dans le labyrinthe , Natural Breaker s'élance en avant pour démonter les obstacles et ouvrir le chemin vers la liberté.\n Pouvoir spécial (-4) : Briser les pièges\n Attaque (-2)\n Montrer le piège (-3)"},
           {"mirrorTime","Dans un coin caché de l'esprit de Gaudí réside Mirror of Time , un manipulateur astucieux du temps et de la perception . Ce personnage a le pouvoir de prendre un tour supplémentaire lui permettant de planifier ses mouvements avec précision . Reflétant la vision holistique de Gaudí , Mirror of Time observe l'avenir et agit avec sagesse , veillant à ce que chaque pas soit décisif dans sa quête pour retrouver  la pièce perdue de Sagrada Familia.\n Pouvoir spécial (-5) : Tour supplémentaire\n Attaque (-2)\n Montrer le piège (-3)"},
           {"chameleonMind","Dans les profondeurs du labyrinthe de l'esprit de Gaudí , où les idées coulent et se transforment , émerge Esprit Caméléon . Cet être intrigant représente l'adaptabilité et polyvalence  de l'architecte . D'un simple toucher , il peut imiter les pouvoirs d'un autre joueur , devenant ainsi un rival redoutable dans la quête pour retrouver  la pièce perdue .\n Pouvoir spécial (?): Copier un pouvoir d'un autre joueur\n Attaque(-2)\n Montrer le piège(-3)"},
     //Player Information
             {"information","Informations sur le joueur"},
             {"life","Vie"},
             {"speed","Pas"},
             {"power","Pouvoir"},
             {"attack","Attaque"},
             {"yourturn"," c'est ton tour  "},
             //Victory
             {"victory","VICTOIRE"}
        }},
        {"gameMaster", new Dictionary<string, string>(){
             {"numberPlayers","Nombre de joueurs"},
             {"name","Entrez votre nom"},
             {"nameError","Votre nom est trés grand"},
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