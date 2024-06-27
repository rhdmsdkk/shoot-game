using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    public float fireRate = 0.5f;

    private float lastShotTime;
    private bool canShoot = true;

    private void Update()
    {
        if (Time.time >= lastShotTime + fireRate)
        {
            canShoot = true;
        }
    }

    public void Shoot()
    {
        if (canShoot)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            lastShotTime = Time.time;
        }
        canShoot = false;
    }
}
