using UnityEngine;

[ExecuteInEditMode]
public class ExecuteDrawable : MonoBehaviour {

    public CustomLabel label;

    public void OnGUI() {
        label = GetComponent<CustomLabel>();
        label.Draw();
    }
}
