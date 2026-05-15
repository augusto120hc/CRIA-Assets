using UnityEngine;
using TMPro;

public class MuralBiblioteca : MonoBehaviour
{
    [Header("UI do Mural")]
    public GameObject painelMural;
    public TMP_Text textoMural;

    [Header("Texto")]
    [TextArea(3, 10)]
    public string mensagem;

    private void Start()
    {
        if (painelMural != null)
            painelMural.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
{
    Debug.Log("Entrou no trigger: " + other.name);

    if (other.CompareTag("Player"))
    {
        Debug.Log("PLAYER DETECTADO");

        painelMural.SetActive(true);
        textoMural.text = mensagem;
    }
}

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (painelMural != null)
                painelMural.SetActive(false);
        }
    }
}