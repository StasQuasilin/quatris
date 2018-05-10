using System.Collections;
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

    internal int scores;
    internal int Level {
        get {
            return timer.currentLevel;
        }
    }

    internal bool isGame = false;
    internal bool isPause = false;
    internal bool isGameOver = false;

    void Start() {

        io = new DataIO( Application.dataPath + "\\data.t" );

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

        isPause = true;
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

        isGameOver = level.GameOver;

        if (isGame && !isGameOver) {

            if (input.Pause()) {
                isPause = !isPause;

                if (!isPause) {
                    Save();
                }
            }

            if (isPause) {

                ////////////////LEVEL ROTATION////////////////

                if (input.LevelRight()) {
                    level.Right();
                } else if (input.LevelLeft()) {
                    level.Left();
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

                    }
                }

                ////////////////SHAPE MOVE////////////////
                if (input.ShapeLeft()) {

                    currentShape.Move( -1, 0 );

                    if (level.Contain( currentShape.matrix ) || !ValidSide) {
                        currentShape.Move( 1, 0 );
                    }
                } else if (input.ShapeRight()) {

                    currentShape.Move( 1, 0 );

                    if (level.Contain( currentShape.matrix ) || !ValidSide) {
                        currentShape.Move( -1, 0 );
                    }
                }

                ////////////////SHAPE FALL////////////////
                timer.Fasta = input.ShapeFall();

                if (timer.isTime()) {
                    currentShape.Move( 0, 1 );

                    if (level.Contain( currentShape.matrix )) {
                        currentShape.Move( 0, -1 );
                        level.Add( currentShape );

                        AddScore( currentShape.matrix.Count );
                        AddScore( level.CheckDrops() * 3 );
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
            }
        } else {
            if (Input.anyKey || Input.touchCount > 0) {
                GameStart();
            }
        }
	}

    void GameStart() {
        if (!isGameOver) {
            isGame = true;
        } else {
            scores = 0;
            timer.currentLevel = 1;
            InitLevel();
        }
    }

    void AddScore(int count) {
        scores += count;
    }

    void OnGUI() {
        gameField.DrawBorder();
        if (isPause) {
            gameField.Draw(currentShape.matrix);
            gameField.Draw(level.levelShape.matrix);
        }
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
            foreach(var pair in currentShape.matrix) {
                if (pair.Key.y < gameField.hSize - 1) {
                    return false;
                }
            }

            return true;
        }
    }

    DataIO io;
    void Save() {

        io.Save( scores, timer.currentLevel, level.levelShape.matrix, currentShape.matrix );
    }

    void Load() {
        Data data = io.Load();

        if (data != null) {
            scores = data._scores;
            timer.currentLevel = data._level;

            InitLevel();
            level.levelShape.Set( data.LevelData );
            currentShape.Set( data.ShapeData );

            level.Align();
        }
    }

    void OnApplicationQuit() {
        Save();
    }
}
