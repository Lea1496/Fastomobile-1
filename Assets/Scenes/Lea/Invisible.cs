
using UnityEngine;
 
public class Invisible : MonoBehaviour
{
    Renderer obj;
    void Start()
    {
        obj = GetComponent<MeshRenderer>();
        obj.enabled= false;
    }

}