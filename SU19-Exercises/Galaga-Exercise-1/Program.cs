using System;
using DIKUArcade;

namespace Galaga_Exercise_1 {
    internal class Program {
        public static void Main(string[] args) {
            Game newGame = new Game();
            newGame.GameLoop();
            /*GameTimer timer = new GameTimer();
            Console.WriteLine(timer);*/
        }
    }
}