using Spectre.Console;

namespace Tiles
{
    class Character : Tile
    {   //Propiedades del jugador
        public string Name { get; set; }
        public int Life { get; set; }
        public int Speed { get; set; }
        public int Attack { get; set; }
        public List<Power> Power { get; set; }


        public Character((int, int) position, Color appearance, string name, int life, int speed, int attack) : base(position, appearance)
        {
            Name = name;
            Life = life;
            Speed = speed;
            Attack = attack;
        }
    }
    class Power
    {
        public Power() { }
    }
}