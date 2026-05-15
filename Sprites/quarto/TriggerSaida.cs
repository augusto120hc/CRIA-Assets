using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TriggerSaida : MonoBehaviour
{
    public string nomeCena = "Fase02";
    public string cenaAtual = "Fase01";
    public string spawnDestino = "Spawn_Fase02";

    
    void Start()
{
    Debug.Log("PLAYER START");
}

void OnEnable()
{
    Debug.Log("PLAYER ENABLE");
}

    void OnTriggerEnter2D(Collider2D other)



{
    Debug.Log("Entrou no trigger");

    if (other.CompareTag("Player"))
    {
        Debug.Log("Player detectado");

        SpawnManager.spawnAtual = spawnDestino;

        StartCoroutine(TrocarCena());
    }
}

IEnumerator TrocarCena()
{
    Debug.Log("COMEÇOU LOAD");

    AsyncOperation load =
        SceneManager.LoadSceneAsync(nomeCena, LoadSceneMode.Additive);

    while (!load.isDone)
    {
        Debug.Log(load.progress);
        yield return null;
    }

    Debug.Log("CENA CARREGADA");

    yield return null;

    Debug.Log("ANTES UNLOAD");

    SceneManager.UnloadSceneAsync(cenaAtual);

    Debug.Log("UNLOAD CHAMADO");
}

}