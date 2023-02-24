using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Invisible : MonoBehaviour
{
    Renderer test;
    //code de https://vionixstudio.com/2021/08/19/make-a-gameobject-invisible-in-unity/
    void Start()
    {
        test= GetComponent<MeshRenderer>();
        test.enabled= false;
    }

}