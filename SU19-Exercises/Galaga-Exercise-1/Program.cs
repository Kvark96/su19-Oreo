using System;
using DIKUArcade.Timers;

namespace Galaga_Exercise_1 {
    internal class Program {
        public static void Main(string[] args) {
            GameTimer timer = new GameTimer();
            Console.WriteLine(timer);
        }
    }
}