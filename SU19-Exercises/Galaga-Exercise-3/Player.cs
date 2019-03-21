using System;
using System.IO;
using System.IO.Pipes;
using System.Xml;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using OpenTK.Graphics.OpenGL;

public class Player : IGameEventProcessor<object> {
    private Game game;
    private DynamicShape dynamicShape;
    public Entity Entity { get; private set; }
    public Player(Game game, DynamicShape shape, IBaseImage image){
        Entity = new Entity(shape, image);
        this.game = game;
        dynamicShape = shape;
    }

    public void ProcessEvent(GameEventType gameEventType, GameEvent<object> gameEvent) {
        if (gameEventType == GameEventType.InputEvent) {
            switch (gameEvent.Message)
            {
                case "KEY_LEFT":
                    Direction(new Vec2F(-0.02f,0.0f));
                    break;
                case "KEY_RIGHT":
                    Direction(new Vec2F(0.02f,0.0f));
                    break;
                case "KEY_RELEASE":
                    Direction(new Vec2F(0.0f,0.0f));
                    break;
            }
                
        }
    }
    
    public void Direction(Vec2F direction) {
        dynamicShape.Direction = direction;
    }

    public void Move() {
        if (dynamicShape.Position.X + dynamicShape.Direction.X < -0.1f ||
            dynamicShape.Position.X + dynamicShape.Direction.X > 1.0f) {
        } else {
            dynamicShape.Move();
        }
    }
    private Image laser = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
    public void Shoot() {
        PlayerShot shot = new PlayerShot(game, new DynamicShape(
            new Vec2F(dynamicShape.Position.X+0.05f, 
                           dynamicShape.Position.Y + 0.08f),
            new Vec2F(0.008f, 0.027f)), laser);
        game.playerShots.Add(shot);

    }

}