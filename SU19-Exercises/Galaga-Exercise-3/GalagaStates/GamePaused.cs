using DIKUArcade.Entities;
using DIKUArcade.State;
using DIKUArcade.Graphics;

namespace Galaga_Exercise_3.GalagaStates {
    public class GamePaused : IGameState {
        private static GamePaused instance = null;

        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeButton;
        private int maxButtons;
        
        
        public void GameLoop() {
        }

        public void InitializeGameState() {
            throw new System.NotImplementedException();
        }

        public void UpdateGameLogic() {
            throw new System.NotImplementedException();
        }

        public void RenderState() {
            throw new System.NotImplementedException();
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            switch (keyValue) {
            case "KEY_UP":
                if (activeButton == 1) {
                    activeButton = 0;
                }
                break;
            case "KEY_DOWN":
                if (activeButton == 0) {
                    activeButton = 1;
                }
                break;
            case "SPACE":
                break;
            default:
                break;
            }
        }
    }
}