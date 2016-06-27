using System;
using Emergence.Utilities;

namespace Emergence.Entities.Character {
    public static class CharacterFactory {
        public static Character GenerateRandom(Random random) {
            var gender = random.Next(2) == 1 ? Gender.Male : Gender.Female;
            var firstName = gender == Gender.Male 
                ? NameGenerator.GenerateName("Male")
                : NameGenerator.GenerateName("Female");
            var lastName = NameGenerator.GenerateName("Last");

            var character = new Character(firstName, lastName, gender);
            for(int i = 0; i < 6; ++i) {
                var skill = EnumExtensions.PickRandom<Skill>(random);
                var attribute = skill.GetGoverningAttribute();
                if(character.Skills[skill] == SkillRank.C || character.Attributes[attribute] > 7) {
                    i -= 1;
                    continue;
                } else {
                    character.Skills[skill] = character.Skills[skill].Next();
                    character.Attributes[attribute] += 1;
                }
            }

            return character;
        }
    }
}
