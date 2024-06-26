﻿namespace Fire_Emblem {
    public class Perceptive : Skill {
        public int Bonus { get; private set; }

        public Perceptive(string name, string description) : base(name, description) {
            Bonus = 12;
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.currentCombat;

            if (owner == combat._attacker)
            {
                int BonusExtra = owner.Spd / 4;
                owner.AddTemporaryBonus("Spd", Bonus + BonusExtra);
            }
        }
    }
}