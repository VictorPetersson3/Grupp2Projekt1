using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathCreator))]
public class PathEditor : Editor
{

    private PathCreator myCreator;
    private Paths myPath
    {
        get
        {
            return myCreator.path;
        }
    }

    private const float mySegmentSelectDistanceThreshold = .1f;
    private int mySelectedSegmentIndex = -1;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUI.BeginChangeCheck();
        if (GUILayout.Button("Add Boost"))
        {
            Undo.RecordObject(myCreator, "Add Boost");
            myCreator.AddBoost();
        }

        if (GUILayout.Button("Delete Boost"))
        {
            Undo.RecordObject(myCreator, "Delete Boost");
            myCreator.DeleteBoost();
        }

        if (GUILayout.Button("DELETE and start over"))
        {
            Undo.RecordObject(myCreator, "DELETE and start over");
            myCreator.StartOver();
        }

        bool isClosed = GUILayout.Toggle(myPath.IsClosed, "Closed");
        if (isClosed != myPath.IsClosed)
        {
            Undo.RecordObject(myCreator, "Toggle closed");
            myPath.IsClosed = isClosed;
        }

        bool autoSetControlPoints = GUILayout.Toggle(myPath.AutoSetControlPoints, "Auto Set Control Points");
        if (autoSetControlPoints != myPath.AutoSetControlPoints)
        {
            Undo.RecordObject(myCreator, "Toggle auto set controls");
            myPath.AutoSetControlPoints = autoSetControlPoints;
        }

        if (EditorGUI.EndChangeCheck())
        {
            SceneView.RepaintAll();
        }
    }

    private void OnSceneGUI()
    {
        Input();
        Draw();
        myCreator.UpdateBoost();
    }

    private void Input()
    {
        Event guiEvent = Event.current;
        Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;

        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift)
        {
            if (mySelectedSegmentIndex != -1)
            {
                Undo.RecordObject(myCreator, "Split segment");
                myPath.SplitSegment(mousePos, mySelectedSegmentIndex);
            }
            else if (!myPath.IsClosed)
            {
                Undo.RecordObject(myCreator, "Add segment");
                myPath.AddSegment(mousePos);
            }
        }

        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 1)
        {
            float minDstToAnchor = myCreator.anchorDiameter * .5f;
            int closestAnchorIndex = -1;

            for (int i = 0; i < myPath.NumPoints; i += 3)
            {
                float dst = Vector2.Distance(mousePos, myPath[i]);
                if (dst < minDstToAnchor)
                {
                    minDstToAnchor = dst;
                    closestAnchorIndex = i;
                }
            }

            if (closestAnchorIndex != -1)
            {
                Undo.RecordObject(myCreator, "Delete segment");
                myPath.DeleteSegment(closestAnchorIndex);
            }
        }

        if (guiEvent.type == EventType.MouseMove)
        {
            float minDstToSegment = mySegmentSelectDistanceThreshold;
            int newSelectedSegmentIndex = -1;

            for (int i = 0; i < myPath.NumSegments; i++)
            {
                Vector2[] points = myPath.GetPointsInSegment(i);
                float dst = HandleUtility.DistancePointBezier(mousePos, points[0], points[3], points[1], points[2]);
                if (dst < minDstToSegment)
                {
                    minDstToSegment = dst;
                    newSelectedSegmentIndex = i;
                }
            }

            if (newSelectedSegmentIndex != mySelectedSegmentIndex)
            {
                mySelectedSegmentIndex = newSelectedSegmentIndex;
                HandleUtility.Repaint();
            }
        }
    }

    private void Draw()
    {
        for (int i = 0; i < myPath.NumSegments; i++)
        {
            Vector2[] points = myPath.GetPointsInSegment(i);
            if (myCreator.displayControlPoints)
            {
                Handles.color = Color.black;
                Handles.DrawLine(points[1], points[0]);
                Handles.DrawLine(points[2], points[3]);
            }
            Color segmentCol = (i == mySelectedSegmentIndex && Event.current.shift) ? myCreator.selectedSegmentCol : myCreator.segmentCol;
            Handles.DrawBezier(points[0], points[3], points[1], points[2], segmentCol, null, 2);
        }


        for (int i = 0; i < myPath.NumPoints; i++)
        {
            if (i % 3 == 0 || myCreator.displayControlPoints)
            {
                Handles.color = (i % 3 == 0) ? myCreator.anchorCol : myCreator.controlCol;
                float handleSize = (i % 3 == 0) ? myCreator.anchorDiameter : myCreator.controlDiameter;
                Vector2 newPos = Handles.FreeMoveHandle(myPath[i], Quaternion.identity, handleSize, Vector2.zero, Handles.CylinderHandleCap);
                if (myPath[i] != newPos)
                {
                    Undo.RecordObject(myCreator, "Move point");
                    myPath.MovePoint(i, newPos);
                }
            }
        }
    }

    private void OnEnable()
    {
        myCreator = (PathCreator)target;
        if (myCreator.path == null)
        {
            myCreator.CreatePath();
        }
    }
}