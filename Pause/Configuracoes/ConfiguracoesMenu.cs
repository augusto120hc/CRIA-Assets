using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfiguracoesMenu : MonoBehaviour
{
    [Header("Audio")]
    public Slider sliderVolume;

    [Header("Brilho")]
    public Slider sliderBrilho;
    public Image painelBrilho;

    [Header("Resolução")]
    public TMP_Dropdown dropdownResolucao;

    Resolution[] resolucoes;

    void Start()
    {
        // ---------------- VOLUME ----------------
        sliderVolume.value = AudioListener.volume;
        sliderVolume.onValueChanged.AddListener(AlterarVolume);

        // ---------------- BRILHO ----------------
        sliderBrilho.value = painelBrilho.color.a;
        sliderBrilho.onValueChanged.AddListener(AlterarBrilho);

        // ---------------- RESOLUÇÕES ----------------
        dropdownResolucao.ClearOptions();

        resolucoes = new Resolution[3];

        resolucoes[0] = new Resolution { width = 1280, height = 720 };
        resolucoes[1] = new Resolution { width = 1920, height = 1080 };
        resolucoes[2] = new Resolution { width = 2560, height = 1440 };

        dropdownResolucao.options.Add(new TMP_Dropdown.OptionData("1280x720"));
        dropdownResolucao.options.Add(new TMP_Dropdown.OptionData("1920x1080"));
        dropdownResolucao.options.Add(new TMP_Dropdown.OptionData("2560x1440"));

        dropdownResolucao.onValueChanged.AddListener(MudarResolucao);
    }

    // ---------------- VOLUME ----------------

    public void AlterarVolume(float valor)
    {
        AudioListener.volume = valor;
    }

    // ---------------- BRILHO ----------------

    public void AlterarBrilho(float valor)
    {
        Color cor = painelBrilho.color;

        cor.a = valor;

        painelBrilho.color = cor;
    }

    // ---------------- RESOLUÇÃO ----------------

    public void MudarResolucao(int index)
    {
        Resolution res = resolucoes[index];

        Screen.SetResolution(res.width, res.height, true);
    }
   // ---------------- FECHAR ----------------
     public void FecharConfiguracoes()
    {
        gameObject.SetActive(false);
    }
}