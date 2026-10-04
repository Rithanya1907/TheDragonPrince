using UnityEngine;

public class SwordPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCombat combat = other.GetComponent<PlayerCombat>();

            if (combat != null)
            {
                combat.CollectSword();
            }

            Destroy(gameObject);

            Debug.Log("🗡️ Wooden Sword Collected!");
        }
    }
}