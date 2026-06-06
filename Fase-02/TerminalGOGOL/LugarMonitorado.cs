using UnityEngine;

public class LugarMonitorado : MonoBehaviour
{
    [Header("Lugar")]
    public string nomeLugar;

    private bool playerDentro = false;

    void Update()
    {
        if (!playerDentro) return;

        GogolData.RegistrarTempo(nomeLugar, Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerDentro = true;
        GogolData.RegistrarEntrada(nomeLugar);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerDentro = false;
    }
}