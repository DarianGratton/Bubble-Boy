using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [Header("Configurations")]
    public float timeToLive = 5.0f;

    private float timeElapsed = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > timeToLive)
        {
            Destroy(gameObject);
        }
    }
}
