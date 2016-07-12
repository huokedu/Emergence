using System.Collections.Generic;
using Emergence.Core;
using Emergence.Entities.Character;
using Emergence.Utilities;
using libtcod;

namespace Emergence.Scenes.Personnel {
    public class PersonnelBioticsScene : BaseScene {
        BaseScene PreviousScene { get; set; }
        List<Character> Characters { get; set; }
        Ui.UiLayout UiLayout { get; set; }
        Ui.UiList<Character> CharacterList { get; set; }

        public PersonnelBioticsScene(Game game, BaseScene previousScene, List<Character> characters, int selected = 0) : base(game) {
            PreviousScene = previousScene;
            Characters = characters;
            UiLayout = Ui.UiLayout.Load("Assets/Layouts/CharacterScreenBiotics.ui");
            CharacterList = new Ui.UiList<Character>(Characters, RenderCharacterListItem);
            CharacterList.SelectedIndex = selected;
        }

        public override void Render(float deltaTime) {
            TCODConsole.root.setForegroundColor(TCODColor.white);
            TCODConsole.root.setBackgroundColor(TCODColor.darkerGrey);
            UiLayout.Render(0, 0);

            RenderCharacterList();
            RenderCharacterName();
            RenderNotes();
            RenderBioticImplantDescription();
            RenderPager();
        }
        public override void Update(float deltaTime) {
            
        }
        public override void KeyPressed(TCODKey keyData) {
            if(keyData.KeyCode == TCODKeyCode.Up) {
                CharacterList.SelectedIndex -= 1;
            } else if(keyData.KeyCode == TCODKeyCode.Down) {
                CharacterList.SelectedIndex += 1;
            } else if(keyData.KeyCode == TCODKeyCode.Escape) {
                Game.ChangeScene(PreviousScene);
            } else if(keyData.KeyCode == TCODKeyCode.Tab) {
                if(!keyData.Shift) {
                    Game.ChangeScene(new PersonnelSkillsScene(Game, PreviousScene, Characters, CharacterList.SelectedIndex));
                } else {
                    Game.ChangeScene(new PersonnelWoundsScene(Game, PreviousScene, Characters, CharacterList.SelectedIndex));
                }
            }
        }

        private void RenderCharacterList() {
            var offset = UiLayout.GetPoint("characterList");
            CharacterList.Render(offset);
            
            offset = UiLayout.GetPoint("scrollBarTop");
            CharacterList.RenderScrollBar(offset, 21);

            offset = UiLayout.GetPoint("personnelTab");
            TCODConsole.root.setForegroundColor(TCODColor.white);
            TCODConsole.root.print(offset.X, offset.Y, 
                $"Personnel {(char)TCODSpecialCharacter.ArrowSouth}/{(char)TCODSpecialCharacter.ArrowNorth}");
        }
        private void RenderCharacterListItem(Point point, Character character, bool isSelected) {
            var characterName = character.Name.ToString("{f}. {L}");
            if(isSelected) {
                TCODConsole.root.setForegroundColor(TCODColor.white);
                TCODConsole.root.putChar(point.X, point.Y, (char)TCODSpecialCharacter.ArrowEast);
                TCODConsole.root.putChar(point.X + characterName.Length + 1, point.Y, (char)TCODSpecialCharacter.ArrowWest);
            } else {
                TCODConsole.root.setForegroundColor(TCODColor.grey);
            }
            TCODConsole.root.print(point.X + 1, point.Y, characterName);
        }
        private void RenderCharacterName() {
            var position = UiLayout.GetPoint("characterName");
            var characterName = CharacterList.Selected.Name.ToString("{F} {N}{?}{L}");
            TCODConsole.root.setForegroundColor(TCODColor.white);
            TCODConsole.root.printEx(position.X, position.Y,
                TCODBackgroundFlag.Set, TCODAlignment.CenterAlignment, characterName);
            TCODConsole.root.hline(position.X - (characterName.Length / 2), position.Y + 1, characterName.Length);
        }
        private void RenderNotes() {
            var offset = UiLayout.GetPoint("noteLabel");
            TCODConsole.root.print(offset.X, offset.Y + 1, "Press [Escape] to go back.");
        }
        private void RenderBioticImplantDescription() {
            var tabPosition = UiLayout.GetPoint("bioticsTab");
            var descriptionPosition = UiLayout.GetPoint("description");
            TCODConsole.root.print(tabPosition.X, tabPosition.Y, "Biotics +/-");
            TCODConsole.root.print(descriptionPosition.X, descriptionPosition.Y,
                "Biotic Implants Not Yet Implemented - Coming Soon(TM)");
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

        private void RenderAttributeCategory(AttributeCategory attributeCategory, Point labelPosition, Point valuePosition) {
            var name = attributeCategory.GetCategoryStatName();
            TCODConsole.root.print(labelPosition.X, labelPosition.Y, name);
            var value = attributeCategory == AttributeCategory.Physical 
                ? CharacterList.Selected.Fitness
                : CharacterList.Selected.MentalAcuity;
            var x = value >= 10 ? valuePosition.X - 1 : valuePosition.X;
            TCODConsole.root.print(x, valuePosition.Y, value.ToString());
        }
        private void RenderAttribute(Attribute attribute, Point labelPosition, Point valuePosition) {
            var name = attribute.GetName();
            TCODConsole.root.print(labelPosition.X, labelPosition.Y, name);
            var value = CharacterList.Selected.Attributes[attribute];
            var x = value >= 10 ? valuePosition.X - 1 : valuePosition.X;
            TCODConsole.root.print(x, valuePosition.Y, value.ToString());
        }
        private void RenderSkill(Skill skill, Point labelPosition, Point valuePosition) {
            var name = skill.GetName();
            TCODConsole.root.print(labelPosition.X, labelPosition.Y, name);
            var value = CharacterList.Selected.Skills[skill].ToString();
            TCODConsole.root.print(valuePosition.X, valuePosition.Y, value);
        }

        private void ColorLine(int x, int y, int length, TCODColor foreground, TCODColor background) {
            for(int i = 0; i < length; ++i) {
                if(foreground != null) {
                    TCODConsole.root.setForegroundColor(foreground);
                }
                if(background != null) {
                    TCODConsole.root.setBackgroundColor(background);
                }
            }
        }
    }
}
