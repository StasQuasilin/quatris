using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButton {

    private KeyCode button;
    private bool guiInput;
    private int reqCount = 0;
    float[] delays;

	public GameButton(KeyCode button, float [] delays) {

        this.button = button;
        this.delays = delays;
    }

    float lastReq;

    public bool ButtonValue() {

        if (Input.GetKey(button) || guiInput) {
            reqCount = Mathf.Clamp( reqCount, 0, delays.Length - 1 );

            if (Time.time > lastReq + delays[reqCount]) {
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
