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
}