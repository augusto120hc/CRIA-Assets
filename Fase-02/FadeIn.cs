using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public float tempoPreto = 4f; //  tempo na tela preta
    public float speed = 2f;        //  velocidade do fade

    IEnumerator Start()
    {
        canvasGroup.alpha = 1f;

        //  fica preto por mais tempo
        yield return new WaitForSeconds(tempoPreto);

        // 🌫 começa o fade
        while (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= Time.deltaTime * speed;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }
}