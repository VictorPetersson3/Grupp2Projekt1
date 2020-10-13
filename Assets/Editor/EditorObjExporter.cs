using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using UnityEngine.SceneManagement;

struct ObjMaterial
{
    public string myName;
    public string myTextureName;
}

public class EditorObjExporter : ScriptableObject
{
    private static int myVertexOffset = 0;
    private static int myNormalOffset = 0;
    private static int myUvOffset = 0;


    //User should probably be able to change this. It is currently left as an excercise for
    //the reader.
    private static string myTargetFolder = "/ExportedObj";


    private static string MeshToString(MeshFilter aMeshFilter, Dictionary<string, ObjMaterial> aMaterialList)
    {
        Mesh m = aMeshFilter.sharedMesh;
        Material[] mats = aMeshFilter.GetComponent<Renderer>().sharedMaterials;

        StringBuilder sb = new StringBuilder();

        sb.Append("g ").Append(aMeshFilter.name).Append("\n");
        foreach (Vector3 lv in m.vertices)
        {
            Vector3 wv = aMeshFilter.transform.TransformPoint(lv);

            //This is sort of ugly - inverting x-component since we're in
            //a different coordinate system than "everyone" is "used to".
            sb.Append(string.Format("v {0} {1} {2}\n", -wv.x, wv.y, wv.z));
        }
        sb.Append("\n");

        foreach (Vector3 lv in m.normals)
        {
            Vector3 wv = aMeshFilter.transform.TransformDirection(lv);

            sb.Append(string.Format("vn {0} {1} {2}\n", -wv.x, wv.y, wv.z));
        }
        sb.Append("\n");

        foreach (Vector3 v in m.uv)
        {
            sb.Append(string.Format("vt {0} {1}\n", v.x, v.y));
        }

        for (int material = 0; material < m.subMeshCount; material++)
        {
            sb.Append("\n");
            sb.Append("usemtl ").Append(mats[material].name).Append("\n");
            sb.Append("usemap ").Append(mats[material].name).Append("\n");

            //See if this material is already in the materiallist.
            try
            {
                ObjMaterial objMaterial = new ObjMaterial();

                objMaterial.myName = mats[material].name;

                if (mats[material].mainTexture)
                    objMaterial.myTextureName = AssetDatabase.GetAssetPath(mats[material].mainTexture);
                else
                    objMaterial.myTextureName = null;

                aMaterialList.Add(objMaterial.myName, objMaterial);
            }
            catch (ArgumentException)
            {
                //Already in the dictionary
            }


            int[] triangles = m.GetTriangles(material);
            for (int i = 0; i < triangles.Length; i += 3)
            {
                //Because we inverted the x-component, we also needed to alter the triangle winding.
                sb.Append(string.Format("f {1}/{1}/{1} {0}/{0}/{0} {2}/{2}/{2}\n",
                    triangles[i] + 1 + myVertexOffset, triangles[i + 1] + 1 + myNormalOffset, triangles[i + 2] + 1 + myUvOffset));
            }
        }

        myVertexOffset += m.vertices.Length;
        myNormalOffset += m.normals.Length;
        myUvOffset += m.uv.Length;

        return sb.ToString();
    }

    private static void Clear()
    {
        myVertexOffset = 0;
        myNormalOffset = 0;
        myUvOffset = 0;
    }

    private static Dictionary<string, ObjMaterial> PrepareFileWrite()
    {
        Clear();

        return new Dictionary<string, ObjMaterial>();
    }

    private static void MeshToFile(MeshFilter aMeshFilter, string folder, string aFilename)
    {
        Dictionary<string, ObjMaterial> materialList = PrepareFileWrite();

        using (StreamWriter sw = new StreamWriter(aFilename + ".obj"))
        {
            sw.Write("mtllib ./" + aFilename + ".mtl\n");
            sw.Write(MeshToString(aMeshFilter, materialList));
        }

       // MaterialsToFile(materialList, folder, filename);
    }

    private static void MeshesToFile(MeshFilter[] aMeshFilter, string folder, string aFilename)
    {
        Dictionary<string, ObjMaterial> materialList = PrepareFileWrite();

        using (StreamWriter sw = new StreamWriter(folder + Path.PathSeparator + aFilename + ".obj"))
        {
            sw.Write("mtllib ./" + aFilename + ".mtl\n");

            for (int i = 0; i < aMeshFilter.Length; i++)
            {
                sw.Write(MeshToString(aMeshFilter[i], materialList));
            }
        }

        //MaterialsToFile(materialList, folder, filename);
    }


    [MenuItem("Custom/Export/Export all MeshFilters in selection to separate OBJs")]
    static void ExportSelectionToSeparate()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        Debug.Log(sceneName);
        //if (!CreateTargetFolder())
            //return;

        Transform[] selection = Selection.GetTransforms(SelectionMode.Editable | SelectionMode.ExcludePrefab);

        if (selection.Length == 0)
        {
            EditorUtility.DisplayDialog("No source object selected!", "Please select one or more target objects", "");
            return;
        }

        int exportedObjects = 0;

        for (int i = 0; i < selection.Length; i++)
        {
            Component[] meshfilter = selection[i].GetComponentsInChildren(typeof(MeshFilter));

            for (int m = 0; m < meshfilter.Length; m++)
            {
                exportedObjects++;
                MeshToFile((MeshFilter)meshfilter[m], myTargetFolder, selection[i].name + "_" + i + "_" + m + "_" + sceneName);
            }
        }

        if (exportedObjects > 0)
            EditorUtility.DisplayDialog("Objects exported", "Exported " + exportedObjects + " objects", "");
        else
            EditorUtility.DisplayDialog("Objects not exported", "Make sure at least some of your selected objects have mesh filters!", "");
    }


}