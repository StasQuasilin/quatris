using System;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour {

    public GameFieldParameters parameters;

    Shapes shapeFactory;
    Level level;

    internal Shapes.ShapeValue nextShape;
    internal Shape currentShape;
    private Shape hiddenShape;


    float scale;
    Rect cubeRect;
    public Rect groupRect;
    
    public void Init() {
        Debug.Log( "Init game field" );
        shapeFactory = FindObjectOfType<Shapes>();
        level = FindObjectOfType<Level>();
        level.gameField = this;
        parameters.Init();

        scale = parameters.Scale;

        cubeRect = parameters.CubeRect;
        groupRect = parameters.GroupRect;

    }
    bool levelWasInitialized = false;

    public void InitLevel() {

        level.Init( shapeFactory.GetShape() );
        InitNext();
        InitCurrent();
        levelWasInitialized = true;
        hiddenShape = new Shape( nextShape, 0, 0 );

        Debug.Log( "Level was initialized" );
    }

    void InitNext() {
        Debug.Log( "Init next shape" );
        nextShape = shapeFactory.GetShape();
    }

    public void InitCurrent() {
        Debug.Log( "Next shape is commin" );
        currentShape = new Shape( nextShape, ( parameters.width - nextShape.xSize ) / 2, -nextShape.ySize / 2 );
        InitNext();
    }


    public void RotateShape() {

        hiddenShape.Set( currentShape.matrix );

        currentShape.CheckBounds();

        if (currentShape.minX == 0 && currentShape.Width < currentShape.Height) {
            currentShape.Move( 1, 0 );
        }

        MatrixUtil.RotateRight( currentShape );

        if (level.Contain( currentShape.matrix )) {
            currentShape.Set( hiddenShape.matrix );
        } else {
            //todo play sound
        }
        
    }

    public void MoveShape(int x, int y) {

        hiddenShape.Set( currentShape.matrix );
        currentShape.Move( x, y );

        bool add = level.Contain( currentShape.matrix ) && x == 0;

        if (!ValidSide || level.Contain( currentShape.matrix )) {
            currentShape.Set( hiddenShape.matrix );
        }

        if (add) {
            level.Add( currentShape, 0 );
            InitCurrent();
        }

    }
    
    public void Draw() {
        if (levelWasInitialized) {
            DrawBorder();
            Draw( level.levelShape.matrix, 1f );
            Draw( currentShape.matrix, 1f );
        }
    }

    public void DrawBorder() {
        GUI.DrawTexture( parameters.borderRect, parameters.border );
    }

    Color drawColor = Color.white;

	public void Draw(Dictionary<Key2D, Color> matrix, float alpha) {

        GUI.BeginGroup( groupRect );
        
        foreach (var pair in matrix) {

            if (
                pair.Key.x >= 0 && 
                pair.Key.x <= parameters.width - 1 && 
                pair.Key.y <= parameters.height) {
                
                drawColor.r = pair.Value.r;
                drawColor.g = pair.Value.g;
                drawColor.b = pair.Value.b;
                drawColor.a = alpha;

                GUI.color = drawColor;

                cubeRect.x = pair.Key.x * parameters.cubeTexture.width * scale;
                cubeRect.y = pair.Key.y * parameters.cubeTexture.height * scale;

                GUI.DrawTexture( cubeRect, parameters.cubeTexture );
            }
        }

        GUI.EndGroup();

    }

    public int FieldCenter {
        get {
            return (int)(parameters.height * 0.8f);
        }
    }

    internal void InitLevel(Dictionary<Key2D, Color> shapeData, Dictionary<Key2D, Color> levelData) {
        InitLevel();
        currentShape.Set(shapeData);
        level.levelShape.Set(levelData);
        level.Align();
    }

    bool ValidSide {
        get {
            foreach (var pair in currentShape.matrix) {
                if (pair.Key.x < 0 || pair.Key.x > parameters.width - 1) {
                    return false;
                }
            }

            return true;
        }
    }
    public bool UnderFloor {
        get {
            foreach (var pair in currentShape.matrix) {
                if (pair.Key.y < parameters.height - 1) {
                    return false;
                }
            }

            return true;
        }
    }

}

[System.Serializable]
public class GameFieldParameters {

    public Texture2D cubeTexture;
    internal Texture2D border;
    public int width;
    public int height;

    public int borderWidth = 1;
    public Color borderColor = Color.white;
    public Color backgroundColor = Color.blue;
    internal Rect borderRect;

    public void Init() {

        if (Screen.width > Screen.height) {
            height = width * Screen.width / Screen.height;
        } else {
            height = width * Screen.height / Screen.width;
        }

        InitBorder();
    }

    void InitBorder() {
        int mI = ( int ) ( 1f * width * cubeTexture.width * Scale );
        int mJ = ( int ) ( 1f * height * cubeTexture.height * Scale );

        border = new Texture2D( mI, mJ );

        for (int i = 0; i < mI; i++) {
            for (int j = 0; j < mJ; j++) {
                if (i < borderWidth || j < borderWidth || i > mI - borderWidth - 1 || j > mJ - borderWidth - 1) {
                    border.SetPixel( i, j, borderColor );
                } else {
                    border.SetPixel( i, j, backgroundColor );
                }
            }
        }

        border.Apply();
        borderRect = new Rect( GroupRect );
        borderRect.x -= borderWidth;
        borderRect.width += borderWidth;
    }

    public float Scale {
        get {
            return 1f * ( Screen.height - borderWidth * 2 ) / ( height * cubeTexture.height ); ;
        }
    }

    public Rect CubeRect {
        get {
            return new Rect( 0, 0, cubeTexture.width * Scale, cubeTexture.height * Scale );
        }
    }

    public Rect GroupRect {
        get {
            return new Rect( ( Screen.width - width * cubeTexture.width * Scale ) / 2, borderWidth, width * cubeTexture.width * Scale, Screen.height - borderWidth );
        }
    }
}
