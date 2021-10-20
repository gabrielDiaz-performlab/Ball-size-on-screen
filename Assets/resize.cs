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
        getPixelWidth();
        getTrigSizeDegs();
    }

    public void getPixelWidth()
    {
        
        Renderer rend = GetComponent<Renderer>();

        // Bounds are in 3D worldspace, but worldToScreenPoint projects them into screen space (in pixels).
        Vector3 posStart = Camera.main.WorldToScreenPoint(new Vector3(rend.bounds.min.x, rend.bounds.min.y, rend.bounds.min.z));
        Vector3 posEnd = Camera.main.WorldToScreenPoint(new Vector3(rend.bounds.max.x, rend.bounds.max.y, rend.bounds.max.z));

        int pixSize = (int)(posEnd.x - posStart.x);
        float screenRes = Screen.currentResolution.height;
        float sphereSizeDegs = Camera.main.fieldOfView * (pixSize / screenRes);

        Debug.Log(Time.frameCount.ToString() + " Pixel width: " + pixSize.ToString());
        Debug.Log(Time.frameCount.ToString() + " screenRes: " + screenRes.ToString());
        Debug.Log(Time.frameCount.ToString() + " fov: " + Camera.main.fieldOfView.ToString());
        Debug.Log(Time.frameCount.ToString() + " sphereSizeDegs: " + sphereSizeDegs.ToString());

        //int widthY = (int)(posEnd.y - posStart.y);
        Vector3 minVec = new Vector3(rend.bounds.min.x, rend.bounds.min.y, rend.bounds.min.z);
        Vector3 maxVec = new Vector3(rend.bounds.max.x, rend.bounds.max.y, rend.bounds.max.z);

        Debug.DrawLine(minVec, maxVec, Color.red, 2.5f);
        
    }

    public void getTrigSizeDegs()
    {

        float dist = Vector3.Distance(Camera.main.transform.position, transform.position);
        //float width = transform.localScale.x;
        float width = GetComponent<Renderer>().bounds.extents.magnitude;

        float sphereSizeDegs = 2.0f * Mathf.Atan(width / dist) * Mathf.Rad2Deg;
        Debug.Log(Time.frameCount.ToString() + " trigSizeDegs: " + sphereSizeDegs.ToString());

        //int widthY = (int)(posEnd.y - posStart.y);
    }

}
