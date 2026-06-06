using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PCInteraction : MonoBehaviour
{
    [SerializeField] private PC_UIController pcUI;
    [SerializeField] private GameObject botaoInteragir;
    [SerializeField] private GameObject setaPC;

    [SerializeField] private GameObject portaSprite;
    [SerializeField] private Collider2D portaCollider;
    [SerializeField] private Light2D luzPorta;

    [SerializeField] private Light2D pcLight;

    private bool jaInteragiu = false;

    void Start()
{
    botaoInteragir.SetActive(false);

    if (GameState.portaLiberada)
    {
        if (portaSprite != null)
            portaSprite.SetActive(false);

        if (portaCollider != null)
            portaCollider.enabled = false;

        if (luzPorta != null)
            luzPorta.enabled = true;

        jaInteragiu = true;
    }
    else
    {
        if (luzPorta != null)
            luzPorta.enabled = false;
    }
}

void LiberarPorta()
{
    Time.timeScale = 1f;

    if (portaSprite != null)
        portaSprite.SetActive(false);

    if (portaCollider != null)
        portaCollider.enabled = false;

    if (luzPorta != null)
        luzPorta.enabled = true;

    GameState.portaLiberada = true;
}
    void LiberarJogo()
{
    Time.timeScale = 1f;
    Debug.Log("JOGO DESPAUSADO");
}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !jaInteragiu)
        {
            botaoInteragir.SetActive(true);
            setaPC.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !jaInteragiu)
        {
            botaoInteragir.SetActive(false);
            setaPC.SetActive(true);
        }
    }

    public void AbrirPC()
    {
        jaInteragiu = true;

        botaoInteragir.SetActive(false);
        setaPC.SetActive(false);

        pcUI.AbrirPC();

        Time.timeScale = 0f;

        if (pcLight != null)
            pcLight.enabled = false;
    }

    void OnEnable()
    {
        if (pcUI != null && pcUI.typewriter != null)
            pcUI.typewriter.OnFinishTyping += LiberarPorta;
    }

    void OnDisable()
    {
        if (pcUI != null && pcUI.typewriter != null)
            pcUI.typewriter.OnFinishTyping -= LiberarPorta;
    }

    
}