using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshBaking : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Material _material;

    [EditorButton]
    private void Bake()
    {
        var bakedGameObject = new GameObject("Baked Line");
        var mesh = new Mesh();

        _lineRenderer.Simplify(0.1f);
        _lineRenderer.BakeMesh(mesh, true);

        bakedGameObject.AddComponent<MeshFilter>().mesh = mesh;
        bakedGameObject.AddComponent<MeshRenderer>().material = _material;
        bakedGameObject.AddComponent<MeshCollider>().sharedMesh = mesh;
    }
}
