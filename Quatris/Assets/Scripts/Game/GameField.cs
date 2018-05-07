using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameField {

    public int wSize, hSize;
    Texture2D cubeTexture;
    float scale;
    Rect rect;

    public GameField(Texture2D cube) {
        cubeTexture = cube;
        wSize = Screen.width / cube.width;
        CalcScale();
    }

    void CalcScale() {

        scale = 1f * Screen.width / (wSize * cubeTexture.height);

        hSize = (int)(Screen.height / (cubeTexture.height * scale));

        rect = new Rect(0, 0, cubeTexture.width * scale, cubeTexture.height * scale);
    }

    public GameField(int wSize, Texture2D cube) {
        cubeTexture = cube;
        this.wSize = wSize;
        CalcScale();
        
    }

    
	public void Draw(Dictionary<Key2D, Color> matrix) {

        foreach (var pair in matrix) {
            GUI.color = pair.Value;

            rect.x = pair.Key.x * cubeTexture.width * scale;
            rect.y = pair.Key.y * cubeTexture.height * scale;

            GUI.DrawTexture(rect, cubeTexture);
        }

    }

    public int FieldCenter {
        get {
            return (int)(hSize * 0.7f);
        }
    }

    
}
