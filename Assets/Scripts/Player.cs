using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Weapon gun;
    public int maxHealth = 100;
    public GameObject deathEffect;
    public HealthBar healthBar;

    private int _health;

    private void Start()
    {
        _health = maxHealth;
        healthBar.SetHealth(_health);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            gun.Shoot();
        }
    }

    public void TakeDamage(int damage)
    {
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

        GetComponent<Player>().enabled = false;
        GetComponent<Move>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.enabled = false;
        }

        StartCoroutine(GameManager.instance.GameOver());
    }
}
