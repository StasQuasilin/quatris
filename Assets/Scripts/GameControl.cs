using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Level))]
public class GameControl : MonoBehaviour {

    public Texture2D fullCube, emptyCube;
    public Shapes shapes;
    public float scale = 0.1f;

    public Level level;
    public float fallSpeed = 1;
    public float totalSpeed = 0;

    Shape currentShape, nextShape;

    void Awake() {
        if (shapes == null) {
            shapes = FindObjectOfType<Shapes>();
        }
    }

    void Start() {
        InitLevel();
        style = new GUIStyle();
        style.fontSize = 9;
        style.normal.textColor = Color.white;
    }

    int levelRotate;
    int fallValue, sideValue;
    void Update() {

        if (currentShape != null) {

            totalSpeed += fallSpeed * Time.deltaTime;

            if (totalSpeed >= fallSpeed) {
                totalSpeed -= fallSpeed;

                if (level.Check( currentShape, 0, 1 )) {
                    currentShape.Move( 0, 1 );
                } else {
                    level.Add( currentShape );
                    UpdateShapes();
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                sideValue = -1;
            } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                sideValue = 1;
            } else {
                sideValue = 0;
            }

            if (sideValue != 0) {
                if (level.Check(currentShape, sideValue, 0)) {
                    currentShape.Move( sideValue, 0 );
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                MatrixUtils.RotateLeft( currentShape.keys );

                if (!level.Check(currentShape, 0, 0)) {
                    MatrixUtils.RotateRight( currentShape.keys );
                }
                    
            }

        } else {
            if (Input.GetKeyDown(KeyCode.Space)) {
                UpdateShapes();
            }
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            levelRotate = -1;
        } else if (Input.GetKeyDown(KeyCode.D)) {
            levelRotate = 1;
        } else {
            levelRotate = 0;
        }

        if (levelRotate != 0) {
            level.Rotate( levelRotate );
        }
    }

    void InitLevel() {
        Shape shape = shapes.RandomShape();
        shape.InitKeys(-shape.xSize / 2, -shape.ySize / 2);

        level.Add( shape );
        InitNext();
    }

    void InitNext() {
        nextShape = shapes.RandomShape();
        nextShape.InitKeys( -nextShape.xSize / 2, -Screen.height / fullCube.height );
    }

    void UpdateShapes() {
        currentShape = nextShape;
        InitNext();
    }

    void OnGUI() {
        Draw();
    }

    GUIStyle style;
    void Draw() {

        if (currentShape != null) {
            foreach(var pair in currentShape.keys) {
                if (pair.Value) {
                    Rect re = new Rect(
                               Camera.main.WorldToScreenPoint( transform.position ).x + pair.Key.X * fullCube.width * scale,
                               Camera.main.WorldToScreenPoint( transform.position ).y + pair.Key.Y * fullCube.height * scale,
                               fullCube.width * scale,
                               fullCube.height * scale );

                    GUI.DrawTexture( re, fullCube );
                }
            }
        }

        foreach (var pair in level.matrix) {

            if (pair.Value) {
                Rect re = new Rect(
                                Camera.main.WorldToScreenPoint( transform.position ).x + pair.Key.X * fullCube.width * scale,
                                Camera.main.WorldToScreenPoint( transform.position ).y + pair.Key.Y * fullCube.height * scale,
                                fullCube.width * scale,
                                fullCube.height * scale );

                GUI.DrawTexture( re, fullCube );

                re.x += 1;
                re.y += 1;
                GUI.Label( re, pair.Key.X + ":" + pair.Key.Y, style );
            }
            
            
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube( Vector3.zero, new Vector3( fullCube.width, fullCube.height, 0 ) );
    }
}
