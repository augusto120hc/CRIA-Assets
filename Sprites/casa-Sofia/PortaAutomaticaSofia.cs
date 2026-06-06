using UnityEngine;

public class PortaAutomaticaSofia : MonoBehaviour
{
    public SpriteRenderer spritePorta;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spritePorta.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spritePorta.enabled = true;
        }
    }
}