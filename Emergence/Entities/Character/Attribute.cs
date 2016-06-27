using System;
using Emergence.Core;

namespace Emergence.Entities.Character {
	public enum AttributeCategory {
		Physical,
		Mental
	}
	public enum Attribute {
		Strength,
		Endurance,
		Agility,
		Will,
		Reason,
		Intuition
	}

	public static class AttributeExtensions {
		public static AttributeCategory GetCategory(this Attribute attribute) {
			switch(attribute) {
				case Attribute.Strength:
				case Attribute.Endurance:
				case Attribute.Agility:
					return AttributeCategory.Physical;

				case Attribute.Will:
				case Attribute.Reason:
				case Attribute.Intuition:
					return AttributeCategory.Mental;
			}
			Logger.Error($"Invalid attribute type '{attribute.ToString()}'.  Defaulting to Physical.");
			return AttributeCategory.Physical;
		}
	}
}