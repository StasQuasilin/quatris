using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shapes : MonoBehaviour {
    
    [System.Serializable]
	public class ShapeValue {
        static int totalNumber = 0;
        private int privateNumber;

        public ShapeValue() {
            privateNumber = totalNumber++;
            ChangeSize(3, 3);
        }

        public int xSize = 0, ySize = 0;
        public bool[] values;

        public void ChangeSize(int x, int y) {
            if (xSize != x || ySize != y) {
                xSize = x;
                ySize = y;

                values = new bool[xSize * ySize];
            }
        }

        public override string ToString() {
            return "Shape " + privateNumber;
        }
    }

    public List<ShapeValue> values;
    List<ShapeValue> lastValues = new List<ShapeValue>();

    ShapeValue resultShape;
	public ShapeValue GetShape() {

        while (lastValues.Contains(resultShape = RandomShape()));

        lastValues.Add(resultShape);

        while (lastValues.Count > values.Count * 0.5) {
            lastValues.RemoveAt(0);
        }

        Debug.Log("Generate " + resultShape);
        return resultShape;
    }

    ShapeValue RandomShape() {
        return values[(int)(Mathf.Floor(values.Count * Random.value))];
    }
}
