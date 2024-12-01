using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float lifeTime;
    [SerializeField]
    float speed;
    [SerializeField]
    GameObject impactEffect;

    public int damage;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        rb.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().health -= damage;
        }
        Instantiate(impactEffect, transform.position, new Quaternion());
        Destroy(gameObject);
    }
}
