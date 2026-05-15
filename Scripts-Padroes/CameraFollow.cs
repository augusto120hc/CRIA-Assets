// using UnityEngine;
// using Cinemachine;
// using UnityEngine.SceneManagement;

// public class CameraFollow : MonoBehaviour
// {
//     void OnEnable()
//     {
//         SceneManager.sceneLoaded += OnSceneLoaded;
//     }

//     void OnDisable()
//     {
//         SceneManager.sceneLoaded -= OnSceneLoaded;
//     }

//     void OnSceneLoaded(Scene scene, LoadSceneMode mode)
//     {
//         GameObject player = GameObject.FindGameObjectWithTag("Player");

//         CinemachineVirtualCamera[] vcams = FindObjectsOfType<CinemachineVirtualCamera>();

//         foreach (var cam in vcams)
//         {
//             // Debug.Log("Camera encontrada: " + cam.name);

//             if (cam.isActiveAndEnabled && player != null)
//             {
//                 cam.Follow = player.transform;
//                 // Debug.Log("Camera ativa configurada: " + cam.name);
//             }
//         }

//         // Debug final
//         // Debug.Log("Player encontrado: " + player);
//     }
// }



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
        CinemachineVirtualCamera[] vcams = FindObjectsOfType<CinemachineVirtualCamera>();

        // Se estiver na Fase01
        if (scene.name == "Fase01")
        {
            GameObject cameraFixa = GameObject.Find("CameraFixa");

            foreach (var cam in vcams)
            {
                if (cam.isActiveAndEnabled && cameraFixa != null)
                {
                    cam.Follow = cameraFixa.transform;
                    cam.LookAt = null;
                }
            }
        }
        else
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            foreach (var cam in vcams)
            {
                if (cam.isActiveAndEnabled && player != null)
                {
                    cam.Follow = player.transform;
                    cam.LookAt = null;
                }
            }
        }
    }
}