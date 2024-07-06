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
    private bool _bulletDetected;
    private bool _isJumping;
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
        if (_playerDetected && !_isJumping)
        {
            gun.Shoot();
        } 
        else
        {
            Patrol();
        }

        if (_bulletDetected)
        {
            float choose = Random.Range(0f, 1f);
            if (choose < 0.2f)
            {
                _isJumping = true;
                Jump();
            }
            _bulletDetected = false;
        }

        if (transform.position.y < -4.5)
        {
            Die();
        }
    }

    void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, 10);
        _isJumping = false;
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
        GetComponentInChildren<Canvas>().enabled = false;

        foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.enabled = false;
        }

        GameManager.instance.enemyCount--;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(new Vector3(0, 180, 0));
    }

    void Patrol()
    {
        Vector2 direction = isFacingRight ? (new(1, 0)) : new(-1, 0);
        float lowerX;
        float upperX;

        if (transform.position.y > 1.6)
        {
            lowerX = -5f;
            upperX = -1.7f;
        } 
        else if (transform.position.y > -1)
        {
            lowerX = 3.3f;
            upperX = 6f;
        }
        else
        {
            lowerX = -6.5f;
            upperX = 6.5f;
        }

        if ((transform.position.x <= lowerX || transform.position.x >= upperX) && (Time.time - _lastFlipTime > 1.5f))
        {

            _lastFlipTime = Time.time;
            Flip();
            direction *= -1;

        }

        _rb.velocity = new Vector2(direction.x * speed, _rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerDetected = true;
        }

        if (collision.CompareTag("Bullet"))
        {
            _bulletDetected = true;
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
