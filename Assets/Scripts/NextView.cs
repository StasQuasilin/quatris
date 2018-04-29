using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextView : MonoBehaviour {

    GameControl2 control;
    public GameObject imagePrefab;
    public GameObject parent;
    
    void Start() {
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 4; j++) {
                GameObject point = Instantiate(imagePrefab, parent.transform);
                RectTransform r = point.GetComponent<RectTransform>();

                r.position = new Vector2(i * 10, j * 10);
            }
        }
    }


}
