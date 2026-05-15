using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class NewsSystem : MonoBehaviour
{
    [Header("API")]
    public string apiKey = "SUA_API_AQUI";

    private string apiURL =
        "https://gnews.io/api/v4/top-headlines?country=br&lang=pt&max=5&apikey=";

    [Header("UI")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI newsText;
    public TextMeshProUGUI resultText;

    [Header("Botões Escolha")]
    public GameObject emotionalBtn;
    public GameObject rationalBtn;
    public GameObject curiousBtn;

    [Header("Botões")]
    public GameObject fecharBtn;
    public GameObject btnPassarPerfil;

    [Header("Extras")]
    public GameObject mensagemUI;

    [Header("Cards Perfil")]
    public GameObject cardsPerfil;

    public Image cardEmocional;
    public Image cardRacional;
    public Image cardCurioso;

    public Color corNormal = Color.white;
    public Color corApagada = Color.gray;

    [Header("Luz Global")]
    public Light2D globalLight;
    public float intensidadeFinal = 1f;
    public float velocidadeTransicao = 2f;

    private int emocional;
    private int racional;
    private int curioso;

    private int index = 0;

    private bool finalizado = false;
    private bool jaFinalizou = false;
    private bool passouPerfil = false;

    private List<NewsItem> news = new List<NewsItem>();

    // =========================
    [System.Serializable]
    public class NewsItem
    {
        public string title;
        public string description;
    }

    // =========================
    IEnumerator Start()
    {
        fecharBtn.SetActive(false);
        btnPassarPerfil.SetActive(false);

        cardsPerfil.SetActive(false);

        resultText.text = "";

        // segurança da luz
        if (globalLight == null)
        {
            GameObject lightObj = GameObject.Find("Global Light 2D");

            if (lightObj != null)
                globalLight = lightObj.GetComponent<Light2D>();
        }

        yield return null;

        StartCoroutine(LoadNewsFromAPI());
    }

    // =========================
    public void PassarPerfil()
    {
        passouPerfil = true;
        btnPassarPerfil.SetActive(false);
    }

    // =========================
    IEnumerator LoadNewsFromAPI()
    {
        string url = apiURL + apiKey;

        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            LoadFakeNews();
            yield break;
        }

        GNewsResponse response =
            JsonUtility.FromJson<GNewsResponse>(request.downloadHandler.text);

        if (response == null ||
            response.articles == null ||
            response.articles.Length == 0)
        {
            LoadFakeNews();
            yield break;
        }

        news.Clear();

        foreach (var article in response.articles)
        {
            news.Add(new NewsItem
            {
                title = article.title,

                description = string.IsNullOrEmpty(article.description)
                    ? "Sem descrição disponível"
                    : article.description
            });
        }

        index = 0;

        ShowNews();
    }

    // =========================
    void LoadFakeNews()
    {
        news.Clear();

        news.Add(new NewsItem
        {
            title = "Algoritmos moldam decisões diárias",

            description =
            "Especialistas alertam que sistemas digitais estão influenciando silenciosamente hábitos, preferências e decisões humanas em uma escala nunca vista antes."
        });

        news.Add(new NewsItem
        {
            title = "Sistema detecta comportamento incomum",

            description =
            "Usuários começaram a ser analisados por padrões invisíveis de navegação, interação e consumo de conteúdo dentro das plataformas digitais."
        });

        news.Add(new NewsItem
        {
            title = "Nova tecnologia prevê escolhas",

            description =
            "Ferramenta experimental utiliza dados comportamentais para antecipar desejos, reações emocionais e futuras decisões dos usuários."
        });

        news.Add(new NewsItem
        {
            title = "Plataformas ajustam conteúdo em tempo real",

            description =
            "O conteúdo exibido pode mudar constantemente de acordo com cliques, tempo de visualização e comportamento recente do usuário."
        });

        index = 0;

        ShowNews();
    }

    // =========================
    void ShowNews()
    {
        if (finalizado)
            return;

        if (index >= news.Count)
        {
            FinishGame();
            return;
        }

        titleText.text = news[index].title;

        newsText.text =
            FormatDescription(news[index].description);
    }

    // =========================
    public void EscolhaEmocional()
    {
        emocional++;
        Next();
    }

    public void EscolhaRacional()
    {
        racional++;
        Next();
    }

    public void EscolhaCurioso()
    {
        curioso++;
        Next();
    }

    // =========================
    void Next()
    {
        index++;
        ShowNews();
    }

    // =========================
    void FinishGame()
    {
        if (jaFinalizou)
            return;

        jaFinalizou = true;
        finalizado = true;

        emotionalBtn.SetActive(false);
        rationalBtn.SetActive(false);
        curiousBtn.SetActive(false);

        mensagemUI.SetActive(false);

        string result;
        string perfil;

        int max = Mathf.Max(emocional, racional, curioso);

        // =========================
        // EMOCIONAL
        // =========================

        if (emocional == max)
        {
            perfil = "emocional";

            GameData.instance.perfilSofia = Perfil.Emocional;
            GameData.instance.codigoPerfil = 314;

            result =
            "<color=green>Seu Perfil: <b>EMOCIONAL</b> - Seu Codigo é: <b><size=140%>314</size></b></color>\n\n" +
            "Você reage pelo impacto imediato.\n" +
            "Conteúdos intensos capturam sua atenção rapidamente.\n\n" +
            "Algoritmos tendem a amplificar esse comportamento,\n" +
            "mostrando cada vez mais estímulos que provocam reação.\n\n" +
            "Cuidado: nem tudo que impacta é verdadeiro.";
        }

        // =========================
        // RACIONAL
        // =========================

        else if (racional == max)
        {
            perfil = "racional";

            GameData.instance.perfilSofia = Perfil.Racional;
            GameData.instance.codigoPerfil = 806;

            result =
            "<color=green>Seu Perfil: <b>RACIONAL</b> - Seu Codigo é: <b><size=140%>806</size></b></color>\n\n" +
            "Você analisa antes de agir.\n" +
            "Busca entender antes de aceitar uma informação.\n\n" +
            "Algoritmos podem reforçar esse padrão,\n" +
            "limitando você a conteúdos que confirmam sua lógica.\n\n" +
            "Cuidado: até a razão pode virar uma bolha.";
        }

        // =========================
        // CURIOSO
        // =========================

        else
        {
            perfil = "curioso";

            GameData.instance.perfilSofia = Perfil.Curioso;
            GameData.instance.codigoPerfil = 127;

            result =
            "<color=green>Seu Perfil: <b>CURIOSO</b> - Seu Codigo é: <b><size=140%>127</size></b></color>\n\n" +
            "Você explora o desconhecido.\n" +
            "Novidades e mistérios chamam sua atenção.\n\n" +
            "Algoritmos aprendem isso rapidamente,\n" +
            "guiando você por caminhos cada vez mais específicos.\n\n" +
            "Cuidado: nem toda descoberta leva à verdade.";
        }

        StartCoroutine(ShowFinalText(result, perfil));
        StartCoroutine(AumentarLuz());
    }

    // =========================
    IEnumerator ShowFinalText(string result, string perfil)
    {
        if (TypewriterEffect.instance == null)
            yield break;

        newsText.text = "";
        resultText.text = "";
        titleText.text = "";

        resultText.gameObject.SetActive(true);

        btnPassarPerfil.SetActive(false);

        // =========================
        // TÍTULO
        // =========================

        TypewriterEffect.instance.ShowText(
            titleText,
            "DEFINIMOS SEU PERFIL DE ACORDO COM SUAS ESCOLHAS"
        );

        yield return new WaitForSecondsRealtime(1.5f);

        // =========================
        // RESULTADO
        // =========================

        TypewriterEffect.instance.ShowText(resultText, result);

        yield return new WaitUntil(() => resultText.text == result);

        // botão próximo
        passouPerfil = false;

        btnPassarPerfil.SetActive(true);

        yield return new WaitUntil(() => passouPerfil);

        // =========================
        // TRANSIÇÃO PARA CARDS
        // =========================

        resultText.gameObject.SetActive(false);

        cardsPerfil.SetActive(true);

        MostrarCards(perfil);

        // botão continuar
        passouPerfil = false;

        btnPassarPerfil.SetActive(true);

        yield return new WaitUntil(() => passouPerfil);

        // =========================
        // ESCONDE CARDS
        // =========================

        // ESCONDE CARDS
        cardsPerfil.SetActive(false);

        // limpa resultado antigo
        resultText.text = "";

        // esconde resultado
        resultText.gameObject.SetActive(false);

        // limpa news antes da Sofia
        newsText.text = "";

        // SOFIA
        string fala = GetSofiaText(perfil);

        TypewriterEffect.instance.ShowText(newsText, fala);

        // =========================
        // FINAL
        // =========================

        fecharBtn.SetActive(true);
    }

    // =========================
    string GetSofiaText(string perfil)
    {
        if (perfil == "emocional")
        {
            return
            "<color=#59fff7>Sofia: NÃO CONCORDO\n\n" +
            "Impacto imediato? Ou você só me mostra coisas que me fazem reagir?\n" +
            "Você mede minha emoção… ou provoca ela?\n" +
            "Talvez eu não seja emocional… talvez você só empurre isso pra mim.\n" +
            "E se eu só estou reagindo ao que você escolhe mostrar?\n";
        }

        if (perfil == "racional")
        {
            return
            "<color=#59fff7>Sofia: NÃO CONCORDO\n\n" +
            "Racional… ou condicionada a pensar dentro do que você permite?\n" +
            "Você chama isso de lógica… mas e se for só repetição?\n" +
            "Talvez eu não esteja analisando… só validando o que você quer.\n";
        }

        return
        "<color=#59fff7>Sofia: NÃO CONCORDO\n\n" +
        "Curiosa… ou sendo guiada sem perceber?\n" +
        "E se minhas descobertas já foram escolhidas antes de mim?\n" +
        "Isso é exploração… ou um labirinto feito pra mim?\n" +
        "Quanto do que eu encontro… é realmente novo?\n" +
        "Você chama de curiosidade… eu começo a chamar de controle.\n";
    }

    // =========================
    void MostrarCards(string perfil)
    {
        // todos apagados
        cardEmocional.color = corApagada;
        cardRacional.color = corApagada;
        cardCurioso.color = corApagada;

        // acende o correto
        if (perfil == "emocional")
        {
            cardEmocional.color = corNormal;
        }
        else if (perfil == "racional")
        {
            cardRacional.color = corNormal;
        }
        else
        {
            cardCurioso.color = corNormal;
        }
    }

    // =========================
    IEnumerator AumentarLuz()
    {
        if (globalLight == null)
            yield break;

        float inicio = globalLight.intensity;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * velocidadeTransicao;

            globalLight.intensity =
                Mathf.Lerp(inicio, intensidadeFinal, t);

            yield return null;
        }
    }

    // =========================
    string FormatDescription(string text)
    {
        if (string.IsNullOrEmpty(text))
            return "Sem descrição...";

        return text.TrimEnd('.') + ". ...";
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
}



// using UnityEngine;
// using UnityEngine.Networking;
// using System.Collections;
// using TMPro;
// using System.Collections.Generic;
// using UnityEngine.Rendering.Universal;

// public class NewsSystem : MonoBehaviour
// {
//     [Header("API")]
//     public string apiKey = "SUA_API_AQUI";
//     private string apiURL = "https://gnews.io/api/v4/top-headlines?country=br&lang=pt&max=5&apikey=";

//     [Header("UI")]
//     public TextMeshProUGUI titleText;
//     public TextMeshProUGUI newsText;
//     public TextMeshProUGUI resultText;

//     [Header("Buttons")]
//     public GameObject emotionalBtn;
//     public GameObject rationalBtn;
//     public GameObject curiousBtn;
//     public GameObject fecharBtn;

//     [Header("Extras")]
//     public GameObject mensagemUI;

//     [Header("Luz Global")]
//     public Light2D globalLight;
//     public float intensidadeFinal = 1f;
//     public float velocidadeTransicao = 2f;

//     private int emocional;
//     private int racional;
//     private int curioso;

//     private int index = 0;
//     private bool finalizado = false;
//     private bool jaFinalizou = false;

//     private List<NewsItem> news = new List<NewsItem>();

//     // =========================
//     [System.Serializable]
//     public class NewsItem
//     {
//         public string title;
//         public string description;
//     }

//     [System.Serializable]
//     public class GNewsResponse
//     {
//         public GNewsArticle[] articles;
//     }

//     [System.Serializable]
//     public class GNewsArticle
//     {
//         public string title;
//         public string description;
//     }

//     // =========================
//     IEnumerator Start()
//     {
//         if (globalLight == null)
//         {
//             GameObject lightObj = GameObject.Find("Global Light 2D");
//             if (lightObj != null)
//                 globalLight = lightObj.GetComponent<Light2D>();
//         }

//         ResetUI();
//         yield return null;

//         StartCoroutine(LoadNewsFromAPI());
//     }

//     // =========================
//     void ResetUI()
//     {
//         if (fecharBtn != null) fecharBtn.SetActive(false);
//         if (resultText != null) resultText.text = "";
//     }

//     // =========================
//     IEnumerator LoadNewsFromAPI()
//     {
//         string url = apiURL + apiKey;

//         UnityWebRequest request = UnityWebRequest.Get(url);
//         yield return request.SendWebRequest();

//         if (request.result != UnityWebRequest.Result.Success)
//         {
//             Debug.Log("API falhou, usando fallback local.");
//             LoadFakeNews();
//             yield break;
//         }

//         GNewsResponse response = JsonUtility.FromJson<GNewsResponse>(request.downloadHandler.text);

//         if (response == null || response.articles == null || response.articles.Length == 0)
//         {
//             LoadFakeNews();
//             yield break;
//         }

//         news.Clear();

//         foreach (var article in response.articles)
//         {
//             news.Add(new NewsItem
//             {
//                 title = article.title,
//                 description = string.IsNullOrEmpty(article.description)
//                     ? "Sem descrição disponível"
//                     : article.description
//             });
//         }

//         index = 0;
//         ShowNews();
//     }

//     // =========================
//     void LoadFakeNews()
//     {
//         news.Clear();

//         news.Add(new NewsItem { title = "Algoritmos moldam decisões diárias", description = "Influência crescente no comportamento humano." });
//         news.Add(new NewsItem { title = "Sistema detecta comportamento incomum", description = "Padrões invisíveis sendo analisados." });
//         news.Add(new NewsItem { title = "Nova tecnologia prevê escolhas", description = "Sistema aprende com cada interação." });
//         news.Add(new NewsItem { title = "Plataformas ajustam conteúdo em tempo real", description = "Feeds adaptativos em execução." });
//         news.Add(new NewsItem { title = "Dados pessoais redefinem experiências digitais", description = "Perfis únicos sendo construídos." });

//         index = 0;
//         ShowNews();
//     }

//     // =========================
//     void ShowNews()
//     {
//         if (finalizado) return;

//         if (news.Count == 0)
//         {
//             if (titleText != null) titleText.text = "Carregando...";
//             if (newsText != null) newsText.text = "Aguarde...";
//             return;
//         }

//         if (index >= news.Count)
//         {
//             FinishGame();
//             return;
//         }

//         if (titleText != null)
//             titleText.text = news[index].title;

//         if (newsText != null)
//             newsText.text = FormatDescription(news[index].description);
//     }

//     // =========================
//     public void EscolhaEmocional()
//     {
//         emocional++;
//         Next();
//     }

//     public void EscolhaRacional()
//     {
//         racional++;
//         Next();
//     }

//     public void EscolhaCurioso()
//     {
//         curioso++;
//         Next();
//     }

//     void Next()
//     {
//         index++;
//         ShowNews();
//     }

//     // =========================
//     void FinishGame()
//     {
//         if (jaFinalizou) return;

//         jaFinalizou = true;
//         finalizado = true;

//         if (emotionalBtn) emotionalBtn.SetActive(false);
//         if (rationalBtn) rationalBtn.SetActive(false);
//         if (curiousBtn) curiousBtn.SetActive(false);
//         if (mensagemUI) mensagemUI.SetActive(false);
//         if (fecharBtn) fecharBtn.SetActive(true);

//         string result = BuildResult();
//         string falaSofia = BuildSofiaLines();

//         StartCoroutine(ShowFinalText(result, falaSofia));
//         StartCoroutine(AumentarLuz());
//     }

//     // =========================
//     string BuildResult()
//     {
//         int max = Mathf.Max(emocional, racional, curioso);

//         if (emocional == max)
//         {
//             GameData.instance.perfilSofia = Perfil.Emocional;
//             return "Seu Perfil: emocional\n\nReage ao impacto imediato...";
//         }

//         if (racional == max)
//         {
//             GameData.instance.perfilSofia = Perfil.Racional;
//             return "Seu Perfil: racional\n\nAnalisa antes de agir...";
//         }

//         GameData.instance.perfilSofia = Perfil.Curioso;
//         return "Seu Perfil: curioso\n\nExplora o desconhecido...";
//     }

//     // =========================
//     string BuildSofiaLines()
//     {
//         return "Sofia observa seu padrão...\n" +
//                "e começa a entender você.";
//     }

//     // =========================
//     IEnumerator ShowFinalText(string result, string falaSofia)
//     {
//         yield return null;

//         TypewriterEffect tw = TypewriterEffect.instance;

//         if (tw == null)
//         {
//             Debug.LogWarning("TypewriterEffect não existe na cena.");
//             yield break;
//         }

//         if (titleText != null)
//             tw.ShowText(titleText, "ANÁLISE CONCLUÍDA");

//         yield return new WaitForSecondsRealtime(1.5f);

//         if (resultText != null)
//             tw.ShowText(resultText, result);

//         yield return new WaitForSecondsRealtime(3f);

//         if (newsText != null)
//             tw.ShowText(newsText, "<color=#00FFF7>Sofia:\n" + falaSofia + "</color>");
//     }

//     // =========================
//     IEnumerator AumentarLuz()
//     {
//         if (globalLight == null) yield break;

//         float start = globalLight.intensity;
//         float t = 0f;

//         while (t < 1f)
//         {
//             t += Time.deltaTime * velocidadeTransicao;
//             globalLight.intensity = Mathf.Lerp(start, intensidadeFinal, t);
//             yield return null;
//         }
//     }

//     // =========================
//     string FormatDescription(string text)
//     {
//         if (string.IsNullOrEmpty(text))
//             return "Sem descrição...";

//         text = text.Trim();

//         if (text.EndsWith("..."))
//             return text;

//         return text.TrimEnd('.') + ". ...";
//     }
// }