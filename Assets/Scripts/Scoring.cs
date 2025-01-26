using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Scoring : MonoBehaviour
{
    public TMP_Text distanceText;
    public float totalDistanceToGo;
    public float totalUnitsToGo;

    private float score;
    private float remainingUnits;
    private float lastYScored;
    private float unitsToKm;

    void Awake()
    {
        remainingUnits = totalUnitsToGo;
        lastYScored = 0;
        unitsToKm = totalDistanceToGo / totalUnitsToGo;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDistance();
    }

    public void UpdateScoreBasedOnSize(float size)
    {
        float currentY = Camera.main.transform.position.y;
        float diff = currentY - lastYScored;
        score += diff * size;

        lastYScored = currentY;
    }

    private void UpdateDistance()
    {
        remainingUnits = totalUnitsToGo - Camera.main.transform.position.y;
        
        //Convert Unity units to km
        float remainingKm = remainingUnits * unitsToKm;

        distanceText.text = ((int)remainingKm).ToString() + "km";
    }

    //Gets progress to reaching the moon out of 100
    public float GetPercentProgress()
    {
        return (totalUnitsToGo - remainingUnits) / totalUnitsToGo * 100;
    }
}
