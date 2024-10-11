using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CroissantBulletScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private float bulletMaxTime = 0.7f;
    
    private bool enemyHit;
    private float bulletTime = 0f;
    private Vector2 lastDirection;
    private float bulletMaxTravel = 7f;
    private float bulletTravel = 0f;

    private Transform target;
    private Transform towerPosition;


    public void SetTarget(Transform _target) {
        target = _target;
    }

    public void SetTowerPosition(Transform _towerPosition) {
        towerPosition = _towerPosition;
    }

    private void FixedUpdate() {
        if (target || enemyHit){
            if (enemyHit) {
                Vector2 backToTower = (towerPosition.position - transform.position).normalized;
                bulletTime += Time.deltaTime;
                
                if(bulletTime >= bulletMaxTime) {
                    rb.velocity = backToTower * bulletSpeed;
                } else {
                    rb.velocity = lastDirection * bulletSpeed;
                }

                if(Vector2.Distance(transform.position, towerPosition.position) <= 0.1f) {
                    Destroy(gameObject);
                }
            } else {
                Vector2 direction = (target.position - transform.position).normalized;

                rb.velocity = direction * bulletSpeed;
                lastDirection = direction;
            }
        } else {
            bulletTravel += Time.deltaTime;
            if(bulletTravel >= bulletMaxTravel) {
                Vector2 backToTower = (towerPosition.position - transform.position).normalized;
                rb.velocity = backToTower * bulletSpeed;

                if(Vector2.Distance(transform.position, towerPosition.position) <= 0.1f) {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        enemyHit = true;
        other.gameObject.GetComponent<HealthScript>().TakeDamage(bulletDamage);
    }
}
