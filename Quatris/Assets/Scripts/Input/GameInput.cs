using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {

    GameButton LeftArrow, RightArrow;

    void Awake() {
        LeftArrow = new GameButton(KeyCode.LeftArrow, 3, 0.1f);
        RightArrow = new GameButton(KeyCode.RightArrow, 3, 0.1f);
    }

	public bool ShapeLeft() {

        return LeftArrow.ButtonValue();
    }

    public bool ShapeRight() {

        return RightArrow.ButtonValue();

    }

    public bool ShapeFall() {
        return Input.GetKey(KeyCode.DownArrow);
    }

    public bool ShapeRotate() {
        return Input.GetKeyDown( KeyCode.UpArrow );
    }

    public bool LevelRight() {
        return Input.GetKeyDown( KeyCode.D );
    }

    public bool LevelLeft() {
        return Input.GetKeyDown( KeyCode.A );
    }
}
