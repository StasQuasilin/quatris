using UnityEngine;

[ExecuteInEditMode]
public class ScreenRect : MonoBehaviour {

    static Vector3 vlp;
    static Rect result;

    public static Rect ScreenR() {

        result = new Rect(
                Camera.main.ScreenToWorldPoint(Vector3.zero),
                Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelWidth, 0))
            );

        return result;
    }

    public static Bounds ScreenBound() {
        vlp = new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0f);

        return new Bounds(Camera.main.ScreenToWorldPoint(Vector3.zero), 
            Camera.main.ScreenToWorldPoint(vlp));
    }

    private void OnDrawGizmos() {

        Rect r = ScreenR();

        Gizmos.DrawWireSphere(new Vector3(r.x, r.y, 0), 0.2f);

    }

    Rect r = new Rect( 2, 2, 180, 20 );
    void OnGUI() {
        GUI.Label( r, string.Format( "Screen: {0}x{1}", Screen.width, Screen.height ) );
    }

}
