using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class resize : MonoBehaviour
{

    private float curDistFromViewToBall;
    private float previousDistFromViewToBall = -1.0f;
    private float previousBallLocalXScale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //scaleBallRadiusByGain();
        getWidthFromExtents();
        getTrigSizeDegs();


        if ( Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Translate(0.0f, 1.0f, 0.0f);
            return;
        }

        if ( Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.Translate(0.0f, -1.0f, 0.0f);
            return;
        }

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

     public void scaleBallRadiusByGain() {

       
        
        float expansionGain = 0.0f;
        
        // camera transform
        Transform camTrans = Camera.main.transform;
        
        // camTrans.InverseTransformPoint(x,y,z) assumes x,y,z are in world coords, and converts them into x',y',z' in local space (defined by camTrans, so in camera space).
        // magnitude then takes the length of the vector from 0,0,0 to x',y',z' in that local space
        curDistFromViewToBall = camTrans.InverseTransformPoint(transform.position).magnitude;

        //prevDistFromViewToBall = previousCamTransform.InverseTransformPoint(previousBallTransform.position).magnitude;
        
        // delta distance is zero!
        //Debug.Log("delta distance: " + (curDistFromViewToBall-prevDistFromViewToBall).ToString());

        float curAngularRadiusRadians = Mathf.Atan( (transform.localScale[0]/2.0f) / curDistFromViewToBall);
        float prevAngularRadiusRadians = Mathf.Atan( (previousBallLocalXScale/2.0f) / previousDistFromViewToBall);

        // What would the angular radius be on the next frame, if scaled by the gain term?
        float scaledAngularRadiusRads = prevAngularRadiusRadians + (curAngularRadiusRadians - prevAngularRadiusRadians) * expansionGain;

        //Debug.Log(Time.frameCount.ToString() + " scaledAngularRadiusRads: " + (scaledAngularRadiusRads * Mathf.Rad2Deg).ToString());

        // What physical radius (m) would bring about this angular subtense?
        //float newBallRadius = curDistFromViewToBall * Mathf.Tan(scaledAngularRadiusRads);
        float newBallDiameter = 2.0f * curDistFromViewToBall * Mathf.Tan(scaledAngularRadiusRads );

        //Debug.Log(Time.frameCount.ToString() + " newBallDiameter (meters): " + newBallDiameter.ToString());
        //Debug.Log(Time.frameCount.ToString() + " " + (Mathf.Atan((0.5f*newBallDiameter)  / curDistFromViewToBall)*Mathf.Rad2Deg).ToString() );        
        
        transform.localScale = new Vector3(newBallDiameter, newBallDiameter, newBallDiameter);

        previousDistFromViewToBall = curDistFromViewToBall;
        previousBallLocalXScale = transform.localScale[0];

        // this code assumes that each component of the balls local scale is equal to its diameter        

        // Notes:
        // newBallRadius is constant when the gain is zero.  That's good!
        // Works: float newBallRadius = curDistFromViewToBall * Mathf.Tan(1.0f * Mathf.Deg2Rad);
        // Does not work: float newBallRadius = curDistFromViewToBall * Mathf.Tan(prevAngularRadiusRadians);
        // there is an error in the line newBallRadius

    }

    

}
