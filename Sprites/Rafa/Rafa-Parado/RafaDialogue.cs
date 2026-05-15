using UnityEngine;

public class RafaDialogue : MonoBehaviour
{
    public GameObject botaoInteragir;

    private bool playerPerto = false;

    void Start()
    {
        botaoInteragir.SetActive(false);
    }

    void Update()
    {
        if (playerPerto && Input.GetKeyDown(KeyCode.E))
        {
            Conversar();
        }
    }

    void Conversar()
    {
        Debug.Log("Rafa iniciou conversa com Sofia");

        // aqui você chama diálogo futuramente
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerPerto = true;

            botaoInteragir.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerPerto = false;

            botaoInteragir.SetActive(false);
        }
    }
}