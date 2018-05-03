using System.Collections.Generic;
using UnityEngine;


public class Level2 : MonoBehaviour {


	Scores scores;
    GameControl2 game;
    DataIO io;

	void Awake() {
		scores = FindObjectOfType<Scores> ();
        game = FindObjectOfType<GameControl2>();
        io = new DataIO( Application.dataPath + "\\data.t" );
        

	}

    LevelShape levelShape;

    public void Init(Shape shape) {
        levelShape = new LevelShape(
            shape, (game.fieldW - shape.xSize) / 2, game.FieldCenter);
        levelShape.Check();
        Allign();
    }

    public void LoadData() {
        levelShape = new LevelShape(io.Load());
    }

    public void SaveData() {
        io.Save( levelShape.matrix );
    }

	public void Add(GameShape shape) {
            
        levelShape.Add(shape);

        levelShape.Check();
        Allign();
        CalcScores();
        CalcScores();
    }

    void CalcScores() {

        for (int i = 0; i < 4; i++) {
            CheckLines();
            Right();
            levelShape.Check();
            Allign();
        }
    }

    void CheckLines() {

        Debug.Log("### CHECK LINES ###");

        bool dropW;
        //Check Lines
        for (int i = levelShape.minY; i <= levelShape.maxY; ) {
            Debug.Log("\tCheck line " + i);
            dropW = true;
            int j = 0 ;

            for (; j < game.fieldW; j++) {
                if (!levelShape.Contain(j, i)) {
                    Debug.Log("######## No line " + i);
                    dropW = false;
                    break;
                }
            }

            if (dropW) {
                Debug.Log("######## Drop line " + i);
                scores.AddScores( levelShape.DropLine(i) * 7);
            } else {
                i++;
            }
        }
    }

    public Color Color(int x, int y) {
        return levelShape.matrix[new Vector2Int(x, y)];
    }

	Vector2Int tempKey;
    public bool Contain(int x, int y) {

		tempKey = new Vector2Int( x, y );

        return levelShape.matrix.ContainsKey( tempKey );

    }

	public bool Contain(Dictionary<Vector2Int, Color> keys, int x, int y) {

        bool result = false;

        foreach (var pair in keys) {

			tempKey = new Vector2Int( pair.Key.x + x, pair.Key.y + y );

            if (levelShape.matrix.ContainsKey(tempKey)) {
                result = true;
            }
        }

        return result;
    }

    public void Right() {
        MatrixUtils.Right(levelShape.matrix);
        levelShape.Check();
        Allign();
    }

    public void Left() {
        MatrixUtils.Left(levelShape.matrix);
        levelShape.Check();
        Allign();
    }

    void Allign() {

        int vA = (game.fieldW - (levelShape.minX + levelShape.maxX)) / 2;
        int hA =  game.FieldCenter - (levelShape.maxY - (levelShape.maxY - levelShape.minY) / 2);

        levelShape.TotalMove(vA, hA);

    }

    public bool Empty {
        get {
            return levelShape == null || levelShape.matrix.Count == 0;
        }
    }
}
