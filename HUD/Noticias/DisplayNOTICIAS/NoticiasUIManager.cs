using UnityEngine;

public class NoticiasUIManager : MonoBehaviour
{
    public static NoticiasUIManager instance;

    [Header("Painel")]
    public GameObject painelNoticias;

    private void Awake()
    {
        instance = this;
    }
}