using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga_Exercise_2.Galaga_Entities;
using Galaga_Exercise_2.Squadrons;

namespace Galaga_Exercise_2 {
    public class ArrowForm : ISquadron{
        private int maxEnemies;
        private EntityContainer<Enemy> enemies;
        EntityContainer<Enemy> ISquadron.Enemies => enemies;
        private Game game;
        int ISquadron.MaxEnemies => maxEnemies;
        private int MaxEnemies() { return 8; }
        private EntityContainer<Enemy> Enemies { get; }

        public ArrowForm(Game game) {
            this.game = game;
        }
        
        public void CreateEnemies(List<Image> enemyStrides) {
            for (Vec2F i = new Vec2F(0.05f, 0.9f);
                i.X < 0.55f && i.Y > 0.70f;
                i.X += 0.1f, i.Y -= 0.04f) {
                Enemy newEnemy = new Enemy(game,
                    new DynamicShape(i.X,i.Y,0.1f,0.1f), 
                    new ImageStride(80,enemyStrides));
                game.EnemyContainer.AddDynamicEntity(newEnemy);
            }
            for (Vec2F j = new Vec2F(0.55f, 0.78f);
                j.X < 0.95f && j.Y < 0.94f;
                j.X += 0.1f, j.Y += 0.04f) {
                Enemy newEnemy = new Enemy(game,
                    new DynamicShape(j.X,j.Y,0.1f,0.1f), 
                    new ImageStride(80,enemyStrides));
                game.EnemyContainer.AddDynamicEntity(newEnemy);
            }
        }
    }
}
