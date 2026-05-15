using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static string spawnAtual;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}