using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    int bounty;
    [SerializeField]
    float maxHealth;
    [SerializeField]
    Image healthBar;
    [SerializeField]
    Canvas healthBarCanvas;
    [SerializeField]
    GameObject deathEffect;
    [SerializeField]
    float minDistanceFromPlayer;
    [SerializeField]
    float maxDistanceFromPlayer;
    
    float health;
    NavMeshAgent agent;
    Transform player;

    void Start()
    {
        health = maxHealth;
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
            float distanceFromPlayer = Mathf.Abs(Vector3.Distance(transform.position, player.position));
            if (distanceFromPlayer >= maxDistanceFromPlayer)
                transform.position += (player.position - transform.position).normalized * (distanceFromPlayer - minDistanceFromPlayer);
        }
        healthBarCanvas.transform.LookAt(Camera.main.transform);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.fillAmount = health / maxHealth;
    }
}