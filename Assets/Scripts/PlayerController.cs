using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool GOD_MODE; // for easier debugging
    public float sizeSpeedMod;
    public float sizeScaleMod;
    public float sizeZoomMod;
    public float size;
    public float sizeLossOnHit;
    public string obstacleTagName;
    public string bubbleTagName;

    public Canvas loseScreen;
    float increaseSize;

    bool pauseVelocity = false;
    void Awake()
    {
        UpdateSpeed();
        UpdateSize();
        UpdateZoom();
    }

    void Update()
    {
        UpdateScore();

        //USE FOR TESTING PURPOSES WHEN CHANGING SIZE IN EDITOR
        //UpdateSpeed();
        //UpdateSize();
        //UpdateZoom();
    }

    //When the player runs into a collectable bubble they grow and start moving faster
    private void CollectBubble(float bubbleSize)
    {
        //Update score for previous segment before size changed
        UpdateScore();

        size += bubbleSize;
        UpdateSpeed();
        UpdateSize();
        UpdateZoom();
    }

    //When getting hit, the player loses size and speed
    private void TakeDamage()
    {
        //Update score for previous segment before size changed
        UpdateScore();

        size -= sizeLossOnHit;

        //Pop/lose game if too small
        if (size < 1)
            LoseGame();

        UpdateSpeed();
        UpdateSize();
        UpdateZoom();
    }

    //ADD CODE HERE WHEN THE PLAYER'S BUBBLE POPS
    private void LoseGame()
    {
        //loseScreen.enabled = true;
        //Time.timeScale = 0f;

        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("LoseScene");
    }

    private void UpdateScore()
    {
        Scoring scoreTracker = GetComponent<Scoring>();
        if (scoreTracker)
            scoreTracker.UpdateScoreBasedOnSize(size);
    }

    //Helper function to correct speed once size changes
    private void UpdateSize()
    {
        float finalScale = size * sizeScaleMod;
        transform.localScale = new Vector3(finalScale, finalScale, finalScale);
        
        
    }


    

    //Helper function to correct speed once size changes
    private void UpdateSpeed()
    {
        //Update player velocity THIS WILL BE BAD ONCE WE HAVE OTHER Y AXIS FORCES, CHANGE LATER
        //Rigidbody rbP = GetComponent<Rigidbody>();
        //if (rbP)
        //    rbP.linearVelocity = new Vector3(rbP.linearVelocity.x, size * sizeSpeedMod, rbP.linearVelocity.z);

        //Update camera velocity
        Rigidbody rbCam = Camera.main.GetComponent<Rigidbody>();
        if (rbCam && pauseVelocity == false)
            rbCam.linearVelocity = new Vector3(rbCam.linearVelocity.x, size * sizeSpeedMod, rbCam.linearVelocity.z);
        else if(pauseVelocity == true)
            rbCam.linearVelocity = Vector3.zero;
    }

 
    //Helper function to correct camera zoom once size changes
    private void UpdateZoom()
    {
        Vector3 camPos = Camera.main.transform.position;
        Vector3 finalPos = new Vector3(camPos.x, camPos.y, -size * sizeZoomMod - 0.5f);
        StartCoroutine(SmoothZoom(camPos,finalPos));
    }

    IEnumerator SmoothZoom(Vector3 startPos, Vector3 finalPos){
        float percent = 0f;
        float rate = 0.04f;
        pauseVelocity = true;
        while (percent < 0.99f)
        {
            percent += rate;
            Vector3 newPos = Vector3.Lerp(startPos, finalPos, percent);
            Camera.main.transform.position = newPos;
            yield return new WaitForEndOfFrame();
        }
        pauseVelocity = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        //If collided object is an obstacle, take damage and shrink the bubble
        //ignore if god mode on.
        if (collider.gameObject.CompareTag(obstacleTagName) && GOD_MODE == false)
        {
            LoseGame();
        }
        else if (collider.gameObject.CompareTag(bubbleTagName))
        {
            increaseSize += 0.3f;
            float finalSize = Mathf.Pow(increaseSize, 0.7f);

            Debug.Log(finalSize);

            CollectBubble(finalSize);
        }
    }
}
