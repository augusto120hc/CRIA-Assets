using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogoLucas : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialogueUI;

    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nomeText;

    [Header("Imagem Personagem")]
    public Image personagemImage;

    public Sprite spriteSofia;
    public Sprite spriteLucas;

    [Header("Botão")]
    public GameObject botaoInteragir;

    public Button btnProximo;

    public TextMeshProUGUI textoBtnProximo;

    // =========================
    private bool playerPerto = false;

    private int dialogoIndex = 0;

    private string[] dialogos;
    private string[] personagens;

    private bool dialogoAtivo = false;

    private bool digitando = false;

    [Header("Imagens finais")]
    public GameObject imagem1;
    public GameObject imagem2;
    public GameObject imagem3;
    public GameObject imagem4;

    [Header("Som")]
    public AudioSource audioSource;

    public AudioClip somMemoria;

    // =========================
    void Start()
    {
        if (dialogueUI != null)
            dialogueUI.SetActive(false);

        if (botaoInteragir != null)
            botaoInteragir.SetActive(false);

        if (btnProximo != null)
            btnProximo.gameObject.SetActive(false);

        btnProximo.onClick.AddListener(ProximoDialogo);

        textoBtnProximo.text = "Próximo";
    }

    // =========================
    void Update()
    {
        if (playerPerto)
        {
            AtualizarPosicaoBotao();

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!dialogoAtivo)
                {
                    IniciarDialogo();
                }
            }
        }
    }

    // =========================
    void AtualizarPosicaoBotao()
    {
        if (botaoInteragir == null) return;

        Vector3 pos = Camera.main.WorldToScreenPoint(
            transform.position + Vector3.up * 1.5f
        );

        botaoInteragir.transform.position = pos;
    }

    // =========================
    public void IniciarDialogo()
    {

        imagem1.SetActive(false);
        imagem2.SetActive(false);
        imagem3.SetActive(false);
        imagem4.SetActive(false);

        dialogoAtivo = true;

        dialogueUI.SetActive(true);

        dialogoIndex = 0;

        btnProximo.gameObject.SetActive(true);

        dialogos = new string[]
        {
            "Você é a Sofia, não é?",

"Sim... como sabe meu nome?",

"Os algoritmos mostram você o tempo inteiro.\nMesmo quem nunca falou com você acha que já conhece sua identidade.",

"Então as pessoas recebem uma versão minha antes mesmo de me conhecer de verdade...?",

"A cidade inteira funciona assim agora.\nPerfis substituíram presença.\nRepetição virou personalidade.",

"O Museu está incompleto.\nPartes da nossa cultura desapareceram sob excesso de conteúdo e distração.",

"As Memórias Subversivas precisam ser preservadas.\nElas representam cultura, identidade e essência.",

"Então vou recuperar cada peça esquecida.\nVou levar essas informações ao\n Museu!"
        };

        personagens = new string[]
        {
            "Lucas",
            "Sofia",
            "Lucas",
            "Sofia",
            "Lucas",
             "Lucas",
              "Lucas",
              "Sofia"
        };

        MostrarDialogo();
    }

    // =========================
    void MostrarDialogo()
    {
        if (dialogoIndex >= dialogos.Length)
        {
            FecharDialogo();
            return;
        }

        string personagem = personagens[dialogoIndex];

        nomeText.text = personagem;

        // =========================
        // TROCA IMAGEM
        if (personagemImage != null)
        {
            if (personagem == "Sofia")
            {
                personagemImage.sprite = spriteSofia;
            }
            else
            {
                personagemImage.sprite = spriteLucas;
            }

             if(dialogoIndex == dialogos.Length - 1)
            {
                imagem1.SetActive(true);
                imagem2.SetActive(true);
                imagem3.SetActive(true);
                imagem4.SetActive(true);

                 // toca som
            if(audioSource != null && somMemoria != null)
            {
                audioSource.PlayOneShot(somMemoria);
            }
            }
        }

        // =========================
        string texto = dialogos[dialogoIndex];

        // Sofia em ciano
        if (personagem == "Sofia")
        {
            texto = "<color=#59fff7>" + texto + "</color>";
        }

        if (TypewriterEffect.instance != null)
        {
            digitando = true;

            TypewriterEffect.instance.ShowText(dialogueText, texto);

            StartCoroutine(LiberarDigitacao(texto));
        }

        dialogoIndex++;
    }

    // =========================
    IEnumerator LiberarDigitacao(string texto)
    {
        float tempo = texto.Length * 0.03f;

        yield return new WaitForSecondsRealtime(tempo);

        digitando = false;
    }

    // =========================
    public void ProximoDialogo()
    {
        if (digitando)
            return;

        MostrarDialogo();
    }

    // =========================
    void FecharDialogo()
    {
        dialogoAtivo = false;

        if (dialogueUI != null)
            dialogueUI.SetActive(false);

        if (dialogueText != null)
            dialogueText.text = "";

        if (nomeText != null)
            nomeText.text = "";

        if (btnProximo != null)
            btnProximo.gameObject.SetActive(false);

        if (TypewriterEffect.instance != null)
        {
            TypewriterEffect.instance.StopTyping(dialogueText);
        }
    }

    // =========================
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerPerto = true;

            if (botaoInteragir != null)
                botaoInteragir.SetActive(true);
        }
    }

    // =========================
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerPerto = false;

            if (botaoInteragir != null)
                botaoInteragir.SetActive(false);

            FecharDialogo();
        }
    }
}