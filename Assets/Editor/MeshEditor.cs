using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshCreator))]
public class MeshEditor : Editor
{
    private MeshCreator creator;


    private void OnSceneGUI()
    {
        if (creator.previewGeneratedMesh && Event.current.type == EventType.Repaint)
        {
            creator.UpdateMesh();
        }
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUI.BeginChangeCheck();
        
        GUILayout.Label(" ");
        //updateMesh = GUILayout.Toggle(updateMesh, "Update mesh while editing.");
        //{
        //    creator.previewGeneratedMesh = true;
        //}
        if (GUILayout.Button("Update 3D mesh"))
        {
            Undo.RecordObject(creator, "Update 3D mesh");
            creator.UpdateMesh();
        }
        GUILayout.Label(" ");
        GUILayout.Label("Options to manipulate the mesh on the spline.");
        GUILayout.Label(" ");
        if (GUILayout.Button("Generate 3D Mesh"))
        {
            Undo.RecordObject(creator, "Generate 3D Mesh");
            creator.CreateMesh();
        }
    }

    private void OnEnable()
    {
        creator = (MeshCreator)target;
    }
}
