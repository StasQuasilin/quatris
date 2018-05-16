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
    public Rect groupRect;
    Rect borderRect;

    Texture2D border;

    void Awake() {

        CalcScale();
        CreateBorder();

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

        borderRect = new Rect( groupRect );
        borderRect.width += borderSize;
    }

    void CalcScale() {

        scale = 1f * (Screen.height - borderSize * 2)  / (hSize * cubeTexture.height);

        rect = new Rect(0, 0, cubeTexture.width * scale, cubeTexture.height * scale);
        groupRect = new Rect( ( Screen.width - wSize * cubeTexture.width * scale ) / 2, borderSize, wSize * cubeTexture.width * scale, Screen.height - borderSize );

    }

    public void DrawBorder() {
        GUI.DrawTexture( borderRect, border );
    }

    Color drawColor = Color.white;

	public void Draw(Dictionary<Key2D, Color> matrix, float alpha) {

        GUI.BeginGroup( groupRect );
        
        foreach (var pair in matrix) {

            if (pair.Key.x >= 0 && pair.Key.x <= wSize - 1) {
                
                drawColor.r = pair.Value.r;
                drawColor.g = pair.Value.g;
                drawColor.b = pair.Value.b;
                drawColor.a = alpha;

                GUI.color = drawColor;

                rect.x = pair.Key.x * cubeTexture.width * scale;
                rect.y = pair.Key.y * cubeTexture.height * scale;

                GUI.DrawTexture( rect, cubeTexture );
            }
        }

        GUI.EndGroup();

    }

    public int FieldCenter {
        get {
            return (int)(hSize * 0.8f);
        }
    }

    
}
