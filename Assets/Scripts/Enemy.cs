using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int speed;
    public GameObject deathEffect;
    public Weapon gun;
    public bool isFacingRight;
    public HealthBar healthBar;

    private Rigidbody2D _rb;
    private bool _playerDetected;
    private float _lastFlipTime = 0f;
    private int _health;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _health = maxHealth;
        healthBar.SetHealth(_health);
    }

    private void Update()
    {
        if (_playerDetected)
        {
            gun.Shoot();
        } else
        {
            Patrol();
        }
    }

    public void TakeDamage(int damage)
    {
        if (!_playerDetected)
        {
            Flip();
        }

        _health -= damage;

        healthBar.SetHealth(_health);

        if (_health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Instantiate(deathEffect, transform.position, transform.rotation);

        GetComponent<Enemy>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.enabled = false;
        }

        StartCoroutine(GameManager.instance.GameOver());
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(new Vector3(0, 180, 0));
    }

    void Patrol()
    {
        Vector2 direction = isFacingRight ? (new(1, 0)) : new(-1, 0);
        if ((transform.position.x <= -6.5 || transform.position.x >= 6.5) && (Time.time - _lastFlipTime > 1.5f))
        {

            _lastFlipTime = Time.time;
            Flip();
            direction *= -1;

        }
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
