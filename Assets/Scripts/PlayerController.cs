using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip popSound;
    public int life = 3;
    private bool isInvisble = false;
    public float sizeSpeedMod;
    public float sizeScaleMod;
    public float sizeZoomMod;
    public float size;
    public float sizeLossOnHit;
    public string obstacleTagName;
    public string bubbleTagName;

    public Canvas loseScreen;
    float increaseSize;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {

        }

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
        if (rbCam)
            rbCam.linearVelocity = new Vector3(rbCam.linearVelocity.x, size * sizeSpeedMod, rbCam.linearVelocity.z);
    }

 
    //Helper function to correct camera zoom once size changes
    private void UpdateZoom()
    {
        Vector3 camPos = Camera.main.transform.position;
        Camera.main.transform.position = new Vector3(camPos.x, camPos.y, -size * sizeZoomMod - 0.5f);
    }



    private void OnTriggerEnter(Collider collider)
    {
        //If collided object is an obstacle, take damage and shrink the bubble
        if (collider.gameObject.CompareTag(obstacleTagName) && life > 1)
        {
            audioSource.PlayOneShot(popSound);
            GotHitFeedback();
            BubbleLoseLife();
        }

        else if (collider.gameObject.CompareTag(obstacleTagName) && life == 1)
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

    private void BubbleLoseLife()
    {
        life = life - 1;
    }

    private void hideBubble()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void showBubble()
    {
        GetComponent<MeshRenderer>().enabled = true;
    }
    private void GotHitFeedback()
    {
        isInvisble = true;
        for (int i = 0; i < 3; i++)
        {
            float delay = 0.5f * (i+ 1); //0.5s, 1.0s, 1.5s

            Invoke(nameof(hideBubble), delay); 
            Invoke(nameof(showBubble), delay + 0.15f);
        }
        
    }
}
