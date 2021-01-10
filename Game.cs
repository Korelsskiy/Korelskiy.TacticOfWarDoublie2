using Korelskiy.TacticOfWar;
using System;
using System.Collections.Generic;

namespace Korelskiy.TacticOfWarDoublie2
{
    enum Direction
    {
        Left,
        Right,
        Forward,
        Back
    }
    enum Terrain
    {
        Field,
        Forest,
        Rise,
        Top,
        Swamp,
        Farm
    }

    enum Weather
    {
        ClearSky,
        Rainy,
        Foggy,
        NoWeather
    }

    enum Spawn
    {
        USSR,
        ThirdReich,
        No
    }

    class Game
    {
        public static bool hitBool;
        public static Random rnd = new Random();

        public static List<Player> PlayersInGame = new List<Player>();

        public static bool turnDone = true;
        public static int Turn { get; set; } = 1;

        public static Player goesNow = new Player();
        public static Player notGo = new Player();
        public static void DoTurn(Player thisPlayer, Player enemyPlayer)
        {
            Console.Clear();


            Console.WriteLine($"Ход: {Turn} Ходит: {thisPlayer.NickName}");
            foreach (Soldier soldier in thisPlayer.Team)
            {
                Console.WriteLine($"Cолдат {soldier.Name}({soldier.Location.X}:{soldier.Location.Y}) ждет приказов:");

                while (soldier.Points > 0)
                {
                    Console.WriteLine($"Очков ОД осталось: {soldier.Points}");
                    Cell.DrawMap();
                    Console.WriteLine("1.Повернуть бойца");
                    Console.WriteLine("2.Передвинуть бойца");
                    Console.WriteLine("3.Закончить ход");
                    Console.WriteLine("4.Выйти в меню");
                    string str = Console.ReadLine();
                    if (str == "1")
                    {
                        Console.WriteLine("1. Лево");
                        Console.WriteLine("2. Право");
                        Console.WriteLine("3. Вперед");
                        Console.WriteLine("4. Назад");
                        string str1 = Console.ReadLine();

                        if (str1 == "1")
                        {
                            if (soldier.Direction == Direction.Left)
                            {
                                Console.WriteLine("Вы уже туда повернуты");
                                Console.ReadLine();
                                DoTurn(thisPlayer, enemyPlayer);
                            }

                            else
                            {
                                SoldierTurn(enemyPlayer, soldier, Direction.Left);
                            }
                        }

                        else if (str1 == "2")
                        {
                            if (soldier.Direction == Direction.Right)
                            {
                                Console.WriteLine("Вы уже туда повернуты");
                                Console.ReadLine();
                                DoTurn(thisPlayer, enemyPlayer);
                            }

                            else
                            {
                                SoldierTurn(enemyPlayer, soldier, Direction.Right);
                            }
                        }

                        else if (str1 == "3")
                        {
                            if (soldier.Direction == Direction.Forward)
                            {
                                Console.WriteLine("Вы уже туда повернуты");
                                Console.ReadLine();
                                DoTurn(thisPlayer, enemyPlayer);
                            }

                            else
                            {
                                SoldierTurn(enemyPlayer, soldier, Direction.Forward);
                            }
                        }

                        else if (str1 == "4")
                        {
                            if (soldier.Direction == Direction.Back)
                            {
                                Console.WriteLine("Вы уже туда повернуты");
                                Console.ReadLine();
                                DoTurn(thisPlayer, enemyPlayer);
                            }

                            else
                            {
                                SoldierTurn(enemyPlayer, soldier, Direction.Back);
                            }
                        }

                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Такой команды нет!");
                            Console.ReadLine();
                            DoTurn(thisPlayer, enemyPlayer);
                        }
                    }
                    else if (str == "2")
                    {
                        Console.WriteLine("1. Лево");
                        Console.WriteLine("2. Право");
                        Console.WriteLine("3. Вперед");
                        Console.WriteLine("4. Назад");
                        string str1 = Console.ReadLine();
                        if (str1 == "1")
                            SoldierGo(enemyPlayer, soldier, Direction.Left);
                        if (str1 == "2")
                            SoldierGo(enemyPlayer, soldier, Direction.Right);
                        if (str1 == "3")
                            SoldierGo(enemyPlayer, soldier, Direction.Forward);
                        if (str1 == "4")
                            SoldierGo(enemyPlayer, soldier, Direction.Back);
                    }
                    else if (str == "3")
                    {
                        Console.Clear();
                        Console.WriteLine("Ход сдан");
                        Console.ReadLine();
                        DoTurn(enemyPlayer, thisPlayer);
                    }
                    else if (str == "4")
                    {
                        Console.Clear();
                        Menu.MenuShow(thisPlayer);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Такой команды нет!");
                        Console.ReadLine();
                        DoTurn(thisPlayer, enemyPlayer);
                    }

                }
            }

            Console.Clear();
            Console.WriteLine($"Ход игрока {thisPlayer.NickName} завершен.");

            Console.ReadLine();
            DoTurn(enemyPlayer, thisPlayer);
        }

        public static void SoldierGo(Player enemyPlayer, Soldier soldier, Direction direction)
        {
            if (direction == Direction.Left)
            {
                Console.Clear();

                if ((soldier.Location.GoLeft == true) && (soldier.Direction == Direction.Left))
                {
                    soldier.Points -= 2;
                    List<Cell> xCells = Cell.Cells.FindAll(item => item.X == soldier.Location.X - 1);
                    soldier.Location = xCells.Find(item => item.Y == soldier.Location.Y);
                    Console.WriteLine($"{soldier.Name}:Я в квадрате {soldier.Location.X};{soldier.Location.Y} {Cell.GetTerrain(soldier.Location)}");
                    FindCellsInFieldOfView(enemyPlayer, soldier, soldier.Direction);
                }
                else if (soldier.Location.GoLeft != true)
                {
                    Console.WriteLine($"{soldier.Name}: в данном направлении прохода нет, командир!");
                }
                else if (soldier.Direction != Direction.Left)
                {
                    Console.WriteLine($"{soldier.Name}: сначала нужно туда развернуться, командир!");
                }
            }

            if (direction == Direction.Right)
            {
                Console.Clear();

                if ((soldier.Location.GoRight == true) && (soldier.Direction == Direction.Right))
                {
                    soldier.Points -= 2;
                    List<Cell> xCells = Cell.Cells.FindAll(item => item.X == soldier.Location.X + 1);
                    soldier.Location = xCells.Find(item => item.Y == soldier.Location.Y);
                    Console.WriteLine($"{soldier.Name}:Я в квадрате {soldier.Location.X};{soldier.Location.Y} {Cell.GetTerrain(soldier.Location)}");
                    FindCellsInFieldOfView(enemyPlayer, soldier, soldier.Direction);
                }
                else if (soldier.Location.GoRight != true)
                {
                    Console.WriteLine($"{soldier.Name}: в данном направлении прохода нет, командир!");
                }
                else if (soldier.Direction != Direction.Right)
                {
                    Console.WriteLine($"{soldier.Name}: сначала нужно туда развернуться, командир!");
                }
            }

            if (direction == Direction.Forward)
            {
                Console.Clear();

                if ((soldier.Location.GoUp == true) && (soldier.Direction == Direction.Forward))
                {
                    soldier.Points -= 2;
                    List<Cell> xCells = Cell.Cells.FindAll(item => item.X == soldier.Location.X);
                    soldier.Location = xCells.Find(item => item.Y == soldier.Location.Y + 1);
                    Console.WriteLine($"{soldier.Name}:Я в квадрате {soldier.Location.X};{soldier.Location.Y} {Cell.GetTerrain(soldier.Location)}");
                    FindCellsInFieldOfView(enemyPlayer, soldier, soldier.Direction);
                }
                else if (soldier.Location.GoUp != true)
                {
                    Console.WriteLine($"{soldier.Name}: в данном направлении прохода нет, командир!");
                }
                else if (soldier.Direction != Direction.Forward)
                {
                    Console.WriteLine($"{soldier.Name}: сначала нужно туда развернуться, командир!");
                }
            }

            if (direction == Direction.Back)
            {
                Console.Clear();

                if ((soldier.Location.GoDown == true) && (soldier.Direction == Direction.Back))
                {
                    soldier.Points -= 2;
                    List<Cell> xCells = Cell.Cells.FindAll(item => item.X == soldier.Location.X);
                    soldier.Location = xCells.Find(item => item.Y == soldier.Location.Y - 1);
                    Console.WriteLine($"{soldier.Name}:Я в квадрате {soldier.Location.X};{soldier.Location.Y} {Cell.GetTerrain(soldier.Location)}");
                    FindCellsInFieldOfView(enemyPlayer, soldier, soldier.Direction);
                }
                else if (soldier.Location.GoDown != true)
                {
                    Console.WriteLine($"{soldier.Name}: в данном направлении прохода нет, командир!");
                }
                else if (soldier.Direction != Direction.Back)
                {
                    Console.WriteLine($"{soldier.Name}: сначала нужно туда развернуться, командир!");
                }
            }
        }
        public static void SoldierTurn(Player enemyPlayer, Soldier soldier, Direction direction)
        {
            Console.Clear();
            soldier.Direction = direction;
            soldier.Points -= 1;

            if (soldier.Direction == Direction.Left)
                Console.WriteLine($"{soldier.Name}({soldier.Location.X}:{soldier.Location.Y}): смотрю налево");
            if (soldier.Direction == Direction.Right)
                Console.WriteLine($"{soldier.Name}({soldier.Location.X}:{soldier.Location.Y}): смотрю направо");
            if (soldier.Direction == Direction.Forward)
                Console.WriteLine($"{soldier.Name}({soldier.Location.X}:{soldier.Location.Y}): смотрю вперед");
            if (soldier.Direction == Direction.Back)
                Console.WriteLine($"{soldier.Name}({soldier.Location.X}:{soldier.Location.Y}): смотрю назад");

            FindCellsInFieldOfView(enemyPlayer, soldier, soldier.Direction);
        }

        public static Cell FindCellsInFieldOfView(Player enemyPlayer, Soldier soldier, Direction dir)
        {
            List<Cell> nextcells = new List<Cell>();
            Cell cell = new Cell();

            if (dir == Direction.Right)
            {
                for(int i = 1; i<soldier.FieldOfView+1; i++)
                {
                    nextcells = Cell.Cells.FindAll(item => item.Y == soldier.Location.Y);
                    cell = nextcells.Find(item => item.X == soldier.Location.X + i);
                    if (cell != null)
                    {
                        Console.WriteLine($"{soldier.Name}({soldier.Location.X}:{soldier.Location.Y}): ищу противника в: {cell.X};{cell.Y}");
                        FindEnemyInCell(enemyPlayer, cell, soldier);
                    }
                    else
                    {
                        Console.WriteLine($"{soldier.Name}({soldier.Location.X}:{soldier.Location.Y}): дальше граница карты");
                    }
                }
                
            }

            if (dir == Direction.Back)
            {
                for (int i = 1; i < soldier.FieldOfView+1; i++)
                {
                    nextcells = Cell.Cells.FindAll(item => item.Y == soldier.Location.Y-i);
                    cell = nextcells.Find(item => item.X == soldier.Location.X + i);
                    if (cell != null)
                    {
                        Console.WriteLine($"{soldier.Name}({soldier.Location.X}:{soldier.Location.Y}): ищу противника в: {cell.X};{cell.Y}");
                        FindEnemyInCell(enemyPlayer, cell, soldier);
                    }
                    else
                    {
                        Console.WriteLine($"{soldier.Name}({soldier.Location.X}:{soldier.Location.Y}): дальше граница карты");
                    }
                }

            }

            if (dir == Direction.Forward)
            {

                for (int i = 1; i < soldier.FieldOfView+1; i++)
                {
                    nextcells = Cell.Cells.FindAll(item => item.Y == soldier.Location.Y+i);
                    cell = nextcells.Find(item => item.X == soldier.Location.X);
                    if (cell != null)
                    {
                        Console.WriteLine($"{soldier.Name}({soldier.Location.X}:{soldier.Location.Y}): ищу противника в: {cell.X};{cell.Y}");
                        FindEnemyInCell(enemyPlayer, cell, soldier);
                    }
                    else
                    {
                        Console.WriteLine($"{soldier.Name}({soldier.Location.X}:{soldier.Location.Y}): дальше граница карты");
                    }
                }

            }

            if (dir == Direction.Left)
            {
                for (int i = 1; i < soldier.FieldOfView+1; i++)
                {
                    nextcells = Cell.Cells.FindAll(item => item.Y == soldier.Location.Y);
                    cell = nextcells.Find(item => item.X == soldier.Location.X - i);
                    if (cell != null)
                    {
                        Console.WriteLine($"{soldier.Name}({soldier.Location.X}:{soldier.Location.Y}): ищу противника в: {cell.X};{cell.Y}");
                        FindEnemyInCell(enemyPlayer, cell, soldier);
                    }
                    else
                    {
                        Console.WriteLine($"{soldier.Name}({soldier.Location.X}:{soldier.Location.Y}): дальше граница карты");
                    }
                }
            }
            return cell;

        }

        public static void FindEnemyInCell(Player enemyPlayer, Cell cell, Soldier sl)
        {
           
            List<Soldier> soldiers = new List<Soldier>();

            soldiers = enemyPlayer.Team.FindAll(soldier => soldier.Location.Id == cell.Id);

            if (soldiers.Count > 0)
            {
                foreach (Soldier soldier in soldiers)
                {
                    Cell.DrawMap();
                    Console.WriteLine($"{sl.Name}({sl.Location.X}:{sl.Location.Y}): засек противника: {soldier.Name}({soldier.Location.X}:{soldier.Location.Y})");
                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                    {
                        sl.Target = soldier;
                        Console.WriteLine($"{sl.Name}({sl.Location.X}:{sl.Location.Y}): могу открыть огонь по: {soldier.Name}({soldier.Location.X}:{soldier.Location.Y})\nНажмите ");
                        string str = Console.ReadLine();

                        if (str == "Shoot")
                        {
                            double accuracy = CalculateAccuracy(sl, sl.Location.Terrain, RandomWeather(), CalculateRange(sl, soldier));
                            Console.WriteLine($"Вероятность попадания составила: {Math.Round(accuracy, 3)}%");
                            bool isHit = IsHit(accuracy);
                            if (isHit == true)
                            {
                                Console.WriteLine($"Попадание! Нанесено урона {sl.Weapon.Damage}");
                                soldier.Health -= sl.Weapon.Damage;
                                if (soldier.Health <= 0)
                                EndGame(sl.Name, soldier.Name);
                            }
                            else if (isHit == false)
                            {
                                Console.WriteLine("Промах.");
                            }
                            Console.ReadLine();
                        }


                    }


                }

            }
            else
            {
                Console.WriteLine($"{sl.Name}: противника в данном квадрате не обнаружил");
            }

        }

        public static bool IsHit(double accuracy)
        {
            int hit = rnd.Next(0, 100);
            Console.WriteLine($"hit: {hit}");
            

            if (hit > accuracy)
            {
                hitBool = false;
            }
            else if (hit < accuracy)
            {
                hitBool = true;
            }

            return hitBool;
               
        }
        public static Weather RandomWeather()
        {
            int weatherNum = rnd.Next(1, 11);
            Weather weather = Weather.NoWeather;

            if(weatherNum < 6)
            {
                weather = Weather.ClearSky;
            }
            if (weatherNum > 5 && weatherNum < 9)
            {
                weather = Weather.Rainy;
            }
            if(weatherNum > 8 && weatherNum < 11)
            {
                weather = Weather.Foggy;
            }

            return weather;
        }
        public static double CalculateRange(Soldier shooter, Soldier victim)
        {
            

            double range = 0;

            if(shooter.Direction == Direction.Back || shooter.Direction == Direction.Forward)
            {
              range = (Math.Abs(shooter.Location.Y - victim.Location.Y) * 10 + rnd.Next(0,10)) ;
            }

            if (shooter.Direction == Direction.Left || shooter.Direction == Direction.Right)
            {
               range = ( Math.Abs(shooter.Location.X - victim.Location.X)*10 + rnd.Next(0,10));
            }

            
            Console.WriteLine($"range: {range}");

            return range;
        }

        public static double CalculateAccuracy(Soldier soldier, Terrain terrain, Weather weather, double range)
        {
            double cof;
            double weatherCof = 0;
            double terrainCof = 0;

            if (weather == Weather.ClearSky)
                weatherCof = 0.9;

            if (weather == Weather.Foggy)
                weatherCof = 0.25;

            if (weather == Weather.Rainy)
                weatherCof = 0.75;


            if (terrain == Terrain.Field || terrain == Terrain.Rise || terrain == Terrain.Top)
                terrainCof = 0.9;

            if (terrain == Terrain.Swamp)
                terrainCof = 0.75;

            if (terrain == Terrain.Forest || terrain == Terrain.Farm)
                terrainCof = 0.25;

            cof = 1 * weatherCof * terrainCof / range * soldier.Expirience * soldier.Weapon.AccuracyTypeOn100Meters * 100;

            return cof;
        }
        public static void EndGame(string winner, string loser)
        {
            Console.Clear();
            Console.WriteLine($"Игрок {winner} победил игрока {loser}");
            Console.ReadLine();
            Environment.Exit(1);
        }
        public static void StartGame(List<Player> players)
        {
            Console.Clear();
            PlayersInGame.Clear();

            GetFirstPlayer(players);
            Cell.DrawMap();

            foreach (Player player in PlayersInGame)
            {
                SpawnSoldiers(player);
            }

            SoldiersSettings(PlayersInGame);
            Console.ReadLine();

            foreach (Player player in PlayersInGame)
            {
                if (player.DoneTurn != true)
                {
                    DoTurn(player, PlayersInGame.Find(mister => mister.Id != player.Id));
                    player.DoneTurn = true;
                }
            }



            EndGame("Победителя нет", "No winner");
        }

        public static void GetFirstPlayer(List<Player> players)
        {
            Random random = new Random();
            Player firstplayer = players[random.Next(0, players.Count)];
            PlayersInGame.Add(firstplayer);
            foreach (Player player in players)
            {
                if ((PlayersInGame.Find(pl => pl.Fraction != player.Fraction) != null))
                {
                    PlayersInGame.Add(player);
                    break;
                }
            }

            Console.WriteLine($"Игрок {PlayersInGame[0].NickName} ходит первым, игрок {PlayersInGame[1].NickName} ходит вторым");
            Console.ReadLine();
        }

        public static void SpawnSoldiers(Player player)
        {
            foreach (Soldier soldier in player.Team)
            {
                if (player.Fraction == Fraction.USSR)
                {
                    soldier.Location = Cell.Cells[22];
                    soldier.Direction = Direction.Right;
                    Console.WriteLine($"БОТ {soldier.Name} игрока {player.NickName} появился в точке ({soldier.Location.X};{soldier.Location.Y})");
                }
                else
                {
                    soldier.Location = Cell.Cells[0];
                    soldier.Direction = Direction.Left;
                    Console.WriteLine($"БОТ {soldier.Name} игрока {player.NickName} появился в точке ({soldier.Location.X};{soldier.Location.Y})");
                }

            }
        }

        public static void SoldiersSettings(List<Player> players)
        {
            foreach(Player player in players)
            {
                foreach(Soldier soldier in player.Team)
                {
                    if(soldier.Role == Role.Commander)
                    {
                        soldier.FieldOfView = 2;
                    }
                }
            }
        }
    }
}
