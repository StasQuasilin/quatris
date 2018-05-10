using UnityEngine;

[ExecuteInEditMode]
public class CustomLabel : MonoBehaviour {

    public Font font;
    public int fontSize = 18;
    public Color textColor = Color.white;
    public string text = "Text";
    GUIStyle style;
    public Rect rect;
    public TextAnchor anchor = TextAnchor.MiddleCenter;

    void Start() {
        style = new GUIStyle();
    }

    void OnGUI() {
        style.font = font;
        style.fontSize = fontSize;
        style.normal.textColor = textColor;
        style.alignment = anchor;

        GUI.Label( rect, text, style );
    }


}
