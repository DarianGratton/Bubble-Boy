using Unity.VisualScripting;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    [Header("References")]
    public Transform tf;
    public Rigidbody body;

    [Header("Configurations")]
    public float torque;
    private float randomAccelerationForce = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Random acceleration in the left or right direction
        float cameraDistanceZ = -Camera.main.transform.position.z;
        Vector3 cameraTopRightBounds = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, cameraDistanceZ));
        Vector3 cameraTopLeftBounds = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, cameraDistanceZ));
        if (tf.transform.localPosition.x < cameraTopLeftBounds.x / 2) // Spawns too far to the left
            randomAccelerationForce = Random.Range(0, 400f);
        else if (tf.transform.localPosition.x > cameraTopRightBounds.x / 2) // Spawns too far to the right
            randomAccelerationForce = Random.Range(-400f, 0);
        else 
            randomAccelerationForce = Random.Range(-400f, 400);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        body.AddRelativeForce(Vector3.right * randomAccelerationForce);
        body.AddRelativeTorque(Vector3.forward * torque);
    }
}
