using System;
using System.Collections.Generic;
using System.Text;

namespace Korelskiy.TacticOfWarDoublie2
{
    class Player
    {
        public string NickName { get; set; }
        public int Id { get; set; }

        public bool DoneTurn { get; set; } = false;
        public int Money { get; set; } = 10_000;

        public Fraction Fraction { get; set; } = Fraction.Null;

        public List<Soldier> Team { get; set; } = new List<Soldier>();

    }
}
