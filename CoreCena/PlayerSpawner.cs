using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    private GameObject player;
    public string spawnName = "Spawn_Vila";

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // Espera a fase carregar
        Invoke("PosicionarPlayer", 0.1f);
    }

    void PosicionarPlayer()
    {
        GameObject spawn = GameObject.Find(spawnName);

        if (spawn != null)
        {
            player.transform.position = spawn.transform.position;
        }
    }
}