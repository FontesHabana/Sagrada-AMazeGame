using System.ComponentModel;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Security;
using LogicGame;
using MazeBuilder;
using Microsoft.VisualBasic;
using NAudio.Wave;
using Spectre.Console;
using Spectre.Console.Rendering;
using UserInterface;

namespace Tiles
{

    #region player
    class Character : Tile
    {
        #region  Properties
        //Propiedades del jugador
        public string Name { get; set; }
        public int Life { get; set; }
        public int MaxLife { get; set; }
        public int Speed { get; set; }
        public int Attack { get; set; }
        public PowerEnum SpecialPower { get; set; }
        public int Power { get; set; }
        public int MaxPower { get; set; }
        public int PowerIncrease { get; set; }
        public bool haveFlag { get; set; }
        public (int, int) InitialPosition { get; set; }


        public CanvasImage Image { get; set; }
        #endregion


        public Character((int, int) position, Color appearance, CanvasImage image, string name, int life, int speed, PowerEnum specialpower, int power, int powerincrease, int attack) : base(position, appearance)
        {
            Name = name;
            MaxLife = life;
            Life = life;
            Speed = speed;
            SpecialPower = specialpower;
            Power = power;
            MaxPower = power;
            PowerIncrease = powerincrease;
            Attack = attack;
            haveFlag = false;
            InitialPosition = position;
            Image = image;


        }

        public void Respawn(Character player)
        {
            player.Life = player.MaxLife;
            player.Power = player.MaxPower;
            Maze.mainMaze[player.Position.Item1, player.Position.Item2].Occuped = false;

            if (Maze.mainMaze[GameMaster.position[GameMaster.players.IndexOf(player)].Item1, GameMaster.position[GameMaster.players.IndexOf(player)].Item2].Occuped)
            {
                foreach (var item in GameMaster.players)
                {
                    if (item.Position == GameMaster.position[GameMaster.players.IndexOf(player)])
                    {
                        Respawn(item);
                    }
                }
            }
            Position = GameMaster.position[GameMaster.players.IndexOf(player)];
            Maze.mainMaze[player.Position.Item1, player.Position.Item2].Occuped = true;
            haveFlag = false;
        }

        public bool AttackTo()
        {
            if (Power >= 2)
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
                                    GameMaster.Player.Power -= 2;
                                }
                            }
                        }
                    }
                }


                return true;
            }
            return false;
        }

        public bool ShowTrap()
        {
            if (Power >= 3)
            {  //Meter esto en un método
                Power -= 3;

                return true;
            }



            return false;
        }

        //Check if a player have a flag
        public bool HaveFlag()
        {

            if (Position == GameMaster.mainFlag.Position)
            {
                haveFlag = true;
                GameMaster.mainFlag.IsCaptured = true;
                GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["gameMaster"]["flag"]).NoBorder());

                return true;
            }
            if (haveFlag)
            {
                GameMaster.mainFlag.Position = Position;
                return true;
            }

            haveFlag = false;
            GameMaster.mainFlag.IsCaptured = false;
            return false;
        }
    }
    #endregion

    #region power
    public enum PowerEnum
    {
        JumpWall,
        IncreaseSpeed,
        IncreaseLife,
        SwitchPlayer,
        DestroyTrap,
        NewTurn,
        CopyPower,

    }
    static class Power
    {   //Detener turno, es poner un contador a caada player
        //Ataque con distancia?


        //Atravesar paredes y trampas
        public static bool JumpWall(ConsoleKeyInfo keyInput)
        {


            if (GameMaster.Player.Power >= 5)
            {
                (int, int)[] direction = { (0, -1), (1, 0), (0, 1), (-1, 0) };
                ConsoleKey[] key = { ConsoleKey.W, ConsoleKey.D, ConsoleKey.S, ConsoleKey.A };
                for (int i = 0; i < 4; i++)
                {
                    if (keyInput.Key == key[i]
                                && GameMaster.Player.Position.Item1 + direction[i].Item1 >= 0 && GameMaster.Player.Position.Item1 + direction[i].Item1 < Maze.mainWidth
                                && GameMaster.Player.Position.Item2 + direction[i].Item2 >= 0 && GameMaster.Player.Position.Item2 + direction[i].Item2 < Maze.mainHeight
                                && Maze.mainMaze[GameMaster.Player.Position.Item1, GameMaster.Player.Position.Item2].Wall[i]
                                && !Maze.mainMaze[GameMaster.Player.Position.Item1 + direction[i].Item1, GameMaster.Player.Position.Item2 + direction[i].Item2].Occuped)
                    {

                        Maze.mainMaze[GameMaster.Player.Position.Item1, GameMaster.Player.Position.Item2].Occuped = false;
                        GameMaster.Player.Position = (GameMaster.Player.Position.Item1 + direction[i].Item1, GameMaster.Player.Position.Item2 + direction[i].Item2);
                        Maze.mainMaze[GameMaster.Player.Position.Item1, GameMaster.Player.Position.Item2].Occuped = true;

                        DecreasePower(5);
                        GameMaster.Player.HaveFlag();
                        Maze.mainMaze[GameMaster.Player.Position.Item1, GameMaster.Player.Position.Item2].ApplyEffect();

                        return true;
                    }

                }

            }

            return false;
        }
        //Aumentar velocidad
        public static bool IncreaseSpeed(int speed)
        {
            if (GameMaster.Player.Power >= 3)
            {
                GameMaster.playerspeed += speed;
                DecreasePower(3);

                return true;
            }
            return false;
        }
        //Aumentar vida FALTAN COSAS POR HACER
        public static bool IncreaseLife(int life)
        {
            if (GameMaster.Player.Power >= 4)
            {
                GameMaster.Player.Life += life;
                DecreasePower(4);

                return true;
            }
            return false;
        }
        //Teletransportación
        public static bool SwitchPlayer(Character player)
        {
            if (GameMaster.Player.Power >= 5)
            {
                (int, int) aux = GameMaster.Player.Position;
                GameMaster.Player.Position = player.Position;
                player.Position = aux;
                if (player.haveFlag)
                {
                    player.HaveFlag();
                }
                if (GameMaster.Player.haveFlag)
                {
                    GameMaster.Player.HaveFlag();
                }



                DecreasePower(5);

                return true;
            }
            return false;
        }
        //Destroy a trap
        public static bool DestroyTrap()
        {

            if (GameMaster.Player.Power >= 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (GameMaster.Player.Position.Item1 + GameMaster.Player.direction[i].Item1 >= 0 && GameMaster.Player.Position.Item1 + GameMaster.Player.direction[i].Item1 < Maze.mainWidth
                     && GameMaster.Player.Position.Item2 + GameMaster.Player.direction[i].Item2 >= 0 && GameMaster.Player.Position.Item2 + GameMaster.Player.direction[i].Item2 < Maze.mainHeight)
                    {
                        if (!Maze.mainMaze[GameMaster.Player.Position.Item1, GameMaster.Player.Position.Item2].Wall[i])
                        {
                            Cell cell = new Cell(GameMaster.Player.Position.Item1 + GameMaster.Player.direction[i].Item1, GameMaster.Player.Position.Item2 + GameMaster.Player.direction[i].Item2);
                            cell.Visited = true;
                            cell.Wall = Maze.mainMaze[GameMaster.Player.Position.Item1 + GameMaster.Player.direction[i].Item1, GameMaster.Player.Position.Item2 + GameMaster.Player.direction[i].Item2].Wall;
                            Maze.mainMaze[GameMaster.Player.Position.Item1 + GameMaster.Player.direction[i].Item1, GameMaster.Player.Position.Item2 + GameMaster.Player.direction[i].Item2] = cell;

                        }
                    }
                }
                DecreasePower(4);
                return true;
            }
            return false;
        }

        public static bool NewTurn()
        {
            if (GameMaster.Player.Power >= 5)
            {
                DecreasePower(5);
                GameMaster.Turn();

                GameMaster.turn--;

                return true;
            }
            return false;
        }

        public static bool CopyPower(Character player, ConsoleKeyInfo key)
        {

            switch (player.SpecialPower)
            {
                case PowerEnum.JumpWall:

                    if (Power.JumpWall(Console.ReadKey()))
                    {
                        GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["jump"]).NoBorder());
                    }
                    else
                    {
                        GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["noPower"] + MyText.text[MyText.language]["menu"]["noWall"]).NoBorder());
                    }
                    break;
                case PowerEnum.IncreaseLife:
                    if (Power.IncreaseLife(3))
                    {
                        GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["increaseLife"]).NoBorder());
                    }
                    else
                    {
                        GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["noPower"]).NoBorder());
                    }
                    break;
                case PowerEnum.IncreaseSpeed:
                    if (Power.IncreaseSpeed(4))
                    {
                        GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["increaseSpeed"]).NoBorder());
                    }
                    else
                    {
                        GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["noPower"]).NoBorder());
                    }
                    break;
                case PowerEnum.SwitchPlayer:
                    if (GameMaster.Player.Power >= 5)
                    {
                        GameMaster.SwitchMenu.actionMenu(key);
                        GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["switch"]).NoBorder());
                    }
                    else
                    {
                        GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["noPower"]).NoBorder());
                    }


                    break;
                case PowerEnum.DestroyTrap:
                    if (Power.DestroyTrap())
                    {
                        GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["destroyTrap"]).NoBorder());
                    }
                    else
                    {
                        GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["noPower"]).NoBorder());
                    }
                    break;
                case PowerEnum.NewTurn:
                    if (Power.NewTurn())
                    {
                        GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["newTurn"]).NoBorder());
                    }
                    else
                    {
                        GameDisplay.layoutGame["bottom"].Update(new Panel(MyText.text[MyText.language]["menu"]["noPower"]).NoBorder());
                    }
                    break;

                default:
                    break;
            }
            return true;
        }
        #endregion
        private static void DecreasePower(int decrease)
        {
            GameMaster.Player.Power -= decrease;
        }
    }
}