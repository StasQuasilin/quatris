using UnityEngine;

public class Colorizer : MonoBehaviour {

    public static Colorizer Instance;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this);
        }
    }

    public Color[] colors;
    float r, g, b;
    public Color RandomColor() {

        if (colors.Length == 0) {
            return Random();
        } else {
            return colors[(int)Mathf.Floor(UnityEngine.Random.value * colors.Length)];
        }
    }

    public Color Random() {
        r = UnityEngine.Random.value;
        g = UnityEngine.Random.value;
        b = UnityEngine.Random.value;

        return new Color(r, g, b);
    }
}
