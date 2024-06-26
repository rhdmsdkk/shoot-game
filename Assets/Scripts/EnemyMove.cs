using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed;

    private GameObject _player;
    private Rigidbody2D _rb;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        _rb = GetComponent<Rigidbody2D>();
    }

    void Move()
    {
        Vector2 direction = (_player.transform.position - transform.position).normalized;
        _rb.velocity = direction * speed;
    }
}
