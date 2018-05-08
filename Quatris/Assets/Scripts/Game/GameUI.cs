using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour {

    public Font font;
    public int fontSize = 18;
    public Color textColor = Color.white;
    public int betweenLabelSpace = 22;

    GUIStyle labelStyle;

    Game game;

    void Awake() {
        game = FindObjectOfType<Game>();
    }

    void Start() {
        labelStyle = new GUIStyle();
        labelStyle.font = font;
        labelStyle.fontSize = fontSize;
        labelStyle.normal.textColor = textColor;
        labelStyle.alignment = TextAnchor.MiddleCenter;
    }

    Rect r;
	void OnGUI() {

        r = new Rect( Screen.width - 100, 2, 100, 20 );

        GUI.Label( r, "Scores", labelStyle );

        r.y += betweenLabelSpace / 2;

        GUI.Label( r, game.scores.ToString(), labelStyle );

        r.y += betweenLabelSpace;

        GUI.Label( r, "Level", labelStyle );

        r.y += betweenLabelSpace / 2;

        GUI.Label( r, game.Level.ToString(), labelStyle );
    }
}
