using System;
using System.Collections.Generic;
using System.Text;

namespace Korelskiy.TacticOfWarDoublie2
{
    class Weapon
    {
        public static Random rnd = new Random();
        public string Name { get; set; }

        public int Id { get; set; }

        public Fraction Fraction { get; set; }

        public int Price { get; set; }

        public int Damage { get; set; } = rnd.Next(45,56);

        public double AccuracyTypeOn100Meters { get; set; }

        public static List<Weapon> Weapons = new List<Weapon>()
        {
            new Weapon("Винтовка Мосина", 1_000, Fraction.USSR, 1, 1),
            new Weapon("Винтовка Маузера", 1_000, Fraction.ThirdRecih, 2, 1)
        };

        public Weapon(string name, int price, Fraction fraction, int id, double acc)
        {
            Name = name;
            Price = price;
            Fraction = fraction;
            AccuracyTypeOn100Meters = acc;
            Id = id;
        }
        public static void GetWeaponForPlayerFraction(Player player, List<Weapon> weapons)
        {
            foreach (Weapon weapon in Weapons)
            {
                if (player.Fraction == weapon.Fraction)
                {
                    weapons.Add(weapon);
                }

            }
        }
    }
}
