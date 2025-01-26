using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Scoring : MonoBehaviour
{
    public float score;
    private float lastYScored;

    void Awake()
    {
        score = 0;
        lastYScored = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScoreBasedOnSize(float size)
    {
        float currentY = Camera.main.transform.position.y;
        float diff = currentY - lastYScored;
        score += diff * size;

        lastYScored = currentY;
    }
}
