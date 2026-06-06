using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool todasMissoesConcluidas = false;

    public bool memoriasSubversivasDevolvidas = false;

    public bool iniciarFinalAoEntrarNaCasa = false;

    public int score = 0;

    public TMP_Text scoreText;

    [Header("Objetos Memória")]
    public bool coletouObjeto1;
    public bool coletouObjeto2;
    public bool coletouObjeto3;
    public bool coletouObjeto4;

    [Header("Objetos Expostos")]
    public bool objeto1Exposto;
    public bool objeto2Exposto;
    public bool objeto3Exposto;
    public bool objeto4Exposto;

    private void Awake()
    {
        // Debug.Log("GameManager Awake chamado!");

        if (instance == null)
        {
            // Debug.Log("Primeiro GameManager criado.");

            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Debug.Log("GameManager DUPLICADO destruído.");

            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreUI();
        // todasMissoesConcluidas = true;
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