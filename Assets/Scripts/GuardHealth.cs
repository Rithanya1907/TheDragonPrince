using UnityEngine;

public class GuardHealth : MonoBehaviour
{
    public int maxHealth = 1;
    private int currentHealth;

    private GuardPatrol guardPatrol;

    void Start()
    {
        currentHealth = maxHealth;
        guardPatrol = GetComponent<GuardPatrol>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("Guard hit! Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Guard defeated!");

        // Tell GuardManager that this guard has been defeated
        if (guardPatrol != null)
        {
            guardPatrol.GuardDied();
        }

        Destroy(gameObject);
    }
}