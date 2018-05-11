using UnityEngine;

[ExecuteInEditMode]
public class CustomLabel : MonoBehaviour {

    public bool draw;
    public Content[] content;

    void OnGUI() {

        if (draw) {
            if (content != null) {
                foreach (Content c in content) {
                    c.Draw();
                }
            }
        }
    }

    [System.Serializable]
    public class Content {
        public string text = "Text";
        public Font font;
        public int fontSize = 18;
        public Color textColor = Color.white;
        public TextAnchor aligment = TextAnchor.UpperLeft;
        public Vector2 offset;

        GUIStyle style;
        Rect r;

        public Content() {
            style = new GUIStyle();
            r = new Rect( 0, 0, Screen.width, Screen.height );
        }

        void Update() {

            if (style.font != font) {
                style.font = font;
            }

            if (style.fontSize != fontSize) {
                style.fontSize = fontSize;
            }

            if (style.normal.textColor != textColor) {
                style.normal.textColor = textColor;
            }

            if (style.alignment != aligment) {
                style.alignment = aligment;
            }

            if (r.x != offset.x || r.y != offset.y) {
                r = new Rect( offset.x, offset.y, Screen.width, Screen.height );
            }
        }

        public void Draw() {
            Update();
            GUI.Label( r, text, style );
        }
    }
}


