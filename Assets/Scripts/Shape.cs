using System.Collections.Generic;
using System;

[Serializable]
public class Shape {

    public static int index = 0;
    public int privateIndex;

    public bool show = true;
    public int xSize = 4, ySize = 4;
    public bool[] values;
    public List<Key2D> keys = new List<Key2D>();

    public void InitKeys(int x, int y) {
        for (int i = 0; i < xSize; i++) {
            for (int j = 0; j < ySize; j++) {
                if (values[i + j * ySize]) {
                    keys.Add( new Key2D( x + i, y + j ) );
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

    void InitValues(int x1, int x2, int y1, int y2) {
        
        bool[] temp = new bool[ x2 * y2];

        if (values != null) {

            int tX = ( x1 <= x2 ? x1 : x2 );
            int tY = ( y1 <= y2 ? y1 : y2 );

            for (int i = 0; i < tX; i++) {
                for (int j = 0; j < tY; j++) {
                    temp[ i + j * x2 ] = values[ i + j * x2 ];
                }
            }
        }

        values = temp;
    }

}
