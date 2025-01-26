// using System.Numerics;
using UnityEngine;

//keep player inside screen
//box colliders follow player

public class FollowAntBound : MonoBehaviour
{
    [SerializeField] Rigidbody left;
    [SerializeField] Rigidbody right;
    [SerializeField] Rigidbody down;
    [SerializeField] Rigidbody upper;
    [SerializeField] GetScreenBounds screen;
    [SerializeField] GameObject player;
    
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        if(!player) Debug.Log("PLAYER NOT FOUND!!");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLeft();
    }

    void UpdateLeft(){
        float x = screen.GetLeftBound();
        float y = player.transform.position.y;
        Vector3 pos = new Vector3(x, y, 0);
        UnityEngine.Debug.Log("left box" + pos);
        left.transform.position = pos;
        
    }
}
