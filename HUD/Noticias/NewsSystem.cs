using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using System.Xml;
using System.Collections.Generic;
using System.Text;

public class NewsSystem : MonoBehaviour
{
    // =========================
    // API GNEWS
    // =========================
    [Header("API")]
    public string apiKey = "62253ab6730e4866a34cfeb323331d4d";
    private string apiURL = "https://gnews.io/api/v4/top-headlines?country=br&lang=pt&max=10&apikey=";

    // =========================
    // RSS fallback
    // =========================
    public string rssURL = "https://g1.globo.com/rss/g1/";

    // =========================
    // UI
    // =========================
    [Header("UI")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI newsText;
    public TextMeshProUGUI resultText;

    [Header("Buttons")]
    public GameObject emotionalBtn;
    public GameObject rationalBtn;
    public GameObject curiousBtn;

    // =========================
    // STATE
    // =========================
    private int emocional;
    private int racional;
    private int curioso;

    private int index = 0;
    private bool finalizado = false;

    [Header("Glitch")]
    public float glitchSpeed = 0.03f;
    public float glitchDuration = 1.5f;

    private bool isGlitching = false;

    private string originalText;

    [Header("Botão Fechar")]
    public GameObject fecharBtn;



    // =========================
    // NEWS MODEL
    // =========================
    [System.Serializable]
    public class NewsItem
    {
        public string title;
        public string description;
    }

    private List<NewsItem> news = new List<NewsItem>();

    // =========================
    void Start()
    {
        fecharBtn.SetActive(false);
        StartCoroutine(LoadNews());
    }

    // =========================
    // LOAD NEWS
    // =========================
    IEnumerator LoadNews()
    {
        
        string url = apiURL + apiKey;

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Erro API, indo para RSS...");
            yield return StartCoroutine(LoadRSS());
            yield break;
        }

        GNewsResponse response =
            JsonUtility.FromJson<GNewsResponse>(request.downloadHandler.text);

        if (response == null || response.articles == null || response.articles.Length == 0)
        {
            Debug.Log("Resposta vazia, indo para RSS...");
            yield return StartCoroutine(LoadRSS());
            yield break;
        }

        news.Clear();

        int count = Mathf.Min(5, response.articles.Length);

        for (int i = 0; i < count; i++)
        {
            news.Add(new NewsItem
            {
                title = response.articles[i].title,
                description = string.IsNullOrEmpty(response.articles[i].description)
                    ? "Sem descrição disponível"
                    : response.articles[i].description
            });
        }

        index = 0;
        ShowNews();
    }

    // =========================
    // RSS fallback
    // =========================
    IEnumerator LoadRSS()
    {
        UnityWebRequest request = UnityWebRequest.Get(rssURL);
        yield return request.SendWebRequest();

        XmlDocument xml = new XmlDocument();
        xml.LoadXml(request.downloadHandler.text);

        XmlNodeList items = xml.GetElementsByTagName("item");

        news.Clear();

        for (int i = 0; i < 5 && i < items.Count; i++)
        {
            news.Add(new NewsItem
            {
                title = items[i]["title"].InnerText,
                description = "Sem descrição disponível"
            });
        }

        index = 0;
        ShowNews();
    }

    // =========================
    // SHOW NEWS
    // =========================
    void ShowNews()
    {
        if (index >= news.Count || finalizado)
        {
            FinishGame();
            return;
        }

        Debug.Log("Notícia: " + news[index].title);

        titleText.text = news[index].title;
        newsText.text = FormatDescription(news[index].description);
        
    }

    // =========================
    // CHOICES
    // =========================
    public void EscolhaEmocional()
    {
        emocional++;
        Debug.Log("REAÇÃO | emocional = " + emocional);
        Next();
    }

    public void EscolhaRacional()
    {
        racional++;
        Debug.Log("REFLEXÃO | racional = " + racional);
        Next();
    }

    public void EscolhaCurioso()
    {
        curioso++;
        Debug.Log("DESCOBERTA | curioso = " + curioso);
        Next();
    }

    // =========================
    void Next()
    {
        index++;
        ShowNews();
    }



    IEnumerator GlitchText(TMP_Text target, string finalText)
{
    isGlitching = true;

    float timer = 0f;

    while (timer < glitchDuration)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < finalText.Length; i++)
        {
            char c = finalText[i];

            if (Random.value > 0.7f)
            {
                sb.Append(GetRandomChar());
            }
            else
            {
                sb.Append(c);
            }
        }

        target.text = sb.ToString();

        timer += glitchSpeed;
        yield return new WaitForSeconds(glitchSpeed);
    }

    target.text = finalText;
    isGlitching = false;
}

char GetRandomChar()
{
    string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@#$%&*";
    return chars[Random.Range(0, chars.Length)];
}

    // =========================
    void FinishGame()
    {
        finalizado = true;

        emotionalBtn.SetActive(false);
        rationalBtn.SetActive(false);
        curiousBtn.SetActive(false);

        fecharBtn.SetActive(true);

        string result;

        if (emocional >= racional && emocional >= curioso)
            result = "Perfil emocional: você reage pelo impacto.";
        else if (racional >= curioso)
            result = "Perfil racional: você analisa antes de agir.";
        else
            result = "Perfil curioso: você explora o desconhecido.";

        Debug.Log("RESULTADO FINAL: " + result);

        StartCoroutine(GlitchText(titleText, "RESULTADO FINAL"));
        newsText.text = "";
        StartCoroutine(GlitchText(resultText, result));
    }

    string FormatDescription(string text)
    {
        if (string.IsNullOrEmpty(text))
            return "Sem descrição disponível...";

        text = text.Trim();

        if (text.EndsWith("..."))
            return text;

        return text.TrimEnd('.') + ". ... [FIM]";
    }

    // =========================
    [System.Serializable]
    public class GNewsResponse
    {
        public GNewsArticle[] articles;
    }

    [System.Serializable]
    public class GNewsArticle
    {
        public string title;
        public string description; // 🔥 FIX IMPORTANTE
    }
}