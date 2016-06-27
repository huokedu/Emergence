using Emergence.Core;
using Emergence.States.HomeBase;
using Emergence.Utilities;
using libtcod;

namespace Emergence.States {
	public class NewGameState : BaseState {
		private string[] placeholderMessage;

		public NewGameState(Game game) : base(game) {
			placeholderMessage = new string[] {
				"Character creation has not been implemented yet.",
				"Your character will be generated at random.",
				"Press any key to continue."
			};
		}

		public override void Update(float deltaTime) {
		}
		public override void Render(float deltaTime) {
			for(int i = 0; i < placeholderMessage.Length; ++i) {
				var x = (TCODConsole.root.getWidth() / 2);
				var y = (TCODConsole.root.getHeight() / 2) - (placeholderMessage.Length / 2) + i;
				TCODConsole.root.printEx(x, y, TCODBackgroundFlag.Default,
					TCODAlignment.CenterAlignment, placeholderMessage[i]);
			}
		}

		public override void KeyPressed(TCODKey keyData) {
			// TODO: Generate character(s) and go to next state.
			var male = NameGenerator.GenerateName("Male");
			var female = NameGenerator.GenerateName("Female");
			var last = NameGenerator.GenerateName("Last");
			new BlockingMessageModal(TCODColor.white, TCODColor.black, 
				$"Male: {male}", $"Female: {female}", $"Last: {last}").Show();
			Game.ChangeState(new HomeBaseState(Game));
		}
	}
}