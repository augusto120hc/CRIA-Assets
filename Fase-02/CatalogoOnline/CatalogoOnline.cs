using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class CatalogoOnline : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject painel;
    public Image imagemUI;

    [Header("URL da imagem")]
    public string urlImagem;

    private void Start()
    {
        if (painel != null)
            painel.SetActive(false);
    }

    private void OnMouseDown()
    {
        painel.SetActive(true);
        StartCoroutine(CarregarImagem(urlImagem));
    }

    IEnumerator CarregarImagem(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        request.SetRequestHeader("User-Agent", "Mozilla/5.0");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Erro ao carregar imagem: " + request.error);
            yield break;
        }

        Texture2D texture = DownloadHandlerTexture.GetContent(request);

        Sprite sprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f)
        );

        imagemUI.sprite = sprite;
    }
}