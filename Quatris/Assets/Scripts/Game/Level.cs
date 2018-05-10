using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    internal LevelShape levelShape;
    internal GameField gameField;

    public void Add(Shape shape) {
        levelShape.Add(shape.matrix);
        Align();
    }

    public void Init() {
        levelShape = new LevelShape( new Shapes.ShapeValue(), 0, 0 );
    }

    public void Init(Shapes.ShapeValue values) {
        levelShape = new LevelShape(values, 0, 0);
        Align();
    }

    public void Align() {

        levelShape.CheckBounds();

        int hA = (gameField.wSize - (levelShape.minX + levelShape.maxX)) / 2;
        int vA = gameField.FieldCenter - (levelShape.maxY - (levelShape.maxY - levelShape.minY) / 2);

        levelShape.Move(hA, vA);
    }

    public bool IsEmpty {
        get {
            return levelShape == null || levelShape.IsEmpty;
        }
    }

    public bool Contain(Dictionary<Key2D, Color> mtrx) {
        foreach (var pair in mtrx) {
            if (levelShape.Contain(pair.Key)) {
                return true;
            }
        }

        return false;
    }

    public int CheckDrops() {

        int result = Check();

        Right();
        Align();

        result += Check();

        Left();
        Align();

        return result;
    }

    int Check() {

        int result = 0;

        levelShape.CheckBounds();

        //ROWS
        for (int i = levelShape.minY; i < levelShape.maxY + 1;) {
            bool dropIt = true;
            //COLUMNS
            for(int j = 0; j < gameField.wSize; j++) {
                if (!levelShape.Contain(new Key2D(j, i))) {
                    dropIt = false;
                    break;
                }
            }

            if (dropIt) {
                result += levelShape.Drop(i);
            } else {
                i++;
            }
        }

        return result;
    }

    public bool GameOver {
        get {
            foreach(var pair in levelShape.matrix) {
                if (pair.Key.y <= 0) {
                    return true;
                }
            }

            return false;
        }
    }

    public void Right() {
        MatrixUtil.RotateRight( levelShape );
        Align();
    }

    public void Left() {
        MatrixUtil.RotateLeft( levelShape );
        Align();
    }

}
