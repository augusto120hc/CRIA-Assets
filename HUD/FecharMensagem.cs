using UnityEngine;

public class FecharMensagem : MonoBehaviour
{
    public GameObject painelMensagem;

    public void Fechar()
    {
        painelMensagem.SetActive(false);
    }
}