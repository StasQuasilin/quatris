using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour {

    public Texture2D smallCube;
    public Font font;
    public int fontSize = 18;
    public Color textColor = Color.white;
    public int betweenLabelSpace = 22;

    GUIStyle labelStyle;

    Game game;
    GameField field;

    public ICustomLabel gameStartLabel;
    public ICustomLabel pauseLabel;
    public ICustomLabel gameOverLabel;

    void Awake() {
        game = FindObjectOfType<Game>();
        field = FindObjectOfType<GameField>();
    }

    void Start() {

        labelStyle = new GUIStyle();
        labelStyle.font = font;
        labelStyle.fontSize = fontSize;
        labelStyle.normal.textColor = textColor;
        labelStyle.alignment = TextAnchor.MiddleCenter;
    }

    Rect r;
    Rect nR;
	void OnGUI() {

        field.Draw();

        GUI.color = Color.white;

        if (game.IsGameStart) {
            gameStartLabel.Draw();
        }

        if (game.IsPause) {
            pauseLabel.Draw();
        }

        if (game.IsGameOver) {
            gameOverLabel.Draw();
        }

        /*
        r = new Rect( field.groupRect.x + field.groupRect.width + 15, 15, 200, 20 );

        /////NEXT/////
        GUI.Label(r, "Next", labelStyle);

        r.y += betweenLabelSpace *0.75f;

        if (game.next != null) {
            for (int i = 0, k = 0; i < game.next.xSize; i++) {
                for (int j = 0; j < game.next.ySize; j++, k++) {

                    nR = new Rect(
                        r.x + (r.width - smallCube.width * game.next.xSize) / 2 + i * smallCube.width,
                        r.y + j * smallCube.height,
                        smallCube.width,
                        smallCube.height);

                    if (game.next.values[k]) {
                        GUI.DrawTexture(nR, smallCube);
                    }
                }
            }
        }

        r.y += 4 * smallCube.height + betweenLabelSpace * 0.05f;
        
        /////SCORES/////
        GUI.Label( r, "Scores", labelStyle );

        r.y += betweenLabelSpace / 2;

        GUI.Label( r, game.scores.ToString(), labelStyle );

        r.y += betweenLabelSpace;
        
        /////LEVEL/////
        GUI.Label( r, "Level", labelStyle );

        r.y += betweenLabelSpace / 2;

        GUI.Label( r, game.Level.ToString(), labelStyle );

        if (!game.IsGameStart) {

        } else if (game.IsPause) {

        }
        */
    }
}
