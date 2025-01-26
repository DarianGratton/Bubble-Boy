using UnityEngine;

public class RandomScale : MonoBehaviour
{

    [Header("References")]
    public Transform tf;
    public Rigidbody body;

    [Header("Configurations")]
    public float minScale;
    public float maxScale;
    public bool changeMass = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Spawn with random size (Mass will change depending on size)
        float randomXY = Random.Range(minScale, maxScale);
        Vector3 randomScale = new Vector3(randomXY, randomXY, randomXY);
        tf.transform.localScale = randomScale;

        // Update mass of object
        if (changeMass && body != null)
            body.mass *= randomXY;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
