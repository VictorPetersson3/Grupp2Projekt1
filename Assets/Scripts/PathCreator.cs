using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [HideInInspector]
    public Path path;
    [SerializeField]
    private int myBoostStart = 0;
    [SerializeField]
    private int myBoostEnd = 0;

    public Color anchorCol = Color.red;
    public Color controlCol = Color.white;
    public Color segmentCol = Color.green;
    public Color selectedSegmentCol = Color.yellow;
    public float anchorDiameter = .1f;
    public float controlDiameter = .075f;
    public bool displayControlPoints = true;
    private GameObject[] myBoostSpheres;

    private void OnValidate()
    {
        UpdateBoost();
    }

    public void UpdateBoost()
    {
        path.UpdateBoost(myBoostStart, myBoostEnd);

        if (myBoostStart <= 0 || myBoostEnd <= 0)
        {
            if (myBoostSpheres.Length > 0)
            {
                myBoostSpheres[0].transform.position = new Vector3(path.GetFirstPoint().x, path.GetFirstPoint().y, 0);
                myBoostSpheres[1].transform.position = new Vector3(path.GetFirstPoint().x, path.GetFirstPoint().y, 0);
            }

            return;
        }

        Vector2[] points = path.CalculateEvenlySpacedPoints();

        if (myBoostStart >= points.Length || myBoostEnd >= points.Length)
        {
            Debug.LogError("Boost outside of spline length!");
            return;
        }
        if (myBoostEnd < myBoostStart)
        {
            Debug.LogError("myBoostEnd has to be > myBoostStart!");
            return;
        }

        if (myBoostSpheres.Length < 1)
        {
            myBoostSpheres = new GameObject[2];
            Debug.Log("boostSpheres length: " + myBoostSpheres.Length);
            myBoostSpheres[0] = GameObject.Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), transform);
            myBoostSpheres[1] = GameObject.Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), transform);
        }

        myBoostSpheres[0].transform.position = new Vector3(points[myBoostStart].x, points[myBoostStart].y, 0);
        myBoostSpheres[1].transform.position = new Vector3(points[myBoostEnd].x, points[myBoostEnd].y, 0);
    }

    public void CreatePath()
    {
        path = new Path(transform.position);
    }

    public void StartOver()
    {
        Reset();
    }

    private void Reset()
    {
        CreatePath();
    }
}