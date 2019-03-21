using DIKUArcade.Entities;
using Galaga_Exercise_3.Galaga_Entities;
using Galaga_Exercise_3.MovementStrategy;

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