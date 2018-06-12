using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRepeater : MonoBehaviour {

	public void Repeat(AudioSource source, int count) {
        source.Play();

    }
}
