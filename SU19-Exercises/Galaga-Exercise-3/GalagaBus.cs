using DIKUArcade.EventBus;

namespace GalagaGame {
    public class GalagaBus {
        private static GameEventBus<object> eventBus;

        public static GameEventBus<object> GetBus() {
            return GalagaBus.eventBus ?? 
                   (GalagaBus.eventBus = new GameEventBus<object>());
        }
    }
}