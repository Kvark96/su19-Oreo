using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Enemy : Entity {
    private Game game;

    public Enemy(Game game, DynamicShape shape, ImageStride image)
        : base(shape, image) {
        this.game = game;
    }

}
