using UnityEngine;

public class FecharPainel : MonoBehaviour
{
    public GameObject painel;

    public void Fechar()
    {
        painel.SetActive(false);
    }
}