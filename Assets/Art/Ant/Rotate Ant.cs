using UnityEngine;

public class RotateAnt : MonoBehaviour
{
    [SerializeField] float xRotation = 35;
    [SerializeField] float yRotation = 60;

    [SerializeField] float zRotation = 20;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            
            transform.Rotate(xRotation*Time.deltaTime, yRotation*Time.deltaTime, zRotation*Time.deltaTime);
    }
}
