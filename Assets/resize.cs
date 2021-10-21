using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class resize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        getWidthFromExtents();
        getTrigSizeDegs();
    }

    public void getTrigSizeDegs()
    {

        float dist = Vector3.Distance(Camera.main.transform.position, transform.position);        
        float halfHeight = transform.localScale.y/2.0f;
        float trigSizeDegs = 2.0f * Mathf.Atan( halfHeight / dist) * Mathf.Rad2Deg;
        
        // Debug.Log(Time.frameCount.ToString() + " dist: " + dist.ToString());
        // Debug.Log(Time.frameCount.ToString() + " radius: " + halfHeight.ToString());
        Debug.Log(Time.frameCount.ToString() + " trigSizeDegs: " + trigSizeDegs.ToString());

    }

    public void getWidthFromExtents()
    {
        
        Renderer rend = GetComponent<Renderer>();

        Vector3 cen = rend.bounds.center;
        Vector3 ext = rend.bounds.extents;

        // https://docs.unity3d.com/ScriptReference/Camera.WorldToViewportPoint.html
        float minY = Camera.main.WorldToViewportPoint(new Vector3(cen.x, cen.y-ext.y, cen.z)).y;
        float maxY = Camera.main.WorldToViewportPoint(new Vector3(cen.x, cen.y+ext.y, cen.z)).y;

        float normHeight = (maxY-minY);
        float sizeDegs = Camera.main.fieldOfView * normHeight;

        Debug.Log(Time.frameCount.ToString() + " norm height: " + normHeight.ToString());
        //Debug.Log(Time.frameCount.ToString() + " Vert screen res: " + screenVertRes.ToString());
        //Debug.Log(Time.frameCount.ToString() + " fov: " + Camera.main.fieldOfView.ToString());
        Debug.Log(Time.frameCount.ToString() + " extent size: " + sizeDegs.ToString());

        Vector3 minVec = new Vector3(cen.x, cen.y-ext.y, cen.z);
        Vector3 maxVec = new Vector3(cen.x, cen.y+ext.y, cen.z);

        Debug.DrawLine(minVec, maxVec, Color.red, 2.5f);
        
    }

}
