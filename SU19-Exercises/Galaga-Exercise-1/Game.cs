using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
public class Game : IGameEventProcessor<Action> {
    private Window win;
    private DIKUArcade.Timers.GameTimer gameTimer;
    private Player player;
    private List<Image> enemyStrides;
    private List<Enemy> enemies;
    
    public Game() {
        win = new Window("BestWindow", 500, 500);
        gameTimer = new GameTimer(60, 30);
        player = new Player(this,
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
        Enemy singleEnemy = new Enemy(this,
            new DynamicShape(new Vec2F(0.98f, 0.98f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "BlueMonster.png")));
        enemyStrides = ImageStride.CreateStrides(80,
            Path.Combine("Assets", "Images", "BlueMonster.png"));
        enemies = new List<Enemy>();
    }

    public void GameLoop() {
        while (win.IsRunning()) {
            gameTimer.MeasureTime();
            while (gameTimer.ShouldUpdate()) {
                win.PollEvents();
                // Update game logic here
            }

            if (gameTimer.ShouldRender()) {
                win.Clear();
                player.RenderEntity();
                win.SwapBuffers();
            }

            if (gameTimer.ShouldReset()) {
                // 1 second has passed - display last captured ups and fps
                win.Title = "Galaga | UPS: " + gameTimer.CapturedUpdates +
                            ", FPS: " + gameTimer.CapturedFrames;
            }
        }
    }

    public void KeyPress(string key) {
        throw new NotImplementedException();
    }

    public void KeyRelease(string key) {
        throw new NotImplementedException();
    }

    public void ProcessEvent(GameEventType eventType,
        GameEvent<Action> gameEvent) {
        throw new NotImplementedException();
    }
}