using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using System.Collections.Generic;
    using UnityEngine.Rendering.Universal;

public class NewsSystem : MonoBehaviour
{
    [Header("API")]
    public string apiKey = "SUA_API_AQUI";
    private string apiURL = "https://gnews.io/api/v4/top-headlines?country=br&lang=pt&max=5&apikey=";

    [Header("UI")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI newsText;
    public TextMeshProUGUI resultText;

    [Header("Buttons")]
    public GameObject emotionalBtn;
    public GameObject rationalBtn;
    public GameObject curiousBtn;

    public GameObject fecharBtn;

    public TypewriterEffect typewriter;

    private int emocional;
    private int racional;
    private int curioso;

    private int index = 0;
    private bool finalizado = false;
    private bool jaFinalizou = false;

    public GameObject mensagemUI; //FRASE que desaparece no final



    // =========================
    [System.Serializable]
    public class NewsItem
    {
        public string title;
        public string description;
    }

    private List<NewsItem> news = new List<NewsItem>();

    // =========================
    IEnumerator Start()
    {
        // 🔥 pega a luz pelo nome do objeto
        if (globalLight == null)
            globalLight = GameObject.Find("Global Light 2D").GetComponent<UnityEngine.Rendering.Universal.Light2D>();

        fecharBtn.SetActive(false);
        resultText.text = "";

        yield return null;

        StartCoroutine(LoadNewsFromAPI());
    }

    // =========================
    // 🌐 API GNEWS
    // =========================
    IEnumerator LoadNewsFromAPI()
    {
        string url = apiURL + apiKey;

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("API falhou → fallback local");
            LoadFakeNews();
            yield break;
        }

        string json = request.downloadHandler.text;

        GNewsResponse response = JsonUtility.FromJson<GNewsResponse>(json);

        if (response == null || response.articles == null || response.articles.Length == 0)
        {
            Debug.Log("API vazia → fallback local");
            LoadFakeNews();
            yield break;
        }

        news.Clear();

        for (int i = 0; i < response.articles.Length; i++)
        {
            news.Add(new NewsItem
            {
                title = response.articles[i].title,
                description = string.IsNullOrEmpty(response.articles[i].description)
                    ? "Sem descrição disponível"
                    : response.articles[i].description
            });
        }

        Debug.Log("API funcionando ✔ Notícias: " + news.Count);

        index = 0;
        ShowNews();
    }

    // =========================
    //  FALLBACK LOCAL
    // =========================
    void LoadFakeNews()
    {
        // Debug.Log("USANDO NOTÍCIAS LOCAIS");


        news.Clear();

        news.Add(new NewsItem
        
        {
            title = "Algoritmos moldam decisões diárias",
            description = "Estudos indicam influência crescente no comportamento humano."
        });
        news.Add(new NewsItem
        
        {
            title = "Sistema detecta comportamento incomum",
            description = "Usuários estão sendo analisados por padrões invisíveis."
        });

        news.Add(new NewsItem
        {
            title = "Nova tecnologia prevê escolhas",
            description = "Sistema aprende com cada interação do usuário."
        });

        news.Add(new NewsItem
        {
            title = "Plataformas ajustam conteúdo em tempo real",
            description = "O que você vê pode mudar com base no seu comportamento recente."
        });

        news.Add(new NewsItem
        {
            title = "Dados pessoais redefinem experiências digitais",
            description = "Cada interação contribui para um perfil único dentro dos sistemas."
        });

        index = 0;
        ShowNews();
    }

    // =========================
    void ShowNews()
    {
        if (finalizado) return;

        if (news.Count == 0)
        {
            titleText.text = "Carregando notícias...";
            newsText.text = "Verificando conexão...";
            return;
        }

        if (index >= news.Count)
        {
            FinishGame();
            return;
        }

        titleText.text = news[index].title;
        newsText.text = FormatDescription(news[index].description);

        Debug.Log("Mostrando: " + news[index].title);
    }

    // =========================
    public void EscolhaEmocional() { emocional++; Next(); }
    public void EscolhaRacional() { racional++; Next(); }
    public void EscolhaCurioso() { curioso++; Next(); }

    void Next()
    {
        index++;
        ShowNews();
    }

    // =========================
    void FinishGame()
    {
        if (jaFinalizou) return;

        jaFinalizou = true;
        finalizado = true;

        emotionalBtn.SetActive(false);
        rationalBtn.SetActive(false);
        curiousBtn.SetActive(false);
        mensagemUI.SetActive(false);
        fecharBtn.SetActive(true);

        string result;

        int max = Mathf.Max(emocional, racional, curioso);

        if (emocional == max)
            result = "Seu Perfil: emocional\n\n" +
         "Você reage pelo impacto imediato.\n" +
         "Conteúdos intensos capturam sua atenção rapidamente.\n\n" +
         "Algoritmos tendem a amplificar esse comportamento,\n" +
         "mostrando cada vez mais estímulos que provocam reação.\n\n" +
         "Cuidado: nem tudo que impacta é verdadeiro.";

        else if (racional == max)
            result = "Seu Perfil: racional\n\n" +
         "Você analisa antes de agir.\n" +
         "Busca entender antes de aceitar uma informação.\n\n" +
         "Algoritmos podem reforçar esse padrão,\n" +
         "limitando você a conteúdos que confirmam sua lógica.\n\n" +
         "Cuidado: até a razão pode virar uma bolha.";

        else
            result = "Seu Perfil: curioso\n\n" +
         "Você explora o desconhecido.\n" +
         "Novidades e mistérios chamam sua atenção.\n\n" +
         "Algoritmos aprendem isso rapidamente,\n" +
         "guiando você por caminhos cada vez mais específicos.\n\n" +
         "Cuidado: nem toda descoberta leva à verdade.";


        Debug.Log("RESULTADO FINAL: " + result);

        StartCoroutine(ShowFinalText(result));

        StartCoroutine(AumentarLuz());
    }


//Aumentar a luz
    IEnumerator AumentarLuz()
    {
        float intensidadeInicial = globalLight.intensity;

        float tempo = 0f;

        while (tempo < 1f)
        {
            tempo += Time.deltaTime * velocidadeTransicao;

            globalLight.intensity = Mathf.Lerp(intensidadeInicial, intensidadeFinal, tempo);

            yield return null;
        }

        globalLight.intensity = intensidadeFinal;
    }

    IEnumerator ShowFinalText(string result)
    {
        newsText.text = "";
        resultText.text = "";
        titleText.text = "";

        typewriter.ShowText(titleText, "RESULTADO FINAL");

        yield return new WaitForSecondsRealtime(1.5f);

        typewriter.ShowText(resultText, result);
    }

    // =========================
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
        public string description;
    }

    [Header("Luz Global")]
    public Light2D globalLight;
    public float intensidadeFinal = 1f;
    public float velocidadeTransicao = 2f;
}