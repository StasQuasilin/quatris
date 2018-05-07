using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelShape : Shape {

    public LevelShape(Shapes.ShapeValue values, int x, int y) : base (values, x, y) {
        
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
