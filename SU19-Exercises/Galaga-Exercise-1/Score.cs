using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Drawing;

namespace Galaga_Exercise_1 {
    public class Score {
        private int score;
        private Text display;

        public Score(Vec2F position, Vec2F extent) {
            score = 0;
            display = new Text(score.ToString(), position, extent);
        }

        public void AddPoint() {
            score += 1;
        }
        Vec3I newColor = new Vec3I(230,230,230);
        public void RenderScore() {
            display.SetText(string.Format("Score: {0}", score.ToString()));
            display.SetColor(newColor);
            display.RenderText();
        }
    }
}