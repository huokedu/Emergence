﻿using System.Collections.Generic;
using Emergence.Core;
using Emergence.Entities.Personnel;
using Emergence.Utilities;
using libtcod;

namespace Emergence.Scenes.Personnel {
    public class PersonnelSkillsScene : BaseScene {
        BaseScene PreviousScene { get; set; }
        List<Character> Characters { get; set; }
        Ui.UiLayout UiLayout { get; set; }
        Ui.UiList<Character> CharacterList { get; set; }

        public PersonnelSkillsScene(Game game, BaseScene previousScene, List<Character> characters, int selected = 0) : base(game) {
            PreviousScene = previousScene;
            Characters = characters;
            UiLayout = Ui.UiLayout.Load("Assets/Layouts/CharacterScreenSkills.ui");
            CharacterList = new Ui.UiList<Character>(Characters, RenderCharacterListItem);
            CharacterList.SelectedIndex = selected;
        }

        public override void Render(float deltaTime) {
            TCODConsole.root.setForegroundColor(TCODColor.white);
            TCODConsole.root.setBackgroundColor(TCODColor.darkerGrey);
            UiLayout.Render(0, 0);

            RenderCharacterList();
            RenderCharacterName();
            RenderCharacterExp();
            RenderCharacterPhysicalSkills();
            RenderCharacterMentalSkills();
            RenderNotes();
            RenderSkillDescription();
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
                    Game.ChangeScene(new PersonnelWoundsScene(Game, PreviousScene, Characters, CharacterList.SelectedIndex));
                } else {
                    Game.ChangeScene(new PersonnelBioticsScene(Game, PreviousScene, Characters, CharacterList.SelectedIndex));
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
        private void RenderCharacterExp() {
            var labelPosition = UiLayout.GetPoint("experienceLabel");
            var numberPosition = UiLayout.GetPoint("experienceValue");
            var expString = CharacterList.Selected.ExperiencePoints.ToString("D4");

            TCODConsole.root.print(labelPosition.X, labelPosition.Y, "Exp.");
            TCODConsole.root.print(numberPosition.X, numberPosition.Y, expString);
        }
        private void RenderCharacterPhysicalSkills() {
            var character = CharacterList.Selected;

            var labelPosition = UiLayout.GetPoint("fitnessLabel");
            var valuePosition = UiLayout.GetPoint("fitnessValue");
            RenderAttributeCategory(AttributeCategory.Physical, labelPosition, valuePosition);

            labelPosition = UiLayout.GetPoint("strengthLabel");
            valuePosition = UiLayout.GetPoint("strengthValue");
            RenderAttribute(Attribute.Strength, labelPosition, valuePosition);
            labelPosition = new Point(labelPosition.X + 1, labelPosition.Y + 1);
            valuePosition = new Point(valuePosition.X, valuePosition.Y + 1);
            RenderSkill(Skill.LargeMeleeWeapons, labelPosition, valuePosition);
            labelPosition.Y += 1; valuePosition.Y += 1;
            RenderSkill(Skill.ThrownWeapons, labelPosition, valuePosition);
            labelPosition.Y += 1; valuePosition.Y += 1;
            RenderSkill(Skill.Bash, labelPosition, valuePosition);

            labelPosition = UiLayout.GetPoint("enduranceLabel");
            valuePosition = UiLayout.GetPoint("enduranceValue");
            RenderAttribute(Attribute.Endurance, labelPosition, valuePosition);
            labelPosition = new Point(labelPosition.X + 1, labelPosition.Y + 1);
            valuePosition = new Point(valuePosition.X, valuePosition.Y + 1);
            RenderSkill(Skill.HeavyFirearms, labelPosition, valuePosition);
            labelPosition.Y += 1; valuePosition.Y += 1;
            RenderSkill(Skill.ArmorUse, labelPosition, valuePosition);
            labelPosition.Y += 1; valuePosition.Y += 1;
            RenderSkill(Skill.Exert, labelPosition, valuePosition);

            labelPosition = UiLayout.GetPoint("agilityLabel");
            valuePosition = UiLayout.GetPoint("agilityValue");
            RenderAttribute(Attribute.Agility, labelPosition, valuePosition);
            labelPosition = new Point(labelPosition.X + 1, labelPosition.Y + 1);
            valuePosition = new Point(valuePosition.X, valuePosition.Y + 1);
            RenderSkill(Skill.SmallMeleeWeapons, labelPosition, valuePosition);
            labelPosition.Y += 1; valuePosition.Y += 1;
            RenderSkill(Skill.LightFirearms, labelPosition, valuePosition);
            labelPosition.Y += 1; valuePosition.Y += 1;
            RenderSkill(Skill.Stealth, labelPosition, valuePosition);
        }
        private void RenderCharacterMentalSkills() {
            var character = CharacterList.Selected;

            var labelPosition = UiLayout.GetPoint("mentalAcuityLabel");
            var valuePosition = UiLayout.GetPoint("mentalAcuityValue");
            TCODConsole.root.print(labelPosition.X, labelPosition.Y, "Mental Acuity");
            TCODConsole.root.print(valuePosition.X, valuePosition.Y, character.MentalAcuity.ToString());

            labelPosition = UiLayout.GetPoint("willLabel");
            valuePosition = UiLayout.GetPoint("willValue");
            RenderAttribute(Attribute.Will, labelPosition, valuePosition);
            labelPosition = new Point(labelPosition.X + 1, labelPosition.Y + 1);
            valuePosition = new Point(valuePosition.X, valuePosition.Y + 1);
            RenderSkill(Skill.BioticsAffinity, labelPosition, valuePosition);
            labelPosition.Y += 1; valuePosition.Y += 1;
            RenderSkill(Skill.UnarmedCombat, labelPosition, valuePosition);
            labelPosition.Y += 1; valuePosition.Y += 1;
            RenderSkill(Skill.Focus, labelPosition, valuePosition);

            labelPosition = UiLayout.GetPoint("reasonLabel");
            valuePosition = UiLayout.GetPoint("reasonValue");
            RenderAttribute(Attribute.Reason, labelPosition, valuePosition);
            labelPosition = new Point(labelPosition.X + 1, labelPosition.Y + 1);
            valuePosition = new Point(valuePosition.X, valuePosition.Y + 1);
            RenderSkill(Skill.Engineering, labelPosition, valuePosition);
            labelPosition.Y += 1; valuePosition.Y += 1;
            RenderSkill(Skill.Repairing, labelPosition, valuePosition);
            labelPosition.Y += 1; valuePosition.Y += 1;
            RenderSkill(Skill.Healing, labelPosition, valuePosition);

            labelPosition = UiLayout.GetPoint("intuitionLabel");
            valuePosition = UiLayout.GetPoint("intuitionValue");
            RenderAttribute(Attribute.Intuition, labelPosition, valuePosition);
            labelPosition = new Point(labelPosition.X + 1, labelPosition.Y + 1);
            valuePosition = new Point(valuePosition.X, valuePosition.Y + 1);
            RenderSkill(Skill.Dodge, labelPosition, valuePosition);
            labelPosition.Y += 1; valuePosition.Y += 1;
            RenderSkill(Skill.Awareness, labelPosition, valuePosition);
            labelPosition.Y += 1; valuePosition.Y += 1;
            RenderSkill(Skill.SenseMotive, labelPosition, valuePosition);
        }
        private void RenderNotes() {
            var offset = UiLayout.GetPoint("noteLabel");
            TCODConsole.root.print(offset.X, offset.Y, "Press [Escape] to go back.");
            TCODConsole.root.print(offset.X, offset.Y + 1, "Press [Enter] to improve a skill.");
        }
        private void RenderSkillDescription() {
            var tabPosition = UiLayout.GetPoint("skillsTab");
            var descriptionPosition = UiLayout.GetPoint("description");
            TCODConsole.root.print(tabPosition.X, tabPosition.Y, "Skills +/-");
            TCODConsole.root.print(descriptionPosition.X, descriptionPosition.Y,
                "Skill Descriptions Not Yet Implemented - Coming Soon(TM)");
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
