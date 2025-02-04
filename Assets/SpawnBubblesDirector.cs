using System.Collections;
using UnityEngine;

public class SpawnBubblesDirector : MonoBehaviour
{
    [SerializeField] GameObject Bubble;
    [SerializeField] float minTime;
    [SerializeField] float maxTime;
    PlayerController controller;
    float currentTime;
    float nextSpawnTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = FindFirstObjectByType<PlayerController>();
    }

    void Update(){
        currentTime += Time.deltaTime;
        
        if(currentTime > nextSpawnTime){
            nextSpawnTime = currentTime + Random.Range(minTime, maxTime);
            SpawnBubble();
        }
        
        
    }   
    void SpawnBubble(){
        
        GameObject newBubble = Instantiate(Bubble);
        // float
        float yVelocity = controller.size * controller.sizeSpeedMod;
        Rigidbody bubble = newBubble.GetComponent<BubblePU>().GetComponent<Rigidbody>();

        bubble.linearVelocity = new Vector3(bubble.linearVelocity.x, yVelocity, bubble.linearVelocity.z);
    }
}
