using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int speed;
    public GameObject deathEffect;
    public Weapon gun;
    public bool isFacingRight;

    private Rigidbody2D _rb;
    private GameObject _player;
    private bool _playerDetected;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_playerDetected)
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

    void Patrol()
    {
        Vector2 direction = (_player.transform.position - transform.position).normalized;
        _rb.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerDetected = false;
        }
    }
}
