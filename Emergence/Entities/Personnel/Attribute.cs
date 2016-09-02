using System;
using Emergence.Core;

namespace Emergence.Entities.Personnel {
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
        public static string GetCategoryStatName(this AttributeCategory attributeCategory) {
            switch(attributeCategory) {
                case AttributeCategory.Physical:
                    return "Fitness";
                case AttributeCategory.Mental:
                    return "Mental Acuity";
            }
            return "ERR - invalid attribute category";
        }
        public static string GetName(this Attribute attribute) {
            return attribute.ToString();
        }
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