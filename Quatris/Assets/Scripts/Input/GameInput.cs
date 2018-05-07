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
}
