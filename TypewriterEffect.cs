using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    public TMP_Text textUI;
    public float delay = 0.03f;

    public void ShowText(string fullText)
    {
        StopAllCoroutines();
        StartCoroutine(TypeText(fullText));
    }

    IEnumerator TypeText(string fullText)
    {
        textUI.text = "";

        foreach (char c in fullText)
        {
            textUI.text += c;
            yield return new WaitForSecondsRealtime(delay); //  ESSENCIAL
        }
    }
}