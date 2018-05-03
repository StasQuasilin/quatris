using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IInput {
    public override bool LevelLeft() {
        return Input.GetKeyDown( KeyCode.A );
    }

    public override bool LevelRight() {
        return Input.GetKeyDown( KeyCode.D );
    }

    public override bool ShapeFall() {
        return Input.GetKey( KeyCode.DownArrow );
    }

    public override bool ShapeLeft() {
        return Input.GetKeyDown( KeyCode.LeftArrow );
    }

    public override bool ShapeRight() {
        return Input.GetKeyDown( KeyCode.RightArrow );
    }

    public override bool ShapeRotate() {
        return Input.GetKeyDown( KeyCode.UpArrow );
    }
}
