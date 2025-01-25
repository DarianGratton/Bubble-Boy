using Unity.VisualScripting;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    [Header("References")]
    public Transform tf;
    public Rigidbody body;

    [Header("Configurations")]
    public float minSize;
    public float maxSize;
    public float torque;

    private float randomAccelerationForce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Spawn with random size (Mass will change depending on size)
        float randomXY = Random.Range(minSize, maxSize);
        Vector3 randomScale = new Vector3(randomXY, randomXY, randomXY);
        tf.transform.localScale = randomScale;

        // Update mass of object
        body.mass *= randomXY;

        // Random acceleration in the left or right direction
        randomAccelerationForce = 300f;
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
