using System.Collections;
using UnityEngine;

public class Sounds : MonoBehaviour {

    public bool playMusic = true;
    public bool playEffects = true;

    public AudioSource mainMusic;
    public AudioSource pause, shapeRotate, lineDrop, shapeMove, levelRotate, fall;

    public void PlayMusic(bool play) {
        if (play && playMusic) {
            mainMusic.Play();
        }
    }

    public void Fall() {
        if (playEffects) {
            fall.Play();
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

    public void LineDrop(int count) {
        if (playEffects) {
            if (count == 0) {
                lineDrop.Play();
            } else {
                StartCoroutine( repeat( lineDrop, count ) );
            }
        }
    }

    IEnumerator repeat(AudioSource source, int count) {
        Debug.Log( "Play " + count + " times" );
        for (int i = 0; i < count; i++) {
            source.Play();
            yield return new WaitForSeconds( source.clip.length );
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
