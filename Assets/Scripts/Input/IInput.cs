using UnityEngine;

public abstract class IInput : MonoBehaviour{

    public abstract bool LevelLeft();
    public abstract bool LevelRight();
    public abstract bool ShapeLeft();
    public abstract bool ShapeRight();
    public abstract bool ShapeFall();
    public abstract bool ShapeRotate();

}
