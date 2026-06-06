using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Catalogo : MonoBehaviour
{
    [Header("UI")]
    public GameObject painelCatalogo;
    public Image imagemCatalogo;
    public GameObject textoInteragir;

    [Header("Áudio")]
    public AudioSource audioSource;

    public AudioClip musicaCatalogo;


    [Header("LOADING")]
    public TMP_Text textoLoading;

    public Slider barraLoading;


    [Header("Listas de Imagens")]
    public Sprite[] imagensCuriosa;
    public Sprite[] imagensEmocional;
    public Sprite[] imagensRacional;

    private Sprite[] imagensAtuais;

    private int indiceAtual = 0;

    private bool playerPerto = false;

    private float tempoVisualizando = 0f;

    private float[] temposImagens;

    [Header("RESULTADO")]
    public GameObject painelResultado;

    public TMP_Text textoResultado;

    private bool contandoTempo = false;

    [Header("ANÁLISE FINAL")]
        public GameObject painelAnaliseFinal;

        public TMP_Text textoAnalise;

        public Image imagemMaisVista;

    void Update()
    {
        if(playerPerto && Input.GetKeyDown(KeyCode.E))
        {
            AbrirCatalogo();
        }

        if(contandoTempo)
        {
            tempoVisualizando += Time.deltaTime;
        }
    }

    void Start()
    {
        painelResultado.SetActive(false);

        painelCatalogo.SetActive(false);

        if(textoInteragir != null)
        {
            textoInteragir.SetActive(false);
        }

        textoLoading.gameObject.SetActive(false);

        barraLoading.gameObject.SetActive(false);

        painelAnaliseFinal.SetActive(false);
    }

    void AbrirCatalogo()
    {
        painelCatalogo.SetActive(true);

        // TOCA MÚSICA
        if(audioSource != null && musicaCatalogo != null)
        {
            audioSource.clip = musicaCatalogo;

            audioSource.loop = true;

            audioSource.Play();
        }

        StartCoroutine(ProcessoCatalogo());
    }

// void AbrirCatalogo()
    // {
        
    //     painelCatalogo.SetActive(true);

    //     DefinirPerfil();

    //     temposImagens = new float[imagensAtuais.Length];

    //     indiceAtual = 0;

    //     MostrarImagem();
    // }

        IEnumerator ProcessoCatalogo()
        {
            contandoTempo = false;

            imagemCatalogo.gameObject.SetActive(false);

            textoLoading.gameObject.SetActive(true);

            barraLoading.gameObject.SetActive(true);

            // ETAPA 1
            textoLoading.text =
            "<color=#52ff9e>Detectando perfil...</color>";

            barraLoading.value = 0f;

            float tempo = 0f;

            while(tempo < 4f)
            {
                tempo += Time.deltaTime;

                barraLoading.value = tempo / 4f;

                yield return null;
            }

            // DEFINE PERFIL
            DefinirPerfil();

            temposImagens = new float[imagensAtuais.Length];

            string nomePerfil = "";
            Debug.Log(
                "Perfil atual: " +
                PerfilGlobal.instance.perfilAtual
            );

            switch(PerfilGlobal.instance.perfilAtual)
            {
                case PerfilGlobal.Perfil.Curiosa:
                    nomePerfil = "<b><color=#e61e98>\nCURIOSA</color></b>";
                    break;

                case PerfilGlobal.Perfil.Emocional:
                    nomePerfil = "<b><color=#e61e98>\nEMOCIONAL</color></b>";
                    break;

                case PerfilGlobal.Perfil.Racional:
                    nomePerfil = "<b><color=#e61e98>\nRACIONAL</color></b>";
                    break;
            }

            // ETAPA 2
            textoLoading.text =
            "<color=#52ff9e>Carregando imagens para perfil: "
            + nomePerfil +
            "</color>";

            barraLoading.value = 0f;

            tempo = 0f;

            while(tempo < 4f)
            {
                tempo += Time.deltaTime;

                barraLoading.value = tempo / 4f;

                yield return null;
            }

            // ESCONDE LOADING
            textoLoading.gameObject.SetActive(false);

            barraLoading.gameObject.SetActive(false);

            // MOSTRA CATÁLOGO
            imagemCatalogo.gameObject.SetActive(true);

            indiceAtual = 0;

            MostrarImagem();

            tempoVisualizando = 0f;

            contandoTempo = true;
        }

    
    void DefinirPerfil()
    {
        switch(PerfilGlobal.instance.perfilAtual)
        {
            case PerfilGlobal.Perfil.Curiosa:
                imagensAtuais = imagensCuriosa;
                break;

            case PerfilGlobal.Perfil.Emocional:
                imagensAtuais = imagensEmocional;
                break;

            case PerfilGlobal.Perfil.Racional:
                imagensAtuais = imagensRacional;
                break;
        }
    }

    void MostrarImagem()
    {
        if(imagensAtuais.Length > 0)
        {
            imagemCatalogo.sprite = imagensAtuais[indiceAtual];
        }
    }

    public void ProximaImagem()
    {
        if(imagensAtuais == null)
        {
            Debug.Log("Nenhuma imagem carregada.");
            return;
        }

        if(imagensAtuais.Length <= 0)
        {
            Debug.Log("Lista vazia.");
            return;
        }

        SalvarTempoImagem();

        indiceAtual++;

        if(indiceAtual >= imagensAtuais.Length)
        {
            indiceAtual = 0;
        }

        MostrarImagem();
    }

    public void ImagemAnterior()
    {
        if(imagensAtuais == null)
        {
            // Debug.Log("Nenhuma imagem carregada.");
            return;
        }

        if(imagensAtuais.Length <= 0)
        {
            // Debug.Log("Lista vazia.");
            return;
        }

        SalvarTempoImagem();

        indiceAtual--;

        if(indiceAtual < 0)
        {
            indiceAtual = imagensAtuais.Length - 1;
        }

        MostrarImagem();
    }

    public void FecharCatalogo()
    {
        contandoTempo = false;

        SalvarTempoImagem();

        painelCatalogo.SetActive(false);

        // PARA MÚSICA
        // if(audioSource != null)
        // {
        //     audioSource.Stop();
        // }

        MostrarResultado();
    }


        public void FecharResultado()
        {
            painelResultado.SetActive(false);

            MostrarAnaliseFinal();
            
        }

    void MostrarResultado()
    {
        painelResultado.SetActive(true);

        string resultado = "";

        resultado +=
        "<b><color=#52ff9e>O Algoritmo analisou seu comportamento \nTempo que você ficou olhando cada imagem</color></b>\n\n";

        for(int i = 0; i < temposImagens.Length; i++)
        {
            resultado +=
                "Imagem " +
                (i + 1) +
                " analisada por: <color=#52ff9e>" +
                temposImagens[i].ToString("F2") +
                "s</color>\n";
        }

        textoResultado.text = resultado;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerPerto = true;

            if(textoInteragir != null)
                textoInteragir.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerPerto = false;

            if(textoInteragir != null)
                textoInteragir.SetActive(false);
        }
    }


    void SalvarTempoImagem()
        {
            if(temposImagens == null)
                return;

            temposImagens[indiceAtual] += tempoVisualizando;

            // Debug.Log(
            //     "Imagem " +
            //     indiceAtual +
            //     " vista por: " +
            //     tempoVisualizando.ToString("F2") +
            //     " segundos"
            // );

            tempoVisualizando = 0f;
        }

        void MostrarAnaliseFinal()
        {
            painelAnaliseFinal.SetActive(true);

            int indiceMaisVisto = 0;

            float maiorTempo = temposImagens[0];

            for(int i = 1; i < temposImagens.Length; i++)
            {
                if(temposImagens[i] > maiorTempo)
                {
                    maiorTempo = temposImagens[i];
                    indiceMaisVisto = i;
                }
            }

            imagemMaisVista.sprite =
                imagensAtuais[indiceMaisVisto];

            GogolData.imagemMaisObservada =
                imagensAtuais[indiceMaisVisto];

            GogolData.nomeImagemMaisObservada =
                "Imagem " + (indiceMaisVisto + 1);

            GogolData.tempoImagemMaisObservada =
                maiorTempo;

            textoAnalise.text =
                "<b><color=#52ff9e>" +
                "A imagem mais vista é utilizada pelo ALGORITMO " +
                "para referência de futuras recomendações." +
                "</color></b>";
        }

        public void FecharAnaliseFinal()
        {
            painelAnaliseFinal.SetActive(false);

            if(audioSource != null)
            {
                audioSource.Stop();
            }
        }

        
}