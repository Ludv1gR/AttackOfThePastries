using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int currencyDrop = 30;

    private bool isDestroyed = false;

    public void TakeDamage(int dmg) {
        hitPoints -= dmg;

        if(hitPoints <= 0 && !isDestroyed) {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyDrop);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }

}
