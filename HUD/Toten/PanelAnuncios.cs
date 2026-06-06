using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PanelAnuncios : MonoBehaviour
{
    [Header("Painel")]
    public GameObject painel;

    [Header("Textos")]
    public TMP_Text tituloText;
    public TMP_Text perguntaText;

    [Header("Botões")]
    public Button btnGostei;
    public Button btnNeutro;
    public Button btnNaoGostei;

    // =========================
    // DADOS GLOBAIS DO "GOGOL"
    // =========================

    // avaliação do jogador
  // controla tempo dentro do lugar

  private float tempoAtual = 0f;
private bool painelAberto = false;

    private void Awake()
    {
        if (painel != null)
            painel.SetActive(false);
    }

    void Start()
    {
        tituloText.text =
            "<color=#4285F4>G</color>" +
            "<color=#EA4335>o</color>" +
            "<color=#FBBC05>g</color>" +
            "<color=#4285F4>o</color>" +
            "<color=#34A853>l</color>";

        perguntaText.text =
            "O que você achou deste lugar?";

        btnGostei.onClick.AddListener(Gostei);
        btnNeutro.onClick.AddListener(Neutro);
        btnNaoGostei.onClick.AddListener(NaoGostei);
    }

    void Update()
    {
        // conta tempo enquanto painel aberto
        if (painelAberto)
        {
            tempoAtual += Time.deltaTime;
        }
    }

    // =========================
    // ABRIR / FECHAR
    // =========================

    public void Abrir()
    {
        painel.SetActive(true);

        painelAberto = true;

        tempoAtual = 0f;
    }

    public void Fechar()
    {
        painel.SetActive(false);

        painelAberto = false;

        GogolData.RegistrarTempo(
            GogolData.lugarAtual,
            tempoAtual
        );
    }

    public void Alternar()
    {
        painel.SetActive(!painel.activeSelf);
    }

    // =========================
    // RESPOSTAS
    // =========================

    void Gostei()
    {
        GogolData.RegistrarCurtida();

        Debug.Log("Usuário gostou.");

        Fechar();
    }

    void Neutro()
    {
        GogolData.RegistrarNeutro();

        Debug.Log("Usuário neutro.");

        Fechar();
    }

    void NaoGostei()
    {
        GogolData.RegistrarNaoGostou();

        Debug.Log("Usuário não gostou.");

        Fechar();
    }
}