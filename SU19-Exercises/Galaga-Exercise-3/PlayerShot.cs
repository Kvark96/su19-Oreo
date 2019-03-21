using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;



public class PlayerShot : Entity {
    private Game game;
    public DynamicShape dynamicShape;
    public PlayerShot(Game game, DynamicShape shape, IBaseImage image)
        : base(shape, image) {
        this.game = game;
        dynamicShape = shape;
        dynamicShape.Direction = new Vec2F(0.0f, 0.01f);
    }
}