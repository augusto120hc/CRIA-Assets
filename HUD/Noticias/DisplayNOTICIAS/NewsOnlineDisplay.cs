using UnityEngine;
using TMPro;

public class NewsOnlineDisplay : MonoBehaviour
{
    [Header("UI")]
    public GameObject painelNoticias;

    public TMP_Text tituloText;
    public TMP_Text descricaoText;

    [Header("Texto fixo (teste ou narrativa)")]
    [TextArea(3, 10)]
    public string tituloPadrao = "NOTÍCIAS DO SISTEMA";

    [TextArea(3, 10)]
    public string descricaoPadrao =
        "Você está conectado ao fluxo de informações.\n" +
        "Tudo que você vê foi filtrado por algoritmos.";

    private void Start()
    {
        // garante que começa fechado
        if (painelNoticias != null)
            painelNoticias.SetActive(false);
    }

    public void AbrirPainel()
    {
        if (painelNoticias == null) return;

        painelNoticias.SetActive(true);

        if (tituloText != null)
            tituloText.text = tituloPadrao;

        if (descricaoText != null)
            descricaoText.text = descricaoPadrao;
    }

    public void FecharPainel()
    {
        if (painelNoticias != null)
            painelNoticias.SetActive(false);
    }
}