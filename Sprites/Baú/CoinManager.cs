using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [Header("Likes")]
    public int likes = 0;

    [Header("Mensagem 5000")]
    public GameObject caixaMensagem;
    public TMP_Text mensagemText;

    private bool mensagemMostrada = false;

    private void Awake()
    {
        // Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // mantém entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AtualizarHUD();
    }

    public void AdicionarMoeda(int valor)
    {
        likes += valor;
        AtualizarHUD();
        if (likes >= 5000 && !mensagemMostrada)
        {
            mensagemMostrada = true;

            if (caixaMensagem != null)
            {
                caixaMensagem.SetActive(true);

        if (TypewriterEffect.instance != null)
        {
            TypewriterEffect.instance.ShowText(
                mensagemText,
               "<color=#59fff7>Com likes suficientes, agora tenho permisão para entrar no prédio Avatares...   :)</color>"
            );
        }
            }
        }
    }

    public void AtualizarHUD()
    {
        if (HUDMoedas.instance != null)
        {
            HUDMoedas.instance.AtualizarHUD(likes);
        }
    }

    
}