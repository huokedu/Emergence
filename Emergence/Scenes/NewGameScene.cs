using Emergence.Core;
using Emergence.Scenes.HomeBase;
using Emergence.Utilities;
using libtcod;

namespace Emergence.Scenes {
	public class NewGameScene : BaseScene {
		private string[] placeholderMessage;

		public NewGameScene(Game game) : base(game) {
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
			// TODO: Generate character(s) and go to next scene.
			var male = NameGenerator.GenerateName("Male");
			var female = NameGenerator.GenerateName("Female");
			var last = NameGenerator.GenerateName("Last");
			new BlockingMessageModal(TCODColor.white, TCODColor.black, 
				$"Male: {male}", $"Female: {female}", $"Last: {last}").Show();
			Game.ChangeScene(new HomeBaseScene(Game));
		}
	}
}