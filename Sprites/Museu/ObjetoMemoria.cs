using UnityEngine;

public class ObjetoMemoria : MonoBehaviour
{
    public int idObjeto;

    [Header("Audio")]
    public AudioClip somColeta;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // VERIFICA SE EXISTE GAME MANAGER
            if (GameManager.instance == null)
            {
                // Debug.LogError("GameManager NÃO EXISTE!");

                return;
            }

            switch (idObjeto)
            {
                case 1:
                    GameManager.instance.coletouObjeto1 = true;
                    break;

                case 2:
                    GameManager.instance.coletouObjeto2 = true;
                    break;

                case 3:
                    GameManager.instance.coletouObjeto3 = true;
                    break;

                case 4:
                    GameManager.instance.coletouObjeto4 = true;
                    break;
            }

            // Debug.Log("Objeto de memória coletado!");

            // TOCA SOM
            if (audioSource != null && somColeta != null)
            {
                audioSource.PlayOneShot(somColeta);

                // ESCONDE O OBJETO
                GetComponent<SpriteRenderer>().enabled = false;

                // DESATIVA COLLIDER
                GetComponent<Collider2D>().enabled = false;

                // DESTROI DEPOIS DO SOM
                Destroy(gameObject, somColeta.length);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}