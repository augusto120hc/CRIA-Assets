using UnityEngine;
using TMPro;
using System.Collections;

public class AvisoFragmentos : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject painelAviso;

    public TMP_Text textoAviso;

    [Header("Tempo")]
    public float tempoTela = 7f;

    

    void Start()
    {
        if(painelAviso != null)
        {
            painelAviso.SetActive(false);
        }
    }

    void Update()
    {
        if(FragmentosManager.instance == null)
            return;

        // já mostrou antes
        if(FragmentosManager.instance.avisoFragmentosMostrado)
            return;

        // todos fragmentos coletados
        if(
            FragmentosManager.instance.temClareza &&
            FragmentosManager.instance.temVontade &&
            FragmentosManager.instance.temEssencia
        )
        {
            FragmentosManager.instance.avisoFragmentosMostrado = true;

            StartCoroutine(MostrarAviso());
        }
    }

    IEnumerator MostrarAviso()
    {
        painelAviso.SetActive(true);

        textoAviso.text =
        "<b><color=#52ff9e>" +
        "Todos fragmentos coletados.\n" +
        "Leve os fragmentos ao Rafa." +
        "</color></b>";

        yield return new WaitForSeconds(tempoTela);

        painelAviso.SetActive(false);
    }
}