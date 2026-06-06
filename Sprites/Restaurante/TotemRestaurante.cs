// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;
// using System.Collections;

// public class TotemRestaurante : MonoBehaviour
// {
//     [Header("Painel")]
//     public GameObject painelEscolha;

//     [Header("Dialogo Saida")]
//     public GameObject painelDialogo;
//     public TextMeshProUGUI dialogoText;

//     public TMP_Text perguntaText;

//     [Header("Botões")]
//     public Button btnPizza;
//     public Button btnSushi;
//     public Button btnMacarrao;

//     [Header("Botão fechar")]
//     public Button btnFechar;

//     [Header("Pratos")]
//     public string nomePizza = "Pizza";
//     public string nomeSushi = "Sushi";
//     public string nomeMacarrao = "Macarronada";

//     [Header("Interação")]
//     public GameObject painelInteracao;

//     private bool playerPerto = false;

//     void Start()
//     {
//         painelEscolha.SetActive(false);

//         btnPizza.onClick.AddListener(
//             () => EscolherPrato(nomePizza));

//         btnSushi.onClick.AddListener(
//             () => EscolherPrato(nomeSushi));

//         btnMacarrao.onClick.AddListener(
//             () => EscolherPrato(nomeMacarrao));

//         // botão fechar
//         btnFechar.onClick.AddListener(
//             FecharPainel);

//             if(painelInteracao != null)
//         {
//             painelInteracao.SetActive(false);
//         }
//     }

//     void Update()
//     {
//         if(playerPerto &&
//            Input.GetKeyDown(KeyCode.E))
//         {
//             painelEscolha.SetActive(true);

//             string mensagem =
//                 "Olá Sofia\nQual prato você prefere?\n\n";

//             // mostra última escolha
//             if(RestauranteData.ultimaEscolha != "")
//             {
//                 mensagem +=
//                 "Na sua última visita você escolheu " +
//                 RestauranteData.ultimaEscolha +
//                 ".\n\n";
//             }

//             perguntaText.text = mensagem;
//         }
//     }

//     void MostrarDialogoSaida()
//     {
//         string prato = RestauranteData.pratoFavorito;

//         string frase =
//             "Parece que esse outdoor sabe do que eu gosto, " +
//             "acabei de escolher " + prato +
//             " e ele só mostra " + prato + ".";

//         dialogoText.text = frase;

//         painelDialogo.SetActive(true);

//         StartCoroutine(FecharDialogo());
//     }

//     IEnumerator FecharDialogo()
// {
//     yield return new WaitForSeconds(4f);

//     painelDialogo.SetActive(false);
// }



//     void EscolherPrato(string prato)
//     {
//         int porcentagem = 0;

//         // porcentagem diferente para cada prato
//         if(prato == nomePizza)
//         {
//             porcentagem = 97;
//         }
//         else if(prato == nomeSushi)
//         {
//             porcentagem = 81;
//         }
//         else if(prato == nomeMacarrao)
//         {
//             porcentagem = 64;
//         }

//         // salva última escolha
//         RestauranteData.ultimaEscolha =
//             prato;

//         // salva favorita
//         RestauranteData.pratoFavorito =
//             prato;

//         // atualiza perfil
//         RestauranteData.perfilCulinario =
//             "Apreciadora de " + prato;

//         // DESATIVA BOTÕES
//         btnPizza.gameObject.SetActive(false);

//         btnSushi.gameObject.SetActive(false);

//         btnMacarrao.gameObject.SetActive(false);

//         // mensagem do sistema
//         perguntaText.text =
//             porcentagem +
//             "% das pessoas escolhem " +
//             prato +
//             ".\n\n" +

//             "Seu perfil culinário foi atualizado.";

//         Debug.Log(
//             "Sofia prefere: " + prato);
//     }


//     void FecharPainel()
//     {
//         if(painelEscolha != null)
//         {
//             painelEscolha.SetActive(false);
//         }
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if(other.CompareTag("Player"))
//         {
//             playerPerto = true;
//         }

//         if(other.CompareTag("Player"))
//         {
//             playerPerto = true;

//             if(painelInteracao != null)
//             {
//                 painelInteracao.SetActive(true);
//             }
//         }
//     }

//     private void OnTriggerExit2D(Collider2D other)
//     {
//         if(other.CompareTag("Player"))
//         {
//             playerPerto = false;

//             if(painelEscolha != null)
//             {
//                 painelEscolha.SetActive(false);
//             }
//         }

//         if(other.CompareTag("Player"))
//         {
//             playerPerto = false;

//             if(painelInteracao != null)
//             {
//                 painelInteracao.SetActive(false);
//             }

//             if(painelEscolha != null)
//             {
//                 painelEscolha.SetActive(false);
//             }
//         }
//     }
// }
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TotemRestaurante : MonoBehaviour
{
    [Header("Painel Escolha")]
    public GameObject painelEscolha;

    [Header("Texto Pergunta")]
    public TMP_Text perguntaText;

    [Header("Botões")]
    public Button btnPizza;
    public Button btnSushi;
    public Button btnMacarrao;

    [Header("Botão fechar")]
    public Button btnFechar;

    [Header("Interação")]
    public GameObject painelInteracao;

    [Header("Pratos")]
    public string nomePizza = "Pizza";
    public string nomeSushi = "Sushi";
    public string nomeMacarrao = "Macarronada";

    private bool playerPerto = false;

    void Start()
    {
        painelEscolha.SetActive(false);

        if (painelInteracao != null)
            painelInteracao.SetActive(false);

        btnPizza.onClick.AddListener(() => EscolherPrato(nomePizza));
        btnSushi.onClick.AddListener(() => EscolherPrato(nomeSushi));
        btnMacarrao.onClick.AddListener(() => EscolherPrato(nomeMacarrao));

        btnFechar.onClick.AddListener(FecharPainel);
    }

    void Update()
    {
        if (playerPerto && Input.GetKeyDown(KeyCode.E))
        {
            AbrirPainel();
        }
    }

    // =========================
    // ABRIR PAINEL
    // =========================
    void AbrirPainel()
    {
        painelEscolha.SetActive(true);

        string mensagem = "Olá Sofia\nQual prato você prefere?\n\n";

        if (!string.IsNullOrEmpty(GogolData.pratoEscolhido))
        {
            mensagem += "<color=#00FF99>Na sua última visita você escolheu: " +
                        GogolData.pratoEscolhido +
                        ".\n\n</color>";
        }

        perguntaText.text = mensagem;
    }

    // =========================
    // ESCOLHER PRATO
    // =========================
    void EscolherPrato(string prato)
    {
        int porcentagem = 0;

        if (prato == nomePizza)
            porcentagem = 97;
        else if (prato == nomeSushi)
            porcentagem = 81;
        else if (prato == nomeMacarrao)
            porcentagem = 64;

        GogolData.pratoEscolhido = prato;

        RestauranteData.ultimaEscolha = prato;
        RestauranteData.pratoFavorito = prato;
        RestauranteData.perfilCulinario =
            "Apreciadora de " + prato;

        btnPizza.gameObject.SetActive(false);
        btnSushi.gameObject.SetActive(false);
        btnMacarrao.gameObject.SetActive(false);

        perguntaText.text =
            "<b>GOGOL FOOD ANALYTICS</b>\n\n" +

            "Processando preferências...\n" +
            "Perfil identificado.\n" +

            "Escolha registrada: " +
            "<color=#00FF99>" + prato + "</color>\n" +

            porcentagem +
            "% dos usuários com perfil semelhante ao seu também selecionaram esta opção.\n" +

            "Seu comportamento será utilizado para melhorar futuras recomendações.";

        Debug.Log("Sofia prefere: " + prato);
    }

    // =========================
    // FECHAR PAINEL
    // =========================
    void FecharPainel()
    {
        painelEscolha.SetActive(false);
    }

    // =========================
    // COLISÃO
    // =========================
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerPerto = true;

        if (painelInteracao != null)
            painelInteracao.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerPerto = false;

        FecharPainel();

        if (painelInteracao != null)
            painelInteracao.SetActive(false);
    }
}