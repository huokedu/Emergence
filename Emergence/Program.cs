using Emergence.Core;
using Emergence.States;

namespace Emergence {
	internal class Program {
		private static void Main(string[] args) {
			var game = new Game();
			game.Initialize(new SplashscreenState(game));
			game.Start();
		}
	}
}