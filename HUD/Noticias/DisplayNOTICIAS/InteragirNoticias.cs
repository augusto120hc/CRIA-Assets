using UnityEngine;

public class InteragirNoticias : MonoBehaviour
{
    [Header("Referência do Display")]
    public NewsOnlineDisplay display;

    private bool playerPerto = false;

    private void Update()
    {
        if (playerPerto && Input.GetKeyDown(KeyCode.E))
        {
            if (display != null)
            {
                display.AbrirPainel();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerPerto = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerPerto = false;

            if (display != null)
                display.FecharPainel();
        }
    }
}