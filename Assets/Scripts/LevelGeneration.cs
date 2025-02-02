using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelGeneration : MonoBehaviour
{
    [System.Serializable]
    public struct LevelLayer
    {
        public string name;
        public float endingHeight; // Height at which this level ends
        public AudioClip bgm;
        public List<GameObject> spawnObjects;
    }

    public Scoring scoring;
    public List<LevelLayer> levels;
    public AudioSource musicPlayer;
    public float minTimeBetweenSpawns;
    public float maxTimeBetweenSpawns;
    public Canvas winScreen;

    public Color startColor = Color.blue; // Sky starts as blue
    public Color midColor = new Color(1f, 0.6f, 0.2f); // Sunset orange
    public Color endColor = Color.black; // Dark/stars for space

    private float timeUntilNextSpawn;
    private int currentLayerInd;

    void Awake()
    {
        currentLayerInd = 0;
        timeUntilNextSpawn = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
    }

    private void Start()
    {
        // Set the starting music
        if (levels.Count > 0)
        {
            musicPlayer.clip = levels[0].bgm;
            musicPlayer.Play();
        }

        // Set initial sky color
        Camera.main.backgroundColor = startColor;
    }

    void Update()
    {
        CheckForLevelTransition();
        UpdateSpawningObjects();
        UpdateSkyColor(); // Dynamically update the sky color
    }

    private void CheckForLevelTransition()
    {
        if (currentLayerInd < levels.Count)
        {
            float heightThreshold = levels[currentLayerInd].endingHeight;
            float currentHeight = scoring.GetCurrentHeight();

            if (currentHeight >= heightThreshold)
            {
                TriggerNextLevel(++currentLayerInd);
            }
        }
    }

    private void UpdateSpawningObjects()
    {
        timeUntilNextSpawn -= Time.deltaTime;

        // Stop spawning obstacles after "Space"
        if (currentLayerInd >= levels.Count || levels[currentLayerInd].name == "Space")
        {
            return; // No more obstacles spawn
        }

        if (timeUntilNextSpawn <= 0 && levels[currentLayerInd].spawnObjects.Count > 0)
        {
            int rand = Random.Range(0, levels[currentLayerInd].spawnObjects.Count);
            Instantiate(levels[currentLayerInd].spawnObjects[rand], Vector3.zero, Quaternion.identity);
            timeUntilNextSpawn = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
        }
    }

    private void TriggerNextLevel(int layerInd)
    {
        if (layerInd >= levels.Count)
        {
            SceneManager.LoadScene("WinScene");
            return;
        }

        musicPlayer.clip = levels[layerInd].bgm;
        musicPlayer.Play();
        Debug.Log("Entering " + levels[layerInd].name);
    }

    private void UpdateSkyColor()
    {
        float currentHeight = scoring.GetCurrentHeight();

        if (currentHeight <= 250) // Forest to Mountains
        {
            Camera.main.backgroundColor = startColor;
        }
        else if (currentHeight <= 1050) // Mountains to Clouds
        {
            float t = (currentHeight - 250f) / (1050f - 250f); // Ensure 0 to 1 range
            Camera.main.backgroundColor = Color.Lerp(startColor, midColor, t);
        }
        else if (currentHeight <= 1912) // Clouds to Space
        {
            float t = (currentHeight - 1050f) / (1912f - 1050f); // Ensure 0 to 1 range
            Camera.main.backgroundColor = Color.Lerp(midColor, endColor, t);
        }
        else // Atmosphere to Space
        {
            Camera.main.backgroundColor = endColor;
        }
    }

    public LevelLayer GetCurrentLayer()
    {
        return levels[currentLayerInd];
    }
}
