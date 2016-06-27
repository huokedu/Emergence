namespace Emergence.Entities.Character {
	public enum SkillRank {
		F,
		E,
		D,
		C,
		B,
		A
	}

	public static class SkillRankExtensions {
        public static int XpCost(this SkillRank skillRank) {
            return 100;
        }
        public static SkillRank Next(this SkillRank skillRank) {
            var index = (int)skillRank;
            if(index >= 5) {
                return SkillRank.A;
            } else {
                return (SkillRank)(index + 1);
            }
        }
	}
}