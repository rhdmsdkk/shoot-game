using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public GameObject deathEffect;
    public Weapon gun;
    public bool isFacingRight;

    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (transform.position.y <= _player.transform.position.y + 2 && transform.position.y >= _player.transform.position.y - 2)
        {
            gun.Shoot();
        }

        Flip();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void Flip()
    {
        if ((transform.position.x < _player.transform.position.x && !isFacingRight) || (transform.position.x > _player.transform.position.x && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }
}
