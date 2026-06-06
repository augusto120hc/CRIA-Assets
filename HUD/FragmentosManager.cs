using UnityEngine;

public class FragmentosManager : MonoBehaviour
{
    public static FragmentosManager instance;

    [Header("Fragmentos")]
    public bool temClareza;
    public bool temVontade;
    public bool temEssencia;

    [Header("Avisos")]
    public bool avisoFragmentosMostrado = false;

    public bool fragmentosEntreguesAoRafa = false;

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