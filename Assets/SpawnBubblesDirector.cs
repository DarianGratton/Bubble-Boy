using System.Collections;
using UnityEngine;

public class SpawnBubblesDirector : MonoBehaviour
{
    [SerializeField] GameObject Bubble;
    [SerializeField] float minTime;
    [SerializeField] float maxTime;
    [SerializeField] 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(CountdownToSpawn(1f));
    }

    IEnumerator CountdownToSpawn(float time){
        yield return new WaitForSeconds(time);
        SpawnBubble();
    }   
    void SpawnBubble(){
        GameObject newBubble = Instantiate(Bubble);
        // float
        // newBubble.GetComponent<BubblePU>().AddSpeed();
        StartCoroutine(CountdownToSpawn(Random.Range(minTime, maxTime)));
    }
}
