using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Level2))]
public class GameControl2 : MonoBehaviour {

    private bool drawMatrix;
    public Texture2D full, empty;
    public float scale = 1;

    Level2 level;
    Shapes shapes;
    Shape next, current;

    int fieldW;
    int fieldH;

    GUIStyle style;

    void Awake() {
        if (level == null) {
            level = GetComponent<Level2>();
        }

        if (level == null) {
            level = FindObjectOfType<Level2>();
        }

        if (level == null) {
            level = gameObject.AddComponent<Level2>();
        }

        shapes = FindObjectOfType<Shapes>();

    }


    void Start() {

        fieldW = Screen.width / empty.width;

        if (fieldW % 2 == 0) {
            fieldW++;
        }

        scale = 1f * Screen.width / ( fieldW * empty.height );

        fieldH = (int)(Screen.height / (empty.height * scale));

        

        style = new GUIStyle();
        style.fontSize = 9;
        style.normal.textColor = Color.gray;

        InitLevel();

    }

    void InitLevel() {

        Shape shape = shapes.RandomShape();
        shape.InitKeys( (fieldW - shape.xSize) / 2, ( int ) ( fieldH * 0.75f ) );
        level.Add( shape.keys );

        InitNext();
        ChangeShape();

    }

    void InitNext() {

        next = shapes.RandomShape();
        next.InitKeys( ( fieldW - next.xSize ) / 2, ( - next.ySize) / 2 );

    }

    void ChangeShape() {

        Debug.Log( "Change Shape" );
        current = next;
        InitNext();

    }

    int sideInput;
    void Update() {

        drawMatrix = Input.GetKey( KeyCode.M );
        
        if (current != null) {

            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                sideInput = -1;
            } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                sideInput = 1;
            } else {
                sideInput = 0;
            }

            if (sideInput != 0) {
                if (!level.Contain( current.keys, sideInput, 0 )) {
					Debug.Log("###########################################################\n" +
						"\tMove: " + current.keys.Count );
                    current.Move( sideInput, 0 );
					Debug.Log ("###########################################################\n" +
						"Keys after move");

					foreach (var pair in current.keys) {
						Debug.Log ("\t" + pair.Key);
					}
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                MatrixUtils.Right( current.keys );
                if (level.Contain(current.keys, 0, 0)) {
                    MatrixUtils.Left(current.keys);
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow)) {

                if (!level.Contain(current.keys, 0, 1)) {
                    current.Move( 0, 1 );
                } else {
                    level.Add( current.keys );
                    ChangeShape();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            level.Left();
        } else if (Input.GetKeyDown(KeyCode.D)) {
            level.Right();
        }
    }

    Rect r;
    bool drawBlack;
    bool drawWhite;
    void OnGUI() {
        
        for(int i = 0; i < fieldW; i++) {
            for (int j = 0; j < fieldH; j++) {

                r = new Rect( i * empty.width * scale, j * empty.height * scale, empty.width * scale, empty.height * scale );

                if (level.Contain( i, j ) ) {

					GUI.DrawTexture( r, full );

                }

                if (drawMatrix) {
                      
                    if (!level.Contain( i, j) && !current.Contain( i, j )) {
                        GUI.DrawTexture( r, empty );
                    }

                    r.x += 2;
                    r.y += 2;

                    GUI.Label( r, string.Format( "{0}:{1}", i, j ), style );
                    
                }
            }
        }

        foreach (var pair in current.keys) {
            
            r = new Rect( pair.Key.x * empty.width * scale, pair.Key.y * empty.height * scale, empty.width, empty.height );
            GUI.DrawTexture( r, full );

            r.x += 2;
            r.y += 2;

            GUI.Label( r, string.Format( "{0}:{1}", pair.Key.x, pair.Key.y ), style );

        }
    }
}
