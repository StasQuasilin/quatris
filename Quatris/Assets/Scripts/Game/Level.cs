using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    LevelShape levelShape;
    GameField gameField;

    public void Add(Shape shape) {
        levelShape.Add(shape.matrix);
    }

    public void Init(Shapes.ShapeValue values) {
        levelShape = new LevelShape(values, 0, 0);
        Align();
    }

    void Align() {
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

}
