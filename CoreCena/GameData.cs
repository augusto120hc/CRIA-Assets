using UnityEngine;

public enum Perfil
{
    Emocional,
    Racional,
    Curioso
}

public class GameData : MonoBehaviour
{
    public static GameData instance;

    [Header("Perfil da Sofia")]
    public Perfil perfilSofia;

    [Header("Código do Perfil")]
    public int codigoPerfil;

    private void Awake()
    {
        if (instance == null)
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