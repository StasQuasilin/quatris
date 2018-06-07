using UnityEngine;
using UnityEngine.Advertisements;

public class Game : MonoBehaviour {

    public Sounds sounds;
    GameField gameField;
    public GameTimer timer = GameTimer.Timer;
    GameInput input;
    Level level;

    ScoresContainer scores;

    public enum GameState {
        novo,
        start,
        pause,
        game,
        gameOver
    }

    public GameState gameState = GameState.start;
    public bool initNewGame = false;

    public static Game instance;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private string gameId = "1800917";

    void Start() {

        Debug.Log( "Game start" );

        scores = ScoresContainer.Instance;

        if (sounds == null) {
            sounds = FindObjectOfType<Sounds>();
        }

        level = FindObjectOfType<Level>();
        if (level == null) {
            level = gameObject.AddComponent<Level>();
        }

        gameField = FindObjectOfType<GameField>();
        gameField.InitGameField();

        input = FindObjectOfType<GameInput>();
        if(input == null) {
            input = gameObject.AddComponent<GameInput>();
        }

#if UNITY_EDITOR
        if (Advertisement.isSupported) {
            Advertisement.Initialize( gameId, true );
        }
#elif UNITY_ANDROID
        if (Advertisement.isSupported) {
            Advertisement.Initialize( gameId, false );
        }
#endif

        Load();
        initNextScores(500);
    }
    public int nextScores = 10;
    void ShowRevardAd() {
        if (scores.Scores >= nextScores) {
            if (Advertisement.IsReady( "rewardedVideo" )) {
                ShowOptions options = new ShowOptions { resultCallback = HandleShowResult };
                Advertisement.Show( "rewardVideo", options );
            }
        }
    }

    void initNextScores(int step) {
        nextScores = scores.Scores + 1000 + (int)(Random.value * step );
    }

    private void HandleShowResult(ShowResult result) {
        switch (result) {
            case ShowResult.Finished:
                initNextScores( 1000 );
                break;
            case ShowResult.Skipped:
                initNextScores( 0 );
                break;
            case ShowResult.Failed:
                Debug.LogError( "Ad failed to show" );
                initNextScores( 0 );
                break;
        }
    }
    public float time;

    void Update () {

        time = Time.time;

        ShowRevardAd();

        if (level.IsEmpty) {
            gameField.InitLevel();
        }

        if (level.GameOver) {
            gameState = GameState.gameOver;


            if (!initNewGame) {
                initNewGame = !input.AnyKey;
            } else if (input.AnyKey) {
                GameStart();
            }

        } else {
            // IF NOT GAME OVER
            if (input.Pause()) {

                if (gameState == GameState.game) {

                    Debug.Log( "Game was paused" );
                    gameState = GameState.pause;

                    Save();
                } else if (gameState == GameState.pause) {

                    Debug.Log( "Game continued" );
                    gameState = GameState.game;

                }

                sounds.Pause();
            } else 

            if (gameState == GameState.game) {
                CheckInput();

                int targetLevel = scores.Scores / 1000 + 1;

                if (timer.currentLevel != targetLevel) {
                    timer.currentLevel = targetLevel;
                }

            } else if (gameState != GameState.pause && input.AnyKey) {
                GameStart();
            }
        } 
    }

    void CheckInput() {

        ////////////////LEVEL ROTATION////////////////
        if (input.LevelRight()) {
            level.Right();
            sounds.LevelRotate();
        }

        if (input.LevelLeft()) {
            level.Left();
            sounds.LevelRotate();
        }

        ////////////////SHAPE ROTATION////////////////
        if (input.ShapeRotate()) {

            gameField.RotateShape();
            
        }

        ////////////////SHAPE MOVE////////////////
        if (input.ShapeLeft()) {

            gameField.MoveShape( -1, 0 );

        }

        if (input.ShapeRight()) {

            gameField.MoveShape( 1, 0 );
        }

        ////////////////SHAPE FALL////////////////

        timer.Fasta = input.ShapeFall();

        if (timer.isTime) {
            gameField.MoveShape( 0, 1 );
        }
    }


    void GameStart() {

        if (initNewGame) {

            Debug.Log( "Init new game" );

            gameField.ReloadField();
            scores.Scores = 0;
            
            initNewGame = false;


        } else {
            Debug.Log( "Game start" );

            gameState = GameState.game;
        }

        
    }
    
    DataIO io;
    void Save() {

        Debug.Log( "Save data" );

        DataIO.io.Save( new Data( scores.Scores, timer.currentLevel, level.levelShape.matrix, gameField.currentShape.matrix ) );

    }
    void Load() {

        Data data = DataIO.io.Load<Data>();

        if (data != null) {

            Debug.Log( "Load save game" );

            scores.Scores = data._scores;

            timer.currentLevel = data._level;

            gameField.InitLevel(data.ShapeData, data.LevelData);
        } else {
            //todo show demo
            
        }
    }
    void OnApplicationQuit() {
        Save();
    }
    internal bool IsGameStart {
        get {
            return gameState == GameState.start;
        }
    }
    internal bool IsPause {
        get {
            return gameState == GameState.pause;
        }
    }
    internal bool IsGameOver {
        get {
            return gameState == GameState.gameOver;
        }
    }
}

