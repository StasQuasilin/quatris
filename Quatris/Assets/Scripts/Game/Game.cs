using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    Shapes shapeFactory;
    Level level;
    GameField gameField;
    public GameTimer timer;
    GameInput input;

    Shape currentShape;
    internal Shapes.ShapeValue next;
    public List<string> scoresList;

    Scores scoresContainer;

    internal int scores;
    internal int Level {
        get {
            return timer.currentLevel;
        }

        set {
            timer.currentLevel = value;
        }
    }

    public enum GameState {
        novo,
        start,
        pause,
        game,
        over
    }

    internal GameState gameState = GameState.start;
    internal bool canInit = false;
    internal bool notAdded = true;

    
    public AudioSource backMusic;
    public AudioSource shapeMove, levelRotate;
    public AudioSource drop;
    public AudioSource shapeRotate;
    public AudioSource pause;
    public AudioSource lineDrop;

    public static Game instance;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start() {

        shapeFactory = FindObjectOfType<Shapes>();

        gameField = FindObjectOfType<GameField>();

        level = FindObjectOfType<Level>();
        if (level == null) {
            level = gameObject.AddComponent<Level>();
        }
        level.gameField = gameField;

        input = FindObjectOfType<GameInput>();
        if(input == null) {
            input = gameObject.AddComponent<GameInput>();
        }

        Load();
    }

    void InitLevel() {
        level.Init(shapeFactory.GetShape());
        InitNext();
        InitCurrent();
    }

    void InitNext() {
        next = shapeFactory.GetShape();
    }

    void InitCurrent() {
        currentShape = new Shape(next, (gameField.wSize - next.xSize) / 2, -next.ySize / 2);
        InitNext();
    }
	
	void Update () {

        if (level.IsEmpty) {
            InitLevel();
        }

        if( level.GameOver) {
            gameState = GameState.over;
            canInit = !input.AnyKey;

            if (notAdded) {
                notAdded = false;
                scoresContainer.Add( scores );
                Save();
                    
            }
        }

        if (input.Pause()) {

            if (gameState == GameState.game) {

                pause.Play();

                gameState = GameState.pause;
                Save();
                backMusic.Stop();
            } else if (gameState == GameState.pause) {
                gameState = GameState.game;
                backMusic.Play();
            }
        }

        if (gameState == GameState.game) {

            ////////////////LEVEL ROTATION////////////////
            if (input.LevelRight()) {
                level.Right();

                levelRotate.Play();

                CheckShape();
            } else if (input.LevelLeft()) {
                level.Left();

                levelRotate.Play();

                CheckShape();
            }

            ////////////////SHAPE ROTATION////////////////
            if (input.ShapeRotate()) {

                currentShape.CheckBounds();

                if (currentShape.minX == 0 && currentShape.Width < currentShape.Height) {
                    currentShape.Move( 1, 0 );
                }

                MatrixUtil.RotateRight( currentShape );

                if (level.Contain( currentShape.matrix )) {
                    MatrixUtil.RotateLeft( currentShape );
                } else {
                    shapeRotate.Play();
                }
            }

            ////////////////SHAPE MOVE////////////////
            if (input.ShapeLeft()) {

                currentShape.Move( -1, 0 );

                if (level.Contain( currentShape.matrix ) || !ValidSide) {
                    currentShape.Move( 1, 0 );
                } else {
                    shapeMove.Play();
                }
            } else if (input.ShapeRight()) {

                currentShape.Move( 1, 0 );

                if (level.Contain( currentShape.matrix ) || !ValidSide) {
                    currentShape.Move( -1, 0 );
                } else {
                    shapeMove.Play();
                }
            }

            ////////////////SHAPE FALL////////////////
            timer.Fasta = input.ShapeFall();

            if (timer.isTime()) {
                currentShape.Move( 0, 1 );

                if (level.Contain( currentShape.matrix )) {
                    currentShape.Move( 0, -1 );

                    level.Add( currentShape, (int)(Random.value * Level) );

                    drop.Play();

                    AddScore( currentShape.matrix.Count );
                    int s = level.CheckDrops();
                    AddScore( s * 3 );

                    if (s > 0) {
                        lineDrop.Play();
                    }

                    InitCurrent();
                }
            }


            if (UnderFloor) {

                AddScore( -currentShape.matrix.Count * 5 );
                InitCurrent();

            }

            int targetLevel = scores / 1000 + 1;

            if (timer.currentLevel != targetLevel && targetLevel <= timer.maxLevel) {
                timer.currentLevel = targetLevel;
            }

        } else if (gameState != GameState.pause) {
            if (Input.anyKey || Input.touchCount > 0) {
                GameStart();
            }
        }
	}

    void CheckShape() {
        if (currentShape.Contain( level.levelShape.matrix )) {
            level.Add( currentShape, 0 );
            InitCurrent();
        }
    }


    void GameStart() {

        if (canInit) {

            scores = 0;
            timer.currentLevel = 1;

            InitLevel();

            notAdded = true;
        }

        gameState = GameState.game;
    }

    void AddScore(int count) {
        scores += count;
    }

    float alpha;

    void OnGUI() {

        gameField.DrawBorder();

        alpha = ( gameState == GameState.game ? 1 : 0.2f );

        gameField.Draw( currentShape.matrix, alpha );
        gameField.Draw( level.levelShape.matrix, alpha );
    }

    DataIO io;
    void Save() {

        Debug.Log( "Save data" );

        DataIO.io.Save( new Data( scores, timer.currentLevel, level.levelShape.matrix, currentShape.matrix ) );
        DataIO.io.Save( scoresContainer );

    }

    void Load() {

        Data data = DataIO.io.Load<Data>();

        if (data != null) {
            scores = data._scores;
            timer.currentLevel = data._level;

            InitLevel();
            level.levelShape.Set( data.LevelData );
            currentShape.Set( data.ShapeData );

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
    }

    void OnApplicationQuit() {
        Save();
    }

    bool ValidSide {
        get {
            foreach (var pair in currentShape.matrix) {
                if (pair.Key.x < 0 || pair.Key.x > gameField.wSize - 1) {
                    return false;
                }
            }

            return true;
        }
    }
    bool UnderFloor {
        get {
            foreach (var pair in currentShape.matrix) {
                if (pair.Key.y < gameField.hSize - 1) {
                    return false;
                }
            }

            return true;
        }
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
            return gameState == GameState.over;
        }
    }
}
