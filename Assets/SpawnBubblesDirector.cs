using System.Collections;
using UnityEngine;

public class SpawnBubblesDirector : MonoBehaviour
{
    [SerializeField] GameObject Bubble;
    [SerializeField] float minTime;
    [SerializeField] float maxTime;
    PlayerController controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = FindFirstObjectByType<PlayerController>();

        StartCoroutine(CountdownToSpawn());
    }

    IEnumerator CountdownToSpawn(){
        float time = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(time);
        SpawnBubble();
        
    }   
    void SpawnBubble(){
        StopCoroutine("CountdownToSpawn");
        GameObject newBubble = Instantiate(Bubble);
        // float
        float yVelocity = controller.size;
        Debug.Log(yVelocity);
        newBubble.GetComponent<BubblePU>().AddSpeed(yVelocity);
        StartCoroutine(CountdownToSpawn());
    }
}
