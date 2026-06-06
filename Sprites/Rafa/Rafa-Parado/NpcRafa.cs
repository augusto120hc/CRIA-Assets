using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class NpcRafa : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialogueUI;

    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nomeText;

    [Header("Imagem Personagem")]
    public Image personagemImage;

    public Sprite spriteSofia;
    public Sprite spriteRafa;

    [Header("Botões")]
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

    // =========================
    [Header("Animação Final")]
    public Animator animatorRafa;

    public string animacaoRecuperado = "idle-DireitaEsquerda";

    [Header("Objeto")]
    public GameObject celularObjeto;

    private bool recuperado = false;

    [Header("Recompensa")]
    public GameObject bauRecompensa;

    [Header("Som Recompensa")]
    public AudioClip somBau;

    private bool dialogoFinalizado = false;

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
        VerificarRecuperacao();

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
        if (botaoInteragir == null)
            return;

        Vector3 pos = Camera.main.WorldToScreenPoint(
            transform.position + Vector3.up * 1.5f
        );

        botaoInteragir.transform.position = pos;
    }

    // =========================
    public void IniciarDialogo()
    {
        dialogoAtivo = true;

        dialogueUI.SetActive(true);

        dialogoIndex = 0;

        dialogoFinalizado = false;

        btnProximo.gameObject.SetActive(true);

        // =========================
        // TODOS FRAGMENTOS COLETADOS
        if (
            FragmentosManager.instance.temClareza &&
            FragmentosManager.instance.temVontade &&
            FragmentosManager.instance.temEssencia
        )
        {
           dialogos = new string[]
        {
            "Você recuperou todos os fragmentos.",

            "Clareza.Vontade.Essência.",

            "Eu não tinha percebido o quanto estava me perdendo dentro desse fluxo constante.",

            "O Algoritmo não apagou quem você era.\nSó foi deixando isso escondido atrás de excesso, repetição e distração.",

            "Então ainda é possível manter a própria identidade nesse sistema?",

            "Sim.\nMas exige consciência sobre o que consumimos,\no que criamos\n e o que escolhemos manter.",

            "Obrigado por me ajudar a lembrar disso, Sofia.",
            "Desativei as notificações, agora pesquiso o que realmente quero"

        };

        personagens = new string[]
        {
            "Rafa",
            "Rafa",
            "Rafa",
            "Sofia",
            "Rafa",
            "Sofia",
            "Rafa",
            "Rafa"
        };
        }
        else
        {
            dialogos = new string[]
            {
                "Você já sentiu que passa tempo demais vendo as mesmas coisas?",

                "O Algoritmo resumiu minha identidade em números.",

                "Sua personalidade não desapareceu.Ela foi fragmentada. <color=#fffc40>Clareza.Vontade.Essência.</color>",

                "Uma das causas pode ser o Reforço Intermitente Positivo.",

                "Fragmentada...?\nReforço Intermitente Positivo?",

                "Existem partes minhas espalhadas por essa\n rede.",

                "Então eu vou trazer seus fragmentos de volta."
            };

            personagens = new string[]
            {
                "Rafa",
                "Rafa",
                "Sofia",
                "Sofia",
                "Rafa",
                "Rafa",
                "Sofia"
            };
        }

        MostrarDialogo();

        
    }

    // =========================
    void MostrarDialogo()
    {
        if (dialogoIndex >= dialogos.Length)
        {
            dialogoFinalizado = true;

            FecharDialogo();

            return;
        }

        string personagem = personagens[dialogoIndex];

        nomeText.text = personagem;

        // =========================
        // TROCA SPRITE
        if (personagemImage != null)
        {
            if (personagem == "Sofia")
            {
                personagemImage.sprite = spriteSofia;
            }
            else
            {
                personagemImage.sprite = spriteRafa;
            }
        }

        // =========================
        string texto = dialogos[dialogoIndex];

        // cor Sofia
        if (personagem == "Sofia")
        {
            texto = "<color=#59fff7>" + texto + "</color>";
        }

        // cor Rafa
        if (personagem == "Rafa")
        {
            texto = "<color=#ffae42>" + texto + "</color>";
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

        if (
            dialogoFinalizado &&
            FragmentosManager.instance.temClareza &&
            FragmentosManager.instance.temVontade &&
            FragmentosManager.instance.temEssencia
        )
        {

            FragmentosManager.instance.fragmentosEntreguesAoRafa = true;
            
            if (bauRecompensa != null)
            {
                bauRecompensa.SetActive(true);

                if (somBau != null)
            {
                AudioSource.PlayClipAtPoint(
                    somBau,
                    Camera.main.transform.position
                );
            }
            }
        }
    }

    // =========================
    void VerificarRecuperacao()
    {
        if (recuperado)
            return;

        if (
            FragmentosManager.instance.temClareza &&
            FragmentosManager.instance.temVontade &&
            FragmentosManager.instance.temEssencia
        )
        {
            recuperado = true;

            Debug.Log("Rafa recuperado");

            // TOCA ANIMAÇÃO
            if (animatorRafa != null)
            {
                animatorRafa.Play(animacaoRecuperado);
            }

            // VIRA PERSONAGEM
            Vector3 escala = transform.localScale;

            escala.x = -1f;

            transform.localScale = escala;

            // DESATIVA CELULAR
            if (celularObjeto != null)
            {
                celularObjeto.SetActive(false);
            }
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