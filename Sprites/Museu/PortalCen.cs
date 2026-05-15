using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PortalCena : MonoBehaviour
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

        AsyncOperation loadScene =
            SceneManager.LoadSceneAsync(
                novaCena,
                LoadSceneMode.Additive
            );

        while(!loadScene.isDone)
        {
            yield return null;
        }

        GameObject spawn =
            GameObject.Find(nomeSpawn);

        if(spawn != null)
        {
            player.transform.position =
                spawn.transform.position;
        }

        SceneManager.UnloadSceneAsync(cenaAtual);
    }
}