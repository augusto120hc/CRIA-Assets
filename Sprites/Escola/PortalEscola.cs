using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PortalEscola : MonoBehaviour
{
    public string cenaAtual;

    public string novaCena;

    public string nomeSpawn;

    private bool carregando = false;

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

        // carrega nova cena additive
        AsyncOperation loadScene =
            SceneManager.LoadSceneAsync(
                novaCena,
                LoadSceneMode.Additive
            );

        while(!loadScene.isDone)
        {
            yield return null;
        }

        // move player para spawn
        GameObject spawn =
            GameObject.Find(nomeSpawn);

        if(spawn != null)
        {
            player.transform.position =
                spawn.transform.position;
        }

// Debug.Log("Nova Cena: " + novaCena);
        // descarrega cena antiga
        SceneManager.UnloadSceneAsync(cenaAtual);
    }
}