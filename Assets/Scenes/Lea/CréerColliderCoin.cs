using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CréerColliderCoin : MonoBehaviour
{
    void Start()
    {
        MeshCollider meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        meshc.sharedMesh = GetComponent<MeshFilter>().mesh;
    }
    
}
