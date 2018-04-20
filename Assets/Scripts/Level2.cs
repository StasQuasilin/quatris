using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour {

    Dictionary<Key2D, bool> levelMatrix = new Dictionary<Key2D, bool>();

    public void Add(Dictionary<Key2D, bool> keys) {
        foreach(var pair in keys) {

            Add( pair.Key, pair.Value );

        }
    }

    public void Add(Key2D key, bool value) {
        if (levelMatrix.ContainsKey( key )) {
            levelMatrix[ key ] = value;
        } else {
            levelMatrix.Add( key, value );
        }
    }

    Key2D tempKey;
    public bool Contain(int x, int y) {

        tempKey = new Key2D( x, y );

        return levelMatrix.ContainsKey( tempKey ) && levelMatrix[tempKey];

    }

    public bool Contain(Dictionary<Key2D, bool> keys, int x, int y) {

        foreach (var pair in keys) {
            tempKey = new Key2D( pair.Key.X + x, pair.Key.Y + y );

            if (levelMatrix.ContainsKey(tempKey) && levelMatrix[tempKey]) {
                return true;
            }
        }

        return false;
    }
}
