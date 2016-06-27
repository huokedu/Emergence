using System;
using System.IO;
using Emergence.States;
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

		public BaseState CurrentState { get; private set; }
		private BaseState newState { get; set; }
		private bool stateChanged { get; set; }

		public Game() {
			isRunning = false;
			newState = null;
			stateChanged = false;
		}

		public void Initialize(BaseState initialState = null) {
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
			CurrentState = initialState;
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
				// Check for state change
				if(stateChanged) {
					CurrentState = newState;
					newState = null;
					stateChanged = false;
				}

				// Check if the game is still running.
				if(TCODConsole.isWindowClosed() || CurrentState == null) {
					Stop();
					break;
				}

				// Update
				CurrentState.Update(TCODSystem.getLastFrameLength());

				// Render
				TCODConsole.root.clear();
				CurrentState.Render(TCODSystem.getLastFrameLength());
				TCODConsole.flush();

				// Handle user input
				CheckForKeyboardEvents();
				CheckForMouseEvents();
			}
			CleanUp();
		}
		public void ChangeState(BaseState newState) {
			this.newState = newState;
			stateChanged = true;
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
					CurrentState.KeyPressed(key);
				} else {
					CurrentState.KeyReleased(key);
				}
			}
		}
		private void CheckForMouseEvents() {
			var mouseData = TCODMouse.getStatus();
			if(mouseData.CellVelocityX != 0 || mouseData.CellVelocityY != 0) {
				CurrentState.MouseMoved(mouseData);
			}
			if(mouseData.WheelDown || mouseData.WheelUp) {
				CurrentState.MouseWheel(mouseData);
			}
			if(mouseData.LeftButton != previousMouseData.LeftButton) {
				CurrentState.MouseLeftButton(mouseData);
			}
			if(mouseData.MiddleButton != previousMouseData.MiddleButton) {
				CurrentState.MouseMiddleButton(mouseData);
			}
			if(mouseData.RightButton != previousMouseData.RightButton) {
				CurrentState.MouseRightButton(mouseData);
			}
			previousMouseData = mouseData;
		}
	}
}