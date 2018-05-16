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

    public CustomLabel gameStartLabel;
    public CustomLabel pauseLabel;
    public CustomLabel gameOverLabel;

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

    void Update() {

        if (!game.IsGameOver) {

            if (gameStartLabel.draw != game.IsGameStart) {
                gameStartLabel.draw = ( game.IsGameStart );
            }

            if (pauseLabel.draw != game.IsPause) {
                pauseLabel.draw = ( game.IsPause );
            }

            if (gameOverLabel.draw) {
                gameOverLabel.draw = ( false );
            }
        } else {
            if (gameStartLabel.draw) {
                gameStartLabel.draw = ( false );
            }

            if (pauseLabel.draw) {
                pauseLabel.draw = ( false );
            }

            if (gameOverLabel.draw != game.IsGameOver) {
                gameOverLabel.draw = ( game.IsGameOver );
            }
        }

        
    }

    Rect r;
    Rect nR;
	void OnGUI() {
        
        r = new Rect( field.groupRect.x + field.groupRect.width + 15, 15, 100, 20 );

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
    }
}
