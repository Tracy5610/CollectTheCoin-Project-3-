using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    // Variables
    public NavMeshAgent enemy;
    public Transform player;

    [SerializeField] private float timer = 5;
    private float bulletTime;

    public GameObject enemyBullet;
    public Transform spawnPoint;
    public float bulletSpeed;

    void Start()
    {
        bulletTime = timer; // Initialize bulletTime to start shooting immediately
    }

    void Update()
    {
        enemy.SetDestination(player.position); // Follows player
        ShootAtPlayer();
    }

    void ShootAtPlayer()
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0) return;

        bulletTime = timer;

        // Calculate direction towards the player
        Vector3 directionToPlayer = (player.position - spawnPoint.position).normalized;

        // Instantiate bullet with proper rotation and position
        GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.position, Quaternion.LookRotation(directionToPlayer));
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();

        // Set bullet velocity to move straight forward
        bulletRig.velocity = directionToPlayer * bulletSpeed;

        Destroy(bulletObj, 5f);
    }

}