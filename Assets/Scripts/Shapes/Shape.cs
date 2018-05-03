using System;

[Serializable]
public class Shape {

    public static int index = 0;
    public int privateIndex;

    public bool show = true;
    public bool[] values;
    public int xSize, ySize;

    public Shape() {
        privateIndex = index++;
    }

    public override string ToString() {
        return "Shape " + privateIndex;
    }
}
