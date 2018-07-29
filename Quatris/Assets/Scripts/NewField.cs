using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewField : MonoBehaviour {

	public Texture2D cubeTexture;
	public int fieldWidth, fieldHeight;
	public Image image;

	void Start () {
		for (int i = 0; i < fieldWidth; i++) {
			for (int j = 0; j < fieldHeight; j++) {
				Image img = Instantiate (image, gameObject.transform) as Image;
				img.rectTransform.position = new Vector3 (i * 32, j * 32);
			}
		}
	}
}
