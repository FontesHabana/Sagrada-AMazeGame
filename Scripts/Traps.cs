using System.Security;
using LogicGame;
using Tiles;
using UserInterface;
namespace MazeBuilder
{
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
                    System.Console.WriteLine("Attack");
                    break;

                case TrapEffect.Teletransportation:
                    Teletransportation();
                    break;
                default:
                    break;
            }
        }

        //Genera un nuevo laberinto sobre el cual juegan los personajes
        private static void NewMaze()
        {

            //Genera el laberinto
            Maze.MainMaze();
            //Imprime el laberinto
            MazeCanvas.PrintMaze();
            for (int i = 0; i < GameMaster.playeramount; i++)
            {
                Maze.mainMaze[GameMaster.players[i].Position.Item1, GameMaster.players[i].Position.Item2].Occuped = true;
                MazeCanvas.AddTile(GameMaster.players[i]);
            }
            System.Console.WriteLine("NewMaze");
        }
        private static bool Teletransportation()
        {
            Random rnd = new Random();
            int newX;
            int newY;
            MazeCanvas.RemoveTile(GameMaster.Player);
            //Establece una posición de la x en el borde del lado opuesto del tablero
            if (GameMaster.Player.Position.Item1 < 6)
            {
                newX = rnd.Next(Maze.mainWidth - 4, Maze.mainWidth);
            }
            else
            {
                newX = rnd.Next(0, 4);
            }
            //Establece una posición de la y en el borde del lado opuesto del tablero
            if (GameMaster.Player.Position.Item2 < 6)
            {
                newY = rnd.Next(Maze.mainHeight - 4, Maze.mainHeight);
            }
            else
            {
                newY = rnd.Next(0, 4);
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
            MazeCanvas.AddTile(GameMaster.Player);
            System.Console.WriteLine("Teletransportation");
            return true;
        }

    }

    //Quitar vida

    //Teletransporte

    //Quitar turno

    //Hacer lento
}