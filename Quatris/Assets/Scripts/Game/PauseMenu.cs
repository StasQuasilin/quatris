using UnityEngine;

[ExecuteInEditMode]
public class PauseMenu : ICustomLabel {

    public Font font;
    private GUIStyle style;
    Game game;
	LocalizationManager local;
    Sounds sounds;
	public Color menuColor = Color.white;

    void Awake() {
        sounds = FindObjectOfType<Sounds>();
        style = new GUIStyle();
        style.font = font;
        style.fontSize = 56;
		style.normal.textColor = menuColor;
		style.active.textColor = menuColor;
        style.alignment = TextAnchor.MiddleCenter;
    }

    void Start() {
        game = Game.instance;
		local = LocalizationManager.instance;
    }

    Rect buttonRect;
    public int buttonWidth = 100;
    public int buttoneHeight = 20;
    public int buttonSpace = 2;

	public override void Draw() {


        buttonRect = new Rect((Screen.width - buttonWidth) / 2, (Screen.height - (buttoneHeight * 4) - buttonSpace * 2) / 2, buttonWidth, buttoneHeight);

		if (GUI.Button(buttonRect, local.GetLocalizedValue("help"), style)) {
            game.gameState = Game.GameState.help;
        }

        buttonRect.y += buttoneHeight + buttonSpace;

		if (GUI.Button(buttonRect, local.GetLocalizedValue("continue"), style)) {
            game.gameState = Game.GameState.game;
            sounds.Pause();
        }

        buttonRect.y += buttoneHeight + buttonSpace;

		if (GUI.Button(buttonRect, local.GetLocalizedValue("leaders"), style)) {
            PlayGame.ShowLeaderboardUI();
        }

        buttonRect.y += buttoneHeight + buttonSpace;

		if (GUI.Button(buttonRect, local.GetLocalizedValue("exit"), style)) {
            Application.Quit();
        }

    }

}
