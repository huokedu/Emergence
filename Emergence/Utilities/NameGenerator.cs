using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Emergence.Core;

namespace Emergence.Utilities {
	public static class NameGenerator {
		private static Random random;
		private static Dictionary<string, List<NameEntry>> nameLists;

		public static void Initialize() {
			random = new Random();
			nameLists = new Dictionary<string, List<NameEntry>>();
		}

		public static string GenerateName(string nameList) {
			if(random == null || nameLists == null) {
				Initialize();
			}
			var randomNumber = 100.0f * (float)random.NextDouble();
			var nameEntry = GetNameList(nameList).LastOrDefault(n => n.AggregateProbability < randomNumber);
			if(nameEntry == null) {
				nameEntry = GetNameList(nameList)[random.Next(nameList.Length)];
			}
			return nameEntry.Name;
		}

		// Checks if the name list has already been loaded.  If so it pulls it from the 
		// dictionary.  If not it calls LoadNameList to load it.
		private static List<NameEntry> GetNameList(string nameList) {
			if(!nameLists.ContainsKey(nameList)) {
				nameLists[nameList] = LoadNameList(nameList);
			}
			return nameLists[nameList];
		}

		// Loads a name list from the Assets/NameGenerator directory as a CSV file
		// and processes it into a list of NameEntry objects.
		private static List<NameEntry> LoadNameList(string nameList) {
			try {
				using(var reader = new StreamReader($"Assets/NameGenerator/{nameList}Names.csv")) {
					var newNameList = new List<NameEntry>();
					string[] lineData;
					while(!reader.EndOfStream) {
						lineData = reader.ReadLine().Split(',');
						float temp;
						if(!float.TryParse(lineData[1], out temp) || !float.TryParse(lineData[2], out temp)) {
							Logger.Error($"Couldn't parse {temp} to float.");
						}
						newNameList.Add(new NameEntry() {
							Name = lineData[0],
							Probability = float.Parse(lineData[1]),
							AggregateProbability = float.Parse(lineData[2])
						});
					}
					return newNameList;
				}
			} catch (Exception e) {
				Logger.Error($"Error loading name list '{nameList}': {e.Message}");
				return null;
			}
		}

		private class NameEntry {
			public string Name { get; set; }
			public float Probability { get; set; }
			public float AggregateProbability { get; set; }
		}
	}
}
