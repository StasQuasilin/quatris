using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public Dictionary<Key2D, bool> matrix = new Dictionary<Key2D, bool>();
    

    private void Start() {
        
    }

    Key2D target;
    public bool Check(Shape shape, int x, int y) {

        foreach (var pair in shape.keys) {
            target = new Key2D( pair.Key.X + x, pair.Key.Y );
            if (matrix.ContainsKey(target) && matrix[target]) {
                return false;
            }
        }
        return true;
    }
    public void Add(Shape shape) {
        foreach (var pair in shape.keys) {

            if (matrix.ContainsKey( pair.Key )) {
                matrix[ pair.Key ] = pair.Value;
            } else {
                matrix.Add( pair.Key, pair.Value );
            }

        }

    }

    

    public void Rotate(int value) {
        if (value == 1) {
            //MatrixUtils.RotateLeft( ( maxX  <= maxY ? maxX : maxY), ( minX <= minY ? minX : minY ), matrix );
            MatrixUtils.RotateRight( matrix );
        } else {
            //(minX <= minY ? minX : minY)
            MatrixUtils.RotateLeft( matrix );
        }
    }
    
}
