using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    Shapes shapeFactory;
    Level level;
    public Texture2D cube;
    public int fieldWidth = -1;
    GameField gameField;
    public GameTimer timer;
    GameInput input;

    Shape currentShape;
    Shapes.ShapeValue next;

    bool isGame = false;

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

            timer.Fasta = input.ShapeFall();

            if (timer.isTime()) {
                currentShape.Move(0, 1);

                if (level.Contain(currentShape.matrix)) {
                    currentShape.Move(0, -1);
                    level.Add(currentShape);
                    InitCurrent();
                }
            }
        }
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
                if (pair.Key.x < 0 || 
                    pair.Key.x > gameField.wSize - 1) {
                    return false;
                }
            }

            return true;
        }
    }
}
