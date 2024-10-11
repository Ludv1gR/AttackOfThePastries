using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;

    private void Start() {
        target = LevelManager.main.path[pathIndex];
    }


    private void Update() {
        if(Vector2.Distance(target.position, transform.position) <= 0.1f) {
            pathIndex++;

            if(pathIndex == LevelManager.main.path.Length) {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            } else {
                target = LevelManager.main.path[pathIndex];
            }
        }

        anim.SetFloat("VerticalSpeed", rb.velocity.y);
        anim.SetFloat("HorizontalSpeed", rb.velocity.x);
    }

    private void FixedUpdate() {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * moveSpeed;
    }
}
