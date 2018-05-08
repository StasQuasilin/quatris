using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixUtil {

    static Key2D k1, k2;
    static Dictionary<Key2D, Color> temp = new Dictionary<Key2D, Color>();

	public static void RotateRight(IShape shape) {
        shape.CheckBounds();

        for (int i = shape.minX; i < shape.maxX + 1; i++) {
            for (int j = shape.minY; j < shape.maxY + 1; j++) {

                k1 = new Key2D( i, j );
                k2 = new Key2D( shape.maxX - ( j - shape.minY ), shape.minY + ( i - shape.minX ) );

                if (shape.Contain(k1)) {
                    if (!temp.ContainsKey(k2)) {
                        temp.Add( k2, shape.matrix[ k1 ] );
                    }
                }
            }
        }

        if (temp.Count > 0) {
            shape.matrix.Clear();

            foreach (var p in temp) {
                shape.matrix.Add( p.Key, p.Value );
            }
        }

        temp.Clear();
    }

    public static void RotateLeft(IShape shape) {

        shape.CheckBounds();

        for (int i = shape.minX; i < shape.maxX + 1; i++) {
            for (int j = shape.minY; j < shape.maxY + 1; j++) {

                k1 = new Key2D( i, j );
                k2 = new Key2D( shape.minX + (j - shape.minY), shape.maxY - (i - shape.minX) );

                if (shape.Contain( k1 )) {
                    if (!temp.ContainsKey( k2 )) {
                        temp.Add( k2, shape.matrix[ k1 ] );
                    }
                }
            }
        }

        if (temp.Count > 0) {
            shape.matrix.Clear();

            foreach(var p in temp) {
                shape.matrix.Add( p.Key, p.Value );
            }
        }

        temp.Clear();
    }
}
