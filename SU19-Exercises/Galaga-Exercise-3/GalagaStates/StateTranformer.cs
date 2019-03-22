using System;

namespace Galaga_Exercise_3.GalagaStates {
    public class StateTranformer {
        private static GameStateType TransformStringToState(string state) {
            if (state == "GAME_RUNNING") {
                return GameStateType.GameRunning;
            } else if (state == "GAME_PAUSED") {
                return GameStateType.GamePaused;
            } else if (state == "GAME_MAINMENU") {
                return GameStateType.MainMenu;
            } else {
                throw new ArgumentException();
            }
        }


        private static string TransformStateToString(GameStateType state) {
            if (state == GameStateType.GameRunning) {
                return "GAME_RUNNING";
            } else if (state == GameStateType.GamePaused) {
                return "GAME_PAUSED";
            } else if (state == GameStateType.MainMenu) {
                return "GAME_MAINMENU";
            } else {
                throw new ArgumentException();
            }
        }
    }
}