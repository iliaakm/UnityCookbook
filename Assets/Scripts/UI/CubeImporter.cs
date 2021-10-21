using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AssetImporters;

[ScriptedImporter(1, "cube")]
public class CubeImporter: ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        CubeDescription cubeDescription;
        try
        {
            var text = System.IO.File.ReadAllText(ctx.assetPath);
            cubeDescription = JsonUtility.FromJson<CubeDescription>(text);
        }
        catch (System.ArgumentException e)
        {
            Debug.LogError($"{ctx.assetPath} is not a valid cube {e.Message}");
            return;
        }
        catch (System.Exception e)
        {
            throw e;
        }

        var cubeObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        string name = System.IO.Path.GetFileNameWithoutExtension(ctx.assetPath);

        var cubeMesh = Instantiate(cubeObject.GetComponent<MeshFilter>().sharedMesh);
        var scaleMatrix = Matrix4x4.Scale(cubeDescription.size);
        var vertices = cubeMesh.vertices;

        for (int v = 0; v < vertices.Length; v++)
        {
            vertices[v] = scaleMatrix.MultiplyPoint(vertices[v]);
        }
        cubeMesh.vertices = vertices;
        cubeObject.GetComponent<MeshFilter>().sharedMesh = cubeMesh;
        cubeObject.name = name + " Mesh";

        var cubeMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        cubeMaterial.color = cubeDescription.color;
        cubeMaterial.name = name + " Material";
        cubeObject.GetComponent<MeshRenderer>().material = cubeMaterial;

        ctx.AddObjectToAsset(name, cubeObject);
        ctx.SetMainObject(cubeObject);

        ctx.AddObjectToAsset(cubeMaterial.name, cubeMaterial);
        ctx.AddObjectToAsset(cubeMesh.name, cubeMesh);
    }
}
#endif

public struct CubeDescription
{
    public Vector3 size;
    public Color color;
}