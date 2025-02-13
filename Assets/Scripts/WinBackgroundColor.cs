using UnityEngine;

public class WinBackgroundColor : MonoBehaviour
{

    public Color startColor = Color.blue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Camera.main.backgroundColor = startColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
