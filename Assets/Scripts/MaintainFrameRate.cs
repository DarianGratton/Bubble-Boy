using UnityEngine;

public class MaintainFrameRate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
    }

}
