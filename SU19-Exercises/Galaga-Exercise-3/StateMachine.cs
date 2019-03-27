using DIKUArcade.EventBus;
using DIKUArcade.State;
using GalagaGame.GalagaStates;
using Galaga_Exercise_3.GalagaStates;


namespace GalagaGame.GalagaState {
    public class StateMachine : IGameEventProcessor<object> {
        public IGameState ActiveState { get; private set; }

        public StateMachine() {
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);

            ActiveState = MainMenu1.GetInstance();
        }

        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
                case GameStateType.MainMenu:
                    ActiveState = MainMenu1.GetInstance();
                    break;
                case GameStateType.GamePaused:
                    ActiveState = GamePaused.GetInstance();
                    break;
                case GameStateType.GameRunning:
                    ActiveState = GameRunning.GetInstance();
                    break;
            }
        }

        public void ProcessEvent(GameEventType gameEventType, GameEvent<object> gameEvent) {
            // Process the events to be handled by StateMachine. When the GameState is changed,
            // it will be sent as a GameStateEvent, and StateMachine should change the GameState
            // accordingly (with SwitchState).
            var msg = gameEvent.Message;
            switch (gameEventType) {
                case GameEventType.InputEvent:
                    break;
                case GameEventType.GameStateEvent:
                    switch (msg) {
                        case "GAME_PAUSED":
                            SwitchState(GameStateType.GamePaused);
                            break;
                        case "GAME_RUNNING":
                            SwitchState(GameStateType.GameRunning);
                            break;
                        case "GAME_MAINMENU":
                            SwitchState(GameStateType.MainMenu);
                            break;
                    }
            }
        }
    }
}