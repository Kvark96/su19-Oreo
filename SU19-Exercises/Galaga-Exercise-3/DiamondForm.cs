using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga_Exercise_3.Galaga_Entities;
using Galaga_Exercise_3.Squadrons;

namespace Galaga_Exercise_3 {
    // The Circle formation became a diamond formation instead, because there
    // not being enough space for enemies to make a circle
    public class DiamondForm : ISquadron {
        private int maxEnemies;
        private EntityContainer<Enemy> enemies;
        EntityContainer<Enemy> ISquadron.Enemies => enemies;
        private Game game;
        int ISquadron.MaxEnemies => maxEnemies;
        private int MaxEnemies() { return 8; }
        private EntityContainer<Enemy> Enemies { get; }

        public DiamondForm(Game game) {
            this.game = game;
        }
        
        public void CreateEnemies(List<Image> enemyStrides) {
            for (Vec2F i = new Vec2F(0.45f, 0.9f);
                i.X > 0.25f && i.Y > 0.70f;
                i.X -= 0.1f, i.Y -= 0.1f) {
                Enemy newEnemy = new Enemy(game,
                    new DynamicShape(i.X,i.Y,0.1f,0.1f), 
                    new ImageStride(80,enemyStrides));
                game.EnemyContainer.AddDynamicEntity(newEnemy);
            }
            for (Vec2F j = new Vec2F(0.25f, 0.70f);
                j.X < 0.45f && j.Y > 0.5f;
                j.X += 0.1f, j.Y -= 0.1f) {
                Enemy newEnemy = new Enemy(game,
                    new DynamicShape(j.X,j.Y,0.1f,0.1f), 
                    new ImageStride(80,enemyStrides));
                game.EnemyContainer.AddDynamicEntity(newEnemy);
            }
            for (Vec2F j = new Vec2F(0.45f, 0.5f);
                j.X < 0.75 && j.Y < 0.8f;
                j.X += 0.1f, j.Y += 0.1f) {
                Enemy newEnemy = new Enemy(game,
                    new DynamicShape(j.X,j.Y,0.1f,0.1f), 
                    new ImageStride(80,enemyStrides));
                game.EnemyContainer.AddDynamicEntity(newEnemy);
            }
            Enemy lastNewEnemy = new Enemy(game,
                new DynamicShape(0.55f,0.8f,0.1f,0.1f), 
                new ImageStride(80,enemyStrides));
            game.EnemyContainer.AddDynamicEntity(lastNewEnemy);
        }
    }
}