using UnityEngine;

public class DestoryAfterTimeOffScreen : MonoBehaviour
{
    [Header("Configurations")]
    public float timeToLive = 10.0f;

    private float timeElapsed = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Is object visible on the screen
        if (IsObjectOnScreen())
        {
            return;
        }

        timeElapsed += Time.deltaTime;
        if (timeElapsed > timeToLive)
        {
            Destroy(gameObject);
        }
    }

    // Only works with the xy axis
    private bool IsObjectOnScreen()
    {
        float cameraDistanceZ = -Camera.main.transform.position.z; // distance along z axis from origin. for perspecitve camera. Ortho doesn;t matter.
        float padding = 10.0f;
        Vector3 boundsMAX = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, cameraDistanceZ)); // World coord of TOP RIGHT corner
        Vector3 boundsMIN = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, cameraDistanceZ)); // World coord of BOTTOM LEFT corner

        if (gameObject.transform.position.x > boundsMAX.x + padding ||
            gameObject.transform.position.x < boundsMIN.x - padding)
            return false;

        if (gameObject.transform.position.y > boundsMAX.y + padding ||
            gameObject.transform.position.y < boundsMIN.y - padding)
            return false;

        return true;
    }
}
