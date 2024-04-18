using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Fire_Emblem;

public class Character
{
    [JsonPropertyName("Name")] public string Name { get; set; }
    [JsonPropertyName("Weapon")] public string Weapon { get; set; }
    [JsonPropertyName("Gender")] public string Gender { get; set; }
    [JsonPropertyName("HP")] public int MaxHP { get; set; }
    private int _currentHP;
    public int CurrentHP
    { 
        get => _currentHP; 
        set => _currentHP = Math.Max(value, 0); 
    }
    [JsonConverter(typeof(StringToIntConverter))] [JsonPropertyName("Atk")] public int Atk { get; set; }
    [JsonConverter(typeof(StringToIntConverter))] [JsonPropertyName("Spd")] public int Spd { get; set; }
    [JsonConverter(typeof(StringToIntConverter))] [JsonPropertyName("Def")] public int Def { get; set; }
    [JsonConverter(typeof(StringToIntConverter))] [JsonPropertyName("Res")] public int Res { get; set; }
    public List<Skill> Skills { get; private set; }
    public Dictionary<string, int> TemporaryBonuses { get; private set; }
    public Dictionary<string, int> TemporaryPenalties { get; private set; }
    
    public Dictionary<string, int> TemporaryFirstAttackBonuses { get; private set; }
    public Dictionary<string, int> TemporaryFirstAttackPenalties { get; private set; }
    
    public Dictionary<string, int> TemporaryFollowUpBonuses { get; private set; }
    public Dictionary<string, int> TemporaryFollowUpPenalties { get; private set; }
    
    public bool AreAtkBonusesEnabled { get; set; } = true;
    public bool AreDefBonusesEnabled { get; set; } = true;
    public bool AreResBonusesEnabled { get; set; } = true;
    public bool AreSpdBonusesEnabled { get; set; } = true;
    public bool AreAtkPenaltiesEnabled { get; set; } = true;
    public bool AreDefPenaltiesEnabled { get; set; } = true;
    public bool AreResPenaltiesEnabled { get; set; } = true;
    public bool AreSpdPenaltiesEnabled { get; set; } = true;

    public Character()
    {
        Skills = new List<Skill>();
        TemporaryBonuses = new Dictionary<string, int>();
        TemporaryPenalties = new Dictionary<string, int>();
        TemporaryFirstAttackBonuses = new Dictionary<string, int>();
        TemporaryFirstAttackPenalties = new Dictionary<string, int>();
        TemporaryFollowUpBonuses = new Dictionary<string, int>();
        TemporaryFollowUpPenalties = new Dictionary<string, int>();
    }

    public void AddSkill(Skill skill)
    {
        Skills.Add(skill);
    }

    public void SetSkills(List<Skill> skills)
    {
        Skills = skills;
    }

    

    private void AddToAttributeDictionary(Dictionary<string, int> dictionary, string attribute, int value)
    {
        if (dictionary.ContainsKey(attribute))
            dictionary[attribute] += value;
        else
            dictionary.Add(attribute, value);
    }

    public int GetEffectiveAttribute(string attribute)
    {
        int baseValue = attribute switch {
            "Atk" => Atk,
            "Spd" => Spd,
            "Def" => Def,
            "Res" => Res,
            _ => throw new ArgumentException($"Unknown attribute: {attribute}")
        };

        int bonus = (attribute == "Atk" && AreAtkBonusesEnabled || 
                     attribute == "Spd" && AreSpdBonusesEnabled || 
                     attribute == "Def" && AreDefBonusesEnabled || 
                     attribute == "Res" && AreResBonusesEnabled) && 
                    TemporaryBonuses.ContainsKey(attribute) ? TemporaryBonuses[attribute] : 0;

        int penalty = (attribute == "Atk" && AreAtkPenaltiesEnabled || 
                       attribute == "Spd" && AreSpdPenaltiesEnabled || 
                       attribute == "Def" && AreDefPenaltiesEnabled || 
                       attribute == "Res" && AreResPenaltiesEnabled) && 
                      TemporaryPenalties.ContainsKey(attribute) ? TemporaryPenalties[attribute] : 0;

        return baseValue + bonus + penalty;
    }

    
    public int GetFirstAttackAttribute(string attribute)
    {
        int baseValue = attribute switch {
            "Atk" => Atk,
            "Spd" => Spd,
            "Def" => Def,
            "Res" => Res,
            _ => throw new ArgumentException($"Unknown attribute: {attribute}")
        };

        int bonus = (attribute == "Atk" && AreAtkBonusesEnabled || 
                     attribute == "Spd" && AreSpdBonusesEnabled || 
                     attribute == "Def" && AreDefBonusesEnabled || 
                     attribute == "Res" && AreResBonusesEnabled) && 
                    TemporaryBonuses.ContainsKey(attribute) ? TemporaryBonuses[attribute] : 0;

        int penalty = (attribute == "Atk" && AreAtkPenaltiesEnabled || 
                       attribute == "Spd" && AreSpdPenaltiesEnabled || 
                       attribute == "Def" && AreDefPenaltiesEnabled || 
                       attribute == "Res" && AreResPenaltiesEnabled) && 
                      TemporaryPenalties.ContainsKey(attribute) ? TemporaryPenalties[attribute] : 0;

        int bonusFirstAttack = (attribute == "Atk" && AreAtkBonusesEnabled || 
                                attribute == "Spd" && AreSpdBonusesEnabled || 
                                attribute == "Def" && AreDefBonusesEnabled || 
                                attribute == "Res" && AreResBonusesEnabled) && 
                               TemporaryFirstAttackBonuses.ContainsKey(attribute) ? TemporaryFirstAttackBonuses[attribute] : 0;

        int penaltyFirstAttack = (attribute == "Atk" && AreAtkPenaltiesEnabled || 
                                  attribute == "Spd" && AreSpdPenaltiesEnabled || 
                                  attribute == "Def" && AreDefPenaltiesEnabled || 
                                  attribute == "Res" && AreResPenaltiesEnabled) && 
                                 TemporaryFirstAttackPenalties.ContainsKey(attribute) ? TemporaryFirstAttackPenalties[attribute] : 0;

        return baseValue + bonus + penalty + bonusFirstAttack + penaltyFirstAttack;
    }

    public int GetFollowUpAtribute(string attribute)
    {
        int baseValue = attribute switch {
            "Atk" => Atk,
            "Spd" => Spd,
            "Def" => Def,
            "Res" => Res,
            _ => throw new ArgumentException($"Unknown attribute: {attribute}")
        };

        int bonus = (attribute == "Atk" && AreAtkBonusesEnabled || 
                     attribute == "Spd" && AreSpdBonusesEnabled || 
                     attribute == "Def" && AreDefBonusesEnabled || 
                     attribute == "Res" && AreResBonusesEnabled) && 
                    TemporaryBonuses.ContainsKey(attribute) ? TemporaryBonuses[attribute] : 0;

        int penalty = (attribute == "Atk" && AreAtkPenaltiesEnabled || 
                       attribute == "Spd" && AreSpdPenaltiesEnabled || 
                       attribute == "Def" && AreDefPenaltiesEnabled || 
                       attribute == "Res" && AreResPenaltiesEnabled) && 
                      TemporaryPenalties.ContainsKey(attribute) ? TemporaryPenalties[attribute] : 0;

        int bonusFollowUp = (attribute == "Atk" && AreAtkBonusesEnabled || 
                                attribute == "Spd" && AreSpdBonusesEnabled || 
                                attribute == "Def" && AreDefBonusesEnabled || 
                                attribute == "Res" && AreResBonusesEnabled) && 
                               TemporaryFollowUpBonuses.ContainsKey(attribute) ? TemporaryFollowUpBonuses[attribute] : 0;

        int penaltyFollowUp = (attribute == "Atk" && AreAtkPenaltiesEnabled || 
                                  attribute == "Spd" && AreSpdPenaltiesEnabled || 
                                  attribute == "Def" && AreDefPenaltiesEnabled || 
                                  attribute == "Res" && AreResPenaltiesEnabled) && 
                                 TemporaryFollowUpPenalties.ContainsKey(attribute) ? TemporaryFollowUpPenalties[attribute] : 0;

        return baseValue + bonus + penalty + bonusFollowUp + penaltyFollowUp;
    }


    public void DisableAllBonuses()
    {
        AreAtkBonusesEnabled = false;
        AreDefBonusesEnabled = false;
        AreResBonusesEnabled = false;
        AreSpdBonusesEnabled = false;
    }
    
    public void DisableAllPenalties()
    {
        AreAtkPenaltiesEnabled = false;
        AreDefPenaltiesEnabled = false;
        AreResPenaltiesEnabled = false;
        AreSpdPenaltiesEnabled = false;
    }

    public void ReEnableBonuses()
    {
        AreAtkBonusesEnabled = true;
        AreDefBonusesEnabled = true;
        AreResBonusesEnabled = true;
        AreSpdBonusesEnabled = true;
    }

    public void ReEnablePenalties()
    {
        AreAtkPenaltiesEnabled = true;
        AreDefPenaltiesEnabled = true;
        AreResPenaltiesEnabled = true;
        AreSpdPenaltiesEnabled = true;

    }
    
    public void AddTemporaryBonus(string attribute, int value)
    {
        AddToAttributeDictionary(TemporaryBonuses, attribute, value);
    }

    public void AddTemporaryPenalty(string attribute, int value)
    {
        AddToAttributeDictionary(TemporaryPenalties, attribute, value);
    }

    public void AddTemporaryFirstAttackBonuses(string attribute, int value)
    {
        AddToAttributeDictionary(TemporaryFirstAttackBonuses, attribute, value);
    }
    
    public void AddTemporaryFirstAttackPenalties(string attribute, int value)
    {
        AddToAttributeDictionary(TemporaryFirstAttackPenalties, attribute, value);
    }

    public void AddTemporaryFollowUpBonuses(string attribute, int value)
    {
        AddToAttributeDictionary(TemporaryFollowUpBonuses, attribute, value);
    }
    
    public void AddTemporaryFollowUpPenalties(string attribute, int value)
    {
        AddToAttributeDictionary(TemporaryFollowUpPenalties, attribute, value);
    }
    public void CleanBonuses()
    {
        TemporaryBonuses.Clear();
    }

    public void CleanFirstAttackBonuses()
    {
        TemporaryFirstAttackBonuses.Clear();
    }

    public void CleanFollowUpBonuses()
    {
        TemporaryFollowUpBonuses.Clear();
    }
    public void CleanPenalties()
    {
        TemporaryPenalties.Clear();
    }
    public void CleanFirstAttackPenalties()
    {
        TemporaryFirstAttackPenalties.Clear();
    }

    public void CleanFollowUpPenalties()
    {
        TemporaryFollowUpPenalties.Clear();
    }
    
    
}
