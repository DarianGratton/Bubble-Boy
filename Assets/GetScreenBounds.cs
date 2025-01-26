using UnityEngine;

//return screen bounds
public class GetScreenBounds : MonoBehaviour
{
    float rightBound;
    float leftBound;
    float upperBound;
    float lowerBound;
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
        rightBound = boundsMAX.x ;
        upperBound = boundsMAX.y ;
       
        Vector3 boundsMIN = Camera.main.ViewportToWorldPoint(new Vector3(0,0,cameraDistanceZ)); // World coord of BOTTOM LEFT corner
        leftBound = boundsMIN.x ;
        lowerBound = boundsMIN.y ;

        
//       Debug.Log("Screenbounds: " + boundsMIN);
    }

    public float GetLeftBound(){ return leftBound; }
    public float GetRightBound(){ return rightBound; }
    public float GetLowerBound(){ return lowerBound; }
    public float GetUpperBound(){ return upperBound; }


}
