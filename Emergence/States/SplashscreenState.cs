using System;
using System.IO;
using System.Linq;
using Emergence.Core;
using Emergence.States.MainMenu;
using libtcod;

namespace Emergence.States {
	public class SplashscreenState : BaseState {
		private TCODImage[] splashscreens { get; }
		private float delayPerScreen { get; } = 3.0f;
		private float currentDelay { get; set; }
		private int currentSplashscreenIndex { get; set; }
		private TCODImage currentSplashscreen { get; set; }

		public SplashscreenState(Game game) : base(game) {
			splashscreens = Directory.GetFiles("Assets/Splashscreens")
				.Select(fileName => new TCODImage(fileName)).ToArray();
			if(splashscreens.Length == 0) {
				Game.ChangeState(new MainMenuState(Game));
				return;
			}
			currentDelay = delayPerScreen;
			currentSplashscreenIndex = 0;
			currentSplashscreen = splashscreens[currentSplashscreenIndex];
		}

		public override void KeyPressed(TCODKey keyData) {
			currentDelay = 0.0f;
		}

		public override void Update(float deltaTime) {
			currentDelay -= deltaTime;
			if(currentDelay <= 0.0f) {
				currentSplashscreenIndex += 1;
				currentDelay += delayPerScreen;
				if(currentSplashscreenIndex >= splashscreens.Length) {
					Array.ForEach(splashscreens, s => s.Dispose());
					currentSplashscreen = null; // To keep render from breaking.
					Game.ChangeState(new MainMenuState(Game));
				} else {
					currentSplashscreen = splashscreens[currentSplashscreenIndex];
				}
			}
		}

		public override void Render(float deltaTime) {
			if(currentSplashscreen != null) {
				currentSplashscreen.blit(TCODConsole.root,
					TCODConsole.root.getWidth() / 2,
					TCODConsole.root.getHeight() / 2);
			}
		}

		private TCODImage loadSplashscreen(string fileName) {
			return new TCODImage(fileName);
		}
	}
}