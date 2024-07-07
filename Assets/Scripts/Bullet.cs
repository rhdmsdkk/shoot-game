using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject hitEffect;
    public int damage;

    void Start()
    {
        rb.velocity = transform.right * speed;

        StartCoroutine(DestroyBullet());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(damage);
            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(2f);
        if (gameObject)
        {
            Destroy(gameObject);
        }
    }
}
