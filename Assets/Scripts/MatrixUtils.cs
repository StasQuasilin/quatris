using System.Collections.Generic;
using UnityEngine;

public class MatrixUtils : MonoBehaviour {

	static Vector2Int k1, k2;

	static void Check(Dictionary<Vector2Int, Color> matrix, Vector2Int from, Vector2Int to) {
		
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

	static Dictionary<Vector2Int, Color> temp = new Dictionary<Vector2Int, Color>();
    public static void Left(Dictionary<Vector2Int, Color> matrix) {
        Right(matrix);
        Right(matrix);
        Right(matrix);
    }
    public static void Right(Dictionary<Vector2Int, Color> matrix) {

        CheckSize(matrix);
		string mText = "";

		foreach (var pair in matrix) {
			mText += pair.Key;
		}

		Debug.Log(string.Format("\tRotate: miX:{0}, maX{1}, miY{2}, maY{3}\n\tMatrix keys:{4}", minX, maxX, minY, maxY, mText));

        for (int i = minX; i < maxX +1 ; i++) {
            for (int j = minY; j < maxY +1; j++) {

				k1 = new Vector2Int( i, j );
				k2 = new Vector2Int( maxX - (j - minY), minY + (i - minX) );

				if (matrix.ContainsKey (k1)) {
					
					Debug.Log (string.Format ("\t'{0}\' -> \'{1}\'\n\t-=-=-=-=-=-=-=-", k1, k2));

					if (!temp.ContainsKey (k2)) {
						temp.Add (k2, matrix [k1]);
						Debug.Log ("\t\t#" + k2 + "\n\t\t in temp");
					}
				} 
            }
        }
        
        matrix.Clear();
        if (temp.Count > 0) {
            foreach (var p in temp) {

                matrix.Add( p.Key, p.Value );

            }
        } else {
            Debug.LogError( "Temp matrix are empty" );
        }
        

        temp.Clear();
        
    }

    static public int minX, maxX, minY, maxY;
    static void CheckSize(Dictionary<Vector2Int, Color> matrix) {

        minX = int.MaxValue;
        maxX = int.MinValue;
        minY = int.MaxValue;
        maxY = int.MinValue;

        foreach (var pair in matrix) {
            if (pair.Key.x < minX) {
                minX = pair.Key.x;
			} 

            if (pair.Key.x > maxX) {
                maxX = pair.Key.x;
            }

            if (pair.Key.y < minY) {
                minY = pair.Key.y;
			} 

            if (pair.Key.y > maxY) {
                maxY = pair.Key.y;
            }
        }

        /*
        int min = ( minX <= minY ? minX : minY );
        int max = ( maxX >= maxY ? maxX : maxY );

        for (int i = min; i < max; i++) {
            for (int j = min; j < max; j++) {

                Vector2Int key = new Vector2Int( i, j );
                if (!matrix.ContainsKey( key )) {
                    matrix.Add( key, false );
                }
            }
        }
        */
    }

    static void CheckEmpty(Dictionary<Vector2Int, bool> matrix, Vector2Int key) {
        if (!matrix.ContainsKey( key )) {
            matrix.Add( key, false );
        }
    }
	
}
