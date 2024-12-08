using System.ComponentModel;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
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
        public int PowerIncrease { get; set; }
        public bool haveFlag { get; set; }



        public Character((int, int) position, Color appearance, string name, int life, int speed, int power, int powerincrease, int attack) : base(position, appearance)
        {
            Name = name;
            Life = life;
            Speed = speed;
            Power = power;
            PowerIncrease = powerincrease;
            Attack = attack;
            haveFlag = false;

        }

        public void Respawn(Character player)
        {
            player.Life = 10;
            Maze.mainMaze[player.Position.Item1, player.Position.Item2].Occuped = false;
            Position = GameMaster.position[GameMaster.players.IndexOf(player)];
            Maze.mainMaze[player.Position.Item1, player.Position.Item2].Occuped = true;
            haveFlag = false;
        }

        public bool AttackTo()
        {
            if (Power >= 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (Position.Item1 + direction[i].Item1 >= 0 && Position.Item1 + direction[i].Item1 < Maze.mainWidth
                     && Position.Item2 + direction[i].Item2 >= 0 && Position.Item2 + direction[i].Item2 < Maze.mainHeight)
                    {
                        if (!Maze.mainMaze[Position.Item1, Position.Item2].Wall[i] && Maze.mainMaze[Position.Item1 + direction[i].Item1, Position.Item2 + direction[i].Item2].Occuped)
                        {
                            for (int j = 0; j < GameMaster.players.Count; j++)
                            {
                                if (GameMaster.players[j].Position == (Position.Item1 + direction[i].Item1, Position.Item2 + direction[i].Item2))
                                {
                                    GameMaster.players[j].Life -= Attack;
                                }
                            }
                            Power -= 4;
                            if (Power < 0)
                            {
                                Power = 0;
                            }
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool ShowTrap()
        {
            if (Power > 0)
            {
                Power -= 3;
                if (Power < 3)
                {
                    Power = 0;
                }

                return true;
            }



            return false;
        }

        //Check if a player have a flag
        public bool HaveFlag()
        {
            if (haveFlag)
            {
                GameMaster.mainFlag.Position = Position;
            }
            if (Position == GameMaster.mainFlag.Position)
            {
                haveFlag = true;
                GameMaster.mainFlag.IsCaptured = true;
                return true;
            }

            haveFlag = false;
            GameMaster.mainFlag.IsCaptured = false;
            return false;
        }

    }

    static class Power
    {
        //Atravesar paredes
        public static bool JumpWall(ConsoleKeyInfo keyInput, Character player)
        {
            Tile tile = new Tile(player.Position, player.Appearance);
            //Hacia arriba
            if (player.Power > 5)
            {
                if (keyInput.Key == ConsoleKey.W && player.Position.Item2 != 0
                                 && Maze.mainMaze[player.Position.Item1, player.Position.Item2 - 1].Occuped == false)
                {
                    MazeCanvas.RemoveTile(tile);
                    Maze.mainMaze[player.Position.Item1, player.Position.Item2].Occuped = false;
                    player.Position = (player.Position.Item1, player.Position.Item2 - 1);
                    tile.Position = player.Position;
                    Maze.mainMaze[player.Position.Item1, player.Position.Item2].Occuped = true;
                    MazeCanvas.AddTile(tile);
                    player.Power -= 5;
                    return true;
                }
                //Hacia derecha
                if (keyInput.Key == ConsoleKey.D && player.Position.Item1 != (Maze.mainWidth + 1)
                                                          && Maze.mainMaze[player.Position.Item1 + 1, player.Position.Item2].Occuped == false)
                {
                    MazeCanvas.RemoveTile(tile);
                    Maze.mainMaze[player.Position.Item1, player.Position.Item2].Occuped = false;
                    player.Position = (player.Position.Item1 + 1, player.Position.Item2);
                    tile.Position = player.Position;
                    Maze.mainMaze[player.Position.Item1, player.Position.Item2].Occuped = true;
                    MazeCanvas.AddTile(tile);
                    player.Power -= 5;
                    return true;
                }
                //Hacia izquierda
                if (keyInput.Key == ConsoleKey.A && player.Position.Item1 != 0
                                                         && Maze.mainMaze[player.Position.Item1 - 1, player.Position.Item2].Occuped == false)
                {
                    MazeCanvas.RemoveTile(tile);
                    Maze.mainMaze[player.Position.Item1, player.Position.Item2].Occuped = false;
                    player.Position = (player.Position.Item1 - 1, player.Position.Item2);
                    tile.Position = player.Position;
                    Maze.mainMaze[player.Position.Item1, player.Position.Item2].Occuped = true;
                    MazeCanvas.AddTile(tile);
                    player.Power -= 5;
                    return true;
                }
                //Hacia abajo
                if (keyInput.Key == ConsoleKey.S && player.Position.Item2 != (Maze.mainHeight - 1)
                                                         && Maze.mainMaze[player.Position.Item1, player.Position.Item2 + 1].Occuped == false)
                {
                    MazeCanvas.RemoveTile(tile);
                    Maze.mainMaze[player.Position.Item1, player.Position.Item2].Occuped = false;
                    player.Position = (player.Position.Item1, player.Position.Item2 + 1);
                    tile.Position = player.Position;
                    Maze.mainMaze[player.Position.Item1, player.Position.Item2].Occuped = true;
                    MazeCanvas.AddTile(tile);
                    player.Power -= 5;
                    return true;
                }
            }

            return false;
        }
        //Aumentar velocidad
        public static bool IncreaseSpeed(int speed)
        {
            GameMaster.playerspeed += speed;
            return true;
        }
        //Aumentar vida FALTAN COSAS POR HACER
        public static bool IncreaseLife(int life)
        {
            GameMaster.Player.Life += life;
            //Cuando se generen la lista de jugadores que la vida no supere la vida original del jugador
            return true;
        }
        //Teletransportación

        //
    }

}