using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [Header("Moedas")]
    public int moedas = 0;

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
        moedas += valor;

        Debug.Log("Moedas coletadas: " + moedas);

        AtualizarHUD();

        // ativa mensagem ao chegar em 5000
        if (moedas >= 5000 && !mensagemMostrada)
        {
            mensagemMostrada = true;

            if (caixaMensagem != null)
            {
                caixaMensagem.SetActive(true);

        if (TypewriterEffect.instance != null)
        {
            TypewriterEffect.instance.ShowText(
                mensagemText,
               "Com likes suficientes, posso comprar uma nova máscara digital... talvez fingir ser outro alguém."
            );
        }
            }
        }
    }

    private void AtualizarHUD()
    {
        if (HUDMoedas.instance != null)
        {
            HUDMoedas.instance.AtualizarHUD(moedas);
        }
    }

    
}