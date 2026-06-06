using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class TerminalGogol : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text dadosText;

    [Header("Imagem Monitorada")]
    public Image imagemMonitorada;

    [Header("Imagem Mais Vista (Miniatura)")]
    public Image imagemMaisVista;

    void Update()
    {
        string ranking = CriarRanking();
        string visitas = CriarVisitas();

        // =========================
        // ATUALIZA IMAGEM MAIS VISTA
        // =========================
        if (GogolData.imagemMaisObservada != null && imagemMaisVista != null)
        {
            imagemMaisVista.sprite = GogolData.imagemMaisObservada;
        }

        // =========================
        // TEXTO DO PAINEL
        // =========================
        dadosText.text =
            "<b>LEGADO DA SOFIA</b>\n" +

            "Perfil: " + GogolPerfil.perfilJogador + "\n" +
            "Primeira interação: " + GogolPerfil.horarioPrimeiraInteracao + "\n"+
            "Prato escolhido: " + GogolData.pratoEscolhido + "\n\n" +

            "Último lugar: " + GogolData.ultimoLugarVisitado + "\n" +
            "Lugar favorito: " + GogolData.lugarFavorito + "\n" +
            "Tempo total: " + Mathf.FloorToInt(GogolData.tempoTotal) + "s\n\n" +

            "Imagem mais vista: " + GogolData.nomeImagemMaisObservada + "\n" +
            "Tempo imagem: " + Mathf.FloorToInt(GogolData.tempoImagemMaisObservada) + "s\n\n" +

            "<b>RANKING DE LUGARES (TEMPO)</b>" +
            ranking +

            "\n\n<b>DETALHES POR VISITA</b>" +
            visitas;
    }

    // =========================
    // RANKING SEGURO
    // =========================
    string CriarRanking()
    {
        string result = "";

        List<KeyValuePair<string, float>> lista =
            new List<KeyValuePair<string, float>>(GogolData.tempoPorLugar);

        lista.Sort((a, b) => b.Value.CompareTo(a.Value));

        for (int i = 0; i < lista.Count; i++)
        {
            result +=
                "\n" + (i + 1) +
                ". " + lista[i].Key +
                " - " + Mathf.FloorToInt(lista[i].Value) + "s";
        }

        return result;
    }

    // =========================
    // VISITAS SEGURAS
    // =========================
    string CriarVisitas()
    {
        string result = "";

        foreach (var item in GogolData.visitasPorLugar)
        {
            float tempo = 0f;

            GogolData.tempoPorLugar.TryGetValue(item.Key, out tempo);

            float media = item.Value > 0 ? tempo / item.Value : 0f;

            result +=
                "\n> " +
                item.Key +
                " | visitas: " +
                item.Value +
                " | média: " +
                Mathf.FloorToInt(media) + "s";
        }

        return result;
    }
}