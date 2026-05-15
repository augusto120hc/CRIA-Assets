using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PortalBiblioteca : MonoBehaviour
{
    public string cenaAtual = "Cena02";

    public string novaCena = "Biblioteca";

    public string nomeSpawn = "SpawnBiblioteca";

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

        // carrega nova cena
        AsyncOperation loadScene =
            SceneManager.LoadSceneAsync(
                novaCena,
                LoadSceneMode.Additive
            );

        while(!loadScene.isDone)
        {
            yield return null;
        }

        // move player
        GameObject spawn =
            GameObject.Find(nomeSpawn);

        if(spawn != null)
        {
            player.transform.position =
                spawn.transform.position;
        }

        // descarrega cena antiga
        SceneManager.UnloadSceneAsync(cenaAtual);
    }
}