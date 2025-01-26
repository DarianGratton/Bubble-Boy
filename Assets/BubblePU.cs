using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

//Update speed
//collision handle
public class BubblePU : MonoBehaviour
{
    public void AddSpeed(float newY){
        float speedMagnitude = 0.65f;
        gameObject.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, newY * speedMagnitude,0);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag != "BubblePickup") StartCoroutine(Pop());
    }

    IEnumerator Pop(){
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
