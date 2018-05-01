using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelShape : GameShape {

    public LevelShape(Shape shape, int x, int y) : base(shape, x, y) {

    }

    public void Add(GameShape shape) {
        foreach(var pair in shape.matrix) {
            if (matrix.ContainsKey(pair.Key)) {
                matrix[pair.Key] = pair.Value;
            } else {
                matrix.Add(pair.Key, pair.Value);
            }
        }
    }

    public int DropLine(int line) {
        Vector2Int check;
        int result = 0;

        for (int i = minX; i <= maxX; i++) {
            check = new Vector2Int(i, line);
            if (matrix.ContainsKey(check)) {
                result++;
                matrix.Remove(check);
            }
        }

        return result;
    }
}
