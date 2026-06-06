using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PortalCena : MonoBehaviour
{
    [Header("Cenas")]
    public string cenaAtual;

    [Header("Bloqueio")]
    public GameObject painelBloqueio;
    public TMPro.TMP_Text textoBloqueio;

    public string novaCena;

    [Header("Spawn")]
    public string nomeSpawn;

    [Header("Loading UI")]
    public GameObject painelLoading;

    public Slider barraLoading;

    private bool carregando = false;

    private void Start()
    {
        if(painelLoading != null)
        {
            painelLoading.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || carregando)
            return;

        //  BLOQUEIO POR FRAGMENTOS (seguro contra null)
        if (
    FragmentosManager.instance == null ||
    !FragmentosManager.instance.fragmentosEntreguesAoRafa
        )
        {
            MostrarBloqueio();
            return;
        }

        // ✔ LIBERADO
        StartCoroutine(TrocarCena(other.gameObject));
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        FecharBloqueio();
    }


        void FecharBloqueio()
        {
            if (painelBloqueio != null)
                painelBloqueio.SetActive(false);

            if (textoBloqueio != null)
                textoBloqueio.text = "";
        }
    void MostrarBloqueio()
    {
        if (painelBloqueio != null)
            painelBloqueio.SetActive(true);

        if (textoBloqueio != null)
            textoBloqueio.text = "Você precisa ajudar o Rafa antes de entrar aqui";

        // Debug.Log(" Portal bloqueado: fragmentos não entregues");
    }

    

    IEnumerator TrocarCena(GameObject player)
    {
        carregando = true;

        // MOSTRA LOADING
        if(painelLoading != null)
        {
            painelLoading.SetActive(true);
        }

        if(barraLoading != null)
        {
            barraLoading.value = 0f;
        }

        // pequeno delay
        yield return new WaitForSeconds(0.5f);

        // CARREGA NOVA CENA
        AsyncOperation loadScene =
            SceneManager.LoadSceneAsync(
                novaCena,
                LoadSceneMode.Additive
            );

        loadScene.allowSceneActivation = false;

        float progresso = 0f;

        while(progresso < 0.9f)
        {
            progresso += Time.deltaTime;

            if(barraLoading != null)
            {
                barraLoading.value = progresso;
            }

            yield return null;
        }

        // ativa cena
        loadScene.allowSceneActivation = true;

        while(!loadScene.isDone)
        {
            yield return null;
        }

        // pequeno tempo de estabilização
        yield return new WaitForSeconds(0.3f);

        // MOVE PLAYER
        GameObject spawn =
            GameObject.Find(nomeSpawn);

        if(spawn != null)
        {
            player.transform.position =
                spawn.transform.position;
        }

        // DESCARREGA CENA ANTIGA
        SceneManager.UnloadSceneAsync(cenaAtual);

        // ESCONDE LOADING
        if(painelLoading != null)
        {
            painelLoading.SetActive(false);
        }

        carregando = false;
    }
}