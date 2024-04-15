﻿namespace Fire_Emblem {
    public class BrazenSpdDef : Skill {
        public int Bonus { get; private set; }

        public BrazenSpdDef(string name, string description) : base(name, description) {
            Bonus = 10;
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            if (owner.CurrentHP <= owner.MaxHP * 0.8) {
                owner.AddTemporaryBonus("Spd", Bonus);
                owner.AddTemporaryBonus("Def", Bonus);
            }
        }
    }
}