using UnityEngine;
using TMPro;

public class PortaPredio : MonoBehaviour
{
    [Header("UI")]
    public GameObject painelMensagem;

    public TMP_Text mensagemText;

    private bool playerDentro = false;

    void Start()
    {
        if(painelMensagem != null)
        {
            painelMensagem.SetActive(false);
        }
    }

    void Update()
    {
        if(CoinManager.instance == null)
            return;

        if(!playerDentro)
            return;

        // NÃO TEM LIKES
        if(CoinManager.instance.likes < 5000)
        {
            if(painelMensagem != null)
            {
                painelMensagem.SetActive(true);
            }

            if(mensagemText != null)
            {
                mensagemText.text =
                "Você não tem likes suficientes pra entrar aqui.";
            }
        }
        // TEM LIKES
        else
        {
            if(painelMensagem != null)
            {
                painelMensagem.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerDentro = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerDentro = false;

            if(painelMensagem != null)
            {
                painelMensagem.SetActive(false);
            }
        }
    }
}