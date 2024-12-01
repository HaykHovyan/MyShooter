using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject deathEffect;
    NavMeshAgent agent;
    Transform player;
    public int health;

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
            Instantiate(deathEffect, transform.position, new Quaternion());
            Destroy(gameObject);
        }    
        if (player != null && agent.isActiveAndEnabled)
        {
            agent.destination = player.position;
        }
    }
}
