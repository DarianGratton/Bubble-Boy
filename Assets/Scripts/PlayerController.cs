using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float sizeSpeedMod;
    public float sizeScaleMod;
    public float sizeZoomMod;
    public float size;

    void Awake()
    {
        UpdateSpeed();
        UpdateSize();
        UpdateZoom();
    }

    void Update()
    {
        UpdateSpeed();
        UpdateSize();
        UpdateZoom();
    }

    //When the player runs into a collectable bubble they grow and start moving faster
    void CollectBubble(float bubbleSize)
    {
        size += bubbleSize;
        UpdateSpeed();
        UpdateSize();
        UpdateZoom();
    }

    //When getting hit, the player loses size and speed
    void TakeDamage(float damage)
    {
        size -= damage;
        UpdateSpeed();
        UpdateSize();
        UpdateZoom();
    }

    //Helper function to correct speed once size changes
    void UpdateSize()
    {
        float newScale = size * sizeScaleMod;
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }

    //Helper function to correct speed once size changes
    void UpdateSpeed()
    {
        //Update player velocity THIS WILL BE BAD ONCE WE HAVE OTHER Y AXIS FORCES, CHANGE LATER
        Rigidbody rbP = GetComponent<Rigidbody>();
        if (rbP)
            rbP.linearVelocity = new Vector3(rbP.linearVelocity.x, size * sizeSpeedMod, rbP.linearVelocity.z);

        //Update camera velocity
        Rigidbody rbCam = Camera.main.GetComponent<Rigidbody>();
        if (rbCam)
            rbCam.linearVelocity = new Vector3(rbCam.linearVelocity.x, size * sizeSpeedMod, rbCam.linearVelocity.z);
    }
    
    //Helper function to correct camera zoom once size changes
    void UpdateZoom()
    {
        Vector3 camPos = Camera.main.transform.position;
        Camera.main.transform.position = new Vector3(camPos.x, camPos.y, -size * sizeZoomMod);
    }
}
