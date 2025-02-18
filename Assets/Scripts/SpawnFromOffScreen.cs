// using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

//just attach this script to any pickup, enemy or obstacle you want to spawn above, below or to the sides off screen.
//Choose a direction. 
//This script will spawn a pickup, enemy or obstacle at a random positon above, below or left or right off screen.
//example: if you select Right, the object will spawn to the right off screen at a random Y position
//THIS ONLY WORKS IF THE CAMERA IS LOOKING ALONG THE Z AXIS WITH ALL THE CHARACTER MVMT AND ACTION TAKING PLACE ALONG X AND Y AXIS.
public class SpawnFromOffScreen : MonoBehaviour
{
    //Spawn Above, Below, Left or Right Camera View?
    // 'u' = up above camera
    // 'd' = down below camera
    // 'l' = left of camera
    // 'r' = right of camera 
    [SerializeField] char UpDownLeftRight;
    [SerializeField] float StartVelocityX;
    [SerializeField] float StartVelocityY;
    [SerializeField] bool leftOrRight = false;
    Rigidbody rb;

    //How far off camera to spawn?
    float padding;
    float leftCameraBound, rightCameraBound, upperCameraBound, downCameraBound;

    void Start()
    {
        padding = 0.5f;
        getCameraBounds();
        if (leftOrRight)
            UpDownLeftRight = (Random.Range(0f, 1f) > 0.5f) ? 'R' : 'L';

        if (UpDownLeftRight == 'r' || UpDownLeftRight == 'R')
        {
            StartVelocityX = -StartVelocityX;
        }

        Transform tf = gameObject.GetComponent<Transform>();
        if (UpDownLeftRight == 'l' || UpDownLeftRight == 'L')
        {
            Vector3 rotationVector = new Vector3(0, tf.rotation.y + 180f, 0);
            gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(rotationVector);
        }

        Vector3 vec = moveToSpawnPos();
        tf.position = vec;
        InitialVelocity();
    }

    void InitialVelocity()
    {
        //don't add if 0
        if (StartVelocityX == 0 && StartVelocityY == 0) return;
        rb = GetComponent<Rigidbody>();
        Vector3 StartVelocity = new Vector3(StartVelocityX, StartVelocityY, 0);
        rb.AddForce(StartVelocity);
    }

    public float GetStartVelocityY()
    {
        return StartVelocityY;
    }
    //Returns Up, Down, Left, Right boudns of viewport and what the player can see. 
    //Camera view keeps changing in size so needs to keep being checked on spawn.
    void getCameraBounds()
    {
        //Assuming orthographic camera then z pos of ViewportToWorldPoint doesn't matter.  cameraDistanceZ can be anything and still work.
        //If using perspective camera then z pos does matter. Assuming all gameplay takes place along X and Y axis while not moving along z axis (Z pos = 0).
        //This should work for both.

        float cameraDistanceZ = -Camera.main.transform.position.z; // distance along z axis from origin. for perspecitve camera. Ortho doesn;t matter.

        Vector3 boundsMAX = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, cameraDistanceZ)); // World coord of TOP RIGHT corner
        rightCameraBound = boundsMAX.x;
        upperCameraBound = boundsMAX.y;

        Vector3 boundsMIN = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, cameraDistanceZ)); // World coord of BOTTOM LEFT corner
        leftCameraBound = boundsMIN.x;
        downCameraBound = boundsMIN.y;
    }

    Vector3 moveToSpawnPos()
    {
        //d = Down below camera 
        if (UpDownLeftRight == 'd' || UpDownLeftRight == 'D')
        {
            return new Vector3(getRandomXPos(), downCameraBound - padding, 0);
        }

        //l = Left of camera
        else if (UpDownLeftRight == 'l' || UpDownLeftRight == 'L')
        {
            return new Vector3(leftCameraBound - padding, getRandomYPos(), 0);
        }

        //r = right of camera
        else if (UpDownLeftRight == 'r' || UpDownLeftRight == 'R')
        {
            return new Vector3(rightCameraBound + padding, getRandomYPos(), 0);
        }

        //u = Up Above camera. default
        else
        {
            return new Vector3(getRandomXPos(), upperCameraBound + padding, 0);
        }
    }

    float getRandomXPos()
    {
        return Random.Range(leftCameraBound, rightCameraBound);
    }

    float getRandomYPos()
    {
        return Random.Range(upperCameraBound, downCameraBound);
    }
}