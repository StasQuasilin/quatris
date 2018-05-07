using System.Collections.Generic;
using UnityEngine;

public class Shapes : MonoBehaviour {

    List<Shape> lastShapes = new List<Shape>();

    public List<Shape> shapes = new List<Shape>();

    Shape s;

    public Shape RandomShape() {
        

        while (lastShapes.Contains( s = RShape() )) ;

        lastShapes.Add( s );

        while (lastShapes.Count > shapes.Count * 0.5f) {
            lastShapes.RemoveAt( 0 );
        }
        
        Debug.Log( string.Format( "Get \"{0}\"", s.ToString() ) );
        return s;

    }

    Shape RShape() {
        return shapes[ ( int )Mathf.Floor ( Random.value * ( shapes.Count ) ) ];
    }
}
