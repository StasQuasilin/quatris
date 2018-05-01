﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Level2))]
public class GameControl2 : MonoBehaviour {

    public Texture2D squar;
    public float scale = 1;

    Level2 level;
    GameUI ui;
    Shapes shapes;
    Shape next;
    GameShape current;

	Scores scores;

    public int fieldW;
    public int fieldH;
    public int fieldWight = -1;

    public int FieldCenter {
        get {
            return (int)(fieldH * 0.7f);
        }
    }

    bool isGame = true;

    GUIStyle style;

    [System.Serializable]
    public class Timer {
        public float speed = 1;
        float lastSpeed;
        private float s;

        public bool IsTime() {
            if (Time.time > lastSpeed + s) {
                lastSpeed = Time.time;
                return true;
            }
            return false;
        }

        public bool Fasta {
            set {
                if (value) {
                    s = 0.1f;
                } else {
                    s = speed;
                }
            }
        }
    }

    public Timer timer;

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
		scores = FindObjectOfType<Scores> ();
        ui = FindObjectOfType<GameUI>();

        timer = new Timer();

    }


    void Start() {
        if (fieldWight == -1) {
            fieldW = Screen.width / squar.width;

            if (fieldW % 2 == 0) {
                fieldW++;
            }
        } else {
            fieldW = fieldWight;
        }

        

        scale = 1f * Screen.width / ( fieldW * squar.height );

        fieldH = (int)(Screen.height / (squar.height * scale));

        

        style = new GUIStyle();
        style.fontSize = 9;
        style.normal.textColor = Color.gray;

        InitLevel();

    }

    void InitLevel() {

        level.Init(shapes.RandomShape());

        InitNext();
        ChangeShape();

    }

    void InitNext() {

        next = shapes.RandomShape();

    }

    void ChangeShape() {

        current = new GameShape(next, fieldW / 2, -next.ySize / 2);
        InitNext();

    }

    int sideInput;
    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            isGame = !isGame;

            ui.IsPause(!isGame);
        }

        if (isGame) {

            if (current != null) {

                if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                    sideInput = -1;
                } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                    sideInput = 1;
                } else {
                    sideInput = 0;
                }

                if (sideInput != 0 && Valid(sideInput)) {
                    if (!level.Contain(current.matrix, sideInput, 0)) {
                        
                        current.Move(sideInput, 0);
                        
                    }
                }

                if (Input.GetKeyDown(KeyCode.UpArrow)) {

                    MatrixUtils.Right(current.matrix);

                    if (level.Contain(current.matrix, 0, 0)) {
                        MatrixUtils.Left(current.matrix);
                    }
                }


                timer.Fasta = Input.GetKey(KeyCode.DownArrow);

                if (timer.IsTime()) {

                    if (!level.Contain(current.matrix, 0, 1)) {
                        current.Move(0, 1);
                    } else {
                        level.Add(current);
                        scores.AddScores(current.matrix.Count);
                        ChangeShape();
                    }
                }

                if (UnderFloor()) {
                    scores.AddScores(-current.matrix.Count * 5);
                    ChangeShape();
                }
            }

            if (Input.GetKeyDown(KeyCode.A)) {
                level.Left();
            } else if (Input.GetKeyDown(KeyCode.D)) {
                level.Right();
            }
        }
    }

    bool UnderFloor() {
        foreach (var pair in current.matrix) {
            if (pair.Key.y < fieldH) {
                return false;
            }
        }

        return true;
    }

    bool Valid(int input) {
        foreach(var pair in current.matrix) {
            if (pair.Key.x + input < 0 || pair.Key.x + input > fieldW - 1) {
                return false;
            }
        }
        return true;
    }

    Rect r;
    bool drawBlack;
    bool drawWhite;
    void OnGUI() {

        GUI.depth = 0;

        for(int i = 0; i < fieldW; i++) {
            for (int j = 0; j < fieldH; j++) {

                r = new Rect( 
                    i * squar.width * scale, 
                    j * squar.height * scale, 
                    squar.width * scale, 
                    squar.height * scale );

                if (level.Contain( i, j ) ) {
                    GUI.color = level.Color(i, j);
					GUI.DrawTexture( r, squar );

                    DrawNumbers(r, i, j);
                }

            }
        }

		GUI.color = current.shapeColor;
        foreach (var pair in current.matrix) {
            
            r = new Rect( 
                pair.Key.x * squar.width * scale, 
                pair.Key.y * squar.height * scale, 
                squar.width * scale, 
                squar.height * scale);

            GUI.DrawTexture( r, squar );

            DrawNumbers(r, pair.Key.x, pair.Key.y);

        }
    }

    void DrawNumbers(Rect r, int x, int y) {
        r.x += 2;
        r.y += 2;

        GUI.Label(r, string.Format("{0}:{1}", x, y ), style);
    }
}
