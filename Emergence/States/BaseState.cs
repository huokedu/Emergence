using Emergence.Core;
using libtcod;

namespace Emergence.States {
	public abstract class BaseState {
		public Game Game { get; }

		public BaseState(Game game) {
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