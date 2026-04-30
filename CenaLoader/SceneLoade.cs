using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    public string faseAtual;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        StartCoroutine(IniciarJogo());
    }

    IEnumerator IniciarJogo()
    {
        // 🔥 Carrega Core primeiro
        Scene coreScene = SceneManager.GetSceneByName("CoreScene");

        if (!coreScene.IsValid() || !coreScene.isLoaded)
        {
            yield return SceneManager.LoadSceneAsync("CoreScene", LoadSceneMode.Additive);
        }

        // 🔥 Carrega fase inicial
        yield return StartCoroutine(LoadFaseAsync(faseAtual));

        // 🔍 Debug final
        DebugAudioListeners();
    }

    public void LoadFase(string novaFase)
    {
        StartCoroutine(LoadFaseAsync(novaFase));
    }

    IEnumerator LoadFaseAsync(string novaFase)
    {
        // Debug.Log("Carregando fase: " + novaFase);

        // 🧹 descarrega fase atual
        if (!string.IsNullOrEmpty(faseAtual) && SceneManager.GetSceneByName(faseAtual).isLoaded)
        {
            yield return SceneManager.UnloadSceneAsync(faseAtual);
        }

        // 📦 carrega nova fase
        yield return SceneManager.LoadSceneAsync(novaFase, LoadSceneMode.Additive);

        faseAtual = novaFase;

        // Debug.Log("Fase carregada: " + novaFase);

        // 🔍 Debug após carregar
        DebugAudioListeners();
    }

    // =========================
    // 🔍 DEBUG INTELIGENTE
    // =========================
    void DebugAudioListeners()
    {
        StartCoroutine(DebugAudioDelayed());
    }

    IEnumerator DebugAudioDelayed()
    {
        // espera 1 frame (garante que tudo carregou)
        yield return null;

        var listeners = FindObjectsOfType<AudioListener>(true);

        // Debug.Log("Total AudioListeners: " + listeners.Length);

        foreach (var l in listeners)
        {
            string path = GetFullPath(l.transform);
            // Debug.Log("Listener em: " + path + " | enabled=" + l.enabled);
        }

        // 💣 Corrige automaticamente
        if (listeners.Length > 1)
        {
            Debug.LogWarning("Mais de um AudioListener encontrado! Corrigindo...");

            for (int i = 1; i < listeners.Length; i++)
            {
                listeners[i].enabled = false;
            }
        }
    }

    // 🔍 Caminho completo do objeto
    string GetFullPath(Transform t)
    {
        string path = t.name;

        while (t.parent != null)
        {
            t = t.parent;
            path = t.name + "/" + path;
        }

        return path;
    }

    void DebugScenes()
{
    for (int i = 0; i < SceneManager.sceneCount; i++)
    {
        Scene s = SceneManager.GetSceneAt(i);
        // Debug.Log("Cena carregada: " + s.name);
    }
}

}