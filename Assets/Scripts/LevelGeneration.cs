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
        //music
        //list of obstacles
    }

    public Scoring scoring;
    public List<LevelLayer> levels;
    private int currentLayerInd;

    void Awake()
    {
        currentLayerInd = 0;
    }

    // Update is called once per frame
    void Update()
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

    private void TriggerNextLevel(int layerInd)
    {
        if (layerInd >= levels.Count)
        {
            return; //TRIGGER WIN GAME
        }

        //change music
        Debug.Log("Entering " + levels[layerInd].name);
    }

    public LevelLayer GetCurrentLayer()
    {
        return levels[currentLayerInd];
    }
}
