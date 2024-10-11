using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{

    public static LevelManager main;

    [Header("References")]
    [SerializeField] TextMeshProUGUI playerHealthUI;
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject gameOverScreen;

    public Transform startPoint;
    public Transform[] path;

    public int currency;
    public int playerHealth;
    public float speedScale = 1f;
    public Color fadeColor = Color.black;
    public AnimationCurve Curve = new AnimationCurve(new Keyframe(0, 1),
        new Keyframe(0.5f, 0.5f, -1.5f, -1.5f), new Keyframe(1, 0));
    public bool startFadedOut = false;

    private float alpha = 0f;
    private Texture2D texture;
    private int direction = 0;
    private float time = 0f;

    private void Awake()
    {
        main = this;
    }

    private void Start() {
        currency = 150;
        playerHealth = 5; // TODO: 10

        if (startFadedOut) alpha = 1f; else alpha = 0f;
        texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha));
        texture.Apply();
    }

    public void IncreaseCurrency(int amount) {
        currency += amount;
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

    public void playerHealthLoss()
    {
        playerHealth -= 1;

        if (playerHealth <= 0)
        {

            Debug.Log("YOU DIED");
            gameOverScreen.SetActive(true);

            alpha = 1f;
            time = 0f;
            direction = 1;

            StartCoroutine("Delay");
            
            //Svart screen långsamt
            // Videon spelas och sedan svart skärm med Game Over
            // några sekunder senare man tas tillbaka till StartMenu scene
        }
    }

    public void gameOverUI()
    {
        gameOverText.SetActive(true);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(7);
        gameOverUI();
    }

    private void OnGUI() {
        playerHealthUI.text = playerHealth.ToString();
    }
}
