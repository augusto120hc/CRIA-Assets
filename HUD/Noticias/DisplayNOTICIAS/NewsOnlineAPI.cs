using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class NewsOnlineAPI : MonoBehaviour
{
    [Header("API NEWSAPI")]
    public string apiKey = "62253ab6730e4866a34cfeb323331d4d";

    private string apiURL =
        "https://newsapi.org/v2/top-headlines?country=us&pageSize=5";

    // =========================
    [System.Serializable]
    public class NewsItem
    {
        public string title;
        public string description;
    }

    // =========================
    [System.Serializable]
    public class NewsResponse
    {
        public Article[] articles;
    }

    [System.Serializable]
    public class Article
    {
        public string title;
        public string description;
    }

    // =========================
    public IEnumerator BuscarNoticias(
        System.Action<List<NewsItem>> callback
    )
    {
        Debug.Log("=== INICIANDO API NEWS ===");

        UnityWebRequest request =
            UnityWebRequest.Get(apiURL);

        // NEWSAPI USA HEADER
        request.SetRequestHeader(
            "X-Api-Key",
            apiKey
        );

        yield return request.SendWebRequest();
        Debug.Log("REQUEST TERMINOU");

Debug.Log("RESULT: " + request.result);

Debug.Log("ERROR: " + request.error);

Debug.Log("CODE: " + request.responseCode);

Debug.Log("TEXT:");
Debug.Log(request.downloadHandler.text);

        Debug.Log("RESPOSTA:");
        Debug.Log(request.downloadHandler.text);

        // ERRO
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(
                "ERRO API: " + request.error
            );

            callback(null);

            yield break;
        }

        // JSON
        NewsResponse response =
            JsonUtility.FromJson<NewsResponse>(
                request.downloadHandler.text
            );

        // sem artigos
        if (response == null ||
            response.articles == null ||
            response.articles.Length == 0)
        {
            Debug.LogError(
                "Nenhuma notícia encontrada."
            );

            callback(null);

            yield break;
        }

        List<NewsItem> noticias =
            new List<NewsItem>();

        foreach (var article in response.articles)
        {
            noticias.Add(new NewsItem
            {
                title =
                    string.IsNullOrEmpty(article.title)
                    ? "Sem título"
                    : article.title,

                description =
                    string.IsNullOrEmpty(article.description)
                    ? "Sem descrição."
                    : article.description
            });
        }

        Debug.Log(
            "TOTAL DE NOTÍCIAS: " + noticias.Count
        );

        callback(noticias);
    }
}