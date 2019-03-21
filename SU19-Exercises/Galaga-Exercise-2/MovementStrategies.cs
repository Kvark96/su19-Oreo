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
        public void MoveEnemy(Enemy enemy) {
            enemy.Shape.Position.Y -= 0.001f;
        }

        // Guessing list functions like this, can be used to make them move
        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            foreach (Enemy item in enemies) {
                MoveEnemy(item);
            }
        }
    }
    public class ZigZagDown : IMovementStrategy {
        private DynamicShape dynamicShape;
        private float speed = 0.0003f;
        private float period = 0.075f;
        private float amplitude = 0.005f;
        private float newY;
        private float newY2;
        private float newX;
        private float startX;
        private float startY;
        public void MoveEnemy(Enemy enemy) {
                newY = enemy.Shape.Position.Y;
                newX = enemy.Shape.Position.X;
                startX = enemy.startPos.X;
                startY = enemy.startPos.Y;
                newY -= speed;
                newY2 += speed;
                newX = (float) (startX + amplitude *
                    Math.Sin(2 * Math.PI * (startY - newY2) / period));
                enemy.Shape.Position.X = newX;
                enemy.Shape.Position.Y = newY;
        }

        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            foreach (Enemy item in enemies) {
                MoveEnemy(item);
            }
        }
    }
}


