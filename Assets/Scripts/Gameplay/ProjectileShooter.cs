using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField]
    GameObject projectilePrefab = null;
    [SerializeField]
    float timeBetweenShots = 1;
    [SerializeField]
    float projectileSpeed = 10;

    private void Start()
    {
        StartCoroutine(ShootProjectiles());
    }

    IEnumerator ShootProjectiles()
    {
        while(true)
        {
            ShootNewProjectile();

            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    private void ShootNewProjectile()
    {
        var projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        var rigidbody = projectile.GetComponent<Rigidbody>();
        if(rigidbody == null)
        {
            Debug.LogError("Projectile prefab has no rigidbody");
            return;
        }

        rigidbody.velocity = transform.forward * projectileSpeed;

        var collider = projectile.GetComponent<Collider>();
        var myCollider = this.GetComponent<Collider>();
        if(collider != null && myCollider != null)
        {
            Physics.IgnoreCollision(collider, myCollider);
        }
    }
}
