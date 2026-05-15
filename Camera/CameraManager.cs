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
        AtualizarCamera();
    }

    void AtualizarCamera()
    {
        string nomeCena = SceneManager.GetActiveScene().name;

        if (nomeCena == "Fase01")
        {
            GameObject pontoFixo = GameObject.Find("CameraFixa");

            if (pontoFixo != null)
            {
                // câmera fixa
                vcam.Follow = pontoFixo.transform;
                vcam.LookAt = null;
            }
        }
        else
        {
            // câmera segue player
            vcam.Follow = player;
            vcam.LookAt = null;
        }
    }
}