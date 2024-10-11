using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CrossaintBulletScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform towerPosition;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private float returnTime = 3f;
    
    private bool enemyHit;
    private Vector2 currentSpeed;
    private Vector2 backToTower;
    private float deacceleration = 0.2f;


    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target) return;

        if (enemyHit)
        {
            backToTower = (towerPosition.position - transform.position).normalized;

            rb.velocity = new Vector2(currentSpeed.x - deacceleration * Time.deltaTime, currentSpeed.y - deacceleration * Time.deltaTime);
        }
        else
        {
            Vector2 direction = (target.position - transform.position).normalized;

            rb.velocity = direction * bulletSpeed;
            currentSpeed = rb.velocity;
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<HealthScript>().TakeDamage(bulletDamage);
        enemyHit = true;
    }
}
