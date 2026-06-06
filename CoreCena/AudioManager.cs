using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicaMenu;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += QuandoCenaCarrega;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= QuandoCenaCarrega;
    }

    void QuandoCenaCarrega(Scene cena, LoadSceneMode modo)
    {
        // MENU
        if(cena.name == "Menu")
        {
            if(!musicaMenu.isPlaying)
                musicaMenu.Play();
        }
        else
        {
            musicaMenu.Stop();
        }
    }
}