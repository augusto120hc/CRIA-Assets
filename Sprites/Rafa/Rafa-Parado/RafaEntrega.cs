using UnityEngine;

public class RafaEntrega : MonoBehaviour
{
    private bool playerPerto = false;

    void Update()
    {
        if(playerPerto && Input.GetKeyDown(KeyCode.E))
        {
            EntregarFragmentos();
        }
    }

    void EntregarFragmentos()
    {
        if(FragmentosManager.instance.temClareza)
        {
            Debug.Log("Clareza entregue");
        }

        if(FragmentosManager.instance.temVontade)
        {
            Debug.Log("Vontade entregue");
        }

        if(FragmentosManager.instance.temEssencia)
        {
            Debug.Log("Essência entregue");
        }

        if(
            FragmentosManager.instance.temClareza &&
            FragmentosManager.instance.temVontade &&
            FragmentosManager.instance.temEssencia
        )
        {
            Debug.Log("TODOS OS FRAGMENTOS ENTREGUES");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerPerto = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerPerto = false;
        }
    }
}