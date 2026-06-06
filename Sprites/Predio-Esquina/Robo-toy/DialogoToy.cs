using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;


public class DialogoToy : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialogueUI;

    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nomeText;

    [Header("Imagem Personagem")]
    public Image personagemImage;

    public Sprite spriteSofia;

    public Sprite spriteToy;

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

        textoBtnSim.text = "Sim";
        textoBtnNao.text = "Não";
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
         Debug.Log("BOTAO CLICADO");


        dialogoAtivo = true;

    

        dialogueUI.SetActive(true);

        dialogoIndex = 0;

        esperandoEscolha = false;

        btnProximo.gameObject.SetActive(true);

        dialogos = new string[]
    {
        "Olá Toy!",

        "Olá Sofia.\nQuer saber por que seu cabelo fica azul nesta \nárea?"
    };

    personagens = new string[]
    {
        "Sofia",
        "Toy"
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
        else if (personagem == "Toy")
        {
            personagemImage.sprite = spriteToy;
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
            "Sim.",

            "As empresas observam muitas das coisas que fazemos na internet.",

            "O que elas observam?",

            "Os vídeos que assistimos, os sites que visitamos, aquilo que curtimos e até o tempo que passamos olhando para algo.",

            "Mas por quê?",

            "Porque essas informações possuem valor.\nMuitas empresas enxergam as pessoas como perfis de dados que podem ser analisados.",

            "Então elas sabem tudo sobre mim?",

            "Não tudo.\nMas tentam prever o que você gosta, o que pode comprar e quais conteúdos chamam sua atenção.",

            "Como posso evitar isso?",

            "Você só precisa saber que as empresas usam modelos estatísticos, sistemas de ranking e recomendações baseadas em comportamento.",

            "Esses sistemas tentam adivinhar o que mostrar para cada pessoa.",

            "Por isso é importante pensar antes de clicar e questionar o que aparece para você.",

            "Obrigado Toy.\nEstá ficando tarde.",

            "Boa noite Sofia.\nContinue curiosa e nunca deixe que decidam tudo por você."
        };

        personagens = new string[]
        {
            "Sofia",
            "Toy",
            "Sofia",
            "Toy",
            "Sofia",
            "Toy",
            "Sofia",
            "Toy",
            "Sofia",
            "Toy",
            "Toy",
            "Toy",
            "Sofia",
            "Toy"
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
            "Tudo bem.",

            "Algumas respostas só fazem sentido quando a pessoa está pronta para ouvi-las.",

            "Você é estranho, Toy.",

            "Talvez.\nMas estarei aqui se mudar de ideia."
        };

        personagens = new string[]
        {
            "Toy",
            "Toy",
            "Sofia",
            "Toy"
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