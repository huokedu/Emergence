using libtcod;

namespace Emergence.States.MainMenu {
	public class Cloud {
		public float X { get; set; }
		public int Y { get; set; }
		public TCODColor Color { get; set; }
		public float Speed { get; set; }

		public void Update(float deltaTime) {
			X -= deltaTime * Speed;
			if(X < 0) {
				X = TCODConsole.root.getWidth() - 4;
			}
		}
		public void Render() {
			TCODConsole.root.setForegroundColor(Color);
			TCODConsole.root.putChar((int)X, Y, (int)TCODSpecialCharacter.Block1);
			TCODConsole.root.putChar((int)X + 1, Y, (int)TCODSpecialCharacter.Block1);
			TCODConsole.root.putChar((int)X + 2, Y, (int)TCODSpecialCharacter.Block1);
			TCODConsole.root.putChar((int)X + 3, Y, (int)TCODSpecialCharacter.Block1);
			TCODConsole.root.putChar((int)X + 1, Y - 1, (int)TCODSpecialCharacter.Block1);
			TCODConsole.root.putChar((int)X + 2, Y - 1, (int)TCODSpecialCharacter.Block1);
		}
	}
}