using UnityEngine;

public class ShapeEditor : MonoBehaviour {

    public Shapes shapes;

    void AddShape() {
        shapes.values.Add(new Shapes.ShapeValue());
    }

    Rect r1;
    Rect rr;
    Rect r2;
    Rect r3;
    float rX, rY;

	void OnGUI() {
        r1 = new Rect(2, 2, 100, 20);
        
        if (GUI.Button(r1, "Add")) {
            AddShape();
        }

        r1.y += 18;

        if (shapes.values != null) {
            foreach (Shapes.ShapeValue v in shapes.values) {

                r2 = new Rect( r1 );
                r2.x += 20;
                r2.y += 18;

                r3 = new Rect( r2 );
                r3.x += 50;
                r3.y += 5;

                GUI.Label( r1, v.ToString() );

                int nX = v.xSize;
                int nY = v.ySize;

                GUI.Label( r2, string.Format( "X: {0}", v.xSize ) );
                nX = ( int ) GUI.HorizontalSlider( r3, nX, 1, 4 );

                rX = r3.x + r3.width + 2;
                rY = r2.y;
                r2.y += 18;
                r3.y += 18;


                GUI.Label( r2, string.Format( "Y: {0}", v.ySize ) );
                nY = ( int ) GUI.HorizontalSlider( r3, nY, 1, 4 );

                v.ChangeSize( nX, nY );
                int k = 0;
                for (int i = 0; i < v.xSize; i++) {
                    for (int j = 0; j < v.ySize; j++) {
                        v.values[ k ] = GUI.Toggle( new Rect( i * 15 + rX, j * 15 + rY, 20, 20 ), v.values[ k ], "" );
                        k++;
                    }
                }

                r1.y += 80;
            }
        }
    }
}
