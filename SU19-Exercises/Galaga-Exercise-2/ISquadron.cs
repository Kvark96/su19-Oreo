using Galaga_Exercise_2.Galaga_Entities;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;

namespace Galaga_Exercise_2.Squadrons {
    public interface ISquadron {
        EntityContainer<Enemy> Enemies { get; }
        int MaxEnemies { get; }
        void CreateEnemies(List<Image> enemyStrides);
    }
    internal class StraightFormation : ISquadron {
        private int maxEnemies;
        private EntityContainer<Enemy> enemies;
        EntityContainer<Enemy> ISquadron.Enemies => enemies;

        int ISquadron.MaxEnemies => maxEnemies;
        
        private int MaxEnemies() { return 8; }

        private EntityContainer<Enemy> Enemies { get; }
    
        public void CreateEnemies(List<Image> enemyStrides) {
        }
    }
}