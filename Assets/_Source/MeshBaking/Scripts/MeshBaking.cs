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
    }
}
