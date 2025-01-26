using UnityEngine;

//apply gravity
//Keep within bounds of camera.
public class PlayerGravity : MonoBehaviour
{
    [SerializeField] float gravityForce;

    Rigidbody rb;
    float pushForce;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pushForce = GetComponent<KeyboardInput>().GetStrafeSpeed() + 40f; // push force must be stronger than strafe force.
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
