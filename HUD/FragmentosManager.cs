using UnityEngine;

public class FragmentosManager : MonoBehaviour
{
    public static FragmentosManager instance;

    [Header("Fragmentos")]
    public bool temClareza;
    public bool temVontade;
    public bool temEssencia;

    private void Awake()
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
}