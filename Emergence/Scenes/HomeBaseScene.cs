﻿using System;
using System.Collections.Generic;
using System.Linq;
using Emergence.Core;
using Emergence.Entities;
using Emergence.Entities.HomeBase;
using Emergence.Entities.HomeBase.Tasks;
using Emergence.Scenes.MainMenu;
using Emergence.Scenes.Personnel;
using Emergence.Ui;
using Emergence.Utilities;
using libtcod;

namespace Emergence.Scenes {
	public class HomeBaseScene : BaseScene {
        private HomeBase homeBase;
		private TCODConsole baseImage;
		private TCODConsole labels;
        private int roomSelectionX, roomSelectionY;
		private int cameraX, cameraY;
		private bool roomLabelsEnabled;

		public HomeBaseScene(Game game) : base(game) {
			roomLabelsEnabled = false;

            homeBase = new HomeBase(25, 25);
            homeBase.Rooms[1, 2] = new Room(homeBase, 1, 2, RoomType.LivingQuarters);
            homeBase.Rooms[2, 2] = new Room(homeBase, 2, 2, RoomType.Kitchen);
            homeBase.Rooms[3, 2] = new Room(homeBase, 3, 2, RoomType.Gym);
            homeBase.Rooms[2, 1] = new Room(homeBase, 2, 1, RoomType.FiringRange);
            homeBase.Rooms[2, 3] = new Room(homeBase, 2, 3, RoomType.Generator);

            homeBase.Rooms[1, 2].AddExit(Direction.East, homeBase.Rooms[2, 2]);
            homeBase.Rooms[3, 2].AddExit(Direction.West, homeBase.Rooms[2, 2]);
            homeBase.Rooms[2, 1].AddExit(Direction.South, homeBase.Rooms[2, 2]);
            homeBase.Rooms[2, 3].AddExit(Direction.North, homeBase.Rooms[2, 2]);


            baseImage = RexPaintImageLoader
				.LoadImage("Assets/HomeBase/base.xp");
			labels = RexPaintImageLoader
				.LoadImage("Assets/HomeBase/labels.xp");
			cameraX = cameraY = 0;
            roomSelectionX = 1;
            roomSelectionY = 1;
            MoveRoomSelection(0, 0);
		}

		public override void Render(float deltaTime) {
			TCODConsole.root.setBackgroundColor(TCODColor.black);
			TCODConsole.root.clear();
            homeBase.Render(-cameraX, -cameraY, roomLabelsEnabled);
            RenderRoomSelection();
			RenderMainPanel();
		}
		public override void Update(float deltaTime) {
		}
		public override void KeyPressed(TCODKey keyData) {
			switch(keyData.KeyCode) {
				case TCODKeyCode.Up:
					MoveRoomSelection(0, -1);
					break;
				case TCODKeyCode.Down:
                    MoveRoomSelection(0, 1);
                    break;
				case TCODKeyCode.Left:
                    MoveRoomSelection(-1, 0);
                    break;
				case TCODKeyCode.Right:
                    MoveRoomSelection(1, 0);
                    break;
			}
            if(keyData.KeyCode == TCODKeyCode.Enter) {
                SelectRoom();
                return;
            }
			switch(char.ToUpper(keyData.Character)) {
				case 'P': // [P]ersonnel
                    Game.ChangeScene(new PersonnelSkillsScene(Game, this, Game.State.Personnel));
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
				case 'T': // [T]ask Board
                    Game.ChangeScene(new TaskBoardScene(Game, this));
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
                    EndDay();
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

        private void MoveRoomSelection(int deltaX, int deltaY) {
            roomSelectionX += deltaX;
            roomSelectionY += deltaY;
            if(roomSelectionX < 0) roomSelectionX = 0;
            if(roomSelectionX >= homeBase.Width)
                roomSelectionX = homeBase.Width - 1;
            if(roomSelectionY < 0) roomSelectionY = 0;
            if(roomSelectionY >= homeBase.Height)
                roomSelectionY = homeBase.Height - 1;
            
            // Update camera to loosely follow the room selection
            var roomScreenX = (roomSelectionX * 11) - cameraX;
            var roomScreenY = (roomSelectionY * 11) - cameraY;

            if(roomScreenX < Game.Settings.ScreenWidth / 4) {
                cameraX -= (Game.Settings.ScreenWidth / 4) - roomScreenX;
            } else if(roomScreenX + 11 > Game.Settings.ScreenWidth * 3 /4) {
                cameraX += (roomScreenX + 11) - (Game.Settings.ScreenWidth * 3 / 4);
            }
            if(roomScreenY < Game.Settings.ScreenHeight / 4) {
                cameraY -= (Game.Settings.ScreenHeight / 4) - roomScreenY;
            } else if(roomScreenY + 11 > Game.Settings.ScreenHeight * 3 / 4) {
                cameraY += (roomScreenY + 11) - (Game.Settings.ScreenHeight * 3 / 4);
            }
        }
        private void RenderRoomSelection() {
            int roomScreenX = (roomSelectionX * 11) - cameraX;
            int roomScreenY = (roomSelectionY * 11) - cameraY;
            TCODConsole.root.setForegroundColor(TCODColor.desaturatedGreen);
            TCODConsole.root.printFrame(roomScreenX, roomScreenY, 11, 11, false, TCODBackgroundFlag.None);
        }
        private void SelectRoom() {
            var room = homeBase.Rooms[roomSelectionX, roomSelectionY];

            var validTasks = homeBase.GetValidTasks(roomSelectionX, roomSelectionY);
            if(validTasks == null || validTasks.Count == 0) return;
            var validTaskNames = validTasks.Select(t => t.Title).ToArray();

            var selectedTaskName = new BlockingOptionModal() {
                Foreground = TCODColor.grey,
                Background = TCODColor.black,
                Message = room.RoomType.GetName(),
                Options = validTaskNames
            }.Show();
            var selectedTask = validTasks.FirstOrDefault(t => t.Title == selectedTaskName);
            if(selectedTask == null) return;

            var options = selectedTask.GetOptions(room);
            if(options == null || options.Length == 0) {
                Game.State.Tasks.Add(selectedTask.CreateTask(room, null));
                return;
            }

            var selectedOption = new BlockingOptionModal() {
                Foreground = TCODColor.grey,
                Background = TCODColor.black,
                Message = $"{room.RoomType.GetName()} - {selectedTaskName}",
                Options = options
            }.Show();
            
            Game.State.Tasks.Add(selectedTask.CreateTask(room, selectedOption));
        }

        private void EndDay() {
            Game.State.Tasks.RemoveAll(t => t.Done);
            Game.State.Tasks.ForEach(t => t.Update());
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
            TCODConsole.root.print(23, 5, $"[Enter] Select Room");
            TCODConsole.root.print(23, 6, $"[T]ask Board");
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
