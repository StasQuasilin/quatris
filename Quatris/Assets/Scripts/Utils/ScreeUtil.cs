using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreeUtil : MonoBehaviour {

	public bool is_Landscape;

	void Update() {
		is_Landscape = IsLandscape;
	}

	public static bool isLandscape() {
		#if UNITY_EDITOR
		return Screen.width > Screen.height;
		#else
		return Screen.orientation == ScreenOrientation.Landscape;
		#endif

	}
	public bool IsLandscape {
		get {
			return isLandscape ();
		}
	}
}
