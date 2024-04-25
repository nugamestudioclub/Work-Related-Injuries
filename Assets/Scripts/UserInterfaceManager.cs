using TMPro;
using UnityEngine;

public class UserInterfaceManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    public void UpdateTime(float dur)
    {
        timeText.text = $"{Mathf.FloorToInt(dur / 60f)}:{Mathf.FloorToInt(dur % 60):D2}:{Mathf.FloorToInt((dur * 100) % 100):D2}";
    }

    public void UpdateScore(int score)
    {
        scoreText.text = $"${score}";
    }
}
