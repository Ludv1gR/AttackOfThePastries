using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    [SerializeField] private int maxHealth;
    [SerializeField] private int price;
    [SerializeField] private Transform spawnPosition;

    private int health;


    void Start()
    {
        health = maxHealth;
    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //explosion Instantiate before destroying
        Destroy(gameObject);
    }
}
