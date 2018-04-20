﻿using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shapes : MonoBehaviour {

    List<Shape> lastShapes = new List<Shape>();

    [SerializeField]
    public List<Shape> shapes = new List<Shape>();

    List<Shape> removes = new List<Shape>();
    public void Remove(Shape item) {
        removes.Add( item );
    }

    public void Remove() {
        while(removes.Count > 0) {
            shapes.Remove( removes[ 0 ] );
            removes.RemoveAt( 0 );
        }
    }

    public Shape RandomShape() {
        Shape s;

        while (lastShapes.Contains( s = RShape() )) ;

        lastShapes.Add( s );

        while (lastShapes.Count > 3) {
            lastShapes.RemoveAt( 0 );
        }

        return s;

    }

    Shape RShape() {
        return shapes[ ( int ) ( Random.value * ( shapes.Count - 1 ) ) ];
    }

    public void Clear() {
        shapes.Clear();
        Shape.index = 0;
    }
}
