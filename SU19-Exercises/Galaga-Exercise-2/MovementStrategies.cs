using System;
using System.Threading.Tasks;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using Galaga_Exercise_2.Galaga_Entities;

namespace Galaga_Exercise_2.MovementStrategy {
    public interface IMovementStrategy {
        void MoveEnemy(Enemy enemy);
        void MoveEnemies(EntityContainer<Enemy> enemies);
    }
    
    internal class NoMove : IMovementStrategy {
        public void MoveEnemy(Enemy enemy) {
            // Empty because they don't move
        }

        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            throw new System.NotImplementedException();
        }
    }
    
    public class Down : IMovementStrategy {
        private DynamicShape dynamicShape;
        async Task UseDelay() {
            await Task.Delay(1500); // Waits 1,5 second
        }
        public void Direction(Vec2F direction) {
            dynamicShape.Direction = direction;
        }
        public void MoveEnemy(Enemy enemy) {
            for (int i = 0; i < 10; i++) {
                Direction(new Vec2F(0.0f, -0.02f));
                UseDelay();
            }
        }

        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            throw new System.NotImplementedException();
        }
    }
    
    public class ZigZagDown : IMovementStrategy {
        private DynamicShape dynamicShape;
        private float s = 0.0003f;
        private float p = 0.045f;
        private float a = 0.05f;
        private float yi;
        private float yiMinus1;
        private float xi;
        private float x0;
        private float y0;
        async Task UseDelay() {
            await Task.Delay(1500); // Waits 1,5 second
        }
        public void Direction(Vec2F direction) {
            dynamicShape.Direction = direction;
        }
        public void MoveEnemy(Enemy enemy) {
            for (int i = 0; i < 10; i++) {
                yi = yiMinus1 + s;
                xi = (float) (x0 + a * Math.Sin((2 * Math.PI * (y0 - yi)) / p));
                Direction(new Vec2F(xi, yi));
                yiMinus1 = yi;
                UseDelay();
            }
        }

        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            throw new System.NotImplementedException();
        }
    }
}
