using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PortalMuseu : MonoBehaviour
{
    [Header("Cenas")]
    public string cenaAtual;
    public string novaCena;

    [Header("Spawn")]
    public string nomeSpawn;

    [Header("Loading UI")]
    public GameObject painelLoading;
    public Slider barraLoading;

    [Header("Bloqueio (opcional)")]
    public GameObject painelBloqueio;
    public TMPro.TMP_Text textoBloqueio;

    private bool carregando = false;

    private void Start()
    {
        if (painelLoading != null)
            painelLoading.SetActive(false);

        if (painelBloqueio != null)
            painelBloqueio.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || carregando)
            return;

        StartCoroutine(TrocarCena(other.gameObject));
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        FecharBloqueio();
    }

    void MostrarBloqueio(string msg)
    {
        if (painelBloqueio != null)
            painelBloqueio.SetActive(true);

        if (textoBloqueio != null)
            textoBloqueio.text = msg;
    }

    void FecharBloqueio()
    {
        if (painelBloqueio != null)
            painelBloqueio.SetActive(false);

        if (textoBloqueio != null)
            textoBloqueio.text = "";
    }

    IEnumerator TrocarCena(GameObject player)
    {
        carregando = true;

        if (painelLoading != null)
            painelLoading.SetActive(true);

        if (barraLoading != null)
            barraLoading.value = 0f;

        yield return new WaitForSeconds(0.5f);

        AsyncOperation loadScene =
            SceneManager.LoadSceneAsync(novaCena, LoadSceneMode.Additive);

        loadScene.allowSceneActivation = false;

        float progresso = 0f;

        while (progresso < 0.9f)
        {
            progresso += Time.deltaTime;

            if (barraLoading != null)
                barraLoading.value = progresso;

            yield return null;
        }

        loadScene.allowSceneActivation = true;

        while (!loadScene.isDone)
            yield return null;

        yield return new WaitForSeconds(0.3f);

        GameObject spawn = GameObject.Find(nomeSpawn);

        if (spawn != null)
            player.transform.position = spawn.transform.position;

        SceneManager.UnloadSceneAsync(cenaAtual);

        if (painelLoading != null)
            painelLoading.SetActive(false);

        carregando = false;
    }
}