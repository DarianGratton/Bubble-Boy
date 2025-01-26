using UnityEngine;

//apply gravity
//Keep within bounds of camera.
public class MovementHandler : MonoBehaviour
{
    [SerializeField] float gravityForce;
    [SerializeField] float viewportPadding;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        AddGravity();
        
    }

    void AddGravity(){
        rb.AddForce(new Vector3(0, -gravityForce, 0));
    }

    
}
