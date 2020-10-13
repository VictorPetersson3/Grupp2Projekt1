using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

public static class MeshUtility
{
    public static GameObject Create(string aName, GameObject aParent, params Type[] aComponents)
    {
        var res = new GameObject(aName, aComponents);
        res.transform.parent = aParent.transform;
        res.transform.localPosition = Vector3.zero;
        res.transform.localScale = Vector3.one;
        res.transform.localRotation = Quaternion.identity;
        return res;
    }

}
