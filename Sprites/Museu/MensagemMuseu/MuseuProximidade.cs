using UnityEngine;
using TMPro;

public class MuseuProximidade : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject painel;
    public TMP_Text textoPainel;

    [Header("Frase")]
    [TextArea(3, 6)]
    public string frase =
        "Um espaço de preservação daquilo que não pode ser automatizado, " +
        "memória, presença e percepção humana em estado absoluto.";

    private void Start()
    {
        painel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            painel.SetActive(true);
            textoPainel.text = frase;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            painel.SetActive(false);
        }
    }
}