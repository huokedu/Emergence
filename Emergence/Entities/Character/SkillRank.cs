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
	}
}