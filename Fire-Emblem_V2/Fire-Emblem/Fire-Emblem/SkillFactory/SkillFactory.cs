// SkillFactory.cs
namespace Fire_Emblem {
    public class SkillFactory : ISkillFactory {
        public Skill CreateSkill(string name, string description) {
            switch (name) {
                case "HP +15":
                    return new HPIncreaseSkill(name, description);
                case "Fair Fight":
                    return new FairFightSkill(name, description);
                case "Death Blow":
                    return new DeathBlow(name, description);
                case "Armored Blow":
                    return new ArmoredBlow(name, description);
                case "Darting Blow":
                    return new DartingBlow(name, description);
                case "Warding Blow":
                    return new WardingBlow(name, description);
                case "Sturdy Blow":
                    return new SturdyBlow(name, description);
                case "Steady Blow":
                    return new SteadyBlow(name, description);
                case "Bracing Blow":
                    return new BracingBlow(name, description);
                case "Deadly Blade":
                    return new DeadlyBladeSkill(name, description);
                case "Swift Sparrow":
                    return new SwiftSparrowSkill(name, description);
                case "Mirror Strike":
                    return new MirrorStrikeSkill(name, description);
                case "Swift Strike":
                    return new SwiftStrikeSkill(name, description);
                case "Brazen Atk/Spd":
                    return new BrazenAtkSpd(name, description);
                case "Brazen Atk/Def":
                    return new BrazenAtkDef(name, description);
                case "Brazen Atk/Res":
                    return new BrazenAtkRes(name, description);
                case "Brazen Spd/Def":
                    return new BrazenSpdDef(name, description);
                case "Brazen Spd/Res":
                    return new BrazenSpdRes(name, description);
                case "Brazen Def/Res":
                    return new BrazenDefRes(name, description);
                case "Fire Boost":
                    return new FireBoost(name, description);
                case "Wind Boost":
                    return new WindBoost(name, description);
                case "Earth Boost":
                    return new EarthBoost(name, description);
                case "Water Boost":
                    return new WaterBoost(name, description);
                case "Will to Win":
                    return new WillToWin(name, description);
                case "Perceptive":
                    return new Perceptive(name, description);
                case "Single-Minded":
                    return new SingleMinded(name, description);
                case "Tome Precision":
                    return new TomePrecision(name, description);
                case "Attack +6" :
                    return new AttackPlusSix(name, description);
                case "Speed +5" :
                    return new SpeedPlusFive(name, description);
                case "Defense +5":
                    return new DefensePlusFive(name, description);
                case "Resistance +5":
                    return new ResistancePlusFive(name, description);
                case "Atk/Def +5":
                    return new AtkDefPlusFive(name, description);
                case "Atk/Res +5":
                    return new AtkResPlusFive(name, description);
                case "Spd/Res +5":
                    return new SpdResPlusFive(name, description);
                case "Chaos Style":
                    return new ChaosStyle(name, description);
                case "Resolve":
                    return new Resolve(name, description);
                case "Wrath":
                    return new Wrath(name, description);
                case "Blinding Flash":
                    return new BlindingFlash(name, description);
                case "Not *Quite*":
                    return new NotQuite(name, description);
                case "Stunning Smile":
                    return new StunningSmile(name, description);
                case "Disarming Sigh":
                    return new DisarmingSigh(name, description);
                case "Charmer":
                    return new Charmer(name, description);
                case "Belief in Love":
                    return new BeliefInLove(name, description);
                case "Ignis":
                    return new Ignis(name, description);
                default:
                    return new GenericSkill(name, description);
            }
        }
    }
}