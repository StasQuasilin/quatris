using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour {

    public Text scoresText;
    public Text levelText;
    public int scores;

	public int scoresStep = 1;

	public void AddScores(int count){
		scores += count * scoresStep;
        scoresText.text = string.Format( "{0}", scores );
    }

    public void LevelUp(int value) {
        levelText.text = string.Format( "{0}", value );
    }
}
