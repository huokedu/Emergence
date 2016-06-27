using Emergence.Core;
using Emergence.Scenes;

namespace Emergence {
	internal class Program {
		private static void Main(string[] args) {
			var game = new Game();
			game.Initialize(new SplashscreenScene(game));
			game.Start();
		}
	}
}