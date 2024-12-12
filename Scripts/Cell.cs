using System.Security;
using LogicGame;
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

        public virtual void ApplyEffect()
        {

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
        //Propiedades
        public TrapEffect Effect { get; set; }
        //constructor
        public Trap(int x, int y) : base(x, y)
        {
            Coordenada = (x, y);
            Visited = true;
            //Establece el efecto de la trampa
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
        //Sobrescribe el poder que no aplica una celda para que lo ejecute la trampa
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

        //Genera un nuevo laberinto sobre el cual juegan los personajes
        //Pensar como modificar el código para que no imprima el laberinto en la lógica
        private static void NewMaze()
        {

            //Genera el laberinto
            Maze.MainMaze();

        }
        private static bool Teletransportation()
        {
            Random rnd = new Random();
            int newX;
            int newY;
            //Establece una posición de la x en el borde del lado opuesto del tablero
            if (GameMaster.Player.Position.Item1 < 6)
            {
                newX = rnd.Next(Maze.mainWidth - 4, Maze.mainWidth - 1);
            }
            else
            {
                newX = rnd.Next(1, 4);
            }
            //Establece una posición de la y en el borde del lado opuesto del tablero
            if (GameMaster.Player.Position.Item2 < 6)
            {
                newY = rnd.Next(Maze.mainHeight - 4, Maze.mainHeight - 1);
            }
            else
            {
                newY = rnd.Next(1, 4);
            }
            if (Maze.mainMaze[newX, newY].Occuped == false)
            {
                Maze.mainMaze[GameMaster.Player.Position.Item1, GameMaster.Player.Position.Item2].Occuped = false;
                GameMaster.Player.Position = (newX, newY);
                Maze.mainMaze[newX, newY].Occuped = true;
            }
            else
            {
                Teletransportation();
                return true;
            }

            if (GameMaster.Player.haveFlag)
            {
                GameMaster.mainFlag.Position = GameMaster.Player.Position;

            }
            System.Console.WriteLine("Teletransportation");
            return true;
        }
        private static void Damage()
        {
            Random rnd = new Random();
            GameMaster.Player.Life -= rnd.Next(2, 5);
            if (GameMaster.Player.Life <= 0)
            {
                GameMaster.Player.Respawn(GameMaster.Player);
            }
            //Agregar método que te regresa al inicio
        }
    }

}