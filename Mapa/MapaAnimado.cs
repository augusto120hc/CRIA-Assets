using UnityEngine;
using UnityEngine.UI;

public class MapaAnimado : MonoBehaviour
{
    public Image imagemMapa;
    public Sprite[] frames;

    public float fps = 8f;

    private int frameAtual;

    void Update()
    {
        if (frames.Length == 0)
            return;

        frameAtual = (int)(Time.time * fps) % frames.Length;

        imagemMapa.sprite = frames[frameAtual];
    }
}