using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Shapes))]
public class ShapesEditor : Editor{

    GUILayoutOption[] emptyOpt = new GUILayoutOption[] { };

    Shapes shapes;
    int xSize, ySize;
    bool[] values;


    public override void OnInspectorGUI() {

        serializedObject.Update();
        shapes = target as Shapes;

        foreach (Shape shape in shapes.shapes) {
            GUILayout.BeginHorizontal( emptyOpt );

            shape.show = EditorGUILayout.ToggleLeft( string.Format( "Figure {0}, [ {1}x{2} ]", shape.privateIndex, shape.xSize, shape.ySize ), shape.show );

            if (GUILayout.Button("Delete", emptyOpt)) {
                shapes.Remove( shape );
            }

            GUILayout.EndHorizontal();

            if (shape.show) {
                GUILayout.BeginHorizontal( emptyOpt );

                xSize = shape.xSize;
                ySize = shape.ySize;
                
                xSize = EditorGUILayout.IntSlider( xSize, 1, 5, emptyOpt );
                ySize = EditorGUILayout.IntSlider( ySize, 1, 5, emptyOpt );

                shape.Check( xSize, ySize );
                GUILayout.EndHorizontal();

                Rect r = GUILayoutUtility.GetRect( 16 * xSize, 16 * ySize );
                values = shape.values;

                for (int i = 0; i < xSize; i++) {
                    for (int j = 0; j < ySize; j++) {
                        values[ i + j * ySize ] = EditorGUI.Toggle( new Rect( 20 + i * 16, r.y + j * 16, 16, 16 ), values[ i + j * ySize ] );
                    }
                }

                shape.values = values;
            }
            
        }

        shapes.Remove();

        if (GUILayout.Button( "Add Shape", emptyOpt )) {
            shapes.shapes.Add( new Shape());
        }

        if (GUILayout.Button( "Clear", emptyOpt )) {
            shapes.Clear();
        }

        serializedObject.ApplyModifiedProperties();
        if (GUI.changed) {
            EditorUtility.SetDirty( shapes );
        }
    }

}
