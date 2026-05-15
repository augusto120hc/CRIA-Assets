using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;


public class AnciaoInicio : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialogueUI;

    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nomeText;

    [Header("Imagem Personagem")]
    public Image personagemImage;

    public Sprite spriteSofia;
    public Sprite spriteAnciao;

    [Header("Botões")]
    public GameObject botaoInteragir;

    public Button btnSim;
    public Button btnNao;
    public Button btnProximo;

    public TextMeshProUGUI textoBtnSim;
    public TextMeshProUGUI textoBtnNao;
    public TextMeshProUGUI textoBtnProximo;

    // =========================
    private bool playerPerto = false;

    private int dialogoIndex = 0;

    private string[] dialogos;
    private string[] personagens;

    private bool esperandoEscolha = false;
    private bool dialogoAtivo = false;

    private bool digitando = false;

  

    // =========================
    void Start()
    {
        // UI começa desligada
        if (dialogueUI != null)
            dialogueUI.SetActive(false);

        if (botaoInteragir != null)
            botaoInteragir.SetActive(false);

        if (btnSim != null)
            btnSim.gameObject.SetActive(false);

        if (btnNao != null)
            btnNao.gameObject.SetActive(false);

        if (btnProximo != null)
            btnProximo.gameObject.SetActive(false);

        // Eventos
        btnSim.onClick.AddListener(EscolheuSim);
        btnNao.onClick.AddListener(EscolheuNao);
        btnProximo.onClick.AddListener(ProximoDialogo);

        textoBtnSim.text = "SIM";
        textoBtnNao.text = "NÃO";
        textoBtnProximo.text = "PRÓXIMO";
    }

    // =========================
    void Update()
    {
        if (playerPerto)
        {
            AtualizarPosicaoBotao();

            // tecla E
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!dialogoAtivo)
                {
                    FalarComSofia();
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
    public void FalarComSofia()
    {
        dialogoAtivo = true;

    

        dialogueUI.SetActive(true);

        dialogoIndex = 0;

        esperandoEscolha = false;

        btnProximo.gameObject.SetActive(true);

        dialogos = new string[]
        {
            "Oi Ancião Das Pequenas Histórias!",

            "Oi pequena Sofia.\nVocê interagiu com algum Dispositivo ultimamente?"
        };

        personagens = new string[]
        {
            "Sofia",
            "Ancião"
        };

        MostrarDialogo();
    }

    // =========================
    void MostrarDialogo()
    {
        if (btnSim != null)
            btnSim.gameObject.SetActive(false);

        if (btnNao != null)
            btnNao.gameObject.SetActive(false);

        if (dialogoIndex >= dialogos.Length)
        {
            MostrarEscolha();
            return;
        }

        string personagem = personagens[dialogoIndex];

        nomeText.text = personagem;

        // =========================
        // MUDA IMAGEM
        if (personagemImage != null)
        {
            if (personagem == "Sofia")
            {
                personagemImage.sprite = spriteSofia;
            }
            else
            {
                personagemImage.sprite = spriteAnciao;
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
            if (esperandoEscolha)
                return;

            if (digitando)
                return;

            // acabou todos os diálogos
            if (dialogoIndex >= dialogos.Length)
            {
                // se ainda NÃO mostrou escolha
                if (!esperandoEscolha)
                {
                    // diálogo inicial
                    if (dialogos.Length == 2)
                    {
                        MostrarEscolha();
                    }
                    else
                    {
                        FecharDialogo();
                    }
                }

                return;
            }

            MostrarDialogo();
        }

    // =========================
    void MostrarEscolha()
    {
        esperandoEscolha = true;

        btnProximo.gameObject.SetActive(false);

        btnSim.gameObject.SetActive(true);
        btnNao.gameObject.SetActive(true);

        textoBtnSim.text = "SIM";
        textoBtnNao.text = "NÃO";
    }

    // =========================
    void EscolheuSim()
    {
       

        esperandoEscolha = false;

        btnSim.gameObject.SetActive(false);
        btnNao.gameObject.SetActive(false);

        dialogos = new string[]
        {
            "Você precisa saber...\nsuas escolhas irão definir sua imagem. Você pode não ser você",

            "O que quer dizer com isso?",

            "Estão construindo uma grande estatueta sua na praça central. ",

            "Você inventa cada coisa 😊"
        };

        personagens = new string[]
        {
            "Ancião",
            "Sofia",
            "Ancião",
            "Sofia"
        };

        dialogoIndex = 0;

        btnProximo.gameObject.SetActive(true);

        MostrarDialogo();
    }

    // =========================
    void EscolheuNao()
    {


        esperandoEscolha = false;

        btnSim.gameObject.SetActive(false);
        btnNao.gameObject.SetActive(false);

        dialogos = new string[]
        {
            "Hmm...\nVocê está tentando não se envolver,\nmas pra sair desta ilha você precisa deixar um Legado.",

            "Um Legado?",

            "Sim, novos perfis estão surgindo...\nprepare-se pequena Sofia."
        };

        personagens = new string[]
        {
            "Ancião",
            "Sofia",
            "Ancião"
        };

        dialogoIndex = 0;

        btnProximo.gameObject.SetActive(true);

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

        if (btnSim != null)
            btnSim.gameObject.SetActive(false);

        if (btnNao != null)
            btnNao.gameObject.SetActive(false);

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