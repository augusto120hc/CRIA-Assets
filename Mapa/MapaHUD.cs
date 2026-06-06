using UnityEngine;

public class MapaHUD : MonoBehaviour
{
    public GameObject painelMapa;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            AlternarMapa();
        }
    }

    public void AlternarMapa()
    {
        bool abrir = !painelMapa.activeSelf;

        painelMapa.SetActive(abrir);
    }
}