using System.Collections;
using UnityEngine;
using TMPro;

public class FinalDialogoController : MonoBehaviour
{
    [Header("UI")]
    public GameObject painelFinal;
    public TMP_Text dialogoTexto;

    [Header("Audio")]
    public AudioSource musicaSource;
    public AudioClip musicaFinal;

    [TextArea(2, 5)]
    public string[] falas;

    private int index = 0;
    private bool iniciado = false;

    public void IniciarFinal()
    {
        if (iniciado) return;

        iniciado = true;
        painelFinal.SetActive(true);

        StartCoroutine(RoteiroFinal());
    }

    IEnumerator RoteiroFinal()
    {
        if (musicaSource != null && musicaFinal != null)
        {
            musicaSource.Stop();
            musicaSource.clip = musicaFinal;
            musicaSource.Play();
        }

        yield return new WaitForSeconds(1f);

        while (index < falas.Length)
        {
            yield return StartCoroutine(TypeTexto(falas[index]));
            yield return new WaitForSeconds(2f);
            index++;
        }
    }

    IEnumerator TypeTexto(string texto)
    {
        dialogoTexto.text = "";

        foreach (char c in texto)
        {
            dialogoTexto.text += c;
            yield return new WaitForSeconds(0.03f);
        }
    }
}