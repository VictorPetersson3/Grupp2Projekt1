using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPlacer : MonoBehaviour
{
    [SerializeField]
    private Transform sphereParent = null;
    [SerializeField]
    private GameObject spherePrefab = null;

    public float spacing = 0.1f;
    public float resolution = 1;

    private List<GameObject> spheres = new List<GameObject>();
    private float sphereSize = 0.8f;

    void Start()
    {
        CreateSpheres();
    }

    public void CreateSpheres()
    {
        if (spherePrefab == null)
        {
            Debug.LogError("Spline '" + this + "' has no spherePrefab!");
            return;
        }

        if (sphereParent == null)
        {
            Debug.LogError("Spline '" + this + "' has no sphereParent!");
            return;
        }

        if (spheres.Count > 0)
        {
            foreach (GameObject sphere in spheres)
            { 
                DestroyImmediate(sphere);
            }

            spheres.Clear();
        }

        Vector2[] points = GetComponentInParent<PathCreator>().path.CalculateEvenlySpacedPoints(spacing, resolution);

        foreach (Vector2 p in points)
        {
            GameObject g = Instantiate(spherePrefab);
            g.transform.position = p;
            g.transform.localScale = Vector3.one * spacing * sphereSize;
            g.transform.SetParent(sphereParent);
            spheres.Add(g);
        }
    }
}
