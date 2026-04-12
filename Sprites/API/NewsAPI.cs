using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NewsAPI : MonoBehaviour
{
    private string apiKey = "fee7637190414418b0f399454b4a25c6";

    public void BuscarNoticias()
    {
        StartCoroutine(GetNews());
    }

    IEnumerator GetNews()
    {
        string url = $"https://newsapi.org/v2/top-headlines?country=us&apiKey={apiKey}";

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(request.downloadHandler.text);
        }
        else
        {
            Debug.Log("Erro: " + request.error);
        }
    }
}




