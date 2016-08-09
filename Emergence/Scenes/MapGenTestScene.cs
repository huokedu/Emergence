using System;
using Emergence.Core;
using Emergence.Entities.ExcursionMap;
using libtcod;

namespace Emergence.Scenes {
    public class MapGenTestScene : BaseScene {
        public ExcursionMap Map { get; set; }

        public MapGenTestScene(Game game) : base(game) {
            Map = new ExcursionMap(game.Settings.ScreenWidth, game.Settings.ScreenHeight);
        }

        public override void Render(float deltaTime) {
            Map.Render(0, 0);
        }

        public override void Update(float deltaTime) {
        }

        public override void KeyPressed(TCODKey keyData) {
            if(keyData.KeyCode == TCODKeyCode.Space) {
                Map.Generate(Game.Settings.ScreenWidth, Game.Settings.ScreenHeight);
            }
        }
    }
}
