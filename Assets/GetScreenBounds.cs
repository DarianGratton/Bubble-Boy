using UnityEngine;

//return screen bounds
public class GetScreenBounds : MonoBehaviour
{
    float rightBound, leftBound, upperBound, lowerBound;
    float cameraDistanceZ ;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraDistanceZ = -Camera.main.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 boundsMAX = Camera.main.ViewportToWorldPoint(new Vector3(1,1,cameraDistanceZ)); // World coord of TOP RIGHT corner
        float rightBound = boundsMAX.x ;
        float upperBound = boundsMAX.y ;
       
        Vector3 boundsMIN = Camera.main.ViewportToWorldPoint(new Vector3(0,0,cameraDistanceZ)); // World coord of BOTTOM LEFT corner
        float leftBound = boundsMIN.x ;
        float lowerBound = boundsMIN.y ;

        
       Debug.Log("Screenbounds: " + boundsMIN);
    }

    public float GetLeftBound(){ return leftBound; }
    public float GetRightBound(){ return rightBound; }
    public float GetLowerBound(){ return lowerBound; }
    public float GetUpperBound(){ return upperBound; }

    public Vector3 GetLeftMiddle(){ 
        float midY = Camera.main.transform.position.y;
        Vector3 LeftMid = new Vector3(leftBound, midY, 0);
        return LeftMid;
    }

    public Vector3 GetLowerMiddle(){ 
        float midX = Camera.main.transform.position.x;
        Vector3 LowMid = new Vector3(midX, lowerBound, 0);
        return LowMid;
    }
}
