using UnityEngine;

public class FollowBound : MonoBehaviour
{
    GetScreenBounds bounds;
    [SerializeField] GameObject bottom;
    [SerializeField] GameObject top;
    [SerializeField] GameObject left;
    [SerializeField] GameObject right;
    float cameraDistanceZ;
    float rightBound, leftBound, upperBound, lowerBound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        bottom.transform.position = GetLowerMiddle();//teleport in place
        top.transform.position = GetUpperMiddle();
        left.transform.position = GetLeftMiddle();
        right.transform.position = GetRightMiddle();
    }
    
    void Start()
    {
        bounds = FindFirstObjectByType<GetScreenBounds>();
    }

    // Update is called once per frame
    void Update()
    {
        float cameraDistanceZ = -Camera.main.transform.position.z;
        Vector3 boundsMAX = Camera.main.ViewportToWorldPoint(new Vector3(1,1,cameraDistanceZ)); // World coord of TOP RIGHT corner
        rightBound = boundsMAX.x ;
        upperBound = boundsMAX.y ;
       
        Vector3 boundsMIN = Camera.main.ViewportToWorldPoint(new Vector3(0,0,cameraDistanceZ)); // World coord of BOTTOM LEFT corner
        leftBound = boundsMIN.x ;
        lowerBound = boundsMIN.y ;
        Debug.Log(GetLowerMiddle());
        // Debug.Log(boundsMIN);
        FollowBounds();
    }

    void FollowBounds(){
        //bottom.transform.position = GetLowerMiddle();
        bottom.GetComponent<Rigidbody>().MovePosition(GetLowerMiddle());
        top.GetComponent<Rigidbody>().MovePosition(GetUpperMiddle());
        left.GetComponent<Rigidbody>().MovePosition(GetLeftMiddle());
        right.GetComponent<Rigidbody>().MovePosition(GetRightMiddle());
    }

    Vector3 GetLeftMiddle(){ 
        float midY = Camera.main.transform.position.y;
        Vector3 LeftMid = new Vector3(leftBound, midY, 0);
        return LeftMid;
    }

    Vector3 GetRightMiddle(){ 
        float midY = Camera.main.transform.position.y;
        Vector3 RightMid = new Vector3(rightBound, midY, 0);
        return RightMid;
    }

    Vector3 GetLowerMiddle(){ 
        float midX = Camera.main.transform.position.x;
        Vector3 LowMid = new Vector3(midX, lowerBound, 0);
        return LowMid;
    }

    Vector3 GetUpperMiddle(){ 
        float midX = Camera.main.transform.position.x;
        Vector3 LowMid = new Vector3(midX, upperBound, 0);
        return LowMid;
    }
}
