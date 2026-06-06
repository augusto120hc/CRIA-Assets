using UnityEngine;
using TMPro;

public class ExpoVazio : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject painel;
    public TMP_Text textoPainel;

    [Header("Frase")]
    [TextArea(3, 6)]
    public string frase =
        "Algo muito importante deveria estar aqui. A memória subversiva também precisa ser preservada.";

    private void Start()
    {
        if (painel != null)
        {
            painel.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (painel != null)
        {
            painel.SetActive(true);
        }

        if (textoPainel != null)
        {
            textoPainel.text = frase;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (painel != null)
        {
            painel.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        painel = null;
    }
}