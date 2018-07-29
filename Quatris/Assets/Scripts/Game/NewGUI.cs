using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGUI : MonoBehaviour {

	public string text = "touch it";
	public GUIStyle s;
	Game game;
	LocalizationManager localizationManager;

	void Start() {
		game = Game.instance;
		localizationManager = LocalizationManager.instance;
	}


	void Update() {
		switch (game.gameState) {
		case Game.GameState.start:
			text = localizationManager.GetLocalizedValue ("touch.it");
			break;
		case Game.GameState.gameOver:
			text = localizationManager.GetLocalizedValue ("game.over");
			break;
		default:
			text = "";
			break;
		}
	}

	void OnGUI() {
		Rect r = new Rect (0, 0, Screen.width, Screen.height);
		GUI.Label (r, text, s);
	}
}
