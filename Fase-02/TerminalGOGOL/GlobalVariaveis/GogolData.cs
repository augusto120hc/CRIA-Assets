// using System.Collections.Generic;
// using UnityEngine;

// public static class GogolData
// {
//     // =========================================
//     // DADOS GERAIS
//     // =========================================
//     public static int curtidas = 0;

//     public static int lugaresVisitados = 0;

//     public static float tempoTotal = 0f;

//     public static string lugarAtual = "Nenhum";

//     public static string ultimoLugarVisitado = "Nenhum";

//     // =========================================
//     // LISTAS COMPLETAS
//     // =========================================

//     // todos os lugares visitados
//     public static List<string> historicoLugares =
//         new List<string>();

//     // tempo gasto em cada lugar
//     public static Dictionary<string, float> tempoPorLugar =
//         new Dictionary<string, float>();

//     // total de entradas em cada lugar
//     public static Dictionary<string, int> visitasPorLugar =
//         new Dictionary<string, int>();

//     // =========================================
//     // REGISTRAR VISITA
//     // =========================================

//     public static void RegistrarEntrada(string nomeLugar)
//     {
//         lugarAtual = nomeLugar;

//         ultimoLugarVisitado = nomeLugar;

//         lugaresVisitados++;

//         // adiciona ao histórico
//         historicoLugares.Add(nomeLugar);

//         // cria contador se não existir
//         if(!visitasPorLugar.ContainsKey(nomeLugar))
//         {
//             visitasPorLugar[nomeLugar] = 0;
//         }

//         visitasPorLugar[nomeLugar]++;

//         // cria tempo do lugar se não existir
//         if(!tempoPorLugar.ContainsKey(nomeLugar))
//         {
//             tempoPorLugar[nomeLugar] = 0f;
//         }

//         Debug.Log("GOGOL registrou entrada em: " + nomeLugar);
//     }

//     // =========================================
//     // REGISTRAR TEMPO
//     // =========================================

//     public static void RegistrarTempo(
//         string nomeLugar,
//         float tempo)
//     {
//         tempoTotal += tempo;

//         if(!tempoPorLugar.ContainsKey(nomeLugar))
//         {
//             tempoPorLugar[nomeLugar] = 0f;
//         }

//         tempoPorLugar[nomeLugar] += tempo;
//     }

//     // =========================================
//     // REGISTRAR CURTIDA
//     // =========================================

//     public static void RegistrarCurtida()
//     {
//         curtidas++;
//     }
// // }

// using System.Collections.Generic;
// using UnityEngine;

// public static class GogolData
// {
//     // DADOS GERAIS

//     public static int curtidas = 0;
//     public static int neutros = 0;
//     public static int naoGostou = 0;

//     public static int lugaresVisitados = 0;

//     public static float tempoTotal = 0f;

//     public static string lugarAtual = "Nenhum";
//     public static string ultimoLugarVisitado = "Nenhum";

//     public static string ultimaResposta = "Nenhuma";

//     // DADOS DE PERFIL

//     public static string perfilJogador = "Desconhecido";

//     public static string horarioPrimeiraInteracao = "--:--";

//     // HISTÓRICO

//     public static List<string> historicoLugares =
//         new List<string>();

//     public static Dictionary<string, float> tempoPorLugar =
//         new Dictionary<string, float>();

//     public static Dictionary<string, int> visitasPorLugar =
//         new Dictionary<string, int>();

// //IMAGEM mais VISTA no CATALOGO
//     public static Sprite imagemMaisObservada;

//     public static string nomeImagemMaisObservada = "Nenhuma";

//     public static float tempoImagemMaisObservada = 0f;

//     public static void RegistrarEntrada(string nomeLugar)
//     {
//         lugarAtual = nomeLugar;
//         ultimoLugarVisitado = nomeLugar;

//         lugaresVisitados++;

//         historicoLugares.Add(nomeLugar);

//         if (!visitasPorLugar.ContainsKey(nomeLugar))
//             visitasPorLugar[nomeLugar] = 0;

//         visitasPorLugar[nomeLugar]++;

//         if (!tempoPorLugar.ContainsKey(nomeLugar))
//             tempoPorLugar[nomeLugar] = 0f;
//     }

//     public static void RegistrarTempo(
//         string nomeLugar,
//         float tempo)
//     {
//         tempoTotal += tempo;

//         if (!tempoPorLugar.ContainsKey(nomeLugar))
//             tempoPorLugar[nomeLugar] = 0f;

//         tempoPorLugar[nomeLugar] += tempo;
//     }

//     public static void RegistrarCurtida()
//     {
//         curtidas++;
//         ultimaResposta = "Gostei";
//     }

//     public static void RegistrarNeutro()
//     {
//         neutros++;
//         ultimaResposta = "Neutro";
//     }

//     public static void RegistrarNaoGostou()
//     {
//         naoGostou++;
//         ultimaResposta = "Não Gostei";
//     }

    

  
// }


using System.Collections.Generic;
using UnityEngine;

public static class GogolData
{
    // =========================
    // LUGARES
    // =========================
    public static Dictionary<string, float> tempoPorLugar = new Dictionary<string, float>();
    public static Dictionary<string, int> visitasPorLugar = new Dictionary<string, int>();

    public static string lugarAtual = "";
    public static string lugarFavorito = "";
    public static string ultimoLugarVisitado = "";

    //PRATO PREFERIDO
    public static string pratoEscolhido = "";

    public static float tempoTotal = 0f;

    // =========================
    // PERFIL DE REAÇÃO
    // =========================
    public static int curtidas = 0;
    public static int neutros = 0;
    public static int naoGostou = 0;

    public static void RegistrarCurtida()
    {
        curtidas++;
    }

    public static void RegistrarNeutro()
    {
        neutros++;
    }

    public static void RegistrarNaoGostou()
    {
        naoGostou++;
    }

    // =========================
    // LUGARES
    // =========================
    public static void RegistrarEntrada(string lugar)
    {
        lugarAtual = lugar;
        ultimoLugarVisitado = lugar;

        if (!visitasPorLugar.ContainsKey(lugar))
            visitasPorLugar[lugar] = 0;

        visitasPorLugar[lugar]++;
    }

    public static void RegistrarTempo(string lugar, float tempo)
{
    if (!tempoPorLugar.ContainsKey(lugar))
        tempoPorLugar[lugar] = 0f;

    tempoPorLugar[lugar] += tempo;
    tempoTotal += tempo;

    AtualizarLugarFavorito();
}

    // =========================
    // IMAGEM (CORRIGIDO)
    // =========================
    public static Sprite imagemMaisObservada;
    public static string nomeImagemMaisObservada = "";
    public static float tempoImagemMaisObservada = 0f;

    public static void RegistrarImagem(Sprite img, string nome, float tempo)
    {
        if (tempo > tempoImagemMaisObservada)
        {
            imagemMaisObservada = img;
            nomeImagemMaisObservada = nome;
            tempoImagemMaisObservada = tempo;
        }
    }

    public static void AtualizarLugarFavorito()
{
    string melhorLugar = "";
    float maiorTempo = 0f;

    foreach (var item in tempoPorLugar)
    {
        if (item.Value > maiorTempo)
        {
            maiorTempo = item.Value;
            melhorLugar = item.Key;
        }
    }

    lugarFavorito = melhorLugar;
}

}