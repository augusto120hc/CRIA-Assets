using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class VerificadorMissoes : MonoBehaviour
{
    public GameObject painelMensagem;
    public TMP_Text textoMensagem;

    // Luz global
    public Light2D luzGlobal;

    [SerializeField] private float intensidadeFinal = 0.3f;
    [SerializeField] private float velocidadeEscurecimento = 0.2f;

    private bool escurecendo = false;
    private bool mensagemMostrada = false;

    private bool rafaAvisado = false;
    private bool likesAvisado = false;
    private bool memoriasAvisado = false;

    void Start()
    {
        if (painelMensagem != null)
            painelMensagem.SetActive(false);
    }

    void Update()
    {
        // Escurece gradualmente
        if (escurecendo && luzGlobal != null)
        {
            luzGlobal.intensity = Mathf.MoveTowards(
                luzGlobal.intensity,
                intensidadeFinal,
                velocidadeEscurecimento * Time.deltaTime
            );
        }

        bool rafa = FragmentosManager.instance != null &&
                    FragmentosManager.instance.fragmentosEntreguesAoRafa;

        bool likes = CoinManager.instance != null &&
                     CoinManager.instance.likes >= 5000;

        bool memorias = GameManager.instance != null &&
                        GameManager.instance.memoriasSubversivasDevolvidas;

        if (rafa && !rafaAvisado)
            rafaAvisado = true;

        if (likes && !likesAvisado)
            likesAvisado = true;

        if (memorias && !memoriasAvisado)
            memoriasAvisado = true;

        // Evita executar novamente
        if (mensagemMostrada)
            return;

        // Todas as missões concluídas
        if (rafa && likes && memorias)
        {
            mensagemMostrada = true;

            GameManager.instance.todasMissoesConcluidas = true;

            StartCoroutine(MostrarMensagemComAtraso());
        }
        
    }

    private IEnumerator MostrarMensagemComAtraso()
    {
        // Espera 6 segundos
        yield return new WaitForSeconds(18f);

        // Começa a escurecer quando a mensagem aparecer
        escurecendo = true;

        painelMensagem.SetActive(true);

        if (TypewriterEffect.instance != null)
        {
            TypewriterEffect.instance.ShowText(
                textoMensagem,
                "<color=#1CFF1C>Todas as missões principais foram concluídas.</color>\n" +
                "<color=#59fff7>Está ficando tarde, acho que está tudo pronto por aqui, preciso voltar pra casa.</color>"
            );
        }
    }

    // Botão Fechar
    public void FecharPainel()
    {
        painelMensagem.SetActive(false);
    }
}