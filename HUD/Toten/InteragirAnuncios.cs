using UnityEngine;

public class InteragirAnuncios : MonoBehaviour
{
    [Header("Referência do Painel")]
    public GameObject panelAnuncios;

    private void Start()
    {
        if (panelAnuncios != null)
            panelAnuncios.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (panelAnuncios != null)
                panelAnuncios.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (panelAnuncios != null)
                panelAnuncios.SetActive(false);
        }
    }
}