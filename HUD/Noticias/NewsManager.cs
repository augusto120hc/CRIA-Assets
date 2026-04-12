// using UnityEngine;
// using TMPro;
// using System.Collections;
// using UnityEngine.Networking;
// using System.Collections.Generic;

// public class NewsManager : MonoBehaviour
// {
//     public TMP_Text titleText;
//     public TMP_Text descriptionText;

//     private List<Article> articles = new List<Article>();
//     private int currentIndex = 0;

//     void Start()
//     {
//         StartCoroutine(GetNews());
//     }

//     IEnumerator GetNews()
//     {
//         string url = "https://newsapi.org/v2/top-headlines?country=br&language=pt&apiKey=fee7637190414418b0f399454b4a25c6";

//         UnityWebRequest request = UnityWebRequest.Get(url);
//         yield return request.SendWebRequest();

//         if (request.result != UnityWebRequest.Result.Success)
//         {
//             Debug.Log("Erro: " + request.error);
//             titleText.text = "Erro ao conectar";
//             yield break;
//         }

//         string json = request.downloadHandler.text;
//         Debug.Log("BR: " + json);

//         NewsWrapper wrapper = SafeParse(json);

//         // 🔥 fallback automático
//         if (wrapper == null || wrapper.articles == null || wrapper.articles.Length == 0)
//         {
//             Debug.Log("Brasil vazio, tentando US...");
//             yield return StartCoroutine(GetNewsUS());
//             yield break;
//         }

//         articles = new List<Article>(wrapper.articles);
//         currentIndex = 0;
//         ShowNews();
//     }

//     // 🔥 MÉTODO QUE FALTAVA
//     IEnumerator GetNewsUS()
//     {
//         string url = "https://newsapi.org/v2/top-headlines?country=us&apiKey=fee7637190414418b0f399454b4a25c6";

//         UnityWebRequest request = UnityWebRequest.Get(url);
//         yield return request.SendWebRequest();

//         if (request.result != UnityWebRequest.Result.Success)
//         {
//             Debug.Log("Erro US: " + request.error);
//             titleText.text = "Sem noticias";
//             yield break;
//         }

//         string json = request.downloadHandler.text;
//         Debug.Log("US: " + json);

//         NewsWrapper wrapper = SafeParse(json);

//         if (wrapper == null || wrapper.articles == null || wrapper.articles.Length == 0)
//         {
//             titleText.text = "Sem noticias";
//             descriptionText.text = "";
//             yield break;
//         }

//         articles = new List<Article>(wrapper.articles);
//         currentIndex = 0;
//         ShowNews();
//     }

//     void ShowNews()
// {
//     if (articles == null || articles.Count == 0)
//     {
//         titleText.text = "Sem noticias";
//         descriptionText.text = "";
//         return;
//     }

//     var article = articles[currentIndex];

//     // mostra enquanto traduz (efeito bonito)
//     titleText.text = "Traduzindo...";
//     descriptionText.text = "";

//     StartCoroutine(TranslateText(article.title, (translatedTitle) =>
//     {
//         titleText.text = translatedTitle;
//     }));

//     StartCoroutine(TranslateText(article.description, (translatedDesc) =>
//     {
//         descriptionText.text = translatedDesc;
//     }));
// }

//     public void NextNews()
//     {
//         if (articles == null || articles.Count == 0) return;

//         currentIndex++;

//         if (currentIndex >= articles.Count)
//             currentIndex = 0;

//         ShowNews();
//     }

//     // 🧠 parser seguro (evita crash)
//     NewsWrapper SafeParse(string json)
//     {
//         try
//         {
//             string fixedJson = "{\"articles\":" + json.Split(new string[] { "\"articles\":" }, System.StringSplitOptions.None)[1];
//             return JsonUtility.FromJson<NewsWrapper>(fixedJson);
//         }
//         catch
//         {
//             Debug.Log("Erro ao parsear JSON");
//             return null;
//         }
//     }

//     IEnumerator TranslateText(string text, System.Action<string> callback)
// {
//     if (string.IsNullOrEmpty(text))
//     {
//         callback("");
//         yield break;
//     }

//     string url = "https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl=pt&dt=t&q=" + UnityWebRequest.EscapeURL(text);

//     UnityWebRequest request = UnityWebRequest.Get(url);
//     yield return request.SendWebRequest();

//     if (request.result == UnityWebRequest.Result.Success)
//     {
//         string result = request.downloadHandler.text;

//         // 🧠 extrai só o texto traduzido
//         string translated = ExtractTranslation(result);
//         callback(translated);
//     }
//     else
//     {
//         Debug.Log("Erro tradução: " + request.error);
//         callback(text);
//     }
// }

// string ExtractTranslation(string json)
// {
//     try
//     {
//         // resposta vem tipo: [[["texto traduzido","original",...]]]
//         int firstQuote = json.IndexOf('\"') + 1;
//         int secondQuote = json.IndexOf('\"', firstQuote);
//         return json.Substring(firstQuote, secondQuote - firstQuote);
//     }
//     catch
//     {
//         return json;
//     }
// }
// }

// [System.Serializable]
// public class NewsWrapper
// {
//     public Article[] articles;
// }

// [System.Serializable]
// public class Article
// {
//     public string title;
//     public string description;
// }

// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







// using UnityEngine;
// using UnityEngine.Networking;
// using System.Collections;
// using TMPro;
// using System.Xml;

// public class NewsSystem : MonoBehaviour
// {
//     [Header("Config API")]
//     public string apiKey = "62253ab6730e4866a34cfeb323331d4d";
//     public string apiURL = "https://api.mediastack.com/v1/news?access_key=";

//     [Header("RSS Brasil")]
//     public string rssURL = "https://g1.globo.com/rss/g1/";

//     [Header("UI")]
//     public TextMeshProUGUI textoNoticias;
//     public TextMeshProUGUI textoBotao;

//     private bool usarRSS = false;

//     void Start()
//     {
//         if (textoNoticias == null || textoBotao == null)
//         {
//             Debug.LogError("⚠️ UI não configurada no Inspector!");
//             return;
//         }

//         textoBotao.text = "Fonte: Internacional";
//         AtualizarNoticias();
//     }

//     public void AlternarFonte()
//     {
//         usarRSS = !usarRSS;

//         if (usarRSS)
//             textoBotao.text = "Fonte: Brasil";
//         else
//             textoBotao.text = "Fonte: Internacional";

//         AtualizarNoticias();
//     }

//     void AtualizarNoticias()
//     {
//         StopAllCoroutines();

//         if (usarRSS)
//             StartCoroutine(BuscarNoticiasRSS());
//         else
//             StartCoroutine(BuscarNoticiasAPI());
//     }

//     IEnumerator BuscarNoticiasAPI()
//     {
//         string url = apiURL + apiKey + "&countries=br";

//         Debug.Log("URL: " + url);

//         UnityWebRequest request = UnityWebRequest.Get(url);
//         yield return request.SendWebRequest();

//         if (request.result != UnityWebRequest.Result.Success)
//         {
//             textoNoticias.text = "Erro ao carregar API";
//         }
//         else
//         {
//             string json = request.downloadHandler.text;

//             if (string.IsNullOrEmpty(json) || json.Contains("error"))
//             {
//                 textoNoticias.text = "Erro na API ou chave inválida";
//                 Debug.Log(json);
//             }
//             else
//             {
//                 NewsResponse response = JsonUtility.FromJson<NewsResponse>(json);

//                 if (response != null && response.data != null)
//                 {
//                     string resultado = "";

//                     int count = Mathf.Min(5, response.data.Length);

//                     for (int i = 0; i < count; i++)
//                     {
//                         resultado += "• " + response.data[i].title + "\n\n";
//                     }

//                     textoNoticias.text = resultado;
//                 }
//                 else
//                 {
//                     textoNoticias.text = "Sem notícias disponíveis";
//                 }
//             }
//         }
//     }

//     IEnumerator BuscarNoticiasRSS()
//     {
//         UnityWebRequest request = UnityWebRequest.Get(rssURL);
//         yield return request.SendWebRequest();

//         if (request.result != UnityWebRequest.Result.Success)
//         {
//             textoNoticias.text = "Erro ao carregar RSS";
//         }
//         else
//         {
//             string xmlData = request.downloadHandler.text;

//             XmlDocument xmlDoc = new XmlDocument();
//             xmlDoc.LoadXml(xmlData);

//             XmlNodeList items = xmlDoc.GetElementsByTagName("item");

//             string resultado = "";

//             int count = Mathf.Min(5, items.Count);

//             for (int i = 0; i < count; i++)
//             {
//                 string titulo = items[i]["title"].InnerText;
//                 resultado += "• " + titulo + "\n\n";
//             }

//             textoNoticias.text = resultado;
//         }
//     }

//     // =========================
//     // CLASSES PARA JSON
//     // =========================

//     [System.Serializable]
//     public class NewsResponse
//     {
//         public NewsData[] data;
//     }

//     [System.Serializable]
//     public class NewsData
//     {
//         public string title;
//     }
// }



// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// public string apiKey = "ca198cc998840e2840233f3c56ac9072";






// using UnityEngine;
// using TMPro;
// using System.Collections;
// using UnityEngine.Networking;
// using System.Collections.Generic;

// public class NewsManager : MonoBehaviour
// {
//     // =========================
//     // 🔒 CONTROLE
//     // =========================
//     private bool terminou = false;
//     private bool isRequesting = false;

//     // =========================
//     // 🎮 UI
//     // =========================
//     [Header("UI")]
//     public TMP_Text titleText;
//     public TMP_Text descriptionText;

//     // =========================
//     // 🌍 API
//     // =========================
//     [Header("API")]
//     public string apiKey = "de0531cb0a64ae2b1572d5697ffa2a42";
//     public string apiURL = "https://api.mediastack.com/v1/news?access_key=";

//     private List<Article> articles = new List<Article>();
//     private int currentIndex = 0;

//     // =========================
//     // 🎮 PERFIL DO JOGADOR
//     // =========================
//     public int emocional;
//     public int racional;
//     public int curioso;

//     private int escolhas = 0;

//     // public TypewriterEffect typewriter;

//     // =========================
//     // START
//     // =========================
//     void Start()
//     {
//         // StartCoroutine(GetNewsBR());
//         StartCoroutine(InitNews());
//     }

//     IEnumerator InitNews()
//     {
//         yield return new WaitForSeconds(0.5f); // pequeno buffer

//         yield return StartCoroutine(GetNewsBR());
//     }
//     // =========================
//     // 🌍 BUSCAR NOTÍCIAS BR
//     // =========================
//     IEnumerator GetNewsBR()
//     {
//         if (isRequesting)
//             yield break;

//         isRequesting = true;

//         string url = apiURL + apiKey + "&countries=br&languages=pt";

//         using (UnityWebRequest request = UnityWebRequest.Get(url))
//         {
//             yield return request.SendWebRequest();

//             if (request.result != UnityWebRequest.Result.Success)
//             {
//                 Debug.Log("Erro BR: " + request.error);
//                 yield return StartCoroutine(GetNewsFallback());
//                 isRequesting = false;
//                 yield break;
//             }

//             string json = request.downloadHandler.text;

//             MediastackResponse response =
//                 JsonUtility.FromJson<MediastackResponse>(json);

//             if (response == null || response.data == null || response.data.Length == 0)
//             {
//                 Debug.Log("Fallback ativado...");
//                 yield return StartCoroutine(GetNewsFallback());
//                 isRequesting = false;
//                 yield break;
//             }

//             ConverterNoticias(response.data);
//         }

//         yield return new WaitForSeconds(10f);
//         isRequesting = false;
//     }

//     // =========================
//     // 🔁 FALLBACK
//     // =========================
//     IEnumerator GetNewsFallback()
//     {
//         string url = apiURL + apiKey + "&languages=en";

//         using (UnityWebRequest request = UnityWebRequest.Get(url))
//         {
//             yield return request.SendWebRequest();

//             if (request.result != UnityWebRequest.Result.Success)
//             {
//                 titleText.text = "Sem notícias";
//                 descriptionText.text = "";
//                 yield break;
//             }

//             string json = request.downloadHandler.text;

//             MediastackResponse response =
//                 JsonUtility.FromJson<MediastackResponse>(json);

//             if (response == null || response.data == null || response.data.Length == 0)
//             {
//                 titleText.text = "Sem notícias";
//                 descriptionText.text = "";
//                 yield break;
//             }

//             ConverterNoticias(response.data);
//         }
//     }

//     // =========================
//     // 🔄 CONVERTER DADOS
//     // =========================
//     void ConverterNoticias(MediastackArticle[] data)
//     {
//         articles.Clear();

//         foreach (var item in data)
//         {
//             articles.Add(new Article
//             {
//                 title = item.title,
//                 description = item.description
//             });
//         }

//         currentIndex = 0;
//         ShowNews();
//     }

//     // =========================
//     // 📰 MOSTRAR NOTÍCIA
//     // =========================
//     void ShowNews()
// {
//     if (terminou) return;

//     if (articles == null || articles.Count == 0)
//     {
//         titleText.text = "Sem notícias";
//         descriptionText.text = "";
//         return;
//     }

//     var article = articles[currentIndex];

//     titleText.text = "";
//     descriptionText.text = "";

//     StartCoroutine(TranslateText(article.title, (t) =>
//     {
//         if (!terminou)
//             StartCoroutine(TypeText(titleText, t));
//     }));

//     StartCoroutine(TranslateText(article.description, (d) =>
//     {
//         if (!terminou)
//             StartCoroutine(TypeText(descriptionText, d));
//     }));
// }

//     // =========================
//     // ⏭ PRÓXIMA NOTÍCIA
//     // =========================
//     public void NextNews()
//     {
//         if (articles == null || articles.Count == 0) return;

//         currentIndex++;

//         if (currentIndex >= articles.Count)
//             currentIndex = 0;

//         ShowNews();
//     }

//     // =========================
//     // 🎮 ESCOLHAS
//     // =========================
//     public void EscolherEmocional()
//     {
//         emocional++;
//         ProximaEtapa();
//     }

//     public void EscolherRacional()
//     {
//         racional++;
//         ProximaEtapa();
//     }

//     public void EscolherCurioso()
//     {
//         curioso++;
//         ProximaEtapa();
//     }

//     void ProximaEtapa()
//     {
//         escolhas++;

//         if (escolhas >= 3)
//         {
//             terminou = true;
//             StopAllCoroutines();
//             MostrarResultado();
//         }
//         else
//         {
//             NextNews();
//         }
//     }

//     // =========================
//     // 🧠 RESULTADO FINAL
//     // =========================



//     IEnumerator TypeText(TMP_Text target, string text)
//         {
//             target.text = "";

//             foreach (char c in text)
//             {
//                 target.text += c;
//                 yield return new WaitForSeconds(0.03f);
//             }
//         }


//             void MostrarResultado()
//         {

//             // Debug.Log("Erro BR: " + request.error);
//             int max = Mathf.Max(emocional, racional, curioso);

//             string resultado;

//             if (emocional == max)
//             {
//                 resultado = "Você reage antes de pensar.";
//             }
//             else if (racional == max)
//             {
//                 resultado = "Você analisa antes de agir.";
//             }
//             else
//             {
//                 resultado = "Você busca o desconhecido.";
//             }

//             titleText.text = "Perfil identificado";
//             descriptionText.text = "";

//             StartCoroutine(TypeText(descriptionText, resultado + "\n\nSeu perfil influenciará o mundo."));
//         }

//     // =========================
//     // 🌍 TRADUÇÃO
//     // =========================
//     IEnumerator TranslateText(string text, System.Action<string> callback)
//     {
//         if (string.IsNullOrEmpty(text))
//         {
//             callback("");
//             yield break;
//         }

//         string url =
//             "https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl=pt&dt=t&q="
//             + UnityWebRequest.EscapeURL(text);

//         using (UnityWebRequest request = UnityWebRequest.Get(url))
//         {
//             yield return request.SendWebRequest();

//             if (request.result == UnityWebRequest.Result.Success)
//             {
//                 callback(ExtractTranslation(request.downloadHandler.text));
//             }
//             else
//             {
//                 callback(text);
//             }
//         }
//     }

//     string ExtractTranslation(string json)
//     {
//         try
//         {
//             int first = json.IndexOf('"') + 1;
//             int second = json.IndexOf('"', first);
//             return json.Substring(first, second - first);
//         }
//         catch
//         {
//             return json;
//         }
//     }

//     // =========================
//     // 📦 DADOS
//     // =========================
//     public Article GetCurrentArticle()
//     {
//         if (articles == null || articles.Count == 0)
//             return null;

//         return articles[currentIndex];
//     }
// }

// // =========================
// // 📦 MODELOS
// // =========================
// [System.Serializable]
// public class MediastackResponse
// {
//     public MediastackArticle[] data;
// }

// [System.Serializable]
// public class MediastackArticle
// {
//     public string title;
//     public string description;
// }

// [System.Serializable]
// public class Article
// {
//     public string title;
//     public string description;
// }