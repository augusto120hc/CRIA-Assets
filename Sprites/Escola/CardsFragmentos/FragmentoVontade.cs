using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FragmentoVontade : MonoBehaviour
{
    [Header("UI")]
    public GameObject painelFragmento;

    public Image imagemCard;
    public TMP_Text tituloText;
    public TMP_Text descricaoText;

    [Header("Fragmento")]
    public Sprite spriteFragmento;

    [Header("Audio")]
    public AudioClip somColeta;

    private bool coletado = false;

    // TEXTO FIXO
    private string descricao =
        "<b><color=#fcae1c>O que representa:</color></b> Força de ação e persistência que sustenta a criação mesmo sob desgaste e pressões de padronização.\n" +
        "<b><color=#fcae1c>Problema causado:</color></b> A ação se dissolve em fluxo automático, reduzindo escolhas a repetições vazias e desconectadas de intenção, contexto e identidade.";

    private string titulo =
        "<b>FRAGMENTO DA VONTADE</b>";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !coletado)
        {
            coletado = true;
            Coletar();
        }
    }

    void Coletar()
    {
        if(HUDFragmentos.instance != null)
        {
            HUDFragmentos.instance.ColetouVontade();
        }

        // SOM
        if (somColeta != null)
        {
            AudioSource.PlayClipAtPoint(
                somColeta,
                Camera.main.transform.position
            );
        }

        // ABRE UI
        painelFragmento.SetActive(true);

        // IMAGEM
        imagemCard.sprite = spriteFragmento;

        // TITULO
        tituloText.text = titulo;

        // TYPEWRITER
        TypewriterEffect.instance.ShowText(
            descricaoText,
            descricao
        );

        if(FragmentosManager.instance != null)
        {
            FragmentosManager.instance.temVontade = true;
        }

        // ESCONDE OBJETO
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
}