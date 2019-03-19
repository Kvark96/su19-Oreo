using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using Galaga_Exercise_2.Galaga_Entities;

namespace Galaga_Exercise_2.MovementStrategy {
    public interface IMovementStrategy {
        void MoveEnemy(Enemy enemy);
        void MoveEnemies(EntityContainer<Enemy> enemies);
    }

    // This one should be correct, maybe.
    internal class NoMove : IMovementStrategy {
        public void MoveEnemy(Enemy enemy) {
            // Empty because they don't move
        }
        
        // Don't believe MoveEnemy should be called on this, if they're just
        // still standing 
        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            // Still not moving
        }
    }
    // This one is the one that is closest to be done, I would say still don't
    // know about these EntityContainers though (more is written in Game.cs)
    public class Down : IMovementStrategy {

        public Down() {
        }

        public void MoveEnemy(Enemy enemy) {
            for (int i = 0; i < 45; i++) {
                enemy.Shape.Position.Y -= 0.02f;
            }
        }

        // Guessing list functions like this, can be used to make them move
        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            foreach (Enemy item in enemies) {
                MoveEnemy(item);
            }
        }
    }
    
    // More or less the same as above
    public class ZigZagDown : IMovementStrategy {
        private DynamicShape dynamicShape;
        private float s = 0.0003f;
        private float p = 0.045f;
        private float a = 0.05f;
        private float newY;
        private float oldY;
        private float newX;
        private float startX;
        private float startY;


        public void Direction(Vec2F direction) {
            dynamicShape.Direction = direction;
        }

        public void MoveEnemy(Enemy enemy) {
            for (int i = 0; i < 10; i++) {
                newY = oldY + s;
                newX = (float) (startX + a * Math.Sin((2 * Math.PI * (startY - newY)) / p));
                Direction(new Vec2F(newX, newY));
                oldY = newY;
            }
        }

        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            foreach (Enemy item in enemies) {
                MoveEnemy(item);
            }
        }
    }
}


