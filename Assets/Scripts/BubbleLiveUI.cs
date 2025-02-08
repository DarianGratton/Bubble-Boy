using UnityEngine;

public class BubbleLiveUI : MonoBehaviour
{
    [SerializeField] private int showAtHealthOf = 3;
    public PlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (playerController == null)
        {
            Debug.LogError("PlayerController is not Assigned to BubbleLiveUI! Drag the Player GameObject into the Inspector.");

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.life < showAtHealthOf)
        {
            gameObject.SetActive(false);
        }
    }
}
