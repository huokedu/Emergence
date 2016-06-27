using System;
using libtcod;

namespace Emergence.Core {
	public class SettingsContract {
		public int ScreenWidth { get; set; }
		public int ScreenHeight { get; set; }
		public string Renderer { get; set; }
		public string Font { get; set; }
		public int Fps { get; set; }

		public TCODColor UiForeground { get; set; }
		public TCODColor UiBackground { get; set; }

		public TCODRendererType GetRendererType() {
			TCODRendererType? rendererType = null;
			try {
				rendererType =
					(TCODRendererType)System.Enum.Parse(
						typeof(TCODRendererType), Renderer, true);
			} catch(Exception e) {
				Logger.Error($"Error parsing renderer type: {e.Message}");
				Logger.Warning("Defaulting to SDL.");
			}
			return rendererType ?? TCODRendererType.SDL;
		}
	}
}