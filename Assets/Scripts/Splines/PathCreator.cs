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
    private bool myIsRail = false;
    
    [SerializeField, HideInInspector]
    private bool myHasBoost;

    public Color myAnchorCol = Color.red;
    public Color myControlCol = Color.white;
    public Color mySegmentCol = Color.green;
    public Color mySelectedSegmentCol = Color.yellow;
    public float myAnchorDiameter = .1f;
    public float myControlDiameter = .075f;
    public bool myDisplayControlPoints = true;
    
    private void OnValidate()
    {
        UpdateBoost();
    }
    public bool GetIsRail()
    {
        return myIsRail;
    }
    public int GetBoostStart()
    {
        return myBoostStart;
    }
    public int GetBoostEnd()
    {
        return myBoostEnd;
    }
    public void UpdateBoost()
    {
        if (!myHasBoost)
        {
            return;
        }

        path.UpdateBoost(myBoostStart, myBoostEnd);

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
    }

    public void AddBoost()
    {
        myHasBoost = true;
    }

    public void DeleteBoost()
    {
        myHasBoost = false;
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