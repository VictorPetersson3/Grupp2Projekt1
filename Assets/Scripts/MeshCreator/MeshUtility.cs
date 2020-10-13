using UnityEngine;
using System.Linq;
using System;

public static class MeshUtility
{
    public static GameObject Create(string aName, GameObject aParent, params Type[] someComponents)
    {
        var res = new GameObject(aName, someComponents);
        res.transform.parent = aParent.transform;
        res.transform.localPosition = Vector3.zero;
        res.transform.localScale = Vector3.one;
        res.transform.localRotation = Quaternion.identity;
        return res;
    }

    public static GameObject Instantiate(GameObject aPrefab, Transform aParent)
    {
        var res = UnityEngine.Object.Instantiate(aPrefab, aParent);
        res.transform.localPosition = Vector3.zero;
        res.transform.localRotation = Quaternion.identity;
        res.transform.localScale = Vector3.one;
        return res;
    }

    public static void Destroy(GameObject aGameobject)
    {
        if (Application.isPlaying)
        {
            UnityEngine.Object.Destroy(aGameobject);
        }
        else
        {
            UnityEngine.Object.DestroyImmediate(aGameobject);
        }
    }

    public static void Destroy(Component aComponent)
    {
        if (Application.isPlaying)
        {
            UnityEngine.Object.Destroy(aComponent);
        }
        else
        {
            UnityEngine.Object.DestroyImmediate(aComponent);
        }
    }

    public static void DestroyChildren(GameObject aGameobject)
    {
        var childList = aGameobject.transform.Cast<Transform>().ToList();
        foreach (Transform childTransform in childList)
        {
            Destroy(childTransform.gameObject);
        }
    }
}