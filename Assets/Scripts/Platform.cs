using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private bool _isOnPlatform;

    void Update()
    {
        if (Input.GetKey(KeyCode.S) && _isOnPlatform)
        {
            transform.GetComponent<PlatformEffector2D>().rotationalOffset = 180;
        }
        else
        {
            transform.GetComponent<PlatformEffector2D>().rotationalOffset = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isOnPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isOnPlatform = false;
        }
    }
}
