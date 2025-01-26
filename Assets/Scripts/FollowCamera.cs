using JetBrains.Annotations;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("References")]
    public Transform tf;
    public Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        Rigidbody cameraRb = Camera.main.GetComponent<Rigidbody>();
        if (cameraRb == null)
            return;

        float cameraDelta = cameraRb.linearVelocity.y * Time.fixedDeltaTime;
        tf.position = new Vector3(rb.transform.position.x, tf.position.y + cameraDelta, 0);
    }
}
