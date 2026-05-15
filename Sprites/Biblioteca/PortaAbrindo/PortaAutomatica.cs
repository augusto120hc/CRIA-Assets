using UnityEngine;

public class PortaAutomatica : MonoBehaviour
{
    public Animator animatorPorta;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            animatorPorta.SetBool("Aberta", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            animatorPorta.SetBool("Aberta", false);
        }
    }
}