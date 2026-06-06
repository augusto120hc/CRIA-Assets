using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public Transform player;

    private bool seguir = true;

    void LateUpdate()
    {
        if (!seguir || player == null) return;

        Vector3 pos = transform.position;
        pos.x = player.position.x;
        pos.y = player.position.y;

        transform.position = pos;
    }

    public void AtualizarRegraPorCena()
    {
        string cena = SceneManager.GetActiveScene().name;

        // 🎯 EXCEÇÃO: Fase01 não segue
        if (cena == "Fase01")
        {
            seguir = false;
        }
        else
        {
            seguir = true;
        }
    }
}