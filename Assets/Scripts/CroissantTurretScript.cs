using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class CroissantTurretScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private AudioClip placementSound; // Sound to play when this specific turret is placed
    [SerializeField] private AudioClip shootSound;     // Sound to play when this turret shoots

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 3.2f;
    [SerializeField] private float bps = 0.7f; // bullets per second

    private AudioSource audioSource;
    private Transform target;
    private float timeUntilFire;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = placementSound;


        if (placementSound != null)
        {
            audioSource.Play();
        }
    }

    void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        // Play the shoot sound if it exists
        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        CroissantBulletScript bulletScript = bulletObj.GetComponent<CroissantBulletScript>();
        bulletScript.SetTarget(target);
        bulletScript.SetTowerPosition(transform);
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;

        anim.SetFloat("TurretRotation", angle);
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}