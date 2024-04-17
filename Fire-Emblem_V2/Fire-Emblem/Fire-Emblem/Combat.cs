using Fire_Emblem_View;
namespace Fire_Emblem
{
    public class Combat
    {
        public readonly Character _attacker;
        public readonly Character _defender;
        private readonly string _advantage;
        private readonly View _view;
        public Battle _battle;

        public Combat(Character attacker, Character defender, string advantage, View view, Battle battle)
        {
            _attacker = attacker;
            _defender = defender;
            _advantage = advantage;
            _view = view;
            _battle = battle;
        }

        public void Start()
        {
            PrepareCombat();
            ExecuteCombat();
            FinalizeCombat();
        }

        private void PrepareCombat()
        {
            ApplySkills();
            PrintBonuses(_attacker);
            PrintBonuses(_defender);
            PrintFirstAttackBonuses(_attacker);
            PrintFirstAttackBonuses(_defender);
            PrintBonusNegations(_attacker);
            PrintBonusNegations(_defender);
            PrintPenaltyNegations(_attacker);
            PrintPenaltyNegations(_defender);
        }
        

        private void ExecuteCombat()
        {
            PerformInitialAttack();
            if (_defender.CurrentHP > 0)
            {
                PerformCounterAttack();
            }
            if (_attacker.CurrentHP > 0 && _defender.CurrentHP > 0)
            {
                PerformFollowUp();
            }
        }
        
        private void FinalizeCombat()
        {
            ClearTemporaryBonuses();
            ClearTemporaryPenalties();
            PrintFinalState();
        }

        private void ApplySkills() {
            foreach (var skill in _attacker.Skills) {
                skill.ApplyEffect(_battle, _attacker);
            }
            foreach (var skill in _defender.Skills) {
                skill.ApplyEffect(_battle, _defender);
            }
        }

        private void PerformInitialAttack()
        {
            Attack attack = new Attack(_attacker, _defender, _view);
            attack.PerformAttack(_advantage);
        }

        private void PerformCounterAttack()
        {
            if (_defender.CurrentHP > 0)
            {
                Attack counterAttack = new Attack(_attacker, _defender, _view);
                counterAttack.PerformCounterAttack(_advantage);
            }
        }

        private void PerformFollowUp()
        {
            if (_attacker.CurrentHP > 0 && _defender.CurrentHP > 0)
            {
                Attack followUpAttack = new Attack(_attacker, _defender, _view);
                if (_attacker.GetEffectiveAttribute("Spd") >= _defender.GetEffectiveAttribute("Spd") + 5)
                {
                    followUpAttack.PerformFollowUpAtacker(_advantage);
                }
                else if (_defender.GetEffectiveAttribute("Spd") >= _attacker.GetEffectiveAttribute("Spd") + 5)
                {
                    followUpAttack.PerformFollowUpDefender(_advantage);
                }
                else
                {
                    _view.WriteLine("Ninguna unidad puede hacer un follow up");
                }
            }
        }
        
        private void PrintBonuses(Character character)
        {
            string[] statsOrder = { "Atk", "Spd", "Def", "Res" };
            foreach (var stat in statsOrder)
            {
                int bonus = character.TemporaryBonuses.ContainsKey(stat) ? character.TemporaryBonuses[stat] : 0;
                int penalty = character.TemporaryPenalties.ContainsKey(stat) ? character.TemporaryPenalties[stat] : 0;
                int totalEffect = bonus + penalty;
                if (totalEffect != 0)
                {
                    _view.WriteLine($"{character.Name} obtiene {stat}{totalEffect:+#;-#;+0}");
                }
            }
        }
        
        private void PrintBonusNegations(Character character)
        {
            string[] statsOrder = { "Atk", "Spd", "Def", "Res" };
            foreach (var stat in statsOrder)
            {
                if (character.AreBonusesEnabled == false)
                {
                    _view.WriteLine($"Los bonus de {stat} de {character.Name} fueron neutralizados");
                }
            }
        }
        
        private void PrintPenaltyNegations(Character character)
        {
            string[] statsOrder = { "Atk", "Spd", "Def", "Res" };
            foreach (var stat in statsOrder)
            {
                if (character.ArePenaltiesEnabled == false)
                {
                    _view.WriteLine($"Los penalty de {stat} de {character.Name} fueron neutralizados");
                }
            }
        }
        
        private void PrintFirstAttackBonuses(Character character)
        {
            string[] statsOrder = { "Atk", "Spd", "Def", "Res" };
            foreach (var stat in statsOrder)
            {
                int bonus = character.TemporaryFirstAttackBonuses.ContainsKey(stat) ? character.TemporaryFirstAttackBonuses[stat] : 0;
                int penalty = character.TemporaryFirstAttackPenalties.ContainsKey(stat) ? character.TemporaryFirstAttackPenalties[stat] : 0;
                int totalEffect = bonus + penalty;
                if (totalEffect != 0)
                {
                    _view.WriteLine($"{character.Name} obtiene {stat}{totalEffect:+#;-#;+0} en su primer ataque");
                }
            }
        }

        
        private void ClearTemporaryBonuses()
        {
            _attacker.CleanBonuses();
            _defender.CleanBonuses();
            _attacker.CleanFirstAttackBonuses();
            _defender.CleanFirstAttackBonuses();
            _attacker.AreBonusesEnabled = true;
        }

        private void ClearTemporaryPenalties()
        {
            _attacker.CleanPenalties();
            _defender.CleanPenalties();
            _attacker.CleanFirstAttackPenalties();
            _defender.CleanFirstAttackPenalties();
            _defender.ArePenaltiesEnabled = true;
        }

        private void PrintFinalState()
        {
            _view.WriteLine($"{_attacker.Name} ({_attacker.CurrentHP}) : {_defender.Name} ({_defender.CurrentHP})");
        }
    }
}
