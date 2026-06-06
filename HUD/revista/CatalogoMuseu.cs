using UnityEngine;
using UnityEngine.UI;

public class CatalogoMuseu : MonoBehaviour
{
    [Header("UI")]
    public GameObject painel;
    public Image imagemUI;
    public Button botaoProximo;

    [Header("Catálogo por Perfil")]
    public Sprite[] emocional;
    public Sprite[] racional;
    public Sprite[] curioso;

    private Sprite[] catalogoAtual;
    private int index = 0;

    private void Start()
    {
        painel.SetActive(false);

        botaoProximo.onClick.AddListener(ProximaImagem);
        botaoProximo.gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        AbrirCatalogo();
    }

    void AbrirCatalogo()
    {
        painel.SetActive(true);
        index = 0;

        DefinirCatalogoPorPerfil();
        MostrarImagem();

        botaoProximo.gameObject.SetActive(true);
    }

    void DefinirCatalogoPorPerfil()
    {
        if (GameData.instance == null)
            return;

        switch (GameData.instance.perfilSofia)
        {
            case Perfil.Emocional:
                catalogoAtual = emocional;
                break;

            case Perfil.Racional:
                catalogoAtual = racional;
                break;

            case Perfil.Curioso:
                catalogoAtual = curioso;
                break;

            default:
                catalogoAtual = emocional;
                break;
        }
    }

    void MostrarImagem()
    {
        if (catalogoAtual == null || catalogoAtual.Length == 0)
            return;

        imagemUI.sprite = catalogoAtual[index];
    }

    public void ProximaImagem()
    {
        if (catalogoAtual == null || catalogoAtual.Length == 0)
            return;

        index++;

        if (index >= catalogoAtual.Length)
        {
            index = 0; // volta pro começo (loop infinito do museu)
        }

        MostrarImagem();
    }
}