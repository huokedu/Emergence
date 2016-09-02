using System;
using System.Collections.Generic;
using Emergence.Core;
using Emergence.Entities.Personnel;
using Emergence.Ui;
using libtcod;

namespace Emergence.Scenes {
	public class NewGameScene : BaseScene {

		public NewGameScene(Game game) : base(game) {
            Game.State = new GameState();
            GenerateCharacters();
		}

		public override void Update(float deltaTime) {
		}
		public override void Render(float deltaTime) {
            for(int i = 0; i < 4; ++i) {
                RenderCharacter(Game.State.Personnel[i], 0, 1 + (i * 14));
            }

			var x = TCODConsole.root.getWidth() / 2;
			var y = TCODConsole.root.getHeight() - 2;
			TCODConsole.root.printEx(x, y, TCODBackgroundFlag.Default,
				TCODAlignment.CenterAlignment, "[Enter] Continue       [R] Roll new characters");
		}

		public override void KeyPressed(TCODKey keyData) {
            if(keyData.KeyCode == TCODKeyCode.Enter) {
                if(new BlockingConfirmationModal() {
                    Foreground = Game.Settings.UiForeground,
                    Background = Game.Settings.UiBackground,
                    Message = "Are you sure you want to start with these characters?"
                }.Show()) {
                    Game.ChangeScene(new HomeBaseScene(Game));
                }
            } else if (char.ToUpper(keyData.Character) == 'R') {
                GenerateCharacters();
            }
		}

        private void RenderCharacter(Character character, int x, int y) {
            RenderCharacterBox(character, x, y);
            RenderCharacterStats(character, x, y);
            ColorTableRows(x, y);
        }

        private void GenerateCharacters() {
            var random = new Random();
            Game.State.Personnel = new List<Character>();
            for(int i = 0; i < 4; ++i) {
                Game.State.Personnel.Add(CharacterFactory.GenerateRandom(random));
            }
        }

        private void RenderCharacterBox(Character character, int x, int y) {
            // Render outside box
            TCODConsole.root.setForegroundColor(TCODColor.white);
            TCODConsole.root.setBackgroundColor(new TCODColor(51, 51, 51));
            TCODConsole.root.printFrame(x, y, 60, 13, true, TCODBackgroundFlag.Set);

            // Render character's name
            var genderSymbol = character.Gender == Gender.Male ? (char)11 : (char)12;
            TCODConsole.root.print(x + 1, y + 1,
                $"{character.Name.ToString("{F} {L}")} [{character.Gender.GetSymbol()}]");

            // Add horizontal dividing line
            TCODConsole.root.hline(x + 1, y + 2, 58);
            TCODConsole.root.putChar(x, y + 2, (char)TCODSpecialCharacter.TeeEast);
            TCODConsole.root.putChar(x, y + 59, (char)TCODSpecialCharacter.TeeWest);

            // Add first vertical dividing line
            TCODConsole.root.vline(x + 19, y + 3, 9);
            TCODConsole.root.putChar(x + 19, y + 2, (char)TCODSpecialCharacter.TeeSouth);
            TCODConsole.root.putChar(x + 19, y + 12, (char)TCODSpecialCharacter.TeeNorth);

            // Add second vertical dividing line
            TCODConsole.root.vline(x + 19, y + 3, 9);
            TCODConsole.root.putChar(x + 19, y + 2, (char)TCODSpecialCharacter.TeeSouth);
            TCODConsole.root.putChar(x + 19, y + 12, (char)TCODSpecialCharacter.TeeNorth);
        }

        private void RenderCharacterStats(Character character, int x, int y) {
            TCODConsole.root.print(x + 2, y + 3, "Strength");
            PrintAttributeValue(x + 17, y + 3, character.Strength);
            TCODConsole.root.print(x + 3, y + 4, "Large Melee");
            PrintSkillValue(x + 17, y + 4, character.LargeMeleeWeapons);
            TCODConsole.root.print(x + 3, y + 5, "Thrown Wpns");
            PrintSkillValue(x + 17, y + 5, character.ThrownWeapons);
            TCODConsole.root.print(x + 3, y + 6, "Bash");
            PrintSkillValue(x + 17, y + 6, character.Bash);

            TCODConsole.root.print(x + 2, y + 8,  $"Endurance");
            PrintAttributeValue(x + 17, y + 8, character.Endurance);
            TCODConsole.root.print(x + 3, y + 9, "Hvy Firearms");
            PrintSkillValue(x + 17, y + 9, character.HeavyFirearms);
            TCODConsole.root.print(x + 3, y + 10, "Armor Use");
            PrintSkillValue(x + 17, y + 10, character.ArmorUse);
            TCODConsole.root.print(x + 3, y + 11, "Exert");
            PrintSkillValue(x + 17, y + 11, character.Exert);

            TCODConsole.root.print(x + 21, y + 3, "Agility");
            PrintAttributeValue(x + 38, y + 3, character.Agility);
            TCODConsole.root.print(x + 22, y + 4, "Small Melee");
            PrintSkillValue(x + 38, y + 4, character.SmallMeleeWeapons);
            TCODConsole.root.print(x + 22, y + 5, "Lgt Firearms");
            PrintSkillValue(x + 38, y + 5, character.LightFirearms);
            TCODConsole.root.print(x + 22, y + 6, "Stealth");
            PrintSkillValue(x + 38, y + 6, character.Stealth);

            TCODConsole.root.print(x + 21, y + 8, $"Will");
            PrintAttributeValue(x + 38, y + 8, character.Will);
            TCODConsole.root.print(x + 22, y + 9, "Biotics Afnty");
            PrintSkillValue(x + 38, y + 9, character.BioticsAffinity);
            TCODConsole.root.print(x + 22, y + 10, "Unarmed Combat");
            PrintSkillValue(x + 38, y + 10, character.UnarmedCombat);
            TCODConsole.root.print(x + 22, y + 11, "Focus");
            PrintSkillValue(x + 38, y + 11, character.Focus);

            TCODConsole.root.print(x + 42, y + 3, "Reason");
            PrintAttributeValue(x + 57, y + 3, character.Reason);
            TCODConsole.root.print(x + 43, y + 4, "Engineering");
            PrintSkillValue(x + 57, y + 4, character.Engineering);
            TCODConsole.root.print(x + 43, y + 5, "Repairing");
            PrintSkillValue(x + 57, y + 5, character.Repairing);
            TCODConsole.root.print(x + 43, y + 6, "Healing");
            PrintSkillValue(x + 57, y + 6, character.Healing);

            TCODConsole.root.print(x + 42, y + 8, $"Intuition");
            PrintAttributeValue(x + 57, y + 8, character.Intuition);
            TCODConsole.root.print(x + 43, y + 9, "Dodge");
            PrintSkillValue(x + 57, y + 9, character.Dodge);
            TCODConsole.root.print(x + 43, y + 10, "Awareness");
            PrintSkillValue(x + 57, y + 10, character.Awareness);
            TCODConsole.root.print(x + 43, y + 11, "Sense Motive");
            PrintSkillValue(x + 57, y + 11, character.SenseMotive);
        }

        private void PrintAttributeValue(int x, int y, int value) {
            var foregroundColor = TCODConsole.root.getForegroundColor();
            if(value <= 3) {
                TCODConsole.root.setForegroundColor(TCODColor.red);
            } else if(value == 4) {
                TCODConsole.root.setForegroundColor(TCODColor.yellow);
            } else if(value > 4) {
                TCODConsole.root.setForegroundColor(TCODColor.darkGreen);
            }
            TCODConsole.root.print(x, y, $"{value}");
            TCODConsole.root.setForegroundColor(foregroundColor);
        }

        private void PrintSkillValue(int x, int y, SkillRank value) {
            var foregroundColor = TCODConsole.root.getForegroundColor();
            if(value == SkillRank.F) {
                TCODConsole.root.setForegroundColor(TCODColor.lightRed);
            } else if(value == SkillRank.E) {
                TCODConsole.root.setForegroundColor(TCODColor.yellow);
            } else {
                TCODConsole.root.setForegroundColor(TCODColor.green);
            }
            TCODConsole.root.print(x, y, $"{value}");
            TCODConsole.root.setForegroundColor(foregroundColor);
        }

        private void ColorTableRows(int x, int y) {
            for(int xOffset = 1; xOffset <= 58; ++xOffset) {
                for(int yOffset = 1; yOffset <= 11; ++yOffset) {
                    if(yOffset == 3 || yOffset == 8) {
                        if(xOffset != 17 && xOffset != 38 && xOffset != 57 && xOffset != 19 && xOffset != 40) {
                            TCODConsole.root.setCharForeground(x + xOffset, y + yOffset, new TCODColor(51, 51, 51));
                        }
                        TCODConsole.root.setCharBackground(x + xOffset, y + yOffset, new TCODColor(191, 191, 191));
                    } else if(yOffset == 4 || yOffset == 6 || yOffset == 10) {
                        TCODConsole.root.setCharBackground(x + xOffset, y + yOffset, new TCODColor(77, 77, 77));
                    }
                }
            }
        }
	}
}