using System.IO;
using System.IO.Pipes;
using System.Xml;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Player : Entity {
    private Game game;
    private Shape shape = new Shape();

    public Player(Game game, DynamicShape shape, IBaseImage image)
        : base(shape, image) {
        this.game = game;
    }
    // Not correct either
    public void Direction(Vec2F direction) {
        shape.AsDynamicShape();
    }
    // No fucking clue
    /*public void Move() {
        // pos = (0,0)-(1,1)
        if (shape != null) {
            if (shape.Position + (0.1f,0.0f) > (1,1)) { }
        }
    }*/
}
