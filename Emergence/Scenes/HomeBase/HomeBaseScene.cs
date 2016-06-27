using System;
using Emergence.Core;
using Emergence.Entities;
using Emergence.Scenes.MainMenu;
using Emergence.Utilities;
using libtcod;

namespace Emergence.Scenes.HomeBase {
	public class HomeBaseScene : BaseScene {
		private TCODConsole baseImage;
		private TCODConsole labels;
		private int x, y;
		private bool roomLabelsEnabled;

		public HomeBaseScene(Game game) : base(game) {
			roomLabelsEnabled = false;
			baseImage = RexPaintImageLoader
				.LoadImage("Assets/HomeBase/base.xp");
			labels = RexPaintImageLoader
				.LoadImage("Assets/HomeBase/labels.xp");
			x = y = 0;
		}

		public override void Render(float deltaTime) {
			TCODConsole.root.setBackgroundColor(TCODColor.black);
			TCODConsole.root.clear();
			TCODConsole.blit(baseImage, 0, 0, 60, 60,
				TCODConsole.root, x, y);
			if (roomLabelsEnabled) {
				TCODConsole.blit(labels, 0, 0, 60, 60,
					TCODConsole.root, x, y);
			}
			RenderMainPanel();
		}

		public override void Update(float deltaTime) {
		}

		public override void KeyPressed(TCODKey keyData) {
			switch(keyData.KeyCode) {
				case TCODKeyCode.Up:
					++y;
					break;
				case TCODKeyCode.Down:
					--y;
					break;
				case TCODKeyCode.Left:
					++x;
					break;
				case TCODKeyCode.Right:
					--x;
					break;
			}
			switch(char.ToUpper(keyData.Character)) {
				case 'P': // [P]ersonnel
					new BlockingMessageModal(
						Game.Settings.UiForeground,
						Game.Settings.UiBackground,
						"This feature is not yet implemented."
					).Show();
					break;
				case 'E': // [E]quipment
					new BlockingMessageModal(
						Game.Settings.UiForeground,
						Game.Settings.UiBackground,
						"This feature is not yet implemented."
					).Show();
					break;
				case 'V': // [V]ehicles
					new BlockingMessageModal(
						Game.Settings.UiForeground,
						Game.Settings.UiBackground,
						"This feature is not yet implemented."
					).Show();
					break;
				case 'T': // New [T]ask
					new BlockingMessageModal(
						Game.Settings.UiForeground,
						Game.Settings.UiBackground,
						"This feature is not yet implemented."
					).Show();
					break;
				case 'A': // [A]ssign Personnel
					new BlockingMessageModal(
						Game.Settings.UiForeground,
						Game.Settings.UiBackground,
						"This feature is not yet implemented."
					).Show();
					break;
				case 'G': // [G]o Scavenging
					new BlockingMessageModal(
						Game.Settings.UiForeground,
						Game.Settings.UiBackground,
						"This feature is not yet implemented."
					).Show();
					break;
				case 'R': // [R]oom Labels
					roomLabelsEnabled = !roomLabelsEnabled;
					break;
				case 'S': // Latest [S]ummary
					new BlockingMessageModal(
						Game.Settings.UiForeground,
						Game.Settings.UiBackground,
						"This feature is not yet implemented."
					).Show();
					break;
				case 'W': // [W]ait a Day
					new BlockingMessageModal(
						Game.Settings.UiForeground,
						Game.Settings.UiBackground,
						"This feature is not yet implemented."
					).Show();
					break;
				case 'O': // [O]ptions
					new BlockingMessageModal(
						Game.Settings.UiForeground,
						Game.Settings.UiBackground,
						"This feature is not yet implemented."
					).Show();
					break;
				case 'Q': // Save and [Q]uit
					var confirmationModal = new BlockingConfirmationModal() {
						Foreground = Game.Settings.UiForeground,
						Background = Game.Settings.UiBackground,
						Message = "Are you sure you want to save and quit?"
					};
					if(confirmationModal.Show()) {
						// TODO: Save game
						Game.ChangeScene(new MainMenuScene(Game));
					}
					break;
			}
		}

		private void RenderMainPanel() {
			// Set colors and clear the area
			TCODConsole.root.setForegroundColor(Game.Settings.UiForeground);
			TCODConsole.root.setBackgroundColor(Game.Settings.UiBackground);
			TCODConsole.root.setBackgroundFlag(TCODBackgroundFlag.Set);
			TCODConsole.root.rect(2, 0, TCODConsole.root.getWidth() - 4, 10, true);

			// Draw the frame
			TCODConsole.root.vline(1, 0, 10);
			TCODConsole.root.vline(21, 0, 10);
			TCODConsole.root.vline(TCODConsole.root.getWidth() - 2, 0, 10);
			TCODConsole.root.hline(2, 10, TCODConsole.root.getWidth() - 4);
			TCODConsole.root.putChar(1, 10, (int)TCODSpecialCharacter.SW);
			TCODConsole.root.putChar(21, 10, (int)TCODSpecialCharacter.TeeNorth);
			TCODConsole.root.putChar(TCODConsole.root.getWidth() - 2, 10, (int)TCODSpecialCharacter.SE);

			// Draw current stocks w/ labels
			TCODConsole.root.print(3, 1, 
                $"Population: {Game.State.Personnel.Count.ToString("D2")}/{Game.State.MaxPersonnel.ToString("D2")}");
            var supplyOffset = 3;
            foreach(var value in Enum.GetValues(typeof(SupplyType))) {
                var supplyType = (SupplyType)value;
                TCODConsole.root.print(3, supplyOffset, 
                    $"{supplyType.GetShortName().PadRight(6)}: " +
                    $"{Game.State.Supplies[supplyType].ToString("D4")}/" + 
                    $"{Game.State.MaxSupplies[supplyType].ToString("D4")}");
                supplyOffset += 1;
            }

			// Draw left menu options
			TCODConsole.root.print(23, 1, $"[P]ersonnel");
			TCODConsole.root.print(23, 2, $"[E]quipment");
			TCODConsole.root.print(23, 3, $"[V]ehicles");
			TCODConsole.root.print(23, 5, $"New [T]ask");
			TCODConsole.root.print(23, 6, $"[A]ssign Personnel");
			TCODConsole.root.print(23, 8, $"[G]o Scavenging");

			// Draw right menu options
			int x = TCODConsole.root.getWidth() - 4;
			string arrows = ""
				+ (char)TCODSpecialCharacter.ArrowNorth
				+ (char)TCODSpecialCharacter.ArrowSouth
				+ (char)TCODSpecialCharacter.ArrowWest
				+ (char)TCODSpecialCharacter.ArrowEast;
			TCODConsole.root.setAlignment(TCODAlignment.RightAlignment);
			// Arrows require special characters, and the color of the 
			// room labels menu option changes to reflect when it is enabled.
			TCODConsole.root.print(x, 1, $"[{arrows}] Move View");
			if(roomLabelsEnabled) {
				TCODConsole.root.setForegroundColor(TCODColor.green);
			}
            TCODConsole.root.print(x, 2, $"[R]oom Labels");
			TCODConsole.root.setForegroundColor(Game.Settings.UiForeground);

			TCODConsole.root.print(x, 3, $"Latest [S]ummary");
			TCODConsole.root.print(x, 4, $"[W]ait a Day");
			TCODConsole.root.print(x, 7, $"[O]ptions");
			TCODConsole.root.print(x, 8, $"Save and [Q]uit");

			TCODConsole.root.setAlignment(TCODAlignment.LeftAlignment);
		}
	}
}
