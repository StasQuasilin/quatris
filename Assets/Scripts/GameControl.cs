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

    void Awake() {
        if (shapes == null) {
            shapes = FindObjectOfType<Shapes>();
        }
    }

    void Start() {
        InitLevel();
    }
    int levelRotate;
    void Update() {
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
        shape.InitKeys(shape.xSize, shape.ySize);

        level.Add( shape );
    }

    void OnGUI() {
        Draw();
    }
    void Draw() {
        foreach(var pair in level.matrix) {
            Texture2D t;
            if (pair.Value) {
                t = fullCube;
            } else {
                t = emptyCube;
            }

            GUI.DrawTexture( new Rect( 
                pair.Key.X * t.width * scale, 
                pair.Key.Y * t.height * scale, 
                t.width * scale, 
                t.height * scale ), t );
        }
    }
}
