using UnityEngine;
using TMPro;
using System;

public class PC_UIController : MonoBehaviour
{
    public GameObject telaPC;
    public GameObject telaBoasVindasUI;
    public GameObject telaNoticias;

    public TMP_Text textoData;
    public TMP_Text textoBoasVindas;

    public GameObject botaoInteragir;

    private bool pcAberto = false;

    public PCTypewriter typewriter;

    void Start()
    {
        telaPC.SetActive(false);
        AtualizarData();
    }

    public void AbrirPC()
    {
        if (pcAberto) return;

        pcAberto = true;

        telaPC.SetActive(true);
        telaBoasVindasUI.SetActive(true);
        telaNoticias.SetActive(false);

        // 💡 segurança
        if (typewriter != null && textoBoasVindas != null)
        {
            typewriter.ShowText(
                textoBoasVindas,
                "Sistema inicializado em:\n\nBem-vinda, Sofia...\n\nPrecisamos definir seu PERFIL"
            );
        }
        else
        {
            Debug.LogWarning("Typewriter ou texto não atribuídos no Inspector!");
        }
    }

    void AtualizarData()
    {
        DateTime agora = DateTime.Now;
        textoData.text = agora.ToString("dd/MM/yyyy HH:mm");
    }

    public void FecharPC()
    {
        telaPC.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Continuar()
    {
        telaBoasVindasUI.SetActive(false);
        telaNoticias.SetActive(true);
    }
}