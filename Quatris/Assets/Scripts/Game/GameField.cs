using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour {

    public Texture2D cubeTexture;
    public int wSize, hSize;
    public int borderSize = 1;

    public Color borderColor = Color.white;
    public Color backgroundColor = Color.blue;
    
    float scale;
    Rect rect;
    bool drawBorder;

    Texture2D border;

    void Awake() {
        drawBorder = Screen.width >= Screen.height;

        CalcScale();
        
        if (drawBorder) {
            borderRect = new Rect( 0, 0, wSize * cubeTexture.width * scale, hSize * cubeTexture.height * scale );
            CreateBorder();
        }
    }

    public void CreateBorder() {
        

        int mI = (int)(1f * wSize * cubeTexture.width * scale);
        int mJ = (int)(1f * hSize * cubeTexture.height * scale);

        border = new Texture2D( mI, mJ );

        for (int i = 0; i < mI; i++) {
            for (int j = 0; j < mJ; j++) {
                if (i < borderSize || j < borderSize || i > mI - borderSize - 1 || j > mJ - borderSize - 1) {
                    border.SetPixel( i, j, borderColor );
                } else {
                    border.SetPixel( i, j, backgroundColor );
                }
            }
        }

        border.Apply();
    }

    void CalcScale() {

        scale = 1f * Screen.width / (wSize * cubeTexture.height);

        hSize = (int)(Screen.height / (cubeTexture.height * scale));

        rect = new Rect(0, 0, cubeTexture.width * scale, cubeTexture.height * scale);
    }

    Rect borderRect;
    public void DrawBorder() {
        if (drawBorder) {
            GUI.DrawTexture( new Rect( 0, 0, cubeTexture.width * wSize, cubeTexture.height * hSize ), border );
        }
    }
    
	public void Draw(Dictionary<Key2D, Color> matrix) {

        foreach (var pair in matrix) {

            if (pair.Key.x >= 0 && pair.Key.x <= wSize - 1) {

                GUI.color = pair.Value;

                rect.x = pair.Key.x * cubeTexture.width * scale;
                rect.y = pair.Key.y * cubeTexture.height * scale;

                GUI.DrawTexture( rect, cubeTexture );
            }
        }

    }

    public int FieldCenter {
        get {
            return (int)(hSize * 0.7f);
        }
    }

    
}
