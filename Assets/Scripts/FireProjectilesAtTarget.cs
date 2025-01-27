using System.Runtime.InteropServices.WindowsRuntime;
// using JetBrains.Rider.Unity.Editor;
using UnityEditor;
using UnityEngine;

public class FireProjectilesAtTarget : MonoBehaviour
{
    [Header("References")]
    public GameObject projectile;

    [Header("Configurations")]
    public float fireRate;
    public float projectileSpeed;
    public int maxNumberOfProjectiles = 3;

    private Transform actorTransform;
    private Transform targetTransform;
    private float timeElapsed = 0.0f;
    private int projectilesFired = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actorTransform = GetComponent<Transform>();
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform; // Will fail multiple game objects with player tags, I'm sorry
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (targetTransform == null)
        {
            Debug.LogWarning("Target Transform is null");
            return;
        }

        timeElapsed += Time.deltaTime;
        if (projectilesFired < maxNumberOfProjectiles && timeElapsed > fireRate)
        {
            SpawnProjectile();
            timeElapsed = 0.0f;
            projectilesFired++;
        }
    }

    private void SpawnProjectile()
    {
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform; // Will fail multiple game objects with player tags, I'm sorry
        GameObject newProjectile = Instantiate(projectile, actorTransform.position, actorTransform.rotation);
        Rigidbody newProjectileBody = newProjectile.GetComponent<Rigidbody>();
        if (newProjectileBody == null)
        {
            Debug.Log("Projectile doesn't have a Rigidbody");
            return;
        }
        Vector3 direction = targetTransform.transform.position - actorTransform.position;
        newProjectileBody.linearVelocity = Vector3.Normalize(direction) * projectileSpeed * Time.fixedDeltaTime;
    }
}
