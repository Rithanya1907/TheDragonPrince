using UnityEngine;

public class GuardManager : MonoBehaviour
{
    public GuardPatrol[] guards;
    public GateController gate;

    private int currentGuard = 0;

    void Start()
    {
        for (int i = 0; i < guards.Length; i++)
        {
            if (guards[i] != null)
            {
                guards[i].guardManager = this;
                guards[i].SetActiveGuard(false);
            }
        }

        if (guards.Length > 0 && guards[0] != null)
        {
            guards[0].SetActiveGuard(true);
        }

        Debug.Log("Guard Manager started. Guard 1 is active.");
    }

    public void GuardDefeated()
    {
        currentGuard++;

        Debug.Log("Guard defeated! Moving to guard " + (currentGuard + 1));

        if (currentGuard < guards.Length)
        {
            if (guards[currentGuard] != null)
            {
                guards[currentGuard].SetActiveGuard(true);

                Debug.Log(
                    "⚔️ Guard " + (currentGuard + 1) + " is NOW CHASING!"
                );
            }
        }
        else
        {
            Debug.Log("🏆 ALL GUARDS DEFEATED!");

            if (gate != null)
            {
                gate.OpenGate();
            }
        }
    }
}