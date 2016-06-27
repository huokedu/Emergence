using System;
using libtcod;

namespace Emergence.Utilities {
	public class BlockingConfirmationModal {
		public TCODColor Foreground { get; set; }
		public TCODColor Background { get; set; }
		public string Message { get; set; }

		public bool Show() {
			var x = (TCODConsole.root.getWidth() / 2) - (Message.Length / 2 + 2);
			var width = Math.Max(13, Message.Length + 4); // Two for padding and two for the border
			var y = (TCODConsole.root.getHeight() / 2) - 3;
			var height = 6;

			TCODConsole.root.setForegroundColor(Foreground);
			TCODConsole.root.setBackgroundColor(Background);
			TCODConsole.root.printFrame(x, y, width, height, true, TCODBackgroundFlag.Set);
			TCODConsole.root.print(x + 2, y + 2, Message);
			TCODConsole.root.print(x + (width / 2) - 4, y + 3, "[Y] / [N]");
			TCODConsole.flush();

			TCODKey key = null;
			var character = 'N';
			do {
				key = TCODConsole.waitForKeypress(true);
				character = char.ToUpper(key.Character);
			} while(character != 'Y' && character != 'N');

			return character == 'Y';
		}
	}
}