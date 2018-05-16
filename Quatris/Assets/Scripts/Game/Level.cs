using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    internal LevelShape levelShape;
    internal GameField gameField;

    public void Add(Shape shape, int addRandomPoints) {
        levelShape.Add(shape.matrix);

        levelShape.CheckBounds();

        for (int i = 0; i < addRandomPoints; i++) {

            int rX = Random.Range( levelShape.minX, levelShape.maxX );
            int rY = Random.Range( levelShape.minY, levelShape.maxY );

            Key2D k = new Key2D( rX, rY );
            if (!levelShape.Contain(k)) {
                levelShape.Add( k, Colorizer.Instance.GetColor() );
            }
        }

        Align();
    }

    public void Init() {
        levelShape = new LevelShape( new Shapes.ShapeValue(), 0, 0 );
    }

    public void Init(Shapes.ShapeValue values) {
        levelShape = new LevelShape(values, 0, 0);
        Align();
    }

    public bool Contain(Dictionary<Key2D, Color> mtrx) {
        return levelShape.Contain( mtrx );
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

    

    public int CheckDrops() {

        int result = 0;

        for (int i = 0; i < 4; i++) {
            result += Check();

            Right();
            Align();
        }

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
