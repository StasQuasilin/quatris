using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {

	public bool ShapeLeft() {
        return Input.GetKeyDown(KeyCode.LeftArrow);
    }

    public bool ShapeRight() {
        return Input.GetKeyDown(KeyCode.RightArrow);
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
