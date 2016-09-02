using System;
using Emergence.Core;

namespace Emergence.Entities.Personnel {
	public enum Skill {
		LargeMeleeWeapons,
		ThrownWeapons,
		Bash,
		HeavyFirearms,
		ArmorUse,
		Exert,
		SmallMeleeWeapons,
		LightFirearms,
		Stealth,
		BioticsAffinity,
		UnarmedCombat,
		Focus,
		Engineering,
		Repairing,
		Healing,
		Awareness,
		Dodge,
		SenseMotive
	}

	public static class SkillExtensions {
        public static string GetName(this Skill skill) {
            switch(skill) {
                case Skill.LargeMeleeWeapons:
                    return "Large Melee Weapons";
                case Skill.ThrownWeapons:
                    return "Thrown Weapons";
                case Skill.HeavyFirearms:
                    return "Heavy Firearms";
                case Skill.ArmorUse:
                    return "Armor Use";
                case Skill.SmallMeleeWeapons:
                    return "Small Melee Weapons";
                case Skill.LightFirearms:
                    return "Light Firearms";
                case Skill.BioticsAffinity:
                    return "Biotics Affinity";
                case Skill.UnarmedCombat:
                    return "Unarmed Combat";
                case Skill.SenseMotive:
                    return "Sense Motive";
                default:
                    return skill.ToString();
            }
        }

		public static Attribute GetGoverningAttribute(this Skill skill) {
			switch(skill) {
				case Skill.LargeMeleeWeapons:
				case Skill.ThrownWeapons:
				case Skill.Bash:
					return Attribute.Strength;

				case Skill.HeavyFirearms:
				case Skill.ArmorUse:
				case Skill.Exert:
					return Attribute.Endurance;

				case Skill.SmallMeleeWeapons:
				case Skill.LightFirearms:
				case Skill.Stealth:
					return Attribute.Agility;

				case Skill.BioticsAffinity:
				case Skill.UnarmedCombat:
				case Skill.Focus:
					return Attribute.Will;

				case Skill.Engineering:
				case Skill.Repairing:
				case Skill.Healing:
					return Attribute.Reason;

				case Skill.Awareness:
				case Skill.Dodge:
				case Skill.SenseMotive:
					return Attribute.Intuition;
			}
			Logger.Error($"Invalid skill type '{skill.ToString()}'.  Defaulting to Strength.");
			return Attribute.Strength;
		}
		public static bool IsPassive(this Skill skill) {
			return !skill.IsActive();
		}
		public static bool IsActive(this Skill skill) {
			switch (skill) {
				case Skill.Bash:
				case Skill.Exert:
				case Skill.Stealth:
				case Skill.Focus:
				case Skill.Healing:
				case Skill.SenseMotive:
					return true;
			}
			return false;
		}
	}
}