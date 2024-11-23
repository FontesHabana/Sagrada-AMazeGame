using System.ComponentModel;
using System.Diagnostics;
using System.Security;
using LogicGame;
using MazeBuilder;
using Spectre.Console;
using Spectre.Console.Rendering;
using UserInterface;

namespace Tiles
{
    class Character : Tile
    {   //Propiedades del jugador
        public string Name { get; set; }
        public int Life { get; set; }
        public int Speed { get; set; }
        public int Attack { get; set; }
        public int Power { get; set; }

        public bool haveFlag { get; set; }


        public Character((int, int) position, Color appearance, string name, int life, int speed, int power, int attack) : base(position, appearance)
        {
            Name = name;
            Life = life;
            Speed = speed;
            Power = power;
            Attack = attack;
            haveFlag = false;
        }

        public void Respawn(Character player)
        {
            player.Life = 10;
            MazeCanvas.RemoveTile(player);
            Maze.mainMaze[player.Position.Item1, player.Position.Item2].Occuped = false;
            Position = GameMaster.position[GameMaster.players.IndexOf(player)];
            Maze.mainMaze[player.Position.Item1, player.Position.Item2].Occuped = true;
            MazeCanvas.AddTile(player);
            haveFlag = false;
            MazeCanvas.AddTile(GameMaster.mainFlag);

        }
        public bool AttackTo(ConsoleKeyInfo keyInput)
        {

            if (keyInput.Key == ConsoleKey.D1)
            {//Esta condición está mal, en los bordes del tablero da error y ataca a traves de las paredes

                if (Position.Item1 != 0 && Position.Item1 != Maze.mainWidth - 1 && Position.Item2 != 0 && Position.Item2 != Maze.mainHeight - 1)
                {
                    if ((Maze.mainMaze[Position.Item1 + 1, Position.Item2].Occuped && !Maze.mainMaze[Position.Item1, Position.Item2].Wall[(int)WallDir.E])
                    || (Maze.mainMaze[Position.Item1 - 1, Position.Item2].Occuped && !Maze.mainMaze[Position.Item1, Position.Item2].Wall[(int)WallDir.W])
                    || (Maze.mainMaze[Position.Item1, Position.Item2 + 1].Occuped && !Maze.mainMaze[Position.Item1, Position.Item2].Wall[(int)WallDir.S])
                    || (Maze.mainMaze[Position.Item1, Position.Item2 - 1].Occuped && !Maze.mainMaze[Position.Item1, Position.Item2].Wall[(int)WallDir.N]))
                    {
                        for (int i = 0; i < GameMaster.players.Count; i++)
                        {
                            if (GameMaster.players[i].Position == (Position.Item1 + 1, Position.Item2)
                            || GameMaster.players[i].Position == (Position.Item1 - 1, Position.Item2)
                            || GameMaster.players[i].Position == (Position.Item1, Position.Item2 + 1)
                            || GameMaster.players[i].Position == (Position.Item1, Position.Item2 - 1))
                            {
                                GameMaster.players[i].Life -= Attack;
                            }
                        }
                    }
                }

                return true;
            }


            return false;
        }

        public bool HaveFlag()
        {


            if (haveFlag)
            {
                GameMaster.mainFlag.Position = Position;
                MazeCanvas.AddTile(GameMaster.mainFlag);
            }
            if (Position == GameMaster.mainFlag.Position)
            {
                haveFlag = true;
                GameMaster.mainFlag.IsCaptured = true;
                MazeCanvas.AddTile(GameMaster.mainFlag);
                return true;
            }

            haveFlag = false;
            GameMaster.mainFlag.IsCaptured = false;
            MazeCanvas.AddTile(GameMaster.mainFlag);
            return false;
        }

    }

}