using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
namespace GalagaGame.GalagaStates {
    public class MainMenu : IGameState {
        private static MainMenu instance;

        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;
        
        public static MainMenu GetInstance() {
            return MainMenu.instance ?? (MainMenu.instance = new MainMenu());
        }
        public void RenderState() {
            StationaryShape backGroundShape = new StationaryShape(0.5f, 0.5f,1f, 1f);
            backGroundImage = new Entity(backGroundShape, new Image(Path.Combine("Assets", "Images", "TitleImage.png")));
            backGroundImage.RenderEntity();
            Text NewGame = new Text("New Game", new Vec2F(0.5f, 0.6f), new Vec2F(0.2f, 0.1f));
            Text Quit = new Text("Quit", new Vec2F(0.5f, 0.4f), new Vec2F(0.1f, 0.1f));
            Text[] menuButtons = new Text[2];
            menuButtons[0] = NewGame;
            menuButtons[1] = Quit;
            menuButtons[0].RenderText();
            menuButtons[1].RenderText();
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
                switch (keyAction) {
                case "KEY_UP":
                    if (activeMenuButton == menuButtons[1]) {
                    activeMenuButton = menuButtons[0];
                }
                    break;
                case "KEY_DOWN":
                    if (activeMenuButton == menuButtons[0]) {
                    activeMenuButton = menuButtons[1];
                }
                    break;
                case "KEY_ENTER":
                    eventBus.RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.InputEvent, this, "KEY_RENTER",
                            "", ""));
                    break;
                }
            }
        }
    }
}