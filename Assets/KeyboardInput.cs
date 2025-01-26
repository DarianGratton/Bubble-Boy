// using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    [SerializeField] float strafeSpeed;
    [SerializeField] float floatJumpSpeed;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //Left
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            Debug.Log("left");
            Vector3 left = new Vector3 (-strafeSpeed, 0,0);
            rb.AddForce(left);
        }

        //Right
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            Vector3 right = new Vector3 (strafeSpeed, 0,0);
            rb.AddForce(right);
        }
        //jump
        if(Input.GetKeyDown(KeyCode.Space)){
            Vector3 up = new Vector3 (0, floatJumpSpeed,0);
            rb.AddForce(up);
        }
    }

}
