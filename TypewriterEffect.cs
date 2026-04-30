using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    public float delay = 0.03f;

    Coroutine currentTyping;

    public void ShowText(TMP_Text target, string fullText)
    {
        if (currentTyping != null)
        {
            StopCoroutine(currentTyping);
        }

        currentTyping = StartCoroutine(TypeText(target, fullText));
    }

    IEnumerator TypeText(TMP_Text target, string fullText)
    {
        target.text = "";

        foreach (char c in fullText)
        {
            target.text += c;
            yield return new WaitForSecondsRealtime(delay);
        }

        target.text = fullText;
        target.ForceMeshUpdate();
    }
}