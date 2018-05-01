using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour {

    public Text scoresText;
    public int scores;

	public int scoresSize = 10;

	public void AddScores(int count){
		scores += count * scoresSize;
		scoresText.text = string.Format("{0}", scores);
	}
}
