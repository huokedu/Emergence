using Emergence.Core;
using libtcod;

namespace Emergence.Scenes {
	public abstract class BaseScene {
		public Game Game { get; }

		public BaseScene(Game game) {
			Game = game;
		}

		public abstract void Update(float deltaTime);
		public abstract void Render(float deltaTime);

		public virtual void KeyPressed(TCODKey keyData) {
		}
		public virtual void KeyReleased(TCODKey keyData) {
		}

		public virtual void MouseMoved(TCODMouseData mouseData) {
		}
		public virtual void MouseWheel(TCODMouseData mouseData) {
		}
		public virtual void MouseLeftButton(TCODMouseData mouseData) {
		}
		public virtual void MouseMiddleButton(TCODMouseData mouseData) {
		}
		public virtual void MouseRightButton(TCODMouseData mouseData) {
		}
	}
}