using System.Security;
using LogicGame;
using Spectre.Console;
using Tiles;
using UserInterface;

namespace MazeBuilder
{
    public class Cell
    {
        public (int, int) Coordenada { get; set; }
        public bool[] Wall = { true, true, true, true };
        public bool Visited { get; set; }
        public bool Occuped { get; set; }

        public bool IsATramp { get; set; }


        //constructor
        public Cell(int x, int y)
        {
            Coordenada = (x, y);
            Visited = false;
            Occuped = false;
            IsATramp = false;
        }
        //Constructor
        public virtual void ApplyEffect()
        {
            // Empty method, intended to be overridden
        }


    }
    public enum TrapEffect
    {
        NewMaze,
        Attack,
        Teletransportation,
    }
    public class Trap : Cell
    {

        public TrapEffect Effect { get; set; }
        //constructor
        public Trap(int x, int y) : base(x, y)
        {
            Coordenada = (x, y);
            Visited = true;
            // Set random trap effect
            Random rnd = new Random();
            int effect = rnd.Next(0, 3);
            switch (effect)
            {
                case 0:
                    Effect = TrapEffect.NewMaze;
                    break;
                case 1:
                    Effect = TrapEffect.Attack;
                    break;
                case 2:
                    Effect = TrapEffect.Teletransportation;
                    break;

                default:
                    break;
            }



        }

        public override void ApplyEffect()
        {
            switch (Effect)
            {
                case TrapEffect.NewMaze:
                    NewMaze();
                    break;
                case TrapEffect.Attack:
                    Damage();
                    break;

                case TrapEffect.Teletransportation:
                    Teletransportation();
                    break;
                default:
                    break;
            }
        }



        private static void NewMaze()
        {

            // Generate new maze logic
            Maze.MainMaze();
            foreach (var item in GameMaster.players)
            {
                Maze.mainMaze[item.Position.Item1, item.Position.Item2].Occuped = true;
            }
            GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["trap"]["newMaze"]).NoBorder());
        }
        private static bool Teletransportation()
        {
            Random rnd = new Random();
            int newX;
            int newY;
            // Determine new position based on current player location
            //x position
            if (GameMaster.Player.Position.Item1 < 6)
            {
                newX = rnd.Next(Maze.mainWidth - 4, Maze.mainWidth - 1);
            }
            else
            {
                newX = rnd.Next(1, 4);
            }
            //y position
            if (GameMaster.Player.Position.Item2 < 6)
            {
                newY = rnd.Next(Maze.mainHeight - 4, Maze.mainHeight - 1);
            }
            else
            {
                newY = rnd.Next(1, 4);
            }

            // Check if new position is occupied
            if (!Maze.mainMaze[newX, newY].Occuped)
            {
                // Move player to new position
                Maze.mainMaze[GameMaster.Player.Position.Item1, GameMaster.Player.Position.Item2].Occuped = false;
                GameMaster.Player.Position = (newX, newY);
                Maze.mainMaze[newX, newY].Occuped = true;
            }
            else
            {
                // If occupied, recursively call Teletransportation
                Teletransportation();
                return true;
            }

            // Update flag position if necessary
            if (GameMaster.Player.haveFlag)
            {
                GameMaster.mainFlag.Position = GameMaster.Player.Position;

            }
            System.Console.WriteLine("Teletransportation");
            GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["trap"]["teletransportation"]).NoBorder());
            return true;
        }
        private static void Damage()
        {
            Random rnd = new Random();
            GameMaster.Player.Life -= rnd.Next(2, 5);
            GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["trap"]["damage"]).NoBorder());
            if (GameMaster.Player.Life <= 0)
            {
                GameMaster.Player.Respawn(GameMaster.Player);
                GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["trap"]["damage"]).NoBorder());
            }


        }
    }

}