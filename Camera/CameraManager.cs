using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public Transform player;



    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Invoke(nameof(AtualizarCamera), 0.05f);
    }

    void AtualizarCamera()
    {
        string nomeCena = SceneManager.GetActiveScene().name;

        //  garante player atualizado
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
        }

        if (nomeCena == "Fase01")
        {
            GameObject pontoFixo = GameObject.Find("CameraFixa");

            if (pontoFixo != null)
            {
                vcam.Follow = pontoFixo.transform;
                vcam.LookAt = pontoFixo.transform;
            }
        }
        else
        {
            if (player != null)
            {
                vcam.Follow = player;
                vcam.LookAt = player;
            }
        }
    }
}