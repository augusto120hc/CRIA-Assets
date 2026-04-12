using UnityEngine;

public class PCInteraction : MonoBehaviour
{
    [SerializeField] private PC_UIController pcUI;// Canvas do PC

    private bool playerPerto = false;

    void Update()
    {
        if (playerPerto && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)))
        {
            AbrirPC();
        }
    }

    void AbrirPC()
    {
        pcUI.AbrirPC(); // 🔥 chama o sistema correto
        Time.timeScale = 0f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerPerto = true;
            Debug.Log("Player perto do PC");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerPerto = false;
            Debug.Log("Player saiu do PC");
        }
    }
}