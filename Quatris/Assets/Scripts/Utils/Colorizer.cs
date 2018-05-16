using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorizer : MonoBehaviour {

    public Color[] colors;

    public static Colorizer Instance;

    void Start() {

        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this);
        }

        if (colors.Length == 0) {
            colors = new Color[12];
            for (int i = 0; i < 12; i++) {
                colors[i] = RandomColor();
            }
        }
    }

	public Color GetColor() {
        return colors[(int)(Mathf.Floor(colors.Length * Random.value))];
    }

    static float r, g, b;
    public static Color RandomColor() {
        r = Random.value;
        g = Random.value;
        b = Random.value;

        return new Color(r, g, b);
    }
}
