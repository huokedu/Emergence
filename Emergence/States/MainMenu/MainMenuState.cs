using System;
using System.Collections.Generic;
using Emergence.Core;
using Emergence.Utilities;
using libtcod;

namespace Emergence.States.MainMenu {
	public class MainMenuState : BaseState {
		private TCODConsole background;
		private TCODColor backgroundColor;
		private List<Star> starfield;
		private List<Cloud> clouds;
		private bool creditsDoneRendering { get; set; }

		public MainMenuState(Game game) : base(game) {
			background = RexPaintImageLoader.LoadImage("Assets/MainMenu/Background.xp");
			backgroundColor = new TCODColor(0, 32, 64);
			creditsDoneRendering = false;
			GenerateStarfield();
			GenerateClouds();
		}

		public override void Update(float deltaTime) {
			starfield.ForEach(s => s.Update(deltaTime));
			clouds.ForEach(c => c.Update(deltaTime));
		}

		public override void Render(float deltaTime) {
			TCODConsole.root.setForegroundColor(Game.Settings.UiForeground);
			TCODConsole.root.setBackgroundColor(backgroundColor);
			TCODConsole.root.setBackgroundFlag(TCODBackgroundFlag.Set);
			TCODConsole.root.clear();

			// Render stars and clouds
			starfield.ForEach(s => s.Render(backgroundColor));
			clouds.ForEach(c => c.Render());

			// Render buildings.
			TCODConsole.blit(background, 0, 0, background.getWidth(), background.getHeight(),
				TCODConsole.root, 0, 0);

			// Render credits.
			if(!creditsDoneRendering) {
				creditsDoneRendering =
					TCODConsole.renderCredits(1, TCODConsole.root.getHeight() - 3, true);
			}

			// Reset colors and render menu
			TCODConsole.root.setForegroundColor(Game.Settings.UiForeground);
			TCODConsole.root.setBackgroundColor(Game.Settings.UiBackground);
			TCODConsole.root.printFrame(9, 13, 15, 13, true);
			TCODConsole.root.print(11, 15, "[N]ew Game");
			TCODConsole.root.print(11, 17, "[L]oad Game");
			TCODConsole.root.print(11, 19, "[O]ptions");
			TCODConsole.root.print(11, 21, "[C]redits");
			TCODConsole.root.print(11, 23, "[Q]uit");
		}

		public override void KeyPressed(TCODKey keyData) {
			switch(char.ToUpper(keyData.Character)) {
				case 'N':
					Game.ChangeState(new NewGameState(Game));
					break;

				case 'L':
					new BlockingMessageModal(Game.Settings.UiForeground,
						Game.Settings.UiBackground, "This feature is not yet implemented.").Show();
					break;

				case 'O':
					new BlockingMessageModal(Game.Settings.UiForeground,
						Game.Settings.UiBackground, "This feature is not yet implemented.").Show();
					break;

				case 'C':
					Game.ChangeState(new CreditsState(Game));
					break;

				case 'Q':
					var confirmQuit = new BlockingConfirmationModal() {
						Foreground = Game.Settings.UiForeground,
						Background = Game.Settings.UiBackground,
						Message = "Are you sure you want to quit?"
					}.Show();
					if(confirmQuit) {
						Game.ChangeState(null);
					}
					break;
			}
		}

		private void GenerateStarfield() {
			starfield = new List<Star>();
			var random = new Random();

			int numberOfStars = 50 + random.Next(20);
			for(int i = 0; i < numberOfStars; ++i) {
				starfield.Add(new Star() {
					X = random.Next(TCODConsole.root.getWidth()),
					Y = random.Next(TCODConsole.root.getHeight()),
					Brightness = (float)random.NextDouble(),
					Waxing = random.Next(2) == 1
				});
			}
		}
		private void GenerateClouds() {
			clouds = new List<Cloud>();
			var random = new Random();

			int numberOfClouds = 10 + random.Next(5);
			for(int i = 0; i < numberOfClouds; ++i) {
				clouds.Add(new Cloud() {
					X = random.Next(TCODConsole.root.getWidth()),
					Y = 1 + random.Next(TCODConsole.root.getHeight() - 25), // -25 to account for buildings
					Color = TCODColor.Interpolate(
						TCODColor.white, backgroundColor,
						0.35f + (float)random.NextDouble() * 0.30f),
					Speed = (float)random.NextDouble() + 0.5f
				});
			}
		}
	}
}