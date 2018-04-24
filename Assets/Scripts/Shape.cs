using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Shape {

    public static int index = 0;
    public int privateIndex;

    public bool show = true;
    public int xSize = 4, ySize = 4;
    public bool[] values;

    public Dictionary<Vector2Int, Color> keys = new Dictionary<Vector2Int, Color>();

    public void InitKeys(int x, int y) {
        keys.Clear();

        for (int i = 0; i < xSize; i++) {
            for (int j = 0; j < ySize; j++) {
                if (values[ i + j * ySize ]) {
					keys.Add( new Vector2Int( x + i, y + j ), RandomColor() );
                }
            }
        }
    }

    static Color RandomColor() {
        return Color.black;
    }

    public Shape() {
        privateIndex = index++;
        InitValues( xSize, xSize, ySize, ySize );
    }

    public void Check(int x, int y) {
        if (x != xSize || y != ySize) {
            InitValues( xSize, x, ySize, y );

            xSize = x;
            ySize = y;
        }
    }

	List<Vector2Int> removas = new List<Vector2Int> ();
	List<KeyValuePair<Vector2Int, Color>> addeds = new List<KeyValuePair<Vector2Int, Color>> ();
    public void Move(int x, int y) {

        foreach (var pair in keys) {
			Vector2Int newKey = new Vector2Int (pair.Key.x + x, pair.Key.y + y);

			if (!keys.ContainsKey(newKey)) {
				addeds.Add (new KeyValuePair<Vector2Int, Color>(newKey, pair.Value));
				removas.Add (pair.Key);
			}
        }

		while (removas.Count > 0) {
			keys.Remove (removas [0]);
			removas.RemoveAt (0);
		}

		while (addeds.Count > 0) {
			keys.Add (addeds [0].Key,addeds [0].Value);
			addeds.RemoveAt (0);
		}
    }

    void InitValues(int x1, int x2, int y1, int y2) {
        
        bool[] temp = new bool[ x2 * y2];

        if (values != null) {

            int tX = ( x1 <= x2 ? x1 : x2 );
            int tY = ( y1 <= y2 ? y1 : y2 );

            for (int i = 0; i < tX; i++) {
                for (int j = 0; j < tY; j++) {
                    temp[ i + j * y2 ] = values[ i + j * y2 ];
                }
            }
        }

        values = temp;
    }

    Vector2Int tk;
    public bool Contain(int x, int y) {
		tk = new Vector2Int( x, y );

        return keys.ContainsKey( tk );
    }

    public override string ToString() {
        return "Shape " + privateIndex;
    }

}
