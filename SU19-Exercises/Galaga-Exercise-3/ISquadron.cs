using Galaga_Exercise_3.Galaga_Entities;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;

namespace Galaga_Exercise_3.Squadrons {
    // A nice long code, that dosen't work, most of the details is written in
    // the Game.cs class
    public interface ISquadron {
        EntityContainer<Enemy> Enemies { get; }
        int MaxEnemies { get; }
        void CreateEnemies(List<Image> enemyStrides);
    }
    
    // Didn't know whether I should initiate it here, or the Game class. Have
    // done it in both, but MovemenStrategies.cs is a little further in this
    // department, it actually looks "somewhat" viable, not that it works though.
    /*internal class StraightFormation : ISquadron {
        private int maxEnemies;
        private EntityContainer<Enemy> enemies;
        EntityContainer<Enemy> ISquadron.Enemies => enemies;

        int ISquadron.MaxEnemies => maxEnemies;
        
        private int MaxEnemies() { return 8; }

        private EntityContainer<Enemy> Enemies { get; }
    
        public void CreateEnemies(List<Image> enemyStrides) {
        }
        
        
        
    }*/
}