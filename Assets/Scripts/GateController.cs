using UnityEngine;

public class GateController : MonoBehaviour
{
    public float openHeight = 5f;
    public float openSpeed = 2f;

    private Vector3 closedPosition;
    private Vector3 openPosition;

    private bool isOpening = false;

    void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + Vector3.up * openHeight;
    }

    void Update()
    {
        if (isOpening)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                openPosition,
                openSpeed * Time.deltaTime
            );
        }
    }

    public void OpenGate()
    {
        if (!isOpening)
        {
            isOpening = true;
            Debug.Log("🏰 Gate is opening!");
        }
    }
}