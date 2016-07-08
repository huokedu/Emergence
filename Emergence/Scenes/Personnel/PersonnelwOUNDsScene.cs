using System.Collections.Generic;
using Emergence.Core;
using Emergence.Entities.Character;
using Emergence.Utilities;
using libtcod;

namespace Emergence.Scenes.Personnel {
    public class PersonnelWoundsScene : BaseScene {
        BaseScene PreviousScene { get; set; }
        List<Character> Characters { get; set; }
        int SelectedCharacterIndex { get; set; }
        Ui.UiLayout UiLayout { get; set; }

        public PersonnelWoundsScene(Game game, BaseScene previousScene, List<Character> characters, int selected = 0) : base(game) {
            PreviousScene = previousScene;
            Characters = characters;
            UiLayout = Ui.UiLayout.Load("Assets/Layouts/CharacterScreenWounds.ui");
            SelectedCharacterIndex = selected;
        }

        public override void Render(float deltaTime) {
            TCODConsole.root.setForegroundColor(TCODColor.white);
            TCODConsole.root.setBackgroundColor(TCODColor.darkerGrey);
            UiLayout.Render(0, 0);

            RenderCharacterList();
            RenderCharacterName();
            RenderRecoveryDetails();
            RenderNotes();
            RenderWoundDescription();
            RenderPager();
        }
        public override void Update(float deltaTime) {
            
        }
        public override void KeyPressed(TCODKey keyData) {
            if(keyData.KeyCode == TCODKeyCode.Up) {
                SelectedCharacterIndex -= 1;
                if(SelectedCharacterIndex < 0) {
                    SelectedCharacterIndex = Characters.Count - 1;
                }
            } else if(keyData.KeyCode == TCODKeyCode.Down) {
                SelectedCharacterIndex += 1;
                if(SelectedCharacterIndex == Characters.Count) {
                    SelectedCharacterIndex = 0;
                }
            } else if(keyData.KeyCode == TCODKeyCode.Escape) {
                Game.ChangeScene(PreviousScene);
            } else if(keyData.KeyCode == TCODKeyCode.Tab) {
                if(!keyData.Shift) {
                    Game.ChangeScene(new PersonnelBioticsScene(Game, PreviousScene, Characters, SelectedCharacterIndex));
                } else {
                    Game.ChangeScene(new PersonnelSkillsScene(Game, PreviousScene, Characters, SelectedCharacterIndex));
                }
            }
        }

        private void RenderCharacterList() {
            var offset = UiLayout.GetPoint("characterList");
            var start = System.Math.Max(0, SelectedCharacterIndex - 9);
            for(int i = 0; i < 10; ++i) {
                var currentCharacterIndex = start + i;
                if(currentCharacterIndex >= Characters.Count) {
                    break;
                }
                var characterName = Characters[currentCharacterIndex].Name.ToString("{f}. {L}");

                if(currentCharacterIndex == SelectedCharacterIndex) {
                    TCODConsole.root.setForegroundColor(TCODColor.white);
                    TCODConsole.root.putChar(offset.X - 1, offset.Y + i * 2,
                        (char)TCODSpecialCharacter.ArrowEast);
                    TCODConsole.root.putChar(offset.X + characterName.Length, offset.Y + i * 2,
                        (char)TCODSpecialCharacter.ArrowWest);
                } else {
                    TCODConsole.root.setForegroundColor(TCODColor.grey);
                }

                TCODConsole.root.print(offset.X, offset.Y + i * 2, characterName);
            }

            offset = UiLayout.GetPoint("personnelTab");
            TCODConsole.root.setForegroundColor(TCODColor.white);
            TCODConsole.root.print(offset.X, offset.Y, 
                $"Personnel {(char)TCODSpecialCharacter.ArrowSouth}/{(char)TCODSpecialCharacter.ArrowNorth}");
        }
        private void RenderCharacterName() {
            var position = UiLayout.GetPoint("characterName");
            var characterName = Characters[SelectedCharacterIndex].Name.ToString("{F} {N}{?}{L}");
            TCODConsole.root.setForegroundColor(TCODColor.white);
            TCODConsole.root.printEx(position.X, position.Y,
                TCODBackgroundFlag.Set, TCODAlignment.CenterAlignment, characterName);
            TCODConsole.root.hline(position.X - (characterName.Length / 2), position.Y + 1, characterName.Length);
        }
        private void RenderRecoveryDetails() {
            var labelPosition = UiLayout.GetPoint("recoverDetailsLabel");
            TCODConsole.root.print(labelPosition.X, labelPosition.Y, "Recovery Details");

            labelPosition = UiLayout.GetPoint("severity");
            TCODConsole.root.print(labelPosition.X, labelPosition.Y, "Severity: Coming Soon (TM)");

            labelPosition = UiLayout.GetPoint("status");
            TCODConsole.root.print(labelPosition.X, labelPosition.Y, "Status: Coming Soon (TM)");

            labelPosition = UiLayout.GetPoint("statusDetails");
            TCODConsole.root.print(labelPosition.X, labelPosition.Y, "Status Details...");
            TCODConsole.root.print(labelPosition.X, labelPosition.Y + 1, "Coming Soon (TM)");

            labelPosition = UiLayout.GetPoint("timeToHealLabel");
            TCODConsole.root.print(labelPosition.X, labelPosition.Y, "Time to Heal:");
            labelPosition = UiLayout.GetPoint("timeToHealValue");
            TCODConsole.root.print(labelPosition.X, labelPosition.Y, "NN Days");
        }
        private void RenderNotes() {
            var offset = UiLayout.GetPoint("noteLabel");
            TCODConsole.root.print(offset.X, offset.Y + 1, "Press [Escape] to go back.");
        }
        private void RenderWoundDescription() {
            var tabPosition = UiLayout.GetPoint("woundsTab");
            var descriptionPosition = UiLayout.GetPoint("description");
            TCODConsole.root.print(tabPosition.X, tabPosition.Y, "Wounds +/-");
            TCODConsole.root.print(descriptionPosition.X, descriptionPosition.Y,
                "Wounds Not Yet Implemented - Coming Soon(TM)");
        }
        private void RenderPager() {
            var tabPosition = UiLayout.GetPoint("pagesLabel");
            TCODConsole.root.print(tabPosition.X, tabPosition.Y, "Pages [Tab]");
            var skillsPageLabel = UiLayout.GetPoint("skillPageLabel");
            TCODConsole.root.print(skillsPageLabel.X, skillsPageLabel.Y, "Skills");
            var woundsPageLabel = UiLayout.GetPoint("woundsPageLabel");
            TCODConsole.root.print(woundsPageLabel.X, woundsPageLabel.Y, "Wounds");
            var bioticsPageLabel = UiLayout.GetPoint("bioticsPageLabel");
            TCODConsole.root.print(bioticsPageLabel.X, bioticsPageLabel.Y, "Biotics");
        }
    }
}
