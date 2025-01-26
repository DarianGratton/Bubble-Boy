using UnityEngine;

public class PaperAirPlaneMovement : MonoBehaviour
{
    [Header("Configuration")]
    public float timeGliding = 2.0f;
    public float timeDescending = 5.0f;
    private float descendingAcceleration = 5.0f;

    private Rigidbody rb;
    private float timeElapsed = 0.0f;
    private bool isGliding = true;
    private float acceleration = 0.0f;   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        timeElapsed += Time.deltaTime;
        if (isGliding && timeElapsed > timeGliding)
        {
            acceleration = -descendingAcceleration;
            isGliding = false;
            timeElapsed = 0.0f;
        }
        else if (!isGliding && timeElapsed > timeDescending)
        {
            acceleration = 0.0f;
            isGliding = true;
            timeElapsed = 0.0f;
        }
        rb.AddRelativeForce(Vector3.up * acceleration);
    }
}
