using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {

    GameButton LeftArrow, RightArrow, UpArrow, AButton, DButton;
    public Texture2D ButtonNormalTexture;
    public Texture2D ButtonPressedTexture;
    public Font buttonFont;
    public Color buttontTextColor = Color.white;

    public float[] delays;
    bool shapeFall;

    GUIStyle buttonStyle;
    
    void Awake() {

        LeftArrow = new GameButton  ( KeyCode   .LeftArrow,     delays );
        RightArrow = new GameButton ( KeyCode   .RightArrow,    delays );
        UpArrow = new GameButton    ( KeyCode   .UpArrow,       delays );
        AButton = new GameButton    ( KeyCode   .A,             delays );
        DButton = new GameButton    ( KeyCode   .D,             delays );

        buttonStyle = new GUIStyle();
        buttonStyle.font = buttonFont;
        buttonStyle.fontSize = 56;
        buttonStyle.alignment = TextAnchor.MiddleCenter;
        buttonStyle.normal.textColor = buttontTextColor;
        buttonStyle.active.textColor = buttontTextColor;

        buttonStyle.normal.background = ButtonNormalTexture;
        buttonStyle.active.background = ButtonPressedTexture;

        int w = (int)(1f * Screen.height / 3);
        Rect lR = new Rect( 0, Screen.height - w, w, w );
        Rect rR = new Rect( Screen.width - w, Screen.height - w, w, w );

        r5 = new Rect( lR );
        r6 = new Rect( rR );

        lR.y -= w;
        rR.y -= w;

        r3 = new Rect( lR );
        r4 = new Rect( rR );

        lR.y -= w;
        rR.y -= w;

        r1 = new Rect( lR );
        r2 = new Rect( rR );
    }

	public bool ShapeLeft() {

        return LeftArrow.ButtonValue();
    }

    public bool ShapeRight() {

        return RightArrow.ButtonValue();

    }

    public bool ShapeFall() {

        return Input.GetKey(KeyCode.DownArrow) || shapeFall;

    }

    public bool ShapeRotate() {

        return UpArrow.ButtonValue();

    }

    public bool LevelRight() {

        return DButton.ButtonValue();

    }

    public bool LevelLeft() {

        return AButton.ButtonValue();

    }

    public bool Pause() {

        return Input.GetKeyDown( KeyCode.Escape );

    }

    public bool AnyKey {
        get {
            return !Input.anyKeyDown && Input.touchCount > 0;
        }
    }

    Rect r1, r2, r3, r4, r5, r6;
    void OnGUI() {
        shapeFall =             GUI.RepeatButton    ( r1, "Down",   buttonStyle );
        UpArrow.GUIInput =      GUI.RepeatButton    ( r2, "Up",  buttonStyle );
        LeftArrow.GUIInput =    GUI.RepeatButton    ( r3, "Left A",  buttonStyle );
        RightArrow.GUIInput =   GUI.RepeatButton    ( r4, "Right B",  buttonStyle );
        AButton.GUIInput =      GUI.RepeatButton    ( r5, "Left B",  buttonStyle );
        DButton.GUIInput =      GUI.RepeatButton    ( r6, "Right B",  buttonStyle );
    }
}
