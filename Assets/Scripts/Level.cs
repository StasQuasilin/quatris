using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public Dictionary<Key2D, bool> matrix = new Dictionary<Key2D, bool>();
    public int minX, maxX, minY, maxY;

    public bool Check(List<Key2D> keys) {

        foreach (Key2D key in keys) {
            if (matrix.ContainsKey(key)) {
                return false;
            }
        }
        return true;
    }

    public void Add(Shape shape) {
        foreach (Key2D key in shape.keys) {
            matrix.Add( key, true );
        }

        CheckSize();
    }

    void CheckSize() {

        minX = int.MaxValue;
        maxX = int.MinValue;
        minY = int.MaxValue;
        maxY = int.MinValue;

        foreach(var pair in matrix) {
            if (pair.Key.X < minX) {
                minX = pair.Key.X;
            }

            if (pair.Key.X > maxX) {
                maxX = pair.Key.X;
            }

            if (pair.Key.Y < minY) {
                minY = pair.Key.Y;
            }

            if (pair.Key.Y > maxY) {
                maxY = pair.Key.Y;
            }
        }
    }

    public void Rotate(int value) {
        MatrixUtils.RotateLeft( minX, maxX, minY, maxY, matrix );

        CheckSize();
    }
    
}
