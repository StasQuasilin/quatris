using System.Collections.Generic;
using UnityEngine;

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

        Load();
    }
	
	void Update () {

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
            initNewGame = false;


        } else {
            Debug.Log( "Game start" );
        }

        gameState = GameState.game;
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

