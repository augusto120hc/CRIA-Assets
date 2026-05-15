using UnityEngine;
using TMPro;

public class HUDMoedas : MonoBehaviour
{
    public static HUDMoedas instance;

    [Header("Texto")]
    public TMP_Text moedasText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        AtualizarHUD(0);
    }

    public void AtualizarHUD(int quantidade)
    {
        moedasText.text = quantidade.ToString();
    }
}