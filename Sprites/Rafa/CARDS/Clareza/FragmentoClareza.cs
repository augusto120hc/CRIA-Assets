    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using System.Collections;

    public class FragmentoClareza : MonoBehaviour
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

          public GameObject textoNotificacao;

        public TMP_Text notificacaoText;

        public AudioClip somNotificacao;

        // TEXTO FIXO NO CÓDIGO
            private string descricao =
        "<b><color=#52c2fb>O que representa:</color></b> capacidade de pensar, questionar e identificar padrões no ambiente digital." +
        "\n<b><color=#52c2fb>Impacto observado:</color></b> quando exposto a conteúdos repetitivos, tende a operar em modo automático, reduzindo sua reflexão crítica.";


        private string titulo =
            "<b>FRAGMENTO DA CLAREZA</b>";

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !coletado)
            {
                coletado = true;
                Coletar();
            }
        }

        void Start()
        {
            if(FragmentosManager.instance.temClareza)
            {
                gameObject.SetActive(false);

                return;
            }

            painelFragmento.SetActive(false);
        }

        void Coletar()
        {
            // ATIVA HUD
            if(HUDFragmentos.instance != null)
            {
                HUDFragmentos.instance.ColetouClareza();
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
                FragmentosManager.instance.temClareza = true;
            }


            // ESCONDE OBJETO
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }

        IEnumerator MostrarNotificacao()
        {
            textoNotificacao.SetActive(true);

            notificacaoText.text =
            "<color=#52c2fb>Fragmento CLAREZA coletado</color>";

            yield return new WaitForSeconds(3f);

            textoNotificacao.SetActive(false);
        }

         public void FecharPainel()
        {
            painelFragmento.SetActive(false);

            if(somNotificacao != null)
            {
                AudioSource.PlayClipAtPoint(
                    somNotificacao,
                    Camera.main.transform.position
                );
            }

            StartCoroutine(MostrarNotificacao());
        }
    }