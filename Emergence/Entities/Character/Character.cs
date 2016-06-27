using System;
using System.Collections.Generic;

namespace Emergence.Entities.Character {
	public class Character {
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Nickname { get; set; }
        public Gender Gender { get; set; }
		public string FullName {
			get {
				return $"{FirstName} {LastName}";
			}
		}
		public string ShortName {
			get {
				return Nickname ?? $"{FirstName[0]}. {LastName}";

            }
		}

        #region Attributes
        public Dictionary<Attribute, byte> Attributes;
		public byte Strength {
			get {
				return Attributes[Attribute.Strength];
			}
			set {
				if (value < 1) value = 1;
				if (value > 10) value = 10;
				Attributes[Attribute.Strength] = value;
            }
		}
		public byte Endurance {
			get {
				return Attributes[Attribute.Endurance];
			}
			set {
				if (value < 1) value = 1;
				if (value > 10) value = 10;
				Attributes[Attribute.Endurance] = value;
			}
		}
		public byte Agility {
			get {
				return Attributes[Attribute.Agility];
			}
			set {
				if (value < 1) value = 1;
				if (value > 10) value = 10;
				Attributes[Attribute.Agility] = value;
			}
		}
		public byte Fitness {
			get {
				float total = Strength + Endurance + Agility;
				float average = total / 3.0f;
				return (byte)Math.Round(average);
			}
		}
		public byte Will {
			get {
				return Attributes[Attribute.Will];
			}
			set {
				if (value < 1) value = 1;
				if (value > 10) value = 10;
				Attributes[Attribute.Will] = value;
			}
		}
		public byte Reason {
			get {
				return Attributes[Attribute.Reason];
			}
			set {
				if (value < 1) value = 1;
				if (value > 10) value = 10;
				Attributes[Attribute.Reason] = value;
			}
		}
		public byte Intuition {
			get {
				return Attributes[Attribute.Intuition];
			}
			set {
				if (value < 1) value = 1;
				if (value > 10) value = 10;
				Attributes[Attribute.Intuition] = value;
			}
		}
		public byte MentalAcuity {
			get {
				float total = Will + Reason + Intuition;
				float average = total / 3.0f;
				return (byte)Math.Round(average);
			}
		}
        #endregion Attributes

        #region Skills
        public Dictionary<Skill, SkillRank> Skills;
		public SkillRank LargeMeleeWeapons {
			get {
				return Skills[Skill.LargeMeleeWeapons];
			}
			set {
				Skills[Skill.LargeMeleeWeapons] = value;
			}
		}
		public SkillRank ThrownWeapons {
			get {
				return Skills[Skill.ThrownWeapons];
			}
			set {
				Skills[Skill.ThrownWeapons] = value;
			}
		}
		public SkillRank Bash {
			get {
				return Skills[Skill.Bash];
			}
			set {
				Skills[Skill.Bash] = value;
			}
		}
		public SkillRank HeavyFirearms {
			get {
				return Skills[Skill.HeavyFirearms];
			}
			set {
				Skills[Skill.HeavyFirearms] = value;
			}
		}
		public SkillRank ArmorUse {
			get {
				return Skills[Skill.ArmorUse];
			}
			set {
				Skills[Skill.ArmorUse] = value;
			}
		}
		public SkillRank Exert {
			get {
				return Skills[Skill.Exert];
			}
			set {
				Skills[Skill.Exert] = value;
			}
		}
		public SkillRank SmallMeleeWeapons {
			get {
				return Skills[Skill.SmallMeleeWeapons];
			}
			set {
				Skills[Skill.SmallMeleeWeapons] = value;
			}
		}
		public SkillRank LightFirearms {
			get {
				return Skills[Skill.LightFirearms];
			}
			set {
				Skills[Skill.LightFirearms] = value;
			}
		}
		public SkillRank Stealth {
			get {
				return Skills[Skill.Stealth];
			}
			set {
				Skills[Skill.Stealth] = value;
			}
		}
		public SkillRank BioticsAffinity {
			get {
				return Skills[Skill.BioticsAffinity];
			}
			set {
				Skills[Skill.BioticsAffinity] = value;
			}
		}
		public SkillRank UnarmedCombat {
			get {
				return Skills[Skill.UnarmedCombat];
			}
			set {
				Skills[Skill.UnarmedCombat] = value;
			}
		}
		public SkillRank Focus {
			get {
				return Skills[Skill.Focus];
			}
			set {
				Skills[Skill.Focus] = value;
			}
		}
		public SkillRank Engineering {
			get {
				return Skills[Skill.Engineering];
			}
			set {
				Skills[Skill.Engineering] = value;
			}
		}
		public SkillRank Repairing {
			get {
				return Skills[Skill.Repairing];
			}
			set {
				Skills[Skill.Repairing] = value;
			}
		}
		public SkillRank Healing {
			get {
				return Skills[Skill.Healing];
			}
			set {
				Skills[Skill.Healing] = value;
			}
		}
		public SkillRank Awareness {
			get {
				return Skills[Skill.Awareness];
			}
			set {
				Skills[Skill.Awareness] = value;
			}
		}
		public SkillRank Dodge {
			get {
				return Skills[Skill.Dodge];
			}
			set {
				Skills[Skill.Dodge] = value;
			}
		}
		public SkillRank SenseMotive {
			get {
				return Skills[Skill.SenseMotive];
			}
			set {
				Skills[Skill.SenseMotive] = value;
			}
		}
		#endregion Skills

        public Character(string firstName, string lastName, Gender gender) {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;

            Attributes = new Dictionary<Attribute, byte>();
            foreach(var value in Enum.GetValues(typeof(Attribute))) {
                Attributes[(Attribute)value] = 3;
            }

            Skills = new Dictionary<Skill, SkillRank>();
            foreach(var value in Enum.GetValues(typeof(Skill))) {
                Skills[(Skill)value] = SkillRank.F;
            }
        }
	}

    public enum Gender {
        Male,
        Female
    }

    public static class GenderExtensions {
        public static char GetSymbol(this Gender gender) {
            return gender == Gender.Male ? (char)11 : (char)12;
        }
    }
}