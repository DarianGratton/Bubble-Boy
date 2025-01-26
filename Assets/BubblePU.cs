using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BubblePU : MonoBehaviour
{
    public void AddSpeed(float s){
        float currentYVelocity = 20f;
        
        // float newY = s + 
    }
    private void OnTriggerEnter(Collider other) {
        StartCoroutine(Pop());
    }

    IEnumerator Pop(){
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
