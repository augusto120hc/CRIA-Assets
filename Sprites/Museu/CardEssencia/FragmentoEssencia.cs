using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FragmentoEssencia : MonoBehaviour
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

   private string descricao =
    "<b><color=#e54186>O que representa:</color></b> o núcleo da identidade humana que resiste à padronização — aquilo que permanece mesmo quando hábitos, dados e algoritmos tentam reorganizar quem somos." +
    "\n<b><color=#e54186>Impacto observado:</color></b> quando desconectado da própria essência, o indivíduo passa a agir por repetição, guiado por padrões externos em vez de intenções próprias.";

    private string titulo =
        "<b>FRAGMENTO DA ESSÊNCIA</b>";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!coletado && other.CompareTag("Player"))
        {
            coletado = true;
            StartCoroutine(Coletar());
        }
    }

    private System.Collections.IEnumerator Coletar()
    {
        // garante que HUD/UI terminou o frame antes de aplicar sprite
        yield return null;

        // Debug.Log("Sprite antes: " + imagemCard.sprite);

        // HUD primeiro (evita reset posterior sobrescrever UI)
        if (HUDFragmentos.instance != null)
        {
            HUDFragmentos.instance.ColetouEssencia();
        }

        // SFX
        if (somColeta != null && Camera.main != null)
        {
            AudioSource.PlayClipAtPoint(somColeta, Camera.main.transform.position);
        }

        // ativa UI
        if (painelFragmento != null)
            painelFragmento.SetActive(true);

        // aplica conteúdo do card (UMA vez só)
        if (imagemCard != null)
            imagemCard.sprite = spriteFragmento;

        if (tituloText != null)
            tituloText.text = titulo;

        if (TypewriterEffect.instance != null)
            TypewriterEffect.instance.ShowText(descricaoText, descricao);

        if (FragmentosManager.instance != null)
            FragmentosManager.instance.temEssencia = true;

        // esconde objeto
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false;

        var col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // Debug.Log("Sprite final: " + imagemCard.sprite);
    }
}