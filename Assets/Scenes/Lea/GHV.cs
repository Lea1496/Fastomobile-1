using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GHV : MonoBehaviour
{
    int xRotationLimit = 200;
    int yRotationLimit = 200;
    int zRotationLimit = 200;
 
    private void  Update () {
 
        if(transform.localRotation.eulerAngles.x > xRotationLimit){
            transform.localRotation = Quaternion.identity;
        }
 
        if(transform.localRotation.eulerAngles.y > yRotationLimit){
            transform.localRotation = Quaternion.identity;
        }
 
        if(transform.localRotation.eulerAngles.z > zRotationLimit){
            transform.localRotation = Quaternion.identity;
        }
    }
}
// Start is called before the first frame update
    /*public Transform backLeft;
    public Transform backRight;
    public Transform frontLeft;
    public Transform frontRight;
    public RaycastHit lr;
    public RaycastHit rr;
    public RaycastHit lf;
    public RaycastHit rf;
    public Vector3 upDir;
    
    public GameObject carModel;
    public Transform raycastPoint;
    private float hoverHeight = 1.0f;
    private float speed = 20.0f;
    private float terrainHeight;
    private float rotationAmount;
    private RaycastHit hit;
    private Vector3 pos;
    private Vector3 forwardDirection;
    void Update () {
       /* Physics.Raycast(backLeft.position + Vector3.up, Vector3.down, out lr);
        Physics.Raycast(backRight.position + Vector3.up, Vector3.down, out rr);
        Physics.Raycast(frontLeft.position + Vector3.up, Vector3.down, out lf);
        Physics.Raycast(frontRight.position + Vector3.up, Vector3.down, out rf);*/
        // Get the vectors that connect the raycast hit points
//        Debug.Log(Terrain.activeTerrain.SampleHeight(frontLeft.position));
        // Keep at specific height above terrain
        /*pos = gameObject.transform.position;
        gameObject.transform.position = new Vector3(pos.x, Terrain.activeTerrain.SampleHeight(pos) + 1f, pos.z);
        if (Terrain.activeTerrain.SampleHeight(frontLeft.position) > 1.5f)
        {
            Debug.Log(Terrain.activeTerrain.SampleHeight(frontLeft.position));
            frontLeft.SetPositionAndRotation(new Vector3(frontLeft.position.x, Terrain.activeTerrain.SampleHeight(frontLeft.position) + 1, frontLeft.position.z), frontLeft.transform.rotation);
        }
        if (Terrain.activeTerrain.SampleHeight(backLeft.position) > 1.5f)
        {
            Debug.Log(Terrain.activeTerrain.SampleHeight(backLeft.position));
            backLeft.SetPositionAndRotation(new Vector3(backLeft.position.x, Terrain.activeTerrain.SampleHeight(backLeft.position) + 1, backLeft.position.z), backLeft.transform.rotation);
        }
        if (Terrain.activeTerrain.SampleHeight(frontRight.position) > 1.5f)
        {
            Debug.Log(frontRight.position.y -Terrain.activeTerrain.SampleHeight(frontRight.position));
            frontRight.SetPositionAndRotation(new Vector3(frontRight.position.x, Terrain.activeTerrain.SampleHeight(frontRight.position) + 1, frontRight.position.z), frontRight.transform.rotation);
        }
        if (Terrain.activeTerrain.SampleHeight(backRight.position) > 1.5f)
        {
            Debug.Log(backRight.position.y - Terrain.activeTerrain.SampleHeight(backRight.position));
            backRight.SetPositionAndRotation(new Vector3(backRight.position.x, Terrain.activeTerrain.SampleHeight(backRight.position) + 1, backRight.position.z), backRight.transform.rotation);
        }
        
        /*Vector3 a = rr.point - lr.point;
        Vector3 b = rf.point - rr.point;
        Vector3 c = lf.point - rf.point;
        Vector3 d = rr.point - lf.point;
 
        // Get the normal at each corner
 
        Vector3 crossBA = Vector3.Cross (b, a);
        Vector3 crossCB = Vector3.Cross (c, b);
        Vector3 crossDC = Vector3.Cross (d, c);
        Vector3 crossAD = Vector3.Cross (a, d);
 
        // Calculate composite normal
 
        transform.up = (crossBA + crossCB + crossDC + crossAD ).normalized; 
        
        Debug.DrawRay(rr.point, Vector3.up);
        Debug.DrawRay(lr.point, Vector3.up);
        Debug.DrawRay(lf.point, Vector3.up);
        Debug.DrawRay(rf.point, Vector3.up);*/
       
 
   // }
    
//}
