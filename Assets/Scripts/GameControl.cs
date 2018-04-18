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

    Shape currentShape, moveShape, nextShape;

    void Awake() {
        if (shapes == null) {
            shapes = FindObjectOfType<Shapes>();
        }
    }

    void Start() {
        InitLevel();
        UpdateShapes();
    }

    int levelRotate;
    int fallValue, sideValue;
    void Update() {

        fallValue = 0;
        totalSpeed += fallSpeed * Time.deltaTime;

        if (totalSpeed >= 1) {
            totalSpeed--;
            fallValue = 1;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            sideValue = -1;
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            sideValue = 1;
        } else {
            sideValue = 0;
        }
        
        moveShape.Move( sideValue , fallValue );

        if (!level.Check(moveShape)) {
            currentShape.Move( sideValue, fallValue );
        } else {
            level.Add( currentShape );
            UpdateShapes();
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
        moveShape = nextShape;
        InitNext();
    }

    void OnGUI() {
        Draw();
    }
    void Draw() {
        Texture2D t;
        GUIStyle style = new GUIStyle();
        style.fontSize = 9;

        foreach (var pair in currentShape.keys) {
            if (pair.Value) {
                Rect re = new Rect(
                Camera.main.WorldToScreenPoint( transform.position ).x + pair.Key.X * fullCube.width * scale,
                Camera.main.WorldToScreenPoint( transform.position ).y + pair.Key.Y * fullCube.height * scale,
                fullCube.width * scale,
                fullCube.height * scale );

                GUI.DrawTexture( re, fullCube );
            }
        }

        foreach (var pair in level.matrix) {

            if (pair.Value) {
                t = fullCube;
                style.normal.textColor = Color.white;
            } else {
                t = emptyCube;
                style.normal.textColor = Color.black;
            }

            Rect re = new Rect(
                Camera.main.WorldToScreenPoint( transform.position ).x + pair.Key.X * t.width * scale,
                Camera.main.WorldToScreenPoint( transform.position ).y + pair.Key.Y * t.height * scale,
                t.width * scale,
                t.height * scale );

            GUI.DrawTexture(re , t );
            re.x += 1;
            re.y += 1;
            GUI.Label( re, pair.Key.X + ":" + pair.Key.Y, style );
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube( Vector3.zero, new Vector3( fullCube.width, fullCube.height, 0 ) );
    }
}
