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
            MazeCanvas.AddTile(GameMaster.mainFlag);

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
                newX = rnd.Next(Maze.mainWidth - 4, Maze.mainWidth - 1);
            }
            else
            {
                newX = rnd.Next(0, 3);
            }
            //Establece una posición de la y en el borde del lado opuesto del tablero
            if (GameMaster.Player.Position.Item2 < 6)
            {
                newY = rnd.Next(Maze.mainHeight - 4, Maze.mainHeight - 1);
            }
            else
            {
                newY = rnd.Next(0, 3);
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
            if (GameMaster.Player.haveFlag)
            {
                GameMaster.mainFlag.Position = GameMaster.Player.Position;
                MazeCanvas.AddTile(GameMaster.mainFlag);
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

    //Quitar vida

    //Teletransporte

    //Quitar turno

    //Hacer lento
}