using System;
using System.IO;
using Emergence.Scenes;
using libtcod;
using Newtonsoft.Json;

namespace Emergence.Core {
	public class Game {
		private TCODMouseData previousMouseData;

		public SettingsContract Settings { get; private set; }

		private string windowTitle;
		public string WindowTitle
		{
			get { return windowTitle; }
			set
			{
				windowTitle = value;
				TCODConsole.setWindowTitle(value);
			}
		}

		private bool isRunning;

		public BaseScene CurrentScene { get; private set; }
		private BaseScene newScene { get; set; }
		private bool sceneChanged { get; set; }

		public Game() {
			isRunning = false;
			newScene = null;
			sceneChanged = false;
		}

		public void Initialize(BaseScene initialScene = null) {
			Logger.Initialize();
			windowTitle = "Emergence";
			Settings = LoadSettings("Assets/config.json");
			if(Settings == null) {
				throw new Exception("Error loading config file.");
			}

			TCODConsole.setCustomFont($"Assets/Fonts/{Settings.Font}",
				(int)TCODFontFlags.LayoutAsciiInRow);
			TCODSystem.setFps(Settings.Fps);
			TCODConsole.initRoot(Settings.ScreenWidth, Settings.ScreenHeight,
				windowTitle, false, Settings.GetRendererType());
			previousMouseData = TCODMouse.getStatus();
			CurrentScene = initialScene;
		}
		private SettingsContract LoadSettings(string filename) {
			string jsonData = "";
			try {
				using(var reader = new StreamReader(filename)) {
					jsonData = reader.ReadToEnd();
					reader.Close();
				}
			} catch(Exception e) {
				Logger.Error($"Error reading config file: {e.Message}");
				return null;
			}

			SettingsContract settingsData = null;
			try {
				settingsData =
					JsonConvert.DeserializeObject<SettingsContract>(jsonData);
			} catch(Exception e) {
				Logger.Error($"Error parsing json from config file: {e.Message}");
				return null;
			}

			return settingsData;
		}

		public void Start() {
			isRunning = true;
			while(isRunning) {
				// Check for scene change
				if(sceneChanged) {
					CurrentScene = newScene;
					newScene = null;
					sceneChanged = false;
				}

				// Check if the game is still running.
				if(TCODConsole.isWindowClosed() || CurrentScene == null) {
					Stop();
					break;
				}

				// Update
				CurrentScene.Update(TCODSystem.getLastFrameLength());

				// Render
				TCODConsole.root.clear();
				CurrentScene.Render(TCODSystem.getLastFrameLength());
				TCODConsole.flush();

				// Handle user input
				CheckForKeyboardEvents();
				CheckForMouseEvents();
			}
			CleanUp();
		}
		public void ChangeScene(BaseScene newScene) {
			this.newScene = newScene;
			sceneChanged = true;
		}
		public void Stop() {
			isRunning = false;
		}
		private void CleanUp() {
			Logger.Close();
		}

		private void CheckForKeyboardEvents() {
			var key = TCODConsole.checkForKeypress(
				(int)TCODKeyStatus.KeyPressed |
				(int)TCODKeyStatus.KeyReleased);

			if (key.KeyCode != TCODKeyCode.NoKey) {
				if(key.Pressed) {
					CurrentScene.KeyPressed(key);
				} else {
					CurrentScene.KeyReleased(key);
				}
			}
		}
		private void CheckForMouseEvents() {
			var mouseData = TCODMouse.getStatus();
			if(mouseData.CellVelocityX != 0 || mouseData.CellVelocityY != 0) {
				CurrentScene.MouseMoved(mouseData);
			}
			if(mouseData.WheelDown || mouseData.WheelUp) {
				CurrentScene.MouseWheel(mouseData);
			}
			if(mouseData.LeftButton != previousMouseData.LeftButton) {
				CurrentScene.MouseLeftButton(mouseData);
			}
			if(mouseData.MiddleButton != previousMouseData.MiddleButton) {
				CurrentScene.MouseMiddleButton(mouseData);
			}
			if(mouseData.RightButton != previousMouseData.RightButton) {
				CurrentScene.MouseRightButton(mouseData);
			}
			previousMouseData = mouseData;
		}
	}
}