using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshCreator))]
public class MeshEditor : Editor
{
    private MeshCreator myCreator;

    private void OnSceneGUI()
    {
        if (myCreator.myPreviewGeneratedMesh && Event.current.type == EventType.Repaint)
        {
            myCreator.UpdateMesh();
        }
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUI.BeginChangeCheck();
        
        GUILayout.Label(" ");
        if (GUILayout.Button("Update 3D mesh"))
        {
            Undo.RecordObject(myCreator, "Update 3D mesh");
            myCreator.UpdateMesh();
        }
        GUILayout.Label(" ");
        GUILayout.Label("Options to manipulate the mesh on the spline.");
        GUILayout.Label(" ");
        if (GUILayout.Button("Generate 3D Mesh"))
        {
            Undo.RecordObject(myCreator, "Generate 3D Mesh");
            myCreator.CreateGameObject("SpeedBoost");
        }
    }

    private void OnEnable()
    {
        myCreator = (MeshCreator)target;
    }
}
