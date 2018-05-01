
using System.Collections.Generic;
using UnityEngine;

public class GameShape {

    public static int commonIndex = 0;
    public int privateIndex;
    internal Color shapeColor;

    static Colorizer colorizer = Colorizer.Instance;

    internal Dictionary<Vector2Int, Color> matrix = new Dictionary<Vector2Int, Color>();
    public int minX, minY, maxX, maxY;

    public GameShape(Shape shape, int x, int y) {
        privateIndex = commonIndex++;
        shapeColor = colorizer.RandomColor();
        InitShape(shape, x, y);
    }

    void InitShape(Shape shape, int x, int y) {
        for (int i = 0; i < shape.xSize; i++) {
            for (int j = 0; j < shape.ySize; j++) {
                if (shape.values[i + j * shape.ySize]) {
                    matrix.Add(new Vector2Int(x + i, y + j), shapeColor);
                }
            }
        }
    }

    List<Vector2Int> removas = new List<Vector2Int>();
    List<KeyValuePair<Vector2Int, Color>> addeds = new List<KeyValuePair<Vector2Int, Color>>();
    public void Move(int x, int y) {

        foreach (var pair in matrix) {
            addeds.Add(new KeyValuePair<Vector2Int, Color>(new Vector2Int(pair.Key.x + x, pair.Key.y + y), pair.Value));
            removas.Add(pair.Key);
        }

        while (removas.Count > 0) {
            matrix.Remove(removas[0]);
            removas.RemoveAt(0);
        }

        while (addeds.Count > 0) {
            matrix.Add(addeds[0].Key, addeds[0].Value);
            addeds.RemoveAt(0);
        }
    }

    Vector2Int tk;
    public bool Contain(int x, int y) {
        tk = new Vector2Int(x, y);

        return matrix.ContainsKey(tk);
    }

    internal void Check() {
        minX = minY = int.MaxValue;
        maxX = maxY = int.MinValue;

        foreach (var pair in matrix) {

            if (pair.Key.x < minX) {
                minX = pair.Key.x;
            }

            if (pair.Key.y < minY) {
                minY = pair.Key.y;
            }

            if (pair.Key.x > maxX) {
                maxX = pair.Key.x;
            }

            if (pair.Key.y > maxY) {
                maxY = pair.Key.y;
            }

        }
    }

    public int Min {
        get {
            return (minX < minY ? minX : minY);
        }
    }

    public int Max {
        get {
            return (maxX > maxY ? maxX : maxY);
        }
    }


}
