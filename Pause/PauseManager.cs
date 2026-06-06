using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [Header("Pause")]
    public GameObject painelPause;

    [Header("Sub Panels")]
    public GameObject painelConfiguracoes;
    public GameObject painelCreditos;

    private AudioSource musicaJogo;
    private bool pausado;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //  se está em algum submenu, volta pro pause
            if (painelConfiguracoes.activeSelf || painelCreditos.activeSelf)
            {
                painelConfiguracoes.SetActive(false);
                painelCreditos.SetActive(false);
                painelPause.SetActive(true);
                return;
            }

            //  se está pausado, volta pro jogo
            if (pausado)
            {
                VoltarJogo();
            }
            else
            {
                PausarJogo();
            }
        }
    }

    // ---------------- PAUSE ----------------

    public void PausarJogo()
    {
        AtivarSomente(painelPause);

        Time.timeScale = 0f;

        if (musicaJogo != null)
            musicaJogo.volume = 0.3f;

        pausado = true;
    }

    public void VoltarJogo()
    {
        DesativarTodos();

        Time.timeScale = 1f;

        if (musicaJogo != null)
            musicaJogo.volume = 1f;

        pausado = false;
    }

    // ---------------- CONFIG ----------------

    public void AbrirConfiguracoes()
    {
        AtivarSomente(painelConfiguracoes);
    }

    public void FecharConfiguracoes()
    {
        painelConfiguracoes.SetActive(false);
        painelPause.SetActive(true);
    }

    // ---------------- CRÉDITOS ----------------

    public void AbrirCreditos()
    {
        AtivarSomente(painelCreditos);
    }

    public void FecharCreditos()
    {
        painelCreditos.SetActive(false);
        painelPause.SetActive(true);
    }

    // ---------------- SAIR ----------------

    public void SairJogo()
    {
        Time.timeScale = 1f;
        Application.Quit();

        Debug.Log("Fechando jogo...");
    }

    // ---------------- HELPERS ----------------

    void AtivarSomente(GameObject painel)
    {
        painelPause.SetActive(false);
        painelConfiguracoes.SetActive(false);
        painelCreditos.SetActive(false);

        if (painel != null)
            painel.SetActive(true);
    }

    void DesativarTodos()
    {
        painelPause.SetActive(false);
        painelConfiguracoes.SetActive(false);
        painelCreditos.SetActive(false);
    }
}