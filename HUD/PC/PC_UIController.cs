using UnityEngine;
using TMPro;
using System;

public class PC_UIController : MonoBehaviour
{
    public GameObject telaBoasVindasUI;
    public GameObject telaNoticias;

    public TMP_Text textoData;
    public TMP_Text textoBoasVindasTMP;

    public GameObject botaoInteragir; //Botao entrar no PC


    private bool pcAberto = false;

    public TypewriterEffect typewriter;
    public TMP_Text textoBoasVindas;

    void Start()
    {
        telaPC.SetActive(false); // 🔥 começa fechado

        AtualizarData();
    }

  


    void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                botaoInteragir.SetActive(true);
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                botaoInteragir.SetActive(false);
            }
        }

    public GameObject telaPC; // Canvas do PC

    public void FecharPC()
    {
        telaPC.SetActive(false);

        // opcional: resetar tempo
        Time.timeScale = 1f;
    }

    void AtualizarData()
    {
        DateTime agora = DateTime.Now;
        textoData.text = agora.ToString("dd/MM/yyyy HH:mm");
    }

    // public void AbrirPC()
    // {
    //     if (pcAberto) return;

    //     pcAberto = true;

    //     telaPC.SetActive(true); // 🔥 ATIVA O CANVAS PRIMEIRO

    //     telaBoasVindas.SetActive(true);
    //     telaNoticias.SetActive(false);
    // }

    public void AbrirPC()
        {
            // Debug.Log("Abrindo PC e chamando typewriter");
            if (pcAberto) return;

            pcAberto = true;

            telaPC.SetActive(true);
            telaBoasVindasUI.SetActive(true);
            telaNoticias.SetActive(false);

            typewriter.ShowText(textoBoasVindas, "Sistema inicializado em: \nBem-vinda, Sofia...\nAvalie estas informações para descobrir seu Perfil ");
        }

    public void Continuar()
    {
        telaBoasVindasUI.SetActive(false);
        telaNoticias.SetActive(true);
    }
}