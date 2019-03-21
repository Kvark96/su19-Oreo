using System;
using DIKUArcade.Entities;
using Galaga_Exercise_3.Galaga_Entities;
using Galaga_Exercise_3.MovementStrategy;

    // For some reason, it acts a bit weird.
    // It does zig zag, though we have no explanation
    // as to why it moves out of the screen.

    // We have chosen to change the values of amplitude and period,
    // since, for whatever reason, the original values acted strangely.
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
        // Without newY it is not descending.
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