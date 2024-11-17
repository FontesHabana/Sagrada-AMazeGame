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

    }
}