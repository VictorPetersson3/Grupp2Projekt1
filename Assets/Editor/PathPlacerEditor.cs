using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathPlacer))]
public class PathPlacerEditor : Editor
{
    PathPlacer pathPlacer;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUI.BeginChangeCheck();

        if (GUILayout.Button("Create spheres"))
        {
            Undo.RecordObject(pathPlacer, "Create spheres");
            pathPlacer.CreateSpheres();
        }
    }

    void OnEnable()
    {
        pathPlacer = (PathPlacer)target;
    }
}
