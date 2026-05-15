using UnityEngine;

public class HairColorArea : MonoBehaviour
{
    [SerializeField] private Color novaCor = Color.blue;

    private Color corOriginal;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpriteRenderer sr = other.GetComponent<SpriteRenderer>();

            if (sr != null)
            {
                // salva a cor atual antes de mudar
                corOriginal = sr.material.GetColor("_TargetColor");

                sr.material.SetColor("_TargetColor", novaCor);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpriteRenderer sr = other.GetComponent<SpriteRenderer>();

            if (sr != null)
            {
                sr.material.SetColor("_TargetColor", corOriginal);
            }
        }
    }
}