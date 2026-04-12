using UnityEngine;

public class RevistaTrigger : MonoBehaviour
{
    public GameObject painelUI; // arrasta o painel aqui

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            painelUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            painelUI.SetActive(false);
        }
    }
}