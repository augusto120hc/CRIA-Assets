using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

[System.Serializable]
public class NewsItem
{
    public string title;
    public string description;
}

[System.Serializable]
public class NewsList
{
    public List<NewsItem> articles = new List<NewsItem>();
}

[System.Serializable]
public class NewsAPIResponse
{
    public NewsAPIArticle[] articles;
}

[System.Serializable]
public class NewsAPIArticle
{
    public string title;
    public string description;
}

public class NewsFetcherToJson : MonoBehaviour
{
    [Header("API")]
    public string apiKey = "62253ab6730e4866a34cfeb323331d4d";

    private string baseUrl =
        "https://newsapi.org/v2/top-headlines?country=us&pageSize=30&page=";

    [Header("Config")]
    public int targetAmount = 90;

    private List<NewsItem> allNews = new List<NewsItem>();

    void Start()
    {
        StartCoroutine(FetchAllNews());
    }

    IEnumerator FetchAllNews()
    {
        int page = 1;
        int maxPages = 15;

        while (allNews.Count < targetAmount && page <= maxPages)
        {
            string url = baseUrl + page + "&apiKey=" + apiKey;

            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                // 🔥 headers para evitar bloqueios (426 / restrições)
                request.SetRequestHeader("User-Agent", "Mozilla/5.0");
                request.SetRequestHeader("Accept", "application/json");

                yield return request.SendWebRequest();

                // ⚠️ não mata o loop se falhar
                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogWarning("Erro API página " + page + ": " + request.error);
                    page++;
                    yield return new WaitForSeconds(1f);
                    continue;
                }

                string json = request.downloadHandler.text;

                NewsAPIResponse response =
                    JsonUtility.FromJson<NewsAPIResponse>(json);

                if (response == null || response.articles == null || response.articles.Length == 0)
                {
                    Debug.LogWarning("Página vazia: " + page);
                    page++;
                    yield return new WaitForSeconds(1f);
                    continue;
                }

                foreach (var a in response.articles)
                {
                    if (allNews.Count >= targetAmount)
                        break;

                    if (string.IsNullOrEmpty(a.title))
                        continue;

                    allNews.Add(new NewsItem
                    {
                        title = a.title,
                        description = a.description
                    });
                }
            }

            page++;
            yield return new WaitForSeconds(1f); // evita rate limit
        }

        SaveToJson();
    }

    void SaveToJson()
    {
        NewsList list = new NewsList();
        list.articles = allNews;

        string json = JsonUtility.ToJson(list, true);

        string path = Application.persistentDataPath + "/news_database.json";

        Debug.Log("CAMINHO REAL: " + Application.persistentDataPath);

        File.WriteAllText(path, json);

        Debug.Log("🔥 JSON salvo em: " + path);
        Debug.Log("Total de notícias: " + allNews.Count);
    }
}