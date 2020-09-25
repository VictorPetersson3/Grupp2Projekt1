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
    public float resolution = 10;

    private List<GameObject> spheres = new List<GameObject>();
    private float sphereSize = 0.8f;

    void Start()
    {
        CreateSpheres();
    }

    private void DestroySpheres()
    {
        int childCount = sphereParent.childCount;

        if (childCount > 0 && childCount == spheres.Count)
        {
            for (int i = childCount - 1; i >= 0; i--)
            {
                if (sphereParent.GetChild(i) == spheres[i])
                {
                    DestroyImmediate(sphereParent.GetChild(i).gameObject);
                }
            }
        }

        if (spheres.Count > 0)
        {
            foreach (GameObject sphere in spheres)
            {
                if (sphere != null)
                {
                    DestroyImmediate(sphere);
                }
            }

            spheres.Clear();
        }

        if (childCount > 0)
        {
            //foreach (Transform child in sphereParent.GetComponentsInChildren<Transform>())
            //{
            //    DestroyImmediate(child.gameObject);
            //}

            for (int i = childCount - 1; i <= 0; i--)
            {
                if (sphereParent.GetChild(i) != null)
                {
                    DestroyImmediate(sphereParent.GetChild(i).gameObject);
                }
            }
        }
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

        DestroySpheres();

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
