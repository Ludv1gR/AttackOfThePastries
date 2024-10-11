using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{

    public static LevelManager main;

    [Header("References")]
    [SerializeField] TextMeshProUGUI playerHealthUI;

    public Transform startPoint;
    public Transform[] path;

    public int currency;
    public int playerHealth;

    private void Awake()
    {
        main = this;
    }

    private void Start() {
        currency = 100;
        playerHealth = 5; // TODO: 10
    }

    public void IncreaseCurrency(int amount) {
        currency += amount;
    }

    public void playerHealthLoss() {
        playerHealth -= 1;

        if(playerHealth <= 0) {

            Debug.Log("YOU DIED");
            //Svart screen långsamt
            // Videon spelas och sedan svart skärm med Game Over
            // några sekunder senare man tas tillbaka till StartMenu scene
        }
    }

    public bool SpendCurrency(int amount) {
        if(amount <= currency) {
            currency -= amount;
            return true;
        } else {
            Debug.Log("You do not have neough to purchase this item!");
            return false;
        }
    }

    private void OnGUI() {
        playerHealthUI.text = playerHealth.ToString();
    }
}
