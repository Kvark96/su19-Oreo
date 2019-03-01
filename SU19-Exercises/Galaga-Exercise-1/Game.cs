using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.Timers;
using Galaga_Exercise_1;

public class Game : IGameEventProcessor<object> {
    private Window win;
    private DIKUArcade.Timers.GameTimer gameTimer;
    private Player player;
    private List<Image> enemyStrides;
    private List<Enemy> enemies;
    public List<PlayerShot> playerShots { get; private set; }

    private GameEventBus<object> eventBus;
    private List<Image> explosionStrides;
    private AnimationContainer explosions;

    private Score score;

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
        playerShots = new List<PlayerShot>();
        eventBus = new GameEventBus<object>();
            eventBus.InitializeEventBus(new List<GameEventType>() {
                GameEventType.InputEvent,
                GameEventType.WindowEvent
            });
            win.RegisterEventBus(eventBus);
            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.WindowEvent, this);
            explosionStrides =
                ImageStride.CreateStrides(8, Path.Combine("Assets", "Images", "Explosion.png"));
            explosions = new AnimationContainer(100);
        score = new Score(new Vec2F(0.01f,0.69f), new Vec2F(0.3f,0.3f));
    }

    public void GameLoop() {
        while (win.IsRunning()) {
            gameTimer.MeasureTime();
            while (gameTimer.ShouldUpdate()) {
                win.PollEvents();
                // Update game logic here
                player.Move();
                eventBus.ProcessEvents();
                IterateShots();
            }

            if (gameTimer.ShouldRender()) {
                win.Clear();
                player.RenderEntity();
                foreach (var shot in playerShots) {
                    shot.RenderEntity();
                }
                score.RenderScore();
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
        switch (key) {
        case "KEY_ESCAPE":
            eventBus.RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.WindowEvent, this, "CLOSE_WINDOW", "", ""));
            break;
        case "KEY_LEFT":
            player.Direction(new Vec2F(-0.05f,0.0f));
            break;
        case "KEY_RIGHT":
            player.Direction(new Vec2F(0.05f,0.0f));
            break;
        case "KEY_SPACE":
            player.Shoot();
            break;
        }
    }

    public void KeyRelease(string key) {
        switch (key) {
        case "KEY_ESCAPE":
            eventBus.RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.WindowEvent, this, "CLOSE_WINDOW", "", ""));
            break;
        case "KEY_LEFT":
        case "KEY_RIGHT":
            player.Direction(new Vec2F(0.0f,0.0f));
            break;
        }
    }

    public void ProcessEvent(GameEventType eventType,
        GameEvent<object> gameEvent) {
        if (eventType == GameEventType.WindowEvent) {
            switch (gameEvent.Message) {
            case "CLOSE_WINDOW":
                win.CloseWindow();
                break;
            default:
                break;
            }
        } else if (eventType == GameEventType.InputEvent){
            switch (gameEvent.Parameter1) {
                case "KEY_PRESS":
                    KeyPress(gameEvent.Message);
                    break;
                case "KEY_RELEASE":
                    KeyRelease(gameEvent.Message);
                    break;
            }
            
        }
    }

    public void IterateShots() {
        foreach (var shot in playerShots) {
            shot.Shape.Move();
            if (shot.Shape.Position.Y > 1.0f) {
                shot.DeleteEntity();
            }

            foreach (var enemy in enemies) {
                if (CollisionDetection.Aabb(shot.dynamicShape, enemy.Shape).Collision) {
                    shot.DeleteEntity();
                    enemy.DeleteEntity();
                    AddExplosion(shot.dynamicShape.Position.X, shot.dynamicShape.Position.Y, 1.0f, 1.0f);
                    score.AddPoint();
                }
            }
        }

        List<Enemy> newEnemies = new List<Enemy>();
        foreach (var enemy in enemies) {
            if (!enemy.IsDeleted()) {
                newEnemies.Add(enemy);
            }
        }

        enemies = newEnemies;
        
        List<PlayerShot> newShots = new List<PlayerShot>();
        foreach (var shot in playerShots) {
            if (!shot.IsDeleted()) {
                newShots.Add(shot);
            }
        }
        playerShots = newShots;
    }

    private int explosionLength = 500;

    public void AddExplosion(float posX, float posY,
        float extentX, float extentY) {
        explosions.AddAnimation(
            new StationaryShape(posX, posY, extentX, extentY), explosionLength,
            new ImageStride(explosionLength / 8, explosionStrides));
    }
}

