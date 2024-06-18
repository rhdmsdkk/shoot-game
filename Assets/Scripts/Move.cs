using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    public bool isFacingRight;

    private int _jumpCounter = 0;
    private float _horizontal;

    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && _jumpCounter > 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpPower);
            _jumpCounter--;
        }

        Flip();
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontal * speed, _rb.velocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && _horizontal < 0f || !isFacingRight && _horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            GetComponent<SpriteRenderer>().flipX = isFacingRight;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            _jumpCounter = 2;
        }
    }
}
