using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
    
    GameField gameField;
    public GameTimer timer;
    GameInput input;
    Level level;

    public enum GameState {
        novo,
        start,
        pause,
        game,
        gameOver
    }

    internal GameState gameState = GameState.start;
    internal bool initNewGame = false;

    public static Game instance;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start() {

        Debug.Log( "Game start" );

        level = FindObjectOfType<Level>();
        if (level == null) {
            level = gameObject.AddComponent<Level>();
        }

        gameField = FindObjectOfType<GameField>();
        gameField.Init();

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


        if (gameState == GameState.gameOver) {

            initNewGame = !input.AnyKey;
            
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
            }

            if (gameState == GameState.game) {

                GameInput();

                if (gameField.UnderFloor) {
                    Debug.Log( "Lose shape" );

                    gameField.InitCurrent();

                }

            }
        }

        if (gameState == GameState.start || gameState == GameState.gameOver) {
            if (Input.anyKey || Input.touchCount > 0) {
                GameStart();
            }
        }
	}

    void GameInput() {
        ////////////////LEVEL ROTATION////////////////
        if (input.LevelRight()) {
            level.Right();
        }

        if (input.LevelLeft()) {
            level.Left();
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

        if (timer.isTime()) {
            gameField.MoveShape( 0, 1 );
        }
    }


    void GameStart() {

        Debug.Log( "Game start" );

        if (initNewGame) {
            Debug.Log( "Init new game" );

            
        }

        gameState = GameState.game;
    }
    
    DataIO io;
    void Save() {

        Debug.Log( "Save data" );

//        DataIO.io.Save( new Data( 0, timer.currentLevel, level.levelShape.matrix, currentShape.matrix ) );

    }
    void Load() {
        /*
        Data data = DataIO.io.Load<Data>();

        if (data != null) {
            Debug.Log( "Load save game" );

            //scores = data._scores;
            timer.currentLevel = data._level;

            //InitLevel();

            level.levelShape.Set( data.LevelData );
            //currentShape.Set( data.ShapeData );

            level.Align();
        }

        scoresContainer = DataIO.io.Load<Scores>();

        if (scoresContainer != null ) {
            foreach (ScoresData s in scoresContainer.data) {
                scoresList.Add( s.date + ":" + s.scores );
            }
        } else {
            scoresContainer = new Scores();
        }
        */
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

