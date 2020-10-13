using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [HideInInspector]
    public Paths path;
    [SerializeField]
    private int myBoostStart = 0;
    [SerializeField]
    private int myBoostEnd = 0;
    [SerializeField]
    private GameObject myBoostSpherePrefab = null;
    
    [SerializeField, HideInInspector]
    private bool myHasBoost;
    [SerializeField, HideInInspector]
    private GameObject[] myBoostSpheres;

    public Color anchorCol = Color.red;
    public Color controlCol = Color.white;
    public Color segmentCol = Color.green;
    public Color selectedSegmentCol = Color.yellow;
    public float anchorDiameter = .1f;
    public float controlDiameter = .075f;
    public bool displayControlPoints = true;
    
    private void OnValidate()
    {
        UpdateBoost();
    }

    public void UpdateBoost()
    {
        if (!myHasBoost)
        {
            return;
        }

        path.UpdateBoost(myBoostStart, myBoostEnd);

        if (myBoostStart < 0 || myBoostEnd < 0)
        {
            myBoostSpheres[0].transform.position = new Vector3(path.GetFirstPoint().x, path.GetFirstPoint().y, 0);
            myBoostSpheres[1].transform.position = new Vector3(path.GetFirstPoint().x, path.GetFirstPoint().y, 0);
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

        myBoostSpheres[0].transform.position = new Vector3(points[myBoostStart].x, points[myBoostStart].y, 0);
        myBoostSpheres[1].transform.position = new Vector3(points[myBoostEnd].x, points[myBoostEnd].y, 0);
    }

    public void AddBoost()
    {
        myHasBoost = true;
        myBoostSpheres = new GameObject[2];
        myBoostSpheres[0] = Instantiate(myBoostSpherePrefab, transform);
        myBoostSpheres[1] = Instantiate(myBoostSpherePrefab, transform);
    }

    public void DeleteBoost()
    {
        myHasBoost = false;
        DestroyImmediate(myBoostSpheres[0]);
        DestroyImmediate(myBoostSpheres[1]);
        myBoostSpheres[0] = null;
        myBoostSpheres[1] = null;
    }

    public bool HasBoost()
    {
        return myHasBoost;
    }

    public Vector2 GetBoost()
    {
        return new Vector2(myBoostStart, myBoostEnd);
    }

    public void CreatePath()
    {
        path = new Paths(transform.position);
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