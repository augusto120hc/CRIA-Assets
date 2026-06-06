using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneFinal : MonoBehaviour
{
    [Header("UI")]
    public GameObject painelFinal;
    public TMP_Text nomeText;

    public Image fundoFinal;
    public Sprite fundoSpriteFinal;

    public TMP_Text dialogoText;
    public Image personagemImage;

    [Header("Sprites")]
    public Sprite sofiaSprite;
    public Sprite toySprite;

    [Header("Player")]
    public PlayerMovement movimentoPlayer;

    [Header("Música")]
    public AudioSource musicaSource;
    public AudioClip musicaFinal;
    public AudioSource musicaFase02;

    [Header("Fade")]
    public Image fadePreto;

    private bool cutsceneRodando = false;

    public void IniciarFinal()
    {
        if (cutsceneRodando) return;

        cutsceneRodando = true;
        StartCoroutine(RotinaFinalComFade());
    }

    IEnumerator RotinaFinalComFade()
    {
        movimentoPlayer = FindObjectOfType<PlayerMovement>();

        if (movimentoPlayer != null)
        {
            movimentoPlayer.podeMover = false;
            movimentoPlayer.PararImediatamente();
        }

        // 🔇 PARA MÚSICA DA FASE 02
        if (musicaFase02 != null)
            musicaFase02.Stop();

// prepara fade
if (fadePreto != null)
{
    // 🌑 garante que começa transparente
    fadePreto.color = new Color(0, 0, 0, 0);

    // FADE IN (tela escurece)
    yield return StartCoroutine(FadeParaPreto(1.5f));

    // ⏸ pausa dramática opcional
    yield return new WaitForSeconds(0.3f);

    // 🌤 FADE OUT (tela volta)
    yield return StartCoroutine(FadeDePreto(1.5f));
}

IEnumerator FadeDePreto(float duracao)
{
    Color c = fadePreto.color;
    float t = 0f;

    while (t < duracao)
    {
        t += Time.deltaTime;

        float alpha = Mathf.Lerp(1f, 0f, t / duracao);
        fadePreto.color = new Color(c.r, c.g, c.b, alpha);

        yield return null;
    }

    fadePreto.color = new Color(c.r, c.g, c.b, 0f);
}

        // ativa UI
        painelFinal.SetActive(true);

        // 🎶 música final entra
        if (musicaSource != null && musicaFinal != null)
        {
            musicaSource.Stop();
            musicaSource.clip = musicaFinal;
            musicaSource.Play();
        }

        yield return StartCoroutine(RotinaFinal());
    }

    void Start()
    {
        painelFinal.SetActive(false);

        if (fundoFinal != null && fundoSpriteFinal != null)
        {
            fundoFinal.sprite = fundoSpriteFinal;
            fundoFinal.gameObject.SetActive(true);
        }

        if (GameManager.instance != null &&
            GameManager.instance.iniciarFinalAoEntrarNaCasa)
        {
            GameManager.instance.iniciarFinalAoEntrarNaCasa = false;
            IniciarFinal();
        }
    }

    IEnumerator FadeParaPreto(float duracao)
    {
        Color c = fadePreto.color;
        float t = 0f;

        while (t < duracao)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, t / duracao);
            fadePreto.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        fadePreto.color = new Color(c.r, c.g, c.b, 1f);
    }

    IEnumerator RotinaFinal()
    {
        yield return MostrarFala("SOFIA", sofiaSprite, "Toy... chegamos ao fim da jornada.");
        yield return MostrarFala("TOY", toySprite, "Talvez ao fim da jornada... ou ao começo da verdade.");
        yield return MostrarFala("SOFIA", sofiaSprite, "Durante anos eu achei que estava escolhendo meu próprio caminho.");
        yield return MostrarFala("SOFIA", sofiaSprite, "Achei que minhas opiniões, meus gostos e até meus sonhos eram realmente meus.");
        yield return MostrarFala("TOY", toySprite, "Mas o Algoritmo observava cada passo.");
        yield return MostrarFala("TOY", toySprite, "Cada clique. Cada curtida. Cada segundo de atenção.");
        yield return MostrarFala("SOFIA", sofiaSprite, "E sem perceber... fui me fechando.");
        yield return MostrarFala("SOFIA", sofiaSprite, "Via apenas o que ele queria que eu visse.");
        yield return MostrarFala("TOY", toySprite, "Uma prisão sem grades.");
        yield return MostrarFala("SOFIA", sofiaSprite, "O mais triste é que eu nem sabia que estava presa.");
        yield return MostrarFala("SOFIA", sofiaSprite, "Chamava aquilo de liberdade.");
        yield return MostrarFala("TOY", toySprite, "Muitas pessoas ainda chamam.");
        yield return MostrarFala("SOFIA", sofiaSprite, "Talvez viver de verdade seja justamente isso...");
        yield return MostrarFala("SOFIA", sofiaSprite, "Questionar aquilo que parece confortável.");
        yield return MostrarFala("TOY", toySprite, "E continuar procurando o que existe além da bolha.");
        yield return MostrarFala("SOFIA", sofiaSprite, "Obrigada por caminhar comigo, Toy.");
        yield return MostrarFala("TOY", toySprite, "Sempre estive aqui.");
        yield return MostrarFala("TOY", toySprite, "Agora a escolha é sua, Sofia.");
        yield return MostrarFala("SOFIA", sofiaSprite, "Então... desta vez vou abrir a porta por mim mesma.");

        Debug.Log("FIM DO JOGO");
    }

    IEnumerator MostrarFala(string nome, Sprite imagem, string fala)
    {
        nomeText.text = nome;
        personagemImage.sprite = imagem;

        if (TypewriterEffect.instance != null)
        {
            TypewriterEffect.instance.ShowText(dialogoText, fala);

            float tempo = fala.Length * TypewriterEffect.instance.delay + 4f;
            yield return new WaitForSeconds(tempo);

            TypewriterEffect.instance.StopTyping(dialogoText);
        }
        else
        {
            dialogoText.text = fala;
            yield return new WaitForSeconds(Mathf.Max(3f, fala.Length * 0.05f));
        }
    }
}