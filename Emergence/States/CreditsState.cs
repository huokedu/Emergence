using System;
using System.Collections.Generic;
using System.IO;
using Emergence.Core;
using Emergence.States.MainMenu;
using libtcod;

namespace Emergence.States {
	public class CreditsState : BaseState {
		private List<string> lines;
		private float top, stop, scrollSpeed;

		public CreditsState(Game game) : base(game) {
			LoadLines("Assets/Credits.txt");
			int height = TCODConsole.root.getHeight();
			top = height;
			if(lines.Count < height) {
				stop = (height / 2) - (lines.Count / 2);
				scrollSpeed = 100.0f;
			} else {
				stop = height - lines.Count;
				scrollSpeed = 4.0f;
			}
		}

		public override void Update(float deltaTime) {
			top -= scrollSpeed * deltaTime;
			if(top < stop) {
				top = stop;
			}
		}

		public override void Render(float deltaTime) {
			int x = TCODConsole.root.getWidth() / 2;
			int y = 0;
			int height = TCODConsole.root.getHeight();
			TCODConsole.root.setForegroundColor(TCODColor.white);
			for(int i = 0; i < lines.Count; ++i) {
				y = (int)top + i;
				if(y < 0) {
					continue;
				} else if (y > height - 2) {
					break;
				}
				TCODConsole.root.printEx(x, y, 
					TCODBackgroundFlag.None, 
					TCODAlignment.CenterAlignment, 
					lines[i]);
			}
			TCODConsole.root.printEx(TCODConsole.root.getWidth() - 1, height - 1,
				TCODBackgroundFlag.None, TCODAlignment.RightAlignment,
				"Press [Escape] to return to the main menu.");
		}

		public override void KeyPressed(TCODKey keyData) {
			if(keyData.KeyCode == TCODKeyCode.Escape) {
				Game.ChangeState(new MainMenuState(Game));
			}
		}

		private void LoadLines(string fileName) {
			lines = new List<string>();
			try {
				using(var reader = new StreamReader(fileName)) {
					while(!reader.EndOfStream) {
						lines.Add(reader.ReadLine());
					}
				}
			} catch(Exception e) {
				Logger.Error($"Error loading credits from '{fileName}': {e.Message}");
			}
		}
	}
}
