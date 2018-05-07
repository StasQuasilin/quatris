using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IShape {

    public int minX, maxX;
    public int minY, maxY;

    internal Dictionary<Key2D, Color> matrix;

    public bool Contain(Key2D key) {
        return matrix.ContainsKey(key);
    }

    List<Key2D> removas = new List<Key2D>();
    List<KeyValuePair<Key2D, Color>> addeds = new List<KeyValuePair<Key2D, Color>>();
    public void Move(int x, int y) {

        foreach (var pair in matrix) {
            removas.Add(pair.Key);
            addeds.Add(new KeyValuePair<Key2D, Color>(
                    new Key2D(pair.Key.x + x, pair.Key.y + y),
                    pair.Value
                ));
        }

        while(removas.Count > 0) {
            matrix.Remove(removas[0]);
            removas.RemoveAt(0);
        }

        while(addeds.Count > 0) {
            matrix.Add(addeds[0].Key, addeds[0].Value);
            addeds.RemoveAt(0);
        }
    }

    public void CheckBounds() {

        minX = minY = int.MaxValue;
        maxX = maxY = int.MinValue;

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

    }

}
