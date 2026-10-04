using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI messageText;

    public void UpdateLives(int lives)
    {
        livesText.text = "Lives: " + lives;
    }

    public void ShowMessage(string message)
    {
        messageText.text = message;
    }
}