using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject deathEffect;
    [SerializeField]
    int bounty;
    [SerializeField]
    float minDistanceFromPlayer;
    [SerializeField]
    float maxDistanceFromPlayer;
    [HideInInspector]
    public Collision collision = null;
    public int health;
    NavMeshAgent agent;
    Transform player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerController>().transform;
    }
    void Update()
    {
        if (health <= 0)
        {
            GameController controller = FindObjectOfType<GameController>();
            controller.KillEnemy();
            controller.IncreaseScore(bounty, transform.position);
            Instantiate(deathEffect, transform.position, new Quaternion());
            FindObjectOfType<AudioManager>().OnEnemyDeath();
            Destroy(gameObject);
        }
        if (player != null && agent.isActiveAndEnabled)
        {
            agent.destination = player.position;
            TeleportToPlayer();
        }
    }

    void TeleportToPlayer()
    {
    //    float xCorrection = transform.position.x;
    //    float zCorrection = transform.position.z;
    //    float distanceFromPlayerX = Mathf.Abs(transform.position.x - player.position.x);
    //    float distanceFromPlayerZ = Mathf.Abs(transform.position.z - player.position.z);
    //    if (distanceFromPlayerX >= maxDistanceFromPlayer)
    //    {
    //        xCorrection = player.position.x + minDistanceFromPlayer;
    //    }
    //    if (distanceFromPlayerZ >= maxDistanceFromPlayer)
    //    {
    //        zCorrection = player.position.z + minDistanceFromPlayer;
    //    }

    //    if (distanceFromPlayerX >= maxDistanceFromPlayer || distanceFromPlayerZ >= maxDistanceFromPlayer)
    //        transform.position = new Vector3(xCorrection, transform.position.y, zCorrection);
        float distanceFromPlayer = Mathf.Abs(Vector3.Distance(transform.position, player.position));
        if (distanceFromPlayer >= maxDistanceFromPlayer)
            transform.position += (player.position - transform.position).normalized * (distanceFromPlayer - minDistanceFromPlayer);
    }
}