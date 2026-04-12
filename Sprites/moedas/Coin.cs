using UnityEngine;

public class Coin : MonoBehaviour
{
    public int valor = 1;                  // Quantos pontos vale a moeda
    public AudioClip somColeta;            // Som ao coletar
    private AudioSource audioSource;

    private void Awake()
    {
        // Cria/pega um AudioSource temporário para tocar o som
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = somColeta;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Adiciona pontuação
            if (GameManager.instance != null)
            {
                GameManager.instance.AddScore(valor);
            }

            // Toca o som
            if (somColeta != null)
            {
                audioSource.PlayOneShot(somColeta);
            }

            // Desativa o sprite/colisor antes de destruir para o som tocar
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            // Destrói após o som tocar (ou imediatamente se não tiver som)
            Destroy(gameObject, somColeta != null ? somColeta.length : 0f);
        }
    }
}