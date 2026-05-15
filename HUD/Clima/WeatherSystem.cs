// using UnityEngine;
// using UnityEngine.Networking;
// using System.Collections;
// using TMPro;
    
// public class WeatherSystem : MonoBehaviour
// {
//     [Header("Config API")]
//     public string apiKey = "abff6e913b0e6bd87777071c0916fb36";
//     public string cidade = "Sao Paulo";

//     [Header("UI")]
//     public SpriteRenderer iconeSol;
//     public SpriteRenderer iconeChuva;
//     public SpriteRenderer iconeLua;
//     public SpriteRenderer iconeNublado;
//     public TMP_Text temperaturaText;

//     void Start()
//     {
//         InvokeRepeating("AtualizarClima", 0f, 300f);
//         StartCoroutine(GetWeather());
//     }

//     IEnumerator GetWeather()
//     {
//         string url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&appid={apiKey}&units=metric&lang=pt_br";

//         UnityWebRequest www = UnityWebRequest.Get(url);
//         yield return www.SendWebRequest();

//         if (www.result != UnityWebRequest.Result.Success)
//         {
//             Debug.LogError("Erro API: " + www.error);
//         }
//         else
//         {
//             string json = www.downloadHandler.text;
//             Debug.Log(json);

//             ProcessarClima(json);
//         }
//     }

//     void AtualizarClima()
//         {
//             StartCoroutine(GetWeather());
//         }

//     // 📦 CLASSES PARA LER JSON
//     [System.Serializable]
//     public class WeatherData
//     {
//         public WeatherInfo[] weather;
//         public MainData main;
//     }

//     [System.Serializable]
//     public class WeatherInfo
//     {
//         public string main;
//         public string icon; // 🔥 adiciona isso
//     }

//     [System.Serializable]
//     public class MainData
//     {
//         public float temp;
//     }

//     void ProcessarClima(string json)
//     {
//         WeatherData data = JsonUtility.FromJson<WeatherData>(json);
//         int hora = System.DateTime.Now.Hour;
//         bool isNight = hora >= 18 || hora < 6;

//         // Debug.Log("Hora PC: " + hora + " | Noite: " + isNight);
                

//         if (data == null || data.weather.Length == 0)
//         {
//             Debug.LogError("Erro ao ler dados do clima");
//             return;
//         }

//         string clima = data.weather[0].main.ToLower();
//         float temperatura = data.main.temp;

//         // 🌡️ Atualiza temperatura
//         if (temperaturaText != null)
//             temperaturaText.text = Mathf.RoundToInt(temperatura) + "°C";

//         // 🔄 Desativa todos
//         if (iconeSol != null) iconeSol.SetActive(false);
//         if (iconeChuva != null) iconeChuva.SetActive(false);
//         if (iconeNublado != null) iconeNublado.SetActive(false);
//         if (iconeLua != null) iconeLua.SetActive(false);

//         // 🌦️ Decide clima
//        if (clima.Contains("rain"))
// {
//     if (iconeChuva != null) iconeChuva.SetActive(true);
//     // Debug.Log("Chuva 🌧️");
// }
// else if (clima.Contains("clear"))
// {
//     if (isNight)
//     {
//         if (iconeLua != null) iconeLua.SetActive(true);
//         // Debug.Log("Noite 🌙");
//     }
//     else
//     {
//         if (iconeSol != null) iconeSol.SetActive(true);
//         // Debug.Log("Sol ☀️");
//     }
// }
// else if (clima.Contains("cloud"))
// {
//     if (iconeNublado != null) iconeNublado.SetActive(true);
//     // Debug.Log("Nublado ☁️");
// }
// else
// {
//     if (iconeNublado != null) iconeNublado.SetActive(true);
//     // Debug.Log("Outro clima → Nublado ☁️");
// }
//     }
// }










using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using UnityEngine.Rendering; // 👈 IMPORTANTE

public class WeatherSystem : MonoBehaviour
{
    [Header("Config API")]
    public string apiKey = "SUA_API_AQUI";
    public string cidade = "Sao Paulo";

    [Header("Sprites (mundo 2D)")]
    public SpriteRenderer iconeSol;
    public SpriteRenderer iconeChuva;
    public SpriteRenderer iconeLua;
    public SpriteRenderer iconeNublado;

    [Header("Temperatura (placa no mundo)")]
    public TextMeshPro temperaturaText;// 👈 agora aparece no Inspector

    void Start()
    {
        ConfigurarSortingTexto(); // 👈 aplica ordem 2D
        InvokeRepeating(nameof(AtualizarClima), 0f, 300f);
    }

    void ConfigurarSortingTexto()
    {
        if (temperaturaText == null) return;

        // Pega ou cria SortingGroup
        SortingGroup sorting = temperaturaText.GetComponent<SortingGroup>();

        if (sorting == null)
            sorting = temperaturaText.gameObject.AddComponent<SortingGroup>();

        sorting.sortingLayerName = "Foreground"; // mesma layer da placa
        sorting.sortingOrder = 14; // maior que a placa
    }

    void AtualizarClima()
    {
        StartCoroutine(GetWeather());
    }

    IEnumerator GetWeather()
    {
        string url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&appid={apiKey}&units=metric&lang=pt_br";

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Erro API: " + www.error);
        }
        else
        {
            string json = www.downloadHandler.text;
            ProcessarClima(json);
        }
    }

    // 📦 CLASSES JSON
    [System.Serializable]
    public class WeatherData
    {
        public WeatherInfo[] weather;
        public MainData main;
    }

    [System.Serializable]
    public class WeatherInfo
    {
        public string main;
        public string icon;
    }

    [System.Serializable]
    public class MainData
    {
        public float temp;
    }

    void ProcessarClima(string json)
    {
        WeatherData data = JsonUtility.FromJson<WeatherData>(json);

        if (data == null || data.weather.Length == 0)
        {
            Debug.LogError("Erro ao ler dados do clima");
            return;
        }

        int hora = System.DateTime.Now.Hour;
        bool isNight = hora >= 18 || hora < 6;

        string clima = data.weather[0].main.ToLower();
        float temperatura = data.main.temp;

        // 🌡️ Temperatura
        if (temperaturaText != null)
            temperaturaText.text = Mathf.RoundToInt(temperatura) + "°C";

        // 🔄 Desativa todos
        if (iconeSol != null) iconeSol.enabled = false;
        if (iconeChuva != null) iconeChuva.enabled = false;
        if (iconeLua != null) iconeLua.enabled = false;
        if (iconeNublado != null) iconeNublado.enabled = false;

        // 🌦️ Decide clima
        // 🌙 PRIORIDADE MÁXIMA: NOITE
        if (isNight)
        {
            if (iconeLua != null) iconeLua.enabled = true;
        }
        else
        {
            // 🌦️ Clima só manda durante o dia
            if (clima.Contains("rain"))
            {
                if (iconeChuva != null) iconeChuva.enabled = true;
            }
            else if (clima.Contains("clear"))
            {
                if (iconeSol != null) iconeSol.enabled = true;
            }
            else if (clima.Contains("cloud"))
            {
                if (iconeNublado != null) iconeNublado.enabled = true;
            }
            else
            {
                if (iconeNublado != null) iconeNublado.enabled = true;
            }
        }
    }
}