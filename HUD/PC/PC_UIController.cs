using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

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

    [Header("Permissão")]
    public Toggle togglePermissao;

    public Button botaoFechar;

    [Header("Aviso")]
    public TMP_Text textoAviso;

    [Header("Cores Toggle")]
    public Image imagemToggle;

    public Color corDesmarcado = Color.white;

    public Color corMarcado = Color.green;

    [Header("Políticas de Uso")]
    public GameObject painelPoliticas;
    public TMP_Text textoPoliticas;
    public Button botaoFecharPoliticas;

    void Start()
    {
        telaPC.SetActive(false);

        AtualizarData();

        if(togglePermissao != null)
        {
            togglePermissao.isOn = false;

            AtualizarCorToggle();
        }

        if(textoAviso != null)
        {
            textoAviso.gameObject.SetActive(false);
        }
    }


    public void AtualizarCorToggle()
    {
        if(imagemToggle == null)
            return;

        if(togglePermissao.isOn)
        {
            imagemToggle.color = corMarcado;
        }
        else
        {
            imagemToggle.color = corDesmarcado;
        }
    }


    public void AbrirPC()
    {

        PerfilGlobal.instance.RegistrarPrimeiraInteracao();

        if (pcAberto) return;

        pcAberto = true;

        // SALVA HORÁRIO GLOBAL
        PerfilGlobal.instance.horarioInteracao =
            DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

        telaPC.SetActive(true);

        telaBoasVindasUI.SetActive(true);

        telaNoticias.SetActive(false);

        if (typewriter != null && textoBoasVindas != null)
        {
            typewriter.ShowText(
                textoBoasVindas,
                "Sistema inicializado em:\n\n<size=120%>Bem-vinda, Sofia...\n\nPrecisamos definir seu PERFIL</size>"
            );
        }
    }

    void AtualizarData()
    {
        DateTime agora = DateTime.Now;

        string dataFormatada = agora.ToString("dd/MM/yyyy HH:mm");

        textoData.text = dataFormatada;

        // =========================
        // REGISTRA SÓ UMA VEZ (GARANTIDO)
        // =========================
        if (!GogolPerfil.primeiraInteracaoRegistrada)
        {
            GogolPerfil.horarioPrimeiraInteracao = dataFormatada;
            GogolPerfil.primeiraInteracaoRegistrada = true;
        }
}
    public void FecharPC()
    {
        telaPC.SetActive(false);

        Time.timeScale = 1f;
    }

    public void Continuar()
        {
            // NÃO MARCOU O TOGGLE
            if(togglePermissao != null &&
            !togglePermissao.isOn)
            {
                if(textoAviso != null)
                {
                    textoAviso.gameObject.SetActive(true);

                    textoAviso.text =
                    "<color=#ff4d4d>Você precisa aceitar os termos.</color>";
                }

                return;
            }

            // ESCONDE AVISO
            if(textoAviso != null)
            {
                textoAviso.gameObject.SetActive(false);
            }

            telaBoasVindasUI.SetActive(false);

            telaNoticias.SetActive(true);
        }


public void AbrirPoliticas()
{
    Debug.Log("AbrirPoliticas chamado!");

    if (painelPoliticas == null)
    {
        Debug.LogError("painelPoliticas está NULL!");
        return;
    }

    painelPoliticas.SetActive(true);
}

//         public void AbrirPoliticas()
// {
//     if (painelPoliticas == null) return;

//     painelPoliticas.SetActive(true);

//     if (textoPoliticas != null)
//     {
//         textoPoliticas.text =
//             "POLÍTICAS DE USO\n\n" +
//             "1. Este sistema é apenas para uso interno.\n" +
//             "2. Não compartilhe credenciais.\n" +
//             "3. Todas as ações podem ser registradas.\n" +
//             "4. Uso indevido pode gerar bloqueio.";
//     }
// }

// TEXTO politica de uso
public void FecharPoliticas()
    {
        if (painelPoliticas == null) return;

        painelPoliticas.SetActive(false);
    }
}