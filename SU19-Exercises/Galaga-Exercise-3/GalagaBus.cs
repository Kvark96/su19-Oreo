using DIKUArcade.EventBus;

namespace GalagaGame {
    public static class GalagaBus {
        public static GameEventBus<object> eventBus; // is going to be private

        public static GameEventBus<object> GetBus() {
            return GalagaBus.eventBus ?? 
                   (GalagaBus.eventBus = new GameEventBus<object>());
        }
    }
}