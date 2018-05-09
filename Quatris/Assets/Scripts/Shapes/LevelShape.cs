using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelShape : Shape {

    public LevelShape(Shapes.ShapeValue values, int x, int y) : base (values, x, y) {
        
    }

    public int Drop(int line) {
        int dropResult = 0;

        for (int i = minX; i < maxX + 1; i++) {
            Key2D dKey = new Key2D(i, line);

            if (matrix.ContainsKey(dKey)) {
                matrix.Remove(dKey);
                dropResult++;
            }
        }

        foreach (var pair in matrix) {
            if (pair.Key.y < line) {
                Move(pair, 0, 1);
            }
        }

        UpdateMatrix();

        return dropResult;
    }

    public void Add(Dictionary<Key2D, Color> mtrx) {

        foreach (var pair in mtrx) {
            if (Contain(pair.Key)) {
                matrix[pair.Key] = pair.Value;
            } else {
                matrix.Add(pair.Key, pair.Value);
            }
        }
        
    }

    public bool IsEmpty {
        get {
            return matrix == null || matrix.Count == 0;
        }
    }
}
