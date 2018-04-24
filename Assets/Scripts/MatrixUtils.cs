using System.Collections.Generic;
using UnityEngine;

public class MatrixUtils : MonoBehaviour {

    static bool tmp;
    static Key2D k1, k2;

    public static void RotateLeft(Dictionary<Key2D, bool> matrix) {

        CheckSize( matrix );

        for (int i = minX; i < maxX / 2; i++) {
            for (int j = i; j < maxY - 1 - i; j++) {

                k1 = new Key2D( i, j );

                tmp = matrix.ContainsKey( k1 );

                k2 = new Key2D( maxX - 1 - j, i );
                Check(matrix, k2, k1 );

                k1 = new Key2D( maxY - 1 - i, maxX - 1 - j);
                Check(matrix, k1, k2 );

                k2 = new Key2D( j, maxY - 1 - i );
                Check( matrix, k2, k1 );

                if (tmp) {
                    matrix[ k2 ] = tmp;
                } else {
                    matrix.Remove( k2 );
                }
                
            }
        }
    }

    static void Check(Dictionary<Key2D, bool> matrix, Key2D from, Key2D to) {
        if (matrix.ContainsKey(from)) {
            if (matrix.ContainsKey(to)) {
                matrix[ to ] = matrix[ from ];
            } else {
                matrix.Add( to, matrix[ from ] );
            }
            
        } else {
            matrix.Remove( to );
        }
    }

    public static void RotateRight(Dictionary<Key2D, bool> matrix) {

        CheckSize( matrix );

        Debug.Log( string.Format( "Rotate right, min X: {0}, max X: {1}, min Y: {2}, max Y {3}", minX, maxX, minY, maxY ) );

        int n = ( maxX >= maxY ? maxX : maxY );

        for (int i = minX; i < n / 2; i++) {
            for (int j = minY; j < n - 1 - i; j++) {

                k1 = new Key2D( i, j );
                tmp = matrix.ContainsKey(k1);

                k2 = new Key2D( j, n - 1 - i );
                Check( matrix, k2, k1 );

                k1 = new Key2D( n - 1 - i, n - 1 - j );
                Check( matrix, k1, k2 );

                k2 = new Key2D( n - 1 - j, i );
                Check(matrix, k2, k1 );

                if (tmp) {
                    matrix[ k2 ] = tmp;
                } else {
                    matrix.Remove( k2 );
                }
                
            }
        }
    }

    static Dictionary<Key2D, bool> temp = new Dictionary<Key2D, bool>();

    public static void Right(Dictionary<Key2D, bool> matrix) {

        CheckSize(matrix);
        Debug.Log(string.Format("\tRotate: miX:{0}, maX{1}, miY{2}, maY{3}\n\t\tMatrix count:{4}", minX, maxX, minY, maxY, matrix.Count));

        for (int i = minX; i < maxX +1 ; i++) {
            for (int j = minY; j < maxY +1; j++) {

                k1 = new Key2D( i, j );
                k2 = new Key2D( maxX - (j - minY), minY + (i - minX) );

                Debug.Log( string.Format( "\'{0}\' -> \'{1}\':{2}", k1.Value, k2.Value, matrix.ContainsKey( k1 ) ) );

                if (matrix.ContainsKey( k1 )) {

                    if (temp.ContainsKey( k2 )) {
                        temp[ k2 ] = matrix[ k1 ];
                        Debug.Log("\tTemp[ " + k2.Value + " ] = Matrix[ " + k1.Value + " ]");
                    } else {
                        temp.Add( k2, matrix[ k1 ] );
                        Debug.Log( "\tTemp.Add( " + k2.Value + ", " + matrix[ k1 ] );
                    }

                } else {
                    Debug.Log( "NO KEY " + k1.Value );
                }
                
            }
        }
        
        matrix.Clear();
        Debug.Log( "\tTemp: " + temp.Count );
        foreach(var p in temp) {
            if (true) {
                matrix.Add( p.Key, p.Value );
            }
        }

        temp.Clear();
        
    }

    static public int minX, maxX, minY, maxY;
    static void CheckSize(Dictionary<Key2D, bool> matrix) {

        minX = int.MaxValue;
        maxX = int.MinValue;
        minY = int.MaxValue;
        maxY = int.MinValue;

        foreach (var pair in matrix) {
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

        /*
        int min = ( minX <= minY ? minX : minY );
        int max = ( maxX >= maxY ? maxX : maxY );

        for (int i = min; i < max; i++) {
            for (int j = min; j < max; j++) {

                Key2D key = new Key2D( i, j );
                if (!matrix.ContainsKey( key )) {
                    matrix.Add( key, false );
                }
            }
        }
        */
    }

    static void CheckEmpty(Dictionary<Key2D, bool> matrix, Key2D key) {
        if (!matrix.ContainsKey( key )) {
            matrix.Add( key, false );
        }
    }
	
}
