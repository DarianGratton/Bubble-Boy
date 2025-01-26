using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Canvas menuCanvas;

    void Awake()
    {
        Time.timeScale = 1.0f;
        menuCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPressPauseButton();
        }
    }
    public void OnPressPauseButton()
    {
        Time.timeScale = 0f;
        menuCanvas.enabled = true;
    }

    public void OnClickResume()
    {
        Time.timeScale = 1.0f;
        menuCanvas.enabled = false;
    }

    public void OnClickQuit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
