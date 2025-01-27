using UnityEngine;

public class AngelMovement : MonoBehaviour
{
    [Header("Configurations")]
    public float maxRandomYAccelerationForce = 0f;

    private Transform tf;
    private Rigidbody rb;
    private float randomAccelerationForce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tf = gameObject.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody>();
        if (tf == null || rb == null)
            return;

        if (tf.localPosition.y < 0)
            randomAccelerationForce = Random.Range(1, 2);
        else
            randomAccelerationForce = Random.Range(-2, -1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.AddRelativeForce(Vector3.up * randomAccelerationForce);
    }
}
