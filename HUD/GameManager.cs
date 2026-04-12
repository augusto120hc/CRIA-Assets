using UnityEngine;
using TMPro; // IMPORTANTE

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public TMP_Text scoreText; // Arraste o TextMeshPro da HUD aqui

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int valor)
    {
        score += valor;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = " " + score;
    }
}