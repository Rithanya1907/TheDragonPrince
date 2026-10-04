using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 5;
    public float damageCooldown = 1.5f;

    private int currentLives;
    private float lastDamageTime = -10f;
    private bool gameOver = false;

    public UIManager uiManager;

    void Start()
    {
        currentLives = maxLives;

        uiManager.UpdateLives(currentLives);
        uiManager.ShowMessage("");
    }

    public void LoseLife()
    {
        // Don't take damage after Game Over
        if (gameOver)
        {
            return;
        }

        // Damage cooldown
        if (Time.time < lastDamageTime + damageCooldown)
        {
            return;
        }

        lastDamageTime = Time.time;

        currentLives--;

        // Make sure lives never go below zero
        currentLives = Mathf.Max(currentLives, 0);

        uiManager.UpdateLives(currentLives);

        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        gameOver = true;

        uiManager.ShowMessage("GAME OVER!");

        // Stop the game
        Time.timeScale = 0f;
    }

    public int GetCurrentLives()
    {
        return currentLives;
    }
}