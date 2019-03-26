using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.State;
using DIKUArcade.Graphics;
using GalagaGame;

namespace Galaga_Exercise_3.GalagaStates {
    public class GamePaused : IGameState {
        private static GamePaused instance = null;

        public static GamePaused GetInstance() {
            return GamePaused.instance ?? (GamePaused.instance = new GamePaused());
        }
        
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
            GalagaBus.GetBus();
            switch (keyValue) {
            case "KEY_P":
                GalagaBus.eventBus.RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.InputEvent, this, "GAME_PAUSED",
                        "", ""));
                break;
            }
        }
    }
}