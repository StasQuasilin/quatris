using UnityEngine;

[ExecuteInEditMode]
public class PauseMenu : ICustomLabel {

    public Font font;
    private GUIStyle style;
    Game game;

    void Awake() {
        style = new GUIStyle();
        style.font = font;
        style.fontSize = 56;
        style.normal.textColor = Color.white;
        style.active.textColor = Color.white;
        style.alignment = TextAnchor.MiddleCenter;
    }

    void Start() {
        game = Game.instance;
    }

    Rect buttonRect;
    public int buttonWidth = 100;
    public int buttoneHeight = 20;
    public int buttonSpace = 2;

	public override void Draw() {


        buttonRect = new Rect((Screen.width - buttonWidth) / 2, (Screen.height - (buttoneHeight * 2) - buttonSpace * 1) / 2, buttonWidth, buttoneHeight);

        if (GUI.Button(buttonRect, "Continue", style)) {
            game.gameState = Game.GameState.game;
        }

        buttonRect.y += buttoneHeight + buttonSpace;

        if(GUI.Button(buttonRect, "Exit", style)) {
            Application.Quit();
        }

    }

}
