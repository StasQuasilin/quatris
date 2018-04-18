using System.Collections.Generic;

public class MatrixUtils {

    static bool tmp;
    static Key2D k1, k2;

    public static void RotateRight(int n, Dictionary<Key2D, bool> matrix){

        Dictionary<Key2D, bool> temp = matrix;

        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {

                k1 = new Key2D( i, j );
                k2 = new Key2D( n - j - 1, i );

                CheckEmpty( matrix, k1 );
                CheckEmpty( matrix, k2 );

                temp[ k1 ] = matrix[ k2 ];

                /*
                //tmp = matr[ i ][ j ];
                k1 = new Key2D( i, j );
                CheckEmpty( matrix, k1 );
                tmp = matrix[ k1 ];

                //matr[ i ][ j ] = matr[ n - j - 1 ][ i ];
                k2 = new Key2D( n - j - 1, i );
                CheckEmpty( matrix, k2 );
                matrix[ k1 ] = matrix[ k2 ];

                //matr[ n - j - 1 ][ i ] = matr[ n - i - 1 ][ n - j - 1 ];
                k1 = new Key2D( n - i - 1, n - j - 1 );
                CheckEmpty( matrix, k1 );
                matrix[ k2 ] = matrix[ k1 ];

                //matr[ n - i - 1 ][ n - j - 1 ] = matr[ j ][ n - i - 1 ];
                k2 = new Key2D( j, n - i - 1 );
                CheckEmpty( matrix, k2 );
                matrix[ k1 ] = matrix[ k2 ];

                //matr[ j ][ n - i - 1 ] = tmp;
                matrix[ k2 ] = tmp;
                */
            }
        }

        matrix = temp;
    }

    public static void RotateLeft(int minX, int maxX, int minY, int maxY, Dictionary<Key2D, bool> matrix) {
        for (int i = minX; i < maxX; i++) {
            for (int j = minY; j < maxY; j++) {

                k1 = new Key2D( i, j );
                CheckEmpty( matrix, k1 );
                tmp = matrix[ k1 ];

                k2 = new Key2D( j, maxX - 1 - i );
                CheckEmpty( matrix, k2 );
                matrix[ k1 ] = matrix[ k2 ];

                k1 = new Key2D( maxX - 1 - i, maxY - 1 - j );
                CheckEmpty( matrix, k1 );
                matrix[ k2 ] = matrix[ k1 ];

                k2 = new Key2D( maxY - 1 - j, i );
                CheckEmpty( matrix, k2 );
                matrix[ k1 ] = matrix[ k2 ];

                matrix[ k2 ] = tmp;
            }
        }
    }

    public static void RotateLeft(int N, Dictionary<Key2D, bool> matrix) {
        

        //for (int i = 0; i < n / 2; i++)
        for (int i = 0; i < N / 2; i++) {

            //for (int j = i; j < n - 1 - i; j++)
            for (int j = i; j < N - 1 - i; j++) {

                //tmp = matr[ i ][ j ];
                k1 = new Key2D( i, j );
                CheckEmpty( matrix, k1 );
                tmp = matrix[ k1 ];

                //matr[ i ][ j ] = matr[ j ][ n - 1 - i ];
                k2 = new Key2D( j, N - 1 - i );
                CheckEmpty( matrix, k2 );
                matrix[ k1 ] = matrix[ k2 ];

                //matr[ j ][ n - 1 - i ] = matr[ n - 1 - i ][ n - 1 - j ];
                k1 = new Key2D( N - 1 - i, N - 1 - j );
                CheckEmpty( matrix, k1 );
                matrix[ k2 ] = matrix[ k1 ];

                //matr[ n - 1 - i ][ n - 1 - j ] = matr[ n - 1 - j ][ i ];
                k2 = new Key2D( N - 1 - j, i );
                CheckEmpty( matrix, k2 );
                matrix[ k1 ] = matrix[ k2 ];

                //matr[ n - 1 - j ][ i ] = tmp;
                matrix[ k2 ] = tmp;
            }
        }
    }

    static void CheckEmpty(Dictionary<Key2D, bool> matrix, Key2D key) {
        if (!matrix.ContainsKey( key )) {
            matrix.Add( key, false );
        }
    }
	
}
