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
        public int Effect { get; set; }
        //constructor
        public Trap(int x, int y) : base(x, y)
        {
            Random rand = new Random();
            Effect = rand.Next(0, 3);
            Coordenada = (x, y);
            Visited = true;
            Occuped = false;
        }





    }
}