using System;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour {

    public Sounds sounds;
    public GameFieldParameters parameters;
    Game game;
    ScoresContainer scores;

    Shapes shapeFactory;
    Level level;

    internal Shapes.ShapeValue nextShape;
    internal Shape currentShape;
    private Shape hiddenShape;


    float scale;
    Rect cubeRect;
    internal Rect groupRect;

    bool reloadField;
    bool levelWasInitialized = false;
    GameOverReload reload;

    public void InitGameField() {

        Debug.Log( "Init game field" );

        game = FindObjectOfType<Game>();
        scores = ScoresContainer.Instance;

        sounds = FindObjectOfType<Sounds>();
        shapeFactory = FindObjectOfType<Shapes>();
        level = FindObjectOfType<Level>();
        level.gameField = this;
        parameters.Init();

        scale = parameters.Scale;

        cubeRect = parameters.CubeRect;
        groupRect = parameters.GroupRect;

        reload = new GameOverReload( parameters.height );
    }

    public void InitLevel() {

        if (!reloadField) {

            level.Init( shapeFactory.GetShape() );
            InitNext();
            InitCurrent();
            levelWasInitialized = true;
            hiddenShape = new Shape( nextShape, 0, 0 );
        }

        Debug.Log( "Level was initialized" );
    }

    public void ReloadField() {
        reloadField = true;
        reload.init();
    }

    void Update() {
        if (reloadField) {
            if (!reload.Ready) {

                if (reload.Time) {

                    int r = reload.Row;
                    
                    for (int i = 0; i < parameters.width; i++) {
                        if (reload.Dir == -1) {
                            level.Add( new Key2D( i, r ) );
                        } else {
                            level.Remove( new Key2D( i, r ) );
                        }
                    }
                }

            } else {
                reloadField = false;
                Game.instance.gameState = Game.GameState.game;
                Game.instance.initNewGame = false;
                InitLevel();
            }
        }
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

        MatrixUtil.RotateRight( currentShape );

        currentShape.CheckBounds();

        if (currentShape.minX < 0) {
            currentShape.Move( -currentShape.minX, 0 );
        }

        if (level.Contain( currentShape.matrix )) {
            currentShape.Set( hiddenShape.matrix );
        } else {
            sounds.ShapeRotate();
        }
        
    }

    public void MoveShape(int x, int y) {

        hiddenShape.Set( currentShape.matrix );
        currentShape.Move( x, y );

        bool add = level.Contain( currentShape.matrix ) && x == 0;

        if (!ValidSide || level.Contain( currentShape.matrix )) {
            currentShape.Set( hiddenShape.matrix );
        } else if (y == 0) {
            sounds.ShapeMove();
        }

        if (add) {

            scores.Add( currentShape.matrix.Count );

            level.Add( currentShape, 0 );

            int drop = level.CheckDrops();

            if (drop != 0) {
                Debug.Log( "Drop " + drop + " lines" );
                scores.Add( drop * 3 );
                sounds.LineDrop(drop / parameters.width);
            } else {
                sounds.Fall();
            }

            InitCurrent();



        } else if (UnderFloor) {

            Debug.Log( "Lose shape" );
            scores.Add( -currentShape.matrix.Count * 5 );
            InitCurrent();
        }

    }
    
    public void Draw() {
        if (levelWasInitialized) {
			if (ScreeUtil.isLandscape()) {
				DrawBorder ();
			}

            float a = ( game.isHelp || game.IsPause ? 0.2f : 1 );

            Draw( level.levelShape.matrix, a );
            if (!reloadField || reload.Dir == -1) {
                Draw( currentShape.matrix, a );
            }
        }
    }

    public void DrawBorder() {
        //GUI.DrawTexture( parameters.borderRect, parameters.border );
    }

    Color drawColor = Color.white;

	public void Draw(Dictionary<Key2D, Color> matrix, float alpha) {

		GUI.BeginGroup( parameters.GroupRect );

		for (int i = 0; i < parameters.width; i++) {
			for (int j = 0; j < parameters.height; j++) {
				if (matrix.ContainsKey(new Key2D(i, j))) {
					drawColor.a = 1f;
				} else {
					drawColor.a = 0.01f;
				}

				GUI.color = drawColor;

				cubeRect.x = i * parameters.cubeTexture.width * scale;
				cubeRect.y = j * parameters.cubeTexture.height * scale;

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
		//InitBorder ();
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
        borderRect.y -= borderWidth;
        borderRect.width += borderWidth;
        borderRect.height += borderWidth;
    }

    public float Scale {
        get {
			if (ScreeUtil.isLandscape()) {
				return 1f * (Screen.height - borderWidth * 2) / (height * cubeTexture.height);
			} else {
				return 1f * Screen.width / width * cubeTexture.width;
			}
        }
    }

    public Rect CubeRect {
        get {
            return new Rect( 0, 0, cubeTexture.width * Scale, cubeTexture.height * Scale );
        }
    }

    public Rect GroupRect {
        get {
			if (ScreeUtil.isLandscape()) {
				return new Rect ((Screen.width - width * cubeTexture.width * Scale) / 2, borderWidth, width * cubeTexture.width * Scale, Screen.height - borderWidth);
			} else {
				return new Rect (0, 0, width * cubeTexture.width * Scale, height * cubeTexture.height * Scale);
			}
        }
    }
}

class GameOverReload {
    int height;
    public float reloadSpeed;
    float lastTime;
    int dir = -1;
    int r;
    int linesUpdate;

    public GameOverReload(int height, float reloadSpeed = 0.05f) {
        this.height = height;
        this.reloadSpeed = reloadSpeed;
    }

    public void init() {
        r = height;
        linesUpdate = 0;
    }

    public bool Ready {
        get {
            return linesUpdate == height * 2 + 2;
        }
    } 

    public bool Time {
        get {
            if (UnityEngine.Time.time > lastTime + reloadSpeed) {

                lastTime = UnityEngine.Time.time;
                return true;
            }

            return false;
        }
    }

    public int Row {
        get {
            r += dir;

            if (r == -1) {
                dir = 1;
            }

            if (r == height +1 ) {
                dir = -1;
            }

            linesUpdate++;

            return r;
        }
    }

    public int Dir {
        get {
            return dir;
        }
    }
}
