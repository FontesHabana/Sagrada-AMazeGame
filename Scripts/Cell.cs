namespace MazeBuilder
{
    public class Cell
    {
        public (int, int) Coordenada { get; set; }
        public bool[] Wall = { true, true, true, true };
        public bool Visited { get; set; }


        //constructor
        public Cell(int x, int y)
        {
            Coordenada = (x, y);
            Visited = false;
        }

    }
}