using System.Collections.Generic;
using System;

[Serializable]
public class Shape {

    public static int index = 0;
    public int privateIndex;

    public bool show = true;
    public int xSize = 4, ySize = 4;
    public bool[] values;
    public Dictionary<Key2D, bool> keys = new Dictionary<Key2D, bool>();

    public void InitKeys(int x, int y) {
        keys.Clear();

        for (int i = 0; i < xSize; i++) {
            for (int j = 0; j < ySize; j++) {
                if (values[ i + j * ySize ]) {
                    keys.Add( new Key2D( x + i, y + j ), values[ i + j * ySize ] );
                }
            }
        }
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

    public void Move(int x, int y) {

        foreach (var pair in keys) {
            pair.Key.Add( x, y );
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

    Key2D tk;
    public bool Contain(int x, int y) {
        tk = new Key2D( x, y );

        return keys.ContainsKey( tk ) && keys[ tk ];
    }

    public override string ToString() {
        return "Shape " + privateIndex;
    }

}
