using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PortalEscola : MonoBehaviour
{
    [Header("Cenas")]
    public string cenaAtual;

    public string novaCena;

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
        if(other.CompareTag("Player") && !carregando)
        {
            StartCoroutine(TrocarCena(other.gameObject));
        }
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

        // pequeno delay dramático ✨
        yield return new WaitForSeconds(0.5f);

        // CARREGA CENA
        AsyncOperation loadScene =
            SceneManager.LoadSceneAsync(
                novaCena,
                LoadSceneMode.Additive
            );

        loadScene.allowSceneActivation = false;

        // loading fake suave
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

        // libera ativação
        loadScene.allowSceneActivation = true;

        while(!loadScene.isDone)
        {
            yield return null;
        }

        // espera tudo estabilizar
        yield return new WaitForSeconds(0.3f);

        // MOVE PLAYER
        GameObject spawn =
            GameObject.Find(nomeSpawn);

        if(spawn != null)
        {
            player.transform.position =
                spawn.transform.position;
        }

        // descarrega cena antiga
        SceneManager.UnloadSceneAsync(cenaAtual);

        // esconde loading
        if(painelLoading != null)
        {
            painelLoading.SetActive(false);
        }

        carregando = false;
    }
}  