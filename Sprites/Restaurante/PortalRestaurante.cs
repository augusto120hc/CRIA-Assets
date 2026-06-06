using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PortalRestaurante : MonoBehaviour
{
    [Header("Cena Atual")]
    public string cenaAtual = "Fase02";

    [Header("Nova Cena")]
    public string novaCena = "Restaurante";

    [Header("Spawn")]
    public string nomeSpawn = "SpawnRestaurante";

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

        // espera estabilizar
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