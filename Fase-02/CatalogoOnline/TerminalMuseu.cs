using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class TerminalMuse : MonoBehaviour
{
    [Header("UI")]
    public GameObject painel;
    public Image imagemUI;

    [Header("Loading UI")]
    public GameObject loadingUI;
    public Slider loadingBar;

    [Header("API")]
    public string apiKey;

    private void Start()
    {
        if (painel != null)
            painel.SetActive(false);

        if (loadingUI != null)
            loadingUI.SetActive(false);

        if (loadingBar != null)
            loadingBar.value = 0;
    }

    private void OnMouseDown()
    {
        painel.SetActive(true);
        StartCoroutine(CarregarImagemPorPerfil());
    }

    IEnumerator CarregarImagemPorPerfil()
    {
        string tema = ObterTemaPorPerfil();

        // 🔵 ativa loading
        if (loadingUI != null)
            loadingUI.SetActive(true);

        if (loadingBar != null)
            loadingBar.value = 0;

        string url =
            "https://api.unsplash.com/photos/random?query=" +
            tema +
            "&client_id=" + apiKey;

        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SendWebRequest();

        // 📡 simula progresso de download da API
        while (!request.isDone)
        {
            if (loadingBar != null)
                loadingBar.value = request.downloadProgress;

            yield return null;
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Erro API: " + request.error);

            if (loadingUI != null)
                loadingUI.SetActive(false);

            yield break;
        }

        string json = request.downloadHandler.text;
        string imageUrl = ExtrairUrl(json);

        UnityWebRequest imgReq = UnityWebRequestTexture.GetTexture(imageUrl);
        imgReq.SendWebRequest();

        // 📡 progresso da imagem
        while (!imgReq.isDone)
        {
            if (loadingBar != null)
                loadingBar.value = imgReq.downloadProgress;

            yield return null;
        }

        if (imgReq.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Erro imagem: " + imgReq.error);

            if (loadingUI != null)
                loadingUI.SetActive(false);

            yield break;
        }

        Texture2D tex = DownloadHandlerTexture.GetContent(imgReq);

        imagemUI.sprite = Sprite.Create(
            tex,
            new Rect(0, 0, tex.width, tex.height),
            new Vector2(0.5f, 0.5f)
        );

        // 🟢 desativa loading
        if (loadingUI != null)
            loadingUI.SetActive(false);

        if (loadingBar != null)
            loadingBar.value = 0;
    }

    // 🧠 PERFIL DA FASE 01 (GameData)
    string ObterTemaPorPerfil()
    {
        if (GameData.instance == null)
            return "digital abstract art";

        switch (GameData.instance.perfilSofia)
        {
            case Perfil.Emocional:
                return "glitch emotion chaos face distortion";

            case Perfil.Racional:
                return "data algorithm code network structure";

            case Perfil.Curioso:
                return "exploration mystery unknown universe discovery";

            default:
                return "identity digital abstract system";
        }
    }

    // 📡 extrai URL da resposta JSON
    string ExtrairUrl(string json)
    {
        int start = json.IndexOf("\"regular\":\"") + 11;
        int end = json.IndexOf("\"", start);

        return json.Substring(start, end - start).Replace("\\/", "/");
    }
}