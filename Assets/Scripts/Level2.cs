using System.Collections.Generic;
using UnityEngine;


public class Level2 : MonoBehaviour {

	Dictionary<Vector2Int, Color> levelMatrix = new Dictionary<Vector2Int, Color>();

	public void Add(Dictionary<Vector2Int, Color> keys) {

        foreach(var pair in keys) {

            Add( pair.Key, pair.Value );

        }
    }

	public void Add(Vector2Int key, Color value) {

        if (levelMatrix.ContainsKey( key )) {
            levelMatrix[ key ] = value;
        } else {
            levelMatrix.Add( key, value );
        }

    }

	Vector2Int tempKey;
    public bool Contain(int x, int y) {

		tempKey = new Vector2Int( x, y );

        return levelMatrix.ContainsKey( tempKey );

    }

	public bool Contain(Dictionary<Vector2Int, Color> keys, int x, int y) {

        bool result = false;

        foreach (var pair in keys) {

			tempKey = new Vector2Int( pair.Key.x + x, pair.Key.y + y );

            if (levelMatrix.ContainsKey(tempKey)) {
                result = true;
            }
        }

        return result;
    }

    public void Right() {
        MatrixUtils.Right(levelMatrix);
    }

    public void Left() {
        MatrixUtils.Left(levelMatrix);
    }
}
