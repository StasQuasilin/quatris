using UnityEngine;

public class Sounds : MonoBehaviour {

    public bool playMusic = true;
    public bool playEffects = true;

    public AudioSource mainMusic;
    public AudioSource pause, shapeRotate, lineDrop, shapeMove, levelRotate;

    public void PlayMusic(bool play) {
        if (play && playMusic) {
            mainMusic.Play();
        }
    }

    public void Pause() {
        if (playEffects) {
            pause.Play();
        }
    }

    public void ShapeRotate() {
        if (playEffects) {
            shapeRotate.Play();
        }
    }

    public void LineDrop() {
        if (playEffects) {
            lineDrop.Play();
        }
    }

    public void ShapeMove() {
        if (playEffects) {
            shapeMove.Play();
        }
    }

    public void LevelRotate() {
        if (playEffects) {
            levelRotate.Play();
        }
    }



    
}
