using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    public Image prefab;
    public Transform parent;
    public Text pauseText;

    void Start() {
        //Image i = Instantiate(prefab, parent);
        
    }

    public void IsPause(bool pause) {
        pauseText.gameObject.SetActive(pause);
    }

    public void Draw(GameShape shape) {
        
        
    }
}
