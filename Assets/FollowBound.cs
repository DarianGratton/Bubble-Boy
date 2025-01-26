using System.Collections;
using UnityEngine;

public class FollowBound : MonoBehaviour
{
    GetScreenBounds bounds;
    [SerializeField] GameObject bottom;
    [SerializeField] GameObject top;
    [SerializeField] GameObject left;
    [SerializeField] GameObject right;
    Rigidbody rbBottom, rbTop, rbLeft, rbRight;
    [SerializeField]float offsetBounds = 0.3f;
    float cameraDistanceZ;
    float rightBound, leftBound, upperBound, lowerBound;

    [SerializeField] Rigidbody player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        
        teleportBounds();
        bounds = FindFirstObjectByType<GetScreenBounds>();
        rbBottom = bottom.GetComponent<Rigidbody>();
        rbTop = top.GetComponent<Rigidbody>();
        rbLeft = left.GetComponent<Rigidbody>();
        rbRight = right.GetComponent<Rigidbody>();
        StartCoroutine(WaitToEnable());
        //Stop player from flying off at runtime
        Collider sphere = player.gameObject.GetComponent<SphereCollider>();
        sphere.enabled = false;
        player.constraints = RigidbodyConstraints.FreezePosition;
        StartCoroutine(UnlockPlayer());
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
        // Debug.Log(GetLowerMiddle());
        // Debug.Log(boundsMIN);
        FollowBounds();
        // ForceInBounds();
    }

//come back to this
    // void ForceInBounds(){
    //     float xClamped = Mathf.Clamp(leftBound, rightBound, player.gameObject.transform.position.x);
    //     float yClamped = Mathf.Clamp(lowerBound, upperBound, player.gameObject.transform.position.y);
    //     Vector3 clampedPos = new Vector3(xClamped, yClamped, 0);
    //     Debug.Log(clampedPos);
    //     player.MovePosition(clampedPos);
    //     // player.gameObject.transform.position = clampedPos;
    // }

    IEnumerator UnlockPlayer(){
        yield return new WaitForSeconds(0.15f);
        player.constraints = RigidbodyConstraints.None;
        player.constraints = RigidbodyConstraints.FreezePositionZ;
        Collider sphere = player.gameObject.GetComponent<SphereCollider>();
        sphere.enabled = true;
    }

    //Stop teleporting bounds bug that knocks ball off at runtime.
    IEnumerator WaitToEnable(){
        yield return new WaitForSeconds(0.05f);
        bottom.SetActive(true);
        top.SetActive(true);
        left.SetActive(true);
        right.SetActive(true);
    }
    void FollowBounds(){
        rbBottom.MovePosition(GetLowerMiddle());
        rbTop.MovePosition(GetUpperMiddle());
        rbLeft.MovePosition(GetLeftMiddle());
        rbRight.MovePosition(GetRightMiddle());
    }

    void teleportBounds(){
        bottom.transform.position = GetLowerMiddle();
        top.transform.position = GetUpperMiddle();
        left.transform.position = GetLeftMiddle();
        right.transform.position = GetRightMiddle();
    }

    Vector3 GetLeftMiddle(){ 
        float midY = Camera.main.transform.position.y;
        Vector3 LeftMid = new Vector3(leftBound - offsetBounds, midY, 0);
        return LeftMid;
    }

    Vector3 GetRightMiddle(){ 
        float midY = Camera.main.transform.position.y;
        Vector3 RightMid = new Vector3(rightBound + offsetBounds, midY, 0);
        return RightMid;
    }

    Vector3 GetLowerMiddle(){ 
        float midX = Camera.main.transform.position.x;
        Vector3 LowMid = new Vector3(midX, lowerBound - offsetBounds, 0);
        return LowMid;
    }

    Vector3 GetUpperMiddle(){ 
        float midX = Camera.main.transform.position.x;
        Vector3 LowMid = new Vector3(midX, upperBound + offsetBounds, 0);
        return LowMid;
    }
}
