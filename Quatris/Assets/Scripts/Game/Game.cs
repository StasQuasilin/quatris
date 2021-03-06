﻿using UnityEngine;

public class Game : MonoBehaviour {

    public Sounds sounds;
    GameField gameField;
    public GameTimer timer = GameTimer.Timer;
    GameInput input;
    Level level;

    ScoresContainer scores;

    public enum GameState {
        help,
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

    public AdShow adShow;

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

        adShow = FindObjectOfType<AdShow>();
        adShow.Initialize();
        initNextScores();

        Load();
    }


    public int showScores = 10;

    void initNextScores() {
        showScores = scores.Scores + 800 + (int)(Random.value * 400);
    }

    public void ShowAd() {
        if (scores.Scores >= showScores) {
            adShow.ShowRewardedAd();
            initNextScores();
        }
    }
    
    void Update () {

        ShowAd();

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

                } else if (gameState == GameState.pause || gameState == GameState.help) {

                    Debug.Log( "Game continued" );
                    gameState = GameState.game;

                }

                sounds.Pause();
            } else 

            if ((gameState == GameState.game || gameState == GameState.help) && !adShow.IsShow) {
                CheckInput();

                int targetLevel = scores.Scores / 1000 + 1;

                if (timer.currentLevel != targetLevel) {
                    timer.currentLevel = targetLevel;
                }

            } else if (gameState != GameState.pause && gameState != GameState.help && input.AnyKey) {
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
        if (gameState != GameState.help) {
            timer.Fasta = input.ShapeFall();

            if (timer.isTime) {
                gameField.MoveShape( 0, 1 );
            }
        }
    }


    void GameStart() {

        if (initNewGame) {

            Debug.Log( "Init new game" );

            gameField.ReloadField();

            if (scores.Scores > 0) {
                Manager.Restart( scores.Scores );
            }
            scores.Scores = 0;
            initNextScores();
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
            gameState = GameState.help;
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

    internal bool isHelp {
        get {
            return gameState == GameState.help;
        }
    }
}

