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
using Galaga_Exercise_2;
using Galaga_Exercise_2.Galaga_Entities;

public class Game : IGameEventProcessor<object> {
    private Window win;
    private GameTimer gameTimer;
    private Player player;

    public EntityContainer<Enemy> EnemyContainer;

    private List<Image> enemyStrides;


    public List<PlayerShot> playerShots { get; private set; }

    private GameEventBus<object> eventBus;
    private List<Image> explosionStrides;
    private AnimationContainer explosions;

    private Score score;

    private StraightForm straightForm;
    private ArrowForm arrowForm;
    private DiamondForm diamondForm;
    private NoMove noMove;
    private Down down;
    private ZigZagDown zigZagDown;
    
    
    
    public Game() {
        win = new Window("Galaga v2", 500, 500);
        gameTimer = new GameTimer(60, 30);
        player = new Player(this,
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));

        enemyStrides = ImageStride.CreateStrides(4,
            Path.Combine("Assets", "Images", "BlueMonster.png"));
        EnemyContainer = new EntityContainer<Enemy>(10);
        
        straightForm = new StraightForm(this);
        arrowForm = new ArrowForm(this);
        diamondForm = new DiamondForm(this);
        
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
        eventBus.Subscribe(GameEventType.InputEvent, player);
        score = new Score(new Vec2F(0.01f, 0.69f), new Vec2F(0.3f, 0.3f));
    }

    public void GameLoop() {
        //straightForm.CreateEnemies(enemyStrides);
        arrowForm.CreateEnemies(enemyStrides);
        //diamondForm.CreateEnemies(enemyStrides);
        while (win.IsRunning()) {
            gameTimer.MeasureTime();
            while (gameTimer.ShouldUpdate()) {
                win.PollEvents();
                player.Move();
                
                /*foreach (Enemy enemy in EnemyContainer) {
                    noMove = new NoMove();
                    noMove.MoveEnemy(enemy);
                }*/
                // To chose another formation or movement strategy, just uncomment
                // the wanted formation or movement and out-comment, the previous
                // one. Arrow formation and Down is standard right now
                
                foreach (Enemy enemy in EnemyContainer) {
                    down = new Down();
                    down.MoveEnemy(enemy);
                }
                
                
                // Zigzag can look a little confusing, with any other formation
                // than straightForm. But this of course, can also just make the
                // game harder
                /*foreach (Enemy enemy in EnemyContainer) {
                    zigZagDown = new ZigZagDown();
                    zigZagDown.MoveEnemy(enemy);
                }*/
                eventBus.ProcessEvents();
                IterateShots();
            }

            if (gameTimer.ShouldRender()) {
                win.Clear();
                player.Entity.RenderEntity();

                EnemyContainer.RenderEntities();
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
                    GameEventType.WindowEvent, this, "CLOSE_WINDOW",
                    "", ""));
            break;
        case "KEY_LEFT":
            eventBus.RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.InputEvent, this, "KEY_LEFT",
                    "", ""));
            break;
        case "KEY_RIGHT":
            eventBus.RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.InputEvent, this, "KEY_RIGHT",
                    "", ""));
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
                    GameEventType.WindowEvent, this, "CLOSE_WINDOW",
                    "", ""));
            break;
        case "KEY_LEFT":
        case "KEY_RIGHT":
            eventBus.RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.InputEvent, this, "KEY_RELEASE",
                    "", ""));
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
            }
        } else if (eventType == GameEventType.InputEvent) {
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

            foreach (Enemy enemy in EnemyContainer) {
                if (CollisionDetection.Aabb(shot.dynamicShape, enemy.Shape).Collision) {
                    shot.DeleteEntity();
                    enemy.DeleteEntity();
                    score.AddPoint();
                    AddExplosion(enemy.Shape.Position.X, enemy.Shape.Position.Y - 0.02f,
                        0.1f, 0.1f);
                }
            }
        }

        EntityContainer<Enemy> newEnemyContainer = new EntityContainer<Enemy>();
        foreach (Enemy enemy in EnemyContainer) {
            if (!enemy.IsDeleted()) {
                newEnemyContainer.AddDynamicEntity(enemy);
            }
        }

        EnemyContainer = newEnemyContainer;

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
        if (explosions.AddAnimation(
            new StationaryShape(posX, posY, extentX, extentY), explosionLength,
            new ImageStride(explosionLength / 8, explosionStrides))) {
            explosions.RenderAnimations();
        }
    }
}

