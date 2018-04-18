using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public Dictionary<Key2D, bool> matrix = new Dictionary<Key2D, bool>();
    public int minX, maxX, minY, maxY;

    public bool Check(Shape shape) {

        foreach (var pair in shape.keys) {
            if (matrix.ContainsKey(pair.Key) && matrix[pair.Key]) {
                return false;
            }
        }
        return true;
    }
    public void Add(Shape shape) {
        foreach (var pair in shape.keys) {
            matrix.Add( pair.Key, pair.Value );
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

        int min = ( minX <= minY ? minX : minY );
        int max = ( maxX >= maxY ? maxX : maxY );

        for (int i = min; i < max; i++) {
            for (int j = min; j < max; j++) {

                Key2D key = new Key2D( i, j );
                if (!matrix.ContainsKey(key)) {
                    matrix.Add( key, false );
                }
            }
        }
    }

    public void Rotate(int value) {
        if (value == 1) {
            //MatrixUtils.RotateLeft( ( maxX  <= maxY ? maxX : maxY), ( minX <= minY ? minX : minY ), matrix );
            MatrixUtils.RotateRight( minX, maxX, minY, maxY, matrix );
        } else {
            //(minX <= minY ? minX : minY)
            MatrixUtils.RotateLeft(minX, maxX, minY, maxY, matrix );
        }

        CheckSize();
    }
    
}
