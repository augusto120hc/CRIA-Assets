using System.Collections;
using UnityEngine;
using TMPro;

public class PlacaPublicidade : MonoBehaviour
{
    [Header("Propagandas Pizza")]
    public GameObject[] propagandaPizza;

    [Header("Propagandas Sushi")]
    public GameObject[] propagandaSushi;

    [Header("Propagandas Macarrao")]
    public GameObject[] propagandaMacarrao;

    [Header("Dialogo")]
    public GameObject painelDialogo;
    public TextMeshProUGUI dialogoText;

    private bool playerPerto = false;
    private Coroutine loopCoroutine;

    void Start()
    {
        EsconderTodas();

        if (painelDialogo == null)
            painelDialogo = GameObject.Find("PainelDialogo");

        if (dialogoText == null)
            dialogoText = GameObject.Find("DialogoText")?.GetComponent<TextMeshProUGUI>();

        if (painelDialogo != null)
            painelDialogo.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerPerto = true;

        loopCoroutine = StartCoroutine(LoopEscolha());

        MostrarDialogo(); // 👈 AQUI AGORA
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerPerto = false;

        if (loopCoroutine != null)
            StopCoroutine(loopCoroutine);

        EsconderTodas();

        if (painelDialogo != null)
            painelDialogo.SetActive(false);
}

    IEnumerator LoopEscolha()
    {
        while (playerPerto)
        {
            string escolha = RestauranteData.pratoFavorito;

            GameObject[] atual = null;

            if (escolha == "Pizza") atual = propagandaPizza;
            else if (escolha == "Sushi") atual = propagandaSushi;
            else if (escolha == "Macarronada") atual = propagandaMacarrao;

            if (atual == null || atual.Length == 0)
                yield break;

            int index = 0;

            while (playerPerto)
            {
                EsconderTodas();

                atual[index].SetActive(true);

                index = (index + 1) % atual.Length;

                yield return new WaitForSeconds(2f);
            }
        }
    }

    void MostrarDialogo()
    {
        if (painelDialogo == null || dialogoText == null)
        {
            // Debug.LogWarning("UI do diálogo não encontrada na Fase02");
            return;
        }

        string prato = RestauranteData.pratoFavorito;

        // 🔥 BLOQUEIO IMPORTANTE
        if (string.IsNullOrEmpty(prato) || prato == "Nenhum")
        {
            // Debug.Log("Jogador ainda não escolheu prato no totem.");
            return;
        }

        dialogoText.text =
    "Parece que esse outdoor sabe do que eu gosto, " +
    "acabei de escolher <color=#FFFF00>" + prato + "</color>" +
    " e ele só mostra <color=#FFFF00>" + prato + "</color>.";

        painelDialogo.SetActive(true);

        StartCoroutine(FecharDialogo());
    }

    IEnumerator FecharDialogo()
    {
        yield return new WaitForSeconds(6f);
        painelDialogo.SetActive(false);
    }

    void EsconderTodas()
    {
        foreach (var go in propagandaPizza)
            go.SetActive(false);

        foreach (var go in propagandaSushi)
            go.SetActive(false);

        foreach (var go in propagandaMacarrao)
            go.SetActive(false);
    }
}