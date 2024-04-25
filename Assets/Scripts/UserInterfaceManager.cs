using TMPro;
using UnityEngine;

public class UserInterfaceManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private GameObject gameEndUI;
    [SerializeField]
    private TextMeshProUGUI endScoreText;

    public void UpdateTime(float dur)
    {
        float time = dur > 0 ? dur : 0f;

        timeText.text = $"{Mathf.FloorToInt(time / 60f)}:{Mathf.FloorToInt(time % 60):D2}:{Mathf.FloorToInt((time * 100) % 100):D2}";
    }

    public void UpdateScore(int score)
    {
        scoreText.text = $"${score}";
    }

    public void ShowGameEnd(int score)
    {
        endScoreText.text = $"${score}";
        gameEndUI.SetActive(true);
    }
}
