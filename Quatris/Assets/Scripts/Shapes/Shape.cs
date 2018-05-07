using System.Collections.Generic;
using UnityEngine;

public class Shape : IShape {

    public Shape(Shapes.ShapeValue values, int x, int y) {
        matrix = new Dictionary<Key2D, Color>();
        Color shapeColor = Colorizer.Instance.GetColor();

        int k = 0;
        for (int i = 0; i < values.xSize; i++) {
            for (int j = 0; j < values.ySize; j++) {
                if (values.values[k]) {
                    matrix.Add(new Key2D(x + i, y + j), shapeColor);
                }
                k++;
            }
        }

        
    }
}
