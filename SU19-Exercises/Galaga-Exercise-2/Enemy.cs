using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga_Exercise_2.Squadrons;

namespace Galaga_Exercise_2.Galaga_Entities {

    public class Enemy : Entity {
        private Vec2F Position { get; }
        private Game game;

        public Enemy(Game game, DynamicShape shape, ImageStride image, Vec2F position)
            : base(shape, image) {
            this.game = game;
            Position = position;
        }
    }
}


