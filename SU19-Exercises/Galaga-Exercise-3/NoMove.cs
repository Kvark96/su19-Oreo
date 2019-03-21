using DIKUArcade.Entities;
using Galaga_Exercise_3.Galaga_Entities;
using Galaga_Exercise_3.MovementStrategy;

internal class NoMove : IMovementStrategy {
    public void MoveEnemy(Enemy enemy) {
        // Empty because they don't move
    }
    public void MoveEnemies(EntityContainer<Enemy> enemies) {
        // Still not moving
    }
}