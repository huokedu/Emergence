using libtcod;

namespace Emergence.Scenes.MainMenu {
	public class Star {
		public int X { get; set; }
		public int Y { get; set; }
		public float Brightness { get; set; }
		public bool Waxing { get; set; }

		public void Update(float deltaTime) {
			float deltaBrightness = 0.2f * deltaTime;
			if(Waxing) {
				Brightness += deltaBrightness;
				if(Brightness > 1.0f) {
					Brightness = 1.0f;
					Waxing = false;
				}
			} else {
				Brightness -= deltaBrightness;
				if(Brightness < 0.0f) {
					Brightness = 0.0f;
					Waxing = true;
				}
			}
		}
		public void Render(TCODColor backgroundColor) {
			var color = TCODColor.Interpolate(TCODColor.white, backgroundColor, Brightness);
			TCODConsole.root.setForegroundColor(color);
			TCODConsole.root.putChar(X, Y, '*');
		}
	}
}