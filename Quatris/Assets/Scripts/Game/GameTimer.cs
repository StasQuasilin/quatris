using System.Collections;
using UnityEngine;

[System.Serializable]
public class GameTimer {

    private static GameTimer timer;

    public static GameTimer Timer {
        get {
            if (timer == null) {
                timer = new GameTimer();
            }
            return timer;
        }
    }

    private GameTimer() { }

    public int currentLevel = 1;
    public int maxLevel = 10;
    public float speedScale = 1;

    float speed = 0.1f;
    float lastTime = 0;

    public bool isTime {
        get {
            if (Time.time > lastTime + speed) {
                lastTime = Time.time;
                return true;
            }

            return false;
        }
    }

    public bool Fasta {
        set {
            if (value) {
                speed = 0.1f;
            } else {
                speed = (1f - 1f * currentLevel / maxLevel) * speedScale;
            }
        }
    }
}
