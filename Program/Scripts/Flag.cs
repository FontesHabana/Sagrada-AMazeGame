using Spectre.Console;

namespace Tiles
{

    class Flag : Tile
    {
        public bool IsCaptured;
        public Flag((int, int) position, Color appearance) : base(position, appearance)
        {
            Position = position;
            Appearance = appearance;
            IsCaptured = false;
        }

    }

}