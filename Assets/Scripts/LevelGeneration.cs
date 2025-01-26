using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGeneration : MonoBehaviour
{
    [System.Serializable]
    public struct LevelLayer
    {
        public string name;
        public float endingPercent;
        public AudioClip bgm;
        public List<GameObject> spawnObjects;
    }

    public Scoring scoring;
    public List<LevelLayer> levels;
    public AudioSource musicPlayer;
    public float minTimeBetweenSpawns;
    public float maxTimeBetweenSpawns;

    public float timeUntilNextSpawn;
    private int currentLayerInd;

    void Awake()
    {
        currentLayerInd = 0;
        timeUntilNextSpawn = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
    }

    private void Start()
    {
        //Set the starting music
        if (levels.Count > 0)
        {
            musicPlayer.clip = levels[0].bgm;
            musicPlayer.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckForLevelTransition();

        UpdateSpawningObjects();
    }

    private void CheckForLevelTransition()
    {
        if (currentLayerInd < levels.Count)
        {
            float percentThreshold = levels[currentLayerInd].endingPercent;
            if (scoring.GetPercentProgress() >= percentThreshold)
            {
                TriggerNextLevel(++currentLayerInd);
            }
        }
    }

    private void UpdateSpawningObjects()
    {
        timeUntilNextSpawn -= Time.deltaTime;
        
        if (timeUntilNextSpawn <= 0 && levels[currentLayerInd].spawnObjects.Count > 0)
        {
            int rand = (int)(Random.Range(0, levels[currentLayerInd].spawnObjects.Count));

            Instantiate(levels[currentLayerInd].spawnObjects[rand], Vector3.zero, Quaternion.identity);

            timeUntilNextSpawn = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
        }
    }

    private void TriggerNextLevel(int layerInd)
    {
        if (layerInd >= levels.Count)
        {
            return; //TRIGGER WIN GAME
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
