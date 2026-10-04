using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    public bool hasSword = false;

    public GameObject playerSword;
    public Transform swordPivot;

    public float attackRange = 3f;
    public float attackRadius = 1.7f;
    public int attackDamage = 1;

    public float swingAngle = 100f;
    public float swingDuration = 0.25f;

    private bool isAttacking = false;
    private Quaternion swordStartRotation;

    void Start()
    {
        if (playerSword != null)
        {
            playerSword.SetActive(false);
        }

        if (swordPivot != null)
        {
            swordStartRotation = swordPivot.localRotation;
        }
    }

    void Update()
    {
        if (hasSword && Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartCoroutine(SwordAttack());
        }
    }

    public void CollectSword()
    {
        hasSword = true;

        if (playerSword != null)
        {
            playerSword.SetActive(true);
        }

        Debug.Log("🗡️ Sword equipped!");
    }

    IEnumerator SwordAttack()
    {
        isAttacking = true;

        // Damage guards
        Attack();

        if (swordPivot != null)
        {
            Quaternion startRotation = swordStartRotation;

            // Swing around the handle/pivot
            Quaternion swingRotation =
                startRotation *
                Quaternion.Euler(0f, 0f, -swingAngle);

            float time = 0f;

            // Swing forward
            while (time < swingDuration)
            {
                time += Time.deltaTime;

                float progress = time / swingDuration;

                swordPivot.localRotation =
                    Quaternion.Slerp(
                        startRotation,
                        swingRotation,
                        progress
                    );

                yield return null;
            }

            // Return sword
            time = 0f;

            while (time < swingDuration)
            {
                time += Time.deltaTime;

                float progress = time / swingDuration;

                swordPivot.localRotation =
                    Quaternion.Slerp(
                        swingRotation,
                        startRotation,
                        progress
                    );

                yield return null;
            }

            swordPivot.localRotation = startRotation;
        }

        isAttacking = false;
    }

    void Attack()
    {
        Vector3 attackCenter =
            transform.position +
            transform.forward * (attackRange / 2f) +
            Vector3.up * 1f;

        Collider[] hits = Physics.OverlapSphere(
            attackCenter,
            attackRadius
        );

        bool hitGuard = false;

        foreach (Collider hit in hits)
        {
            GuardHealth guard = hit.GetComponent<GuardHealth>();

            if (guard != null)
            {
                guard.TakeDamage(attackDamage);
                hitGuard = true;

                Debug.Log("⚔️ Guard hit!");
            }
        }

        if (!hitGuard)
        {
            Debug.Log("⚔️ Sword swung - no guard hit.");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector3 attackCenter =
            transform.position +
            transform.forward * (attackRange / 2f) +
            Vector3.up * 1f;

        Gizmos.DrawWireSphere(attackCenter, attackRadius);
    }
}