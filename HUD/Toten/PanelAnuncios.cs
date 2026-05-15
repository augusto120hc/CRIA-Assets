using UnityEngine;

public class PanelAnuncios : MonoBehaviour
{
    [Header("Configuração")]
    public GameObject painel;

    private void Awake()
    {
        // garante que começa fechado
        if (painel != null)
            painel.SetActive(false);
    }

    // abre o painel
    public void Abrir()
    {
        if (painel != null)
            painel.SetActive(true);
    }

    // fecha o painel
    public void Fechar()
    {
        if (painel != null)
            painel.SetActive(false);
    }

    // toggle opcional (caso queira usar depois)
    public void Alternar()
    {
        if (painel != null)
            painel.SetActive(!painel.activeSelf);
    }
}