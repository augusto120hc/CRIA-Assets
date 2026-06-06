using UnityEngine;

public class PerfilGlobal : MonoBehaviour
{
    public static PerfilGlobal instance;

    public enum Perfil
    {
        Curiosa,
        Emocional,
        Racional
    }

    public Perfil perfilAtual;

    // HORÁRIO DA PRIMEIRA INTERAÇÃO
    [HideInInspector]
    public string horarioInteracao;

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

      public void RegistrarPrimeiraInteracao()
    {
        if (string.IsNullOrEmpty(horarioInteracao))
        {
            horarioInteracao =
                System.DateTime.Now.ToString("HH:mm");

            GogolPerfil.horarioPrimeiraInteracao =
                horarioInteracao;

            GogolPerfil.perfilJogador =
                perfilAtual.ToString();
        }
    }

}