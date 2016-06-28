using System;
using System.Linq;
using libtcod;

namespace Emergence.Ui {
	public class BlockingMessageModal {
		public TCODColor Foreground { get; set; }
		public TCODColor Background { get; set; }
		public string[] MessageLines { get; set; }

		public BlockingMessageModal(TCODColor foreground, TCODColor background, params string[] messageLines) {
			Foreground = foreground;
			Background = background;
			MessageLines = messageLines;
		}

		public void Show() {
			var longestLine = MessageLines.Max(line => line.Length);
			var x = (TCODConsole.root.getWidth() / 2) - (longestLine / 2 + 2);
			var width = Math.Max(13, longestLine + 4); // Two for padding and two for the border
			var y = (TCODConsole.root.getHeight() / 2) - (MessageLines.Length / 2 + 2);
			var height = MessageLines.Length + 4; // Two for padding and two for the border

			TCODConsole.root.setForegroundColor(Foreground);
			TCODConsole.root.setBackgroundColor(Background);
			TCODConsole.root.printFrame(x, y, width, height, true, TCODBackgroundFlag.Set);
			for(int i = 0; i < MessageLines.Length; ++i) {
				TCODConsole.root.print(x + 2, y + 2 + i, MessageLines[i]);
			}
			TCODConsole.flush();
			TCODConsole.waitForKeypress(true);
		}
	}
}