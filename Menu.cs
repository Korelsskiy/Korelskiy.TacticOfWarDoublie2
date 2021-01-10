using System;
using System.Collections.Generic;

namespace Korelskiy.TacticOfWarDoublie2
{
    enum Gameplay
    {
        Bots,
        Players
    }

    enum Fraction
    {
        USSR,
        ThirdRecih,
        Null
    }
    class Menu
    {
        private static bool operation = false;
        public static Gameplay gameplay;

        public static List<Player> PlayersinMenu = new List<Player>()
        {
             new Player() { NickName = "Player1", Id = 1 },
             new Player() { NickName = "Player2", Id = 2 }
        };
        
        static void Main(string[] args)
        {

            MenuShow(PlayersinMenu[0]);
        }

        public static void MenuShow(Player player)
        {
            Console.Clear();
            Console.WriteLine("******************МЕНЮ*************************");
            Console.WriteLine($"*****************{player.NickName}*************************");
            Console.WriteLine("1. Выбрать режим игры");
            Console.WriteLine("2. Изменить никнейм");
            Console.WriteLine("3. Выбрать сторону");
            Console.WriteLine("4. Набрать бойцов");
            Console.WriteLine("5. Снарядить бойцов");
            Console.WriteLine("6. Начать игру");
            Console.WriteLine("7. Сменить игрока");
            Console.WriteLine("8. Посмотреть информацию об игроке");
            Console.WriteLine("9. Автозакупка");

            string menuChoose = Console.ReadLine();

            if (menuChoose == "1")
            {
                GameplayChoose(player);
            }

            if (menuChoose == "2")
            {
                NickNameChoose(player);
            }

            if (menuChoose == "3")
            {
                FractionChoose(player);
            }

            if (menuChoose == "4")
            {
                if (player.Fraction != Fraction.Null)
                {
                    SetTeam(player);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Вы не выбрали сторону конфликта!!!!");
                    Console.ReadLine();
                    MenuShow(player);
                }
            }


            if (menuChoose == "5")
            {
                if (player.Team.Count > 0)
                {
                    SetGuns(player);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Вы не наняли бойцов!!!!");
                    Console.ReadLine();
                    MenuShow(player);
                }
            }


            if (menuChoose == "6")
            {
                foreach(Player player1 in PlayersinMenu)
                {
                    if(player1.Fraction == Fraction.Null)
                    {
                        Console.Clear();
                        Console.WriteLine($"Игрок {player1.NickName} не выбрал сторону, начать игру нельзя.");
                        Console.ReadLine();
                        MenuShow(player);
                    }    
                    
                    if(player1.Team.Count == 0)
                    {
                        Console.Clear();
                        Console.WriteLine($"Игрок {player1.NickName} не нанял бойцов.");
                        Console.ReadLine();
                        MenuShow(player);
                    }

                    foreach(Soldier soldier in player1.Team)
                    {
                        if(soldier.Weapon == null)
                        {
                            Console.Clear();
                            Console.WriteLine($"Боец {soldier.Name} у игрока {player1.NickName} не имеет оружия");
                            Console.ReadLine();
                            MenuShow(player);
                        }
                    }
                }

                if(PlayersinMenu[0].Fraction == PlayersinMenu[1].Fraction)
                    {
                    Console.Clear();
                    Console.WriteLine("Игроки выбрали одинаковые стороны конфликта. Выберите разные.");
                    Console.ReadLine();
                    MenuShow(player);
                }
                Game.StartGame(PlayersinMenu);
            }

            if (menuChoose == "7")
            {
                if (player.Id == 1)
                {
                    MenuShow(PlayersinMenu[1]);
                }
                if (player.Id == 2)
                {
                    MenuShow(PlayersinMenu[0]);
                }
            }

            if (menuChoose == "8")
            {
                GetInfo(player);
            }

            if (menuChoose == "9")
            {
                AutoOperation(PlayersinMenu);
            }

            else
            {
                Console.Clear();
                Console.WriteLine("Такой команды нет!");
                Console.ReadLine();
                MenuShow(player);
            }



        }

        public static void AutoOperation(List<Player> players)
        {
            Console.Clear();
            
            if (!operation)
            {
                operation = true;

                players[0].Fraction = Fraction.USSR;
                players[1].Fraction = Fraction.ThirdRecih;

                players[0].Team.Add(Soldier.Soldiers[0]);
                players[1].Team.Add(Soldier.Soldiers[1]);

                players[0].Team[0].Weapon = Weapon.Weapons[0];
                players[1].Team[0].Weapon = Weapon.Weapons[1];

                players[0].Money -= 1_000;
                players[1].Money -= 1_000;

                Console.WriteLine("Автозакупка произведена успешно");
                Console.ReadLine();
                MenuShow(players[0]);
            }
            else
            {
                Console.WriteLine("Автозакупка уже проведена");
                Console.ReadLine();
                MenuShow(players[0]);
            }
            
        }
        public static void GetInfo(Player player)
        {
            Console.Clear();
            Console.WriteLine("******************СВОДКА*************************");
            Console.WriteLine($"Ник: {player.NickName}:");
            Console.WriteLine($"Деньги: {player.Money}:");
            if (player.Fraction == Fraction.Null)
            {
                Console.WriteLine($"Фракция: не установлена");
            }

            else if (player.Fraction == Fraction.ThirdRecih)
            {
                Console.WriteLine($"Фракция: Третий Рейх");
            }

            else
            {
                Console.WriteLine($"Фракция: СССР");
            }


            Console.WriteLine($"Человек в отряде: {player.Team.Count}:");
            foreach (Soldier soldier in player.Team)
            {
                Console.Write($"{soldier.Name} - ");
                if (soldier.Role == Role.Commander)
                {
                    Console.Write("командир отделения, ");
                }
                if (soldier.Weapon != null)
                {
                    Console.WriteLine($"вооружен {soldier.Weapon.Name}");
                }
                else
                {
                    Console.WriteLine($"оружия не имеет");
                }
            }
            Console.ReadLine();
            MenuShow(player);
        }
        public static void GameplayChoose(Player player)
        {
            Console.Clear();
            Console.WriteLine("1.Игра с ботами\n2.Игра с другом");
            string str = Console.ReadLine();
            if (str == "1")
            {
                gameplay = Gameplay.Bots;
            }
            else if (str == "2")
            {
                gameplay = Gameplay.Players;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Такой команды нет!");
                Console.ReadLine();
                GameplayChoose(player);
            }

            MenuShow(player);
        }

        public static void NickNameChoose(Player player)
        {
            Console.Clear();
            Console.WriteLine("Введите ник:");
            string nick = Console.ReadLine();
            if(nick.Length > 15)
            {
                Console.Clear();
                Console.WriteLine("Слишком длинный ник(не более 15 символов)");
                Console.ReadLine();
                NickNameChoose(player);
            }
            player.NickName = nick;
            MenuShow(player);
        }

        private static void GetStandart(Player player)
        {
            foreach (Soldier soldier in player.Team)
            {
                soldier.Weapon = null;
            }
            player.Money = 10_000;
            player.Team.Clear();
        }
        public static void FractionChoose(Player player)
        {
            Console.Clear();

            Console.WriteLine("1.CCCР\n2.Третий Рейх");
            string frChoose = Console.ReadLine();
            if (frChoose == "1")
            {
                player.Fraction = Fraction.USSR;
                GetStandart(player);
            }
                
            else if (frChoose == "2")
            {
                player.Fraction = Fraction.ThirdRecih;
                GetStandart(player);
            }
                
            else
            {
                Console.Clear();
                Console.WriteLine("Такой команды нет!");
                Console.ReadLine();
                FractionChoose(player);
            }
            MenuShow(player);
        }

        public static void SetTeam(Player player)
        {
            Console.Clear();

            Console.WriteLine("Вы можете нанять следующих бойцов:");

            List<Soldier> soldiersForTake = new List<Soldier>();
            Soldier.SortSoldiersByFraction(player, soldiersForTake);

            foreach (Soldier soldier in soldiersForTake)
            {
                Console.WriteLine($"{soldier.Name}");
            }
            try
            {
                int slChoose = int.Parse(Console.ReadLine());

                if (player.Team.Find(sl => sl.Id == soldiersForTake[slChoose - 1].Id) == null)
                {
                    player.Team.Add(soldiersForTake[slChoose - 1]);
                    Console.WriteLine($"Боец {soldiersForTake[slChoose - 1].Name} нанят");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Этот товарищ уже в вашем отряде!");
                    Console.ReadLine();
                    MenuShow(player);
                }
            }

            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("Такого бойца нет!");
                Console.ReadLine();
                SetTeam(player);
            }

            Console.ReadLine();

            MenuShow(player);
        }

        public static void SetGuns(Player player)
        {
            Console.Clear();

            Console.WriteLine("Какому бойцу вы хотите купить оружие:");

            foreach (Soldier soldier in player.Team)
            {
                Console.WriteLine($"{soldier.Name}:");
            }

            Soldier sl = new Soldier();

            try
            {
                int num = int.Parse(Console.ReadLine());

                sl = player.Team[num - 1];

                Console.WriteLine($"Вы можете купить следующее оружие бойцу {sl.Name}");
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("Такого бойца нет!");
                Console.ReadLine();
                SetGuns(player);
            }

            List<Weapon> weaponsForBuy = new List<Weapon>();
            Weapon.GetWeaponForPlayerFraction(player, weaponsForBuy);

            foreach (Weapon weapon in weaponsForBuy)
            {
                Console.WriteLine($"{weapon.Name}");
            }

            try
            {
                int weaponNum = int.Parse(Console.ReadLine());

                if(sl.Weapon == weaponsForBuy[weaponNum - 1])
                {
                    Console.Clear();
                    Console.WriteLine($"У солдата {sl.Name} уже есть оружие {sl.Weapon.Name}");
                    Console.ReadLine();
                    MenuShow(player);
                }

                sl.Weapon = weaponsForBuy[weaponNum - 1];

                if(player.Money < sl.Weapon.Price)
                {
                    Console.Clear();
                    Console.WriteLine($"У игрока {player.NickName} недостаточно средств, нехватает {sl.Weapon.Price - player.Money}");
                    Console.ReadLine();
                    MenuShow(player);
                }
                player.Money -= sl.Weapon.Price;
                Console.WriteLine($"Бойцу {sl.Name} выдано оружие {weaponsForBuy[weaponNum - 1].Name}");
                Console.WriteLine($"Остаток на вашем счете: {player.Money}");
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("Такого оружия нет!");
                Console.ReadLine();
                SetGuns(player);
            }


            Console.ReadLine();

            MenuShow(player);

        }
    }
}
