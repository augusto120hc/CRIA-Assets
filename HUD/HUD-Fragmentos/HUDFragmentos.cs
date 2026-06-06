using UnityEngine;
using UnityEngine.UI;

public class HUDFragmentos : MonoBehaviour
{
    public static HUDFragmentos instance;

    [Header("Ícones")]
    public Image iconeClareza;
    public Image iconeVontade;
    public Image iconeEssencia;

    [Header("Config")]
    [Range(0f, 1f)]
    public float alphaDesativado = 0.2f;

    [Range(0f, 1f)]
    public float alphaAtivado = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InicializarHUD();
    }

    void InicializarHUD()
    {
        DesativarIcone(iconeClareza);
        DesativarIcone(iconeVontade);
        DesativarIcone(iconeEssencia);
    }

    void DesativarIcone(Image img)
    {
        if (img == null)
            return;

        Color c = img.color;

        // escurece MUITO
        c.r = 0.3f;
        c.g = 0.3f;
        c.b = 0.3f;

        // deixa transparente
        c.a = alphaDesativado;

        img.color = c;
    }

    void AtivarIcone(Image img)
    {
        if (img == null)
            return;

        Color c = img.color;

        // volta cor normal
        c.r = 1f;
        c.g = 1f;
        c.b = 1f;

        // totalmente visível
        c.a = alphaAtivado;

        img.color = c;

        // Debug.Log("HUD ativou: " + img.name);
    }

    public void ColetouClareza()
    {
        AtivarIcone(iconeClareza);
    }

    public void ColetouVontade()
    {
        AtivarIcone(iconeVontade);
    }

    public void ColetouEssencia()
    {
        AtivarIcone(iconeEssencia);
    }
}