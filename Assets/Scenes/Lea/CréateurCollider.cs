
using UnityEngine;

public class CréateurCollider : MonoBehaviour
{
    
    private void Start()
    {
        MeshCollider meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        meshc.sharedMesh = GetComponent<MeshFilter>().mesh;
    }

    
}
