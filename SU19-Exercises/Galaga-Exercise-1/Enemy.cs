using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Enemy : Entity {
    private Game game;
    public Enemy(Game game, DynamicShape shape, IBaseImage image) 
        : base(shape, image) {
        this.game = game;
    }
    public List<Image> enemyStrides = new List<Image>();
    public List<Enemy> enemies = new List<Enemy>();
    

    public void AddEnemies(Enemy singleEnemy) {
         enemies.Add(singleEnemy);
    }

}
