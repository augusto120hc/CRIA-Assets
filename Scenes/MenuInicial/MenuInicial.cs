using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuInicial : MonoBehaviour
{
    [Header("Painel Principal")]
    public GameObject painelMenu;

    [Header("Sub Menus")]
    public GameObject painelConfiguracoes;
    public GameObject painelCreditos;

    public void Jogar()
    {
        if (MusicManager.Instance != null)
            MusicManager.Instance.StopMusic();

        StartCoroutine(IniciarJogo());
    }

    public void Configuracoes()
    {
        painelMenu.SetActive(false);
        painelConfiguracoes.SetActive(true);
        painelCreditos.SetActive(false);
    }

    public void Creditos()
    {
        painelMenu.SetActive(false);
        painelCreditos.SetActive(true);
        painelConfiguracoes.SetActive(false);
    }

    //  FECHAR CONFIGURAÇÕES
    public void FecharConfiguracoes()
    {
        painelConfiguracoes.SetActive(false);
        painelMenu.SetActive(true);
    }

    //  FECHAR CRÉDITOS
    public void FecharCreditos()
    {
        painelCreditos.SetActive(false);
        painelMenu.SetActive(true);
    }

    //  FECHAR JOGO (volta para menu ou encerra gameplay UI)
    public void FecharJogo()
    {
        painelConfiguracoes.SetActive(false);
        painelCreditos.SetActive(false);
        painelMenu.SetActive(true);

        // opcional: se estiver na fase, volta pro menu
        SceneManager.LoadScene("MenuInicial");
    }

    public AudioSource musicaMenu;

    
    IEnumerator IniciarJogo()
    {
        musicaMenu.Stop();

        painelMenu.SetActive(false);
        painelConfiguracoes.SetActive(false);
        painelCreditos.SetActive(false);

        AsyncOperation fase =
            SceneManager.LoadSceneAsync("Fase01", LoadSceneMode.Additive);

        while (!fase.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(
            SceneManager.GetSceneByName("Fase01")
        );
    }
}


// using UnityEngine;
// using UnityEngine.SceneManagement;
// using System.Collections;

// public class MenuInicial : MonoBehaviour
// {
//     [Header("Painel")]
//     public GameObject painelMenu;

//     [Header("Sub Menus")]
//     public GameObject painelConfiguracoes;
//     public GameObject painelCreditos;

//     public void Jogar()
//     {
//         StartCoroutine(CarregarFase());
//     }

//     IEnumerator CarregarFase()
//     {
//         //  esconde menu
//         painelMenu.SetActive(false);

//         //  carrega Fase01 direto
//         AsyncOperation fase =
//             SceneManager.LoadSceneAsync("Fase01", LoadSceneMode.Single);

//         while (!fase.isDone)
//         {
//             yield return null;
//         }

//         //  garante cena ativa
//         SceneManager.SetActiveScene(
//             SceneManager.GetSceneByName("Fase01")
//         );
//     }

//     public void Configuracoes()
//     {
//         painelConfiguracoes.SetActive(true);
//         painelMenu.SetActive(false);
//     }

//     public void Creditos()
//     {
//         painelCreditos.SetActive(true);
//         painelMenu.SetActive(false);
//     }

//     public void VoltarMenu()
//     {
//         painelConfiguracoes.SetActive(false);
//         painelCreditos.SetActive(false);
//         painelMenu.SetActive(true);
//     }
// }