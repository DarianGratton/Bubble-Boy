using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using System.Collections;

public class OscilateObj : MonoBehaviour{
    //unchecked. Will oscilate along X axis;
    //checked. Will oscilate along Y axis
    [SerializeField] bool alongXaxis;
    //size of oscilation sin wave.
    [SerializeField] float amplitude;
    float time = 0;
    Rigidbody rb;
    void Start(){
        rb = GetComponent<Rigidbody>();
    }

    void Update(){
        time += Time.deltaTime;
        if(alongXaxis) {
            float newXforce = amplitude * Mathf.Sin(time);
            Vector3 direction = new Vector3(newXforce, 0 , 0);
            rb.AddForce(direction);
        } else {
            float newYforce = amplitude * Mathf.Sin(time);
            Vector3 direction = new Vector3(0, newYforce , 0);
            rb.AddForce(direction);
        }
    }
}
