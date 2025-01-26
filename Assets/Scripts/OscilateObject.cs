// using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
// using System.Collections;

public class OscilateObject : MonoBehaviour{
    //unchecked. Will oscilate along X axis;
    //checked. Will oscilate along Y axis
    [SerializeField] bool alongXaxis;
    //size of oscilation sin wave.
    [SerializeField] float amplitude;
    float time = 0;
    Vector3 startingPos;
    Rigidbody rb;
    void Start(){
        rb = GetComponent<Rigidbody>();
        startingPos = gameObject.transform.position;
    }

    //THIS ONLY WORKS IF OSCILATION IS PERPENDICULAR to velocity
    void FixedUpdate(){
        time += Time.fixedDeltaTime; //time between each frame
        Rigidbody cameraRb = Camera.main.GetComponent<Rigidbody>();
        if (cameraRb == null)
            return;

        startingPos.y += cameraRb.linearVelocity.y * Time.fixedDeltaTime;

        if (alongXaxis) {
            float newXPos = amplitude * Mathf.Sin(time);
            float deltaX = startingPos.x + newXPos;
            Vector3 direction = new Vector3(deltaX, gameObject.transform.position.y , 0);
            rb.transform.position = direction; // sorry
        } else {
            float newYPos = amplitude * Mathf.Sin(time);
            float deltaY = startingPos.y + newYPos;
            Vector3 direction = new Vector3(gameObject.transform.position.x, deltaY, 0);
            rb.transform.position = direction; // sorry
        }
    }
}

