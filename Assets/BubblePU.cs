using System.Collections;
using UnityEngine;

public class BubblePU : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        StartCoroutine(Pop());
    }

    IEnumerator Pop(){
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
