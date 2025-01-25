// using System.Numerics;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    [SerializeField] float StrafeSpeed;
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
            Vector3 left = new Vector3 (-StrafeSpeed, 0,0);
            rb.AddForce(left);
        }

        //Left
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            Debug.Log("right");
            Vector3 left = new Vector3 (-StrafeSpeed, 0,0);
            rb.AddForce(left);
        }
    }
}
