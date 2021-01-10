using Korelskiy.TacticOfWar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Korelskiy.TacticOfWarDoublie2
{
    enum Role
    {
        Commander,
        MG,
        MGAssistent,
        AmmoCarier,
        Fighter,
        PPGunner,
        SecondCommander
    }
    class Soldier
    {
        public string Name { get; set; }

        public Fraction Fraction { get; set; }

        public int Id { get; set; }

        public int Points { get; set; } = 10;
        public Weapon Weapon { get; set; }

        public Cell Location{ get; set; }

        public Direction Direction { get; set; }

        public Soldier Target { get; set; }

        public int Health { get; set; } = 100;

        public double Expirience { get; set; }

        public  Role Role { get; set; }

        public int FieldOfView { get; set; } = 1;
        public bool IsKilled { get; set; } = false;

        public Soldier()
        {

        }

        public static List<Soldier> Soldiers = new List<Soldier>()
        {
            new Soldier("Васян", Fraction.USSR, 1, 0.32) { Role = Role.Commander},
            new Soldier("Фриц", Fraction.ThirdRecih, 2, 0.71) {Role = Role.Commander}
        };

        public Soldier(string name, Fraction fraction, int id, double exp)
        {
            Name = name;

            Fraction = fraction;

            Id = id;

            Expirience = exp;
        }

        public static void SortSoldiersByFraction(Player player, List<Soldier> soldiers)
        {
            foreach (Soldier soldier in Soldiers)
            {
                if(player.Fraction == soldier.Fraction)
                {
                    soldiers.Add(soldier);
                }
                
            }
        }
    }
}
