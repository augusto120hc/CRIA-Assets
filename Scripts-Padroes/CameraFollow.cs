using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
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
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        CinemachineVirtualCamera[] vcams = FindObjectsOfType<CinemachineVirtualCamera>();

        foreach (var cam in vcams)
        {
            // Debug.Log("Camera encontrada: " + cam.name);

            if (cam.isActiveAndEnabled && player != null)
            {
                cam.Follow = player.transform;
                // Debug.Log("Camera ativa configurada: " + cam.name);
            }
        }

        // Debug final
        // Debug.Log("Player encontrado: " + player);
    }
}