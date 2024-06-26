using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage;
    public Rigidbody2D rb;
    public GameObject hitEffect;

    void Start()
    {
        rb.velocity = transform.right * speed;

        StartCoroutine(DestroyBullet());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        Enemy enemy = collision.GetComponent<Enemy>();
        Player player = collision.GetComponent<Player>();
        if (enemy)
        {
            enemy.TakeDamage(damage);
        }
        else if (player)
        {
            player.TakeDamage(damage);
        }

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        */

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
        yield return new WaitForSeconds(1f);
        if (gameObject)
        {
            Destroy(gameObject);
        }
    }
}
