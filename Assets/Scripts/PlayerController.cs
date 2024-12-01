using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float attackSpeed;
    [SerializeField]
    int damage;
    [SerializeField]
    LayerMask floor;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    Transform gunPivot;
    [SerializeField]
    Transform shootingPoint;
    [SerializeField]
    GameObject deathEffect;
    [SerializeField]
    Transform labyrinthStart;

    GameController gameController;
    Rigidbody rb;
    Camera mainCamera;
    Vector3 movementVector;
    Vector3 directionVector;
    Ray ray;
    RaycastHit hit;
    bool canShoot = true;
    bool doubleShot;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        gameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        if (gameController.gameFinished)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        movementVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.velocity = movementVector * moveSpeed;
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, floor))
        {
            Vector3 point = hit.point;
            point.y = gunPivot.position.y;
            directionVector = point - gunPivot.position;
            gunPivot.localRotation = Quaternion.LookRotation(directionVector);
        }
        if (Input.GetMouseButton(0) && canShoot)
        {
            StartCoroutine(Shoot());
            canShoot = false;
        }
    }

    IEnumerator Shoot()
    {
        GameObject bullet1;
        GameObject bullet2;
        if (doubleShot)
        {
            Vector3 offset = shootingPoint.right * 0.5f;
            bullet1 = Instantiate(bullet, shootingPoint.position + offset, gunPivot.rotation);
            bullet2 = Instantiate(bullet, shootingPoint.position - offset, gunPivot.rotation);
            bullet2.GetComponent<Bullet>().damage = damage;
        }
        else
        {
            bullet1 = Instantiate(bullet, shootingPoint.position, gunPivot.rotation);
        }
        bullet1.GetComponent<Bullet>().damage = damage;

        yield return new WaitForSeconds(1 / attackSpeed);
        canShoot = true;
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, new Quaternion());
        gameController.LoseGame();
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Portal")
        {
            transform.localScale /= 2;
            Vector3 labyrinthStart = LabyrinthSpawner.startPosition;
            transform.position = new Vector3(labyrinthStart.x, transform.position.y * transform.localScale.y, labyrinthStart.z);
        }
        else if (collision.gameObject.tag == "WinningTile")
        {
            gameController.GetSecretEnding();
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DoubleShotPU")
        {
            doubleShot = true;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "AttackSpeedPU")
        {
            attackSpeed *= 2;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "DamagePU")
        {
            damage *= 2;
            Destroy(other.gameObject);
        }
    }
}