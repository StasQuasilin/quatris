using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButton {

    private KeyCode button;
    private float firstPause;
    private float secondPause;
    private bool guiInput;
    private int reqCount = 0;

	public GameButton(KeyCode button, float firstPause, float secondPause) {
        this.button = button;
        this.firstPause = firstPause;
        this.secondPause = secondPause;
    }

    float pause;
    float lastReq;

    public bool ButtonValue() {

        if (Input.GetKey(button)) {
            if (reqCount == 0) {
                reqCount++;
                return true;
            }else if (reqCount == 1) {
                pause = firstPause;
            } else {
                pause = secondPause;
            }

            if (Time.time > lastReq + pause) {
                lastReq = Time.time;
                reqCount++;

                return true;
            }
        } else {
            reqCount = 0;
        }

        return false;
    }

    public bool GUIInput {
        get {
            return guiInput;
        }
        set {
            guiInput = value;
        }
    }
}
