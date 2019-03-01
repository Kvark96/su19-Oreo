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
    private Enemy singleEnemy;
    private Enemy singleEnemy2;
    private Enemy singleEnemy3;
    private Enemy singleEnemy4;
    private Enemy singleEnemy5;
    private Enemy singleEnemy6;
    private Enemy singleEnemy7;
    private Enemy singleEnemy8;
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
        
        enemyStrides = ImageStride.CreateStrides(4,
            Path.Combine("Assets", "Images", "BlueMonster.png"));
        
        enemies = new List<Enemy>();
        playerShots = new List<PlayerShot>();
        
        explosionStrides =
            ImageStride.CreateStrides(8, Path.Combine("Assets", "Images", "Explosion.png"));
        
        explosions = new AnimationContainer(8);
        eventBus = new GameEventBus<object>();
        eventBus.InitializeEventBus(new List<GameEventType>() {
                GameEventType.InputEvent,
                GameEventType.WindowEvent
            });
        win.RegisterEventBus(eventBus);
            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.WindowEvent, this);
            score = new Score(new Vec2F(0.01f,0.69f), new Vec2F(0.3f,0.3f));
    }

    public void GameLoop() {
        AddEnemies();
        while (win.IsRunning()) {
            gameTimer.MeasureTime();
            while (gameTimer.ShouldUpdate()) {
                win.PollEvents();
                player.Move();
                
                eventBus.ProcessEvents();
                IterateShots();
            }

            if (gameTimer.ShouldRender()) {
                win.Clear();
                player.RenderEntity();
                foreach(Enemy item in enemies) {
                    item.RenderEntity();
                }
                foreach (var shot in playerShots) {
                    shot.RenderEntity();
                }
                score.RenderScore();
                explosions.RenderAnimations();
                win.SwapBuffers();
                
            }

            if (gameTimer.ShouldReset()) {
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
    private void AddEnemies() {
        // Hard-coded for easiness (and lateness)
        singleEnemy = new Enemy(this,
            new DynamicShape(new Vec2F(0.10f, 0.90f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, enemyStrides));
        singleEnemy2 = new Enemy(this,
            new DynamicShape(new Vec2F(0.20f, 0.90f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, enemyStrides));
        singleEnemy3 = new Enemy(this,
            new DynamicShape(new Vec2F(0.30f, 0.90f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, enemyStrides));
        singleEnemy4 = new Enemy(this,
            new DynamicShape(new Vec2F(0.40f, 0.90f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, enemyStrides));
        singleEnemy5 = new Enemy(this,
            new DynamicShape(new Vec2F(0.50f, 0.90f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, enemyStrides));
        singleEnemy6 = new Enemy(this,
            new DynamicShape(new Vec2F(0.60f, 0.90f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, enemyStrides));
        singleEnemy7 = new Enemy(this,
            new DynamicShape(new Vec2F(0.70f, 0.90f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, enemyStrides));
        singleEnemy8 = new Enemy(this,
            new DynamicShape(new Vec2F(0.80f, 0.90f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, enemyStrides));
        enemies.Add(singleEnemy);
        enemies.Add(singleEnemy2);
        enemies.Add(singleEnemy3);
        enemies.Add(singleEnemy4);
        enemies.Add(singleEnemy5);
        enemies.Add(singleEnemy6);
        enemies.Add(singleEnemy7);
        enemies.Add(singleEnemy8);
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
                    score.AddPoint();
                    AddExplosion(enemy.Shape.Position.X, enemy.Shape.Position.Y-0.02f, 0.1f, 0.1f);
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
        if(explosions.AddAnimation(
            new StationaryShape(posX, posY, extentX, extentY), explosionLength,
            new ImageStride(explosionLength / 8, explosionStrides)))
        {
            explosions.RenderAnimations();
        }
    }
}

