using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;

    private Transform target;
    private float bulletMaxTravel = 7f;
    private float bulletTravel = 0f;

    
    public void SetTarget(Transform _target) {
        target = _target;
    }

    
    private void FixedUpdate() {
        bulletTravel += Time.deltaTime;

        if(bulletTravel >= bulletMaxTravel) {
            Destroy(gameObject);
        }

        if(!target) return;

        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        other.gameObject.GetComponent<HealthScript>().TakeDamage(bulletDamage);
        Destroy(gameObject);
    }
}
