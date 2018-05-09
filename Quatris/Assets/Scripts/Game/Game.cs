using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    Shapes shapeFactory;
    Level level;
    public Texture2D cube;
    public int fieldWidth = -1;
    public GameField gameField;
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

    void Start() {

        shapeFactory = FindObjectOfType<Shapes>();

        if (fieldWidth == -1) {
            gameField = new GameField(cube);
        } else {
            gameField = new GameField(fieldWidth, cube);
        }

        level = FindObjectOfType<Level>();
        if (level == null) {
            level = gameObject.AddComponent<Level>();
        }
        level.gameField = gameField;



        input = FindObjectOfType<GameInput>();
        if(input == null) {
            input = gameObject.AddComponent<GameInput>();
        }

        
    }

    void InitLevel() {
        Debug.Log("INIT LEVEL");

        level.Init(shapeFactory.GetShape());
        InitNext();
        InitCurrent();

        isGame = true;
    }

    void InitNext() {
        next = shapeFactory.GetShape();
    }

    void InitCurrent() {
        currentShape = new Shape(next, (gameField.wSize - next.xSize) / 2, 0);
        InitNext();
    }
	
	void Update () {
		if (level.IsEmpty) {
            InitLevel();
        }

        if (isGame) {

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

                if (level.Contain(currentShape.matrix)) {
                    MatrixUtil.RotateLeft( currentShape );
                    
                }
            }
            
            ////////////////SHAPE MOVE////////////////
            if (input.ShapeLeft()) {

                currentShape.Move(-1, 0);

                if (level.Contain(currentShape.matrix) || !ValidSide) {
                    currentShape.Move(1, 0);
                }
            } else if (input.ShapeRight()) {

                currentShape.Move(1, 0);

                if (level.Contain(currentShape.matrix) || !ValidSide) {
                    currentShape.Move(-1, 0);
                }
            }
            
            ////////////////SHAPE FALL////////////////
            timer.Fasta = input.ShapeFall();

            if (timer.isTime()) {
                currentShape.Move(0, 1);

                if (level.Contain(currentShape.matrix)) {
                    currentShape.Move(0, -1);
                    level.Add(currentShape);

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
	}

    void AddScore(int count) {
        scores += count;
    }

    void OnGUI() {
        if (isGame) {
            gameField.Draw(currentShape.matrix);
            gameField.Draw(level.levelShape.matrix);
        }
    }

    bool ValidSide {
        get {
            foreach (var pair in currentShape.matrix) {
                if (pair.Key.x < 0) {
                    return false;
                }
                if (pair.Key.x > gameField.wSize - 1) {
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

}
