using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TriggerSaida : MonoBehaviour
{
    [Header("Cenas")]
    public string nomeCena = "Fase02";
    public string cenaAtual = "Fase01";
    public string spawnDestino = "Spawn_Fase02";

    [Header("Loading UI")]
    public GameObject painelLoading;
    public Slider barraLoading;

    private bool carregando = false;

    void Start()
    {
        if (painelLoading != null)
            painelLoading.SetActive(false);
    }

//     void Start()
// {
//     if (painelLoading != null)
//         painelLoading.SetActive(false);

//     // 🧪 DEBUG: força missões concluídas
//     if (GameManager.instance != null)
//     {
//         GameManager.instance.todasMissoesConcluidas = true;
//     }
// }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || carregando)
            return;

        carregando = true;

        // FINAL DO JOGO
        if (nomeCena == "Fase01" &&
            GameManager.instance != null &&
            GameManager.instance.todasMissoesConcluidas &&
            !GameManager.instance.iniciarFinalAoEntrarNaCasa)
        {
            Debug.Log("FINAL DO JOGO ATIVADO");

            GameManager.instance.iniciarFinalAoEntrarNaCasa = true;

            // dispara cutscene direto
            CutsceneFinal cutscene = FindObjectOfType<CutsceneFinal>();

            if (cutscene != null)
            {
                cutscene.IniciarFinal();
            }
            else
            {
                Debug.LogError("CutsceneFinal não encontrada na cena!");
            }

            // garante que não entra em loading nem troca de cena
            carregando = false;
            return;
        }

        // Fluxo normal
        SpawnManager.spawnAtual = spawnDestino;

        StartCoroutine(TrocarCena());
    }

    IEnumerator TrocarCena()
    {
        if (painelLoading != null)
            painelLoading.SetActive(true);

        if (barraLoading != null)
            barraLoading.value = 0f;

        yield return new WaitForSeconds(0.5f);

        AsyncOperation load =
            SceneManager.LoadSceneAsync(nomeCena, LoadSceneMode.Additive);

        if (load == null)
        {
            Debug.LogError("Não foi possível carregar a cena: " + nomeCena);
            carregando = false;
            yield break;
        }

        load.allowSceneActivation = false;

        float progresso = 0f;

        while (progresso < 0.9f)
        {
            progresso += Time.deltaTime;

            if (barraLoading != null)
                barraLoading.value = progresso;

            yield return null;
        }

        load.allowSceneActivation = true;

        while (!load.isDone)
            yield return null;

        yield return new WaitForSeconds(0.3f);

        Scene cenaParaDescarregar = SceneManager.GetSceneByName(cenaAtual);

        if (cenaParaDescarregar.IsValid() && cenaParaDescarregar.isLoaded)
        {
            Debug.Log("Descarregando: " + cenaAtual);

            AsyncOperation unload =
                SceneManager.UnloadSceneAsync(cenaAtual);

            if (unload != null)
                yield return unload;
        }
        else
        {
            Debug.LogWarning(
                "Cena não encontrada para descarregar: " + cenaAtual);
        }

        if (painelLoading != null)
            painelLoading.SetActive(false);

        carregando = false;
    }
}