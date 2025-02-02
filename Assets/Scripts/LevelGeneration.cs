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
        public float endingHeight; // Now based on height
        public AudioClip bgm;
        public List<GameObject> spawnObjects;
    }

    public Scoring scoring;
    public List<LevelLayer> levels;
    public AudioSource musicPlayer;
    public float minTimeBetweenSpawns;
    public float maxTimeBetweenSpawns;
    public Canvas winScreen;

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
    }

    void Update()
    {
        CheckForLevelTransition();
        UpdateSpawningObjects();
    }

    private void CheckForLevelTransition()
    {
        if (currentLayerInd < levels.Count)
        {
            float heightThreshold = levels[currentLayerInd].endingHeight; // Now based on height
            float currentHeight = scoring.GetCurrentHeight(); // Uses actual height

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
            SceneManager.LoadScene("WinScene"); // Game ends when reaching "Space"
            return;
        }

        musicPlayer.clip = levels[layerInd].bgm;
        musicPlayer.Play();
        Debug.Log("Entering " + levels[layerInd].name);
    }

    public LevelLayer GetCurrentLayer()
    {
        return levels[currentLayerInd];
    }
}
