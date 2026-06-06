using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    public static TypewriterEffect instance;

    public float delay = 0.03f;

    public AudioSource audioSource;
    public AudioClip typingLoopSound;

    private Dictionary<TMP_Text, Coroutine> typingCoroutines = new();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowText(TMP_Text target, string text)
    {
        if (instance == null || target == null) return;

        if (typingCoroutines.TryGetValue(target, out Coroutine old))
        {
            StopCoroutine(old);
            typingCoroutines.Remove(target);
        }

        target.text = "";

        Coroutine c = StartCoroutine(TypeText(target, text));
        typingCoroutines[target] = c;
    }

       IEnumerator TypeText(TMP_Text target, string text)
{
    if (target == null)
        yield break;

    StartSound();

    string currentText = "";

    bool insideTag = false;

    string cursor = "<color=#52ff9e><size=90%>█</size></color>";

    foreach (char c in text)
    {
        currentText += c;

        if (c == '<')
            insideTag = true;

        if (c == '>')
            insideTag = false;

        target.text = currentText + cursor;

        // delay só fora das tags
        if (!insideTag)
        {
            yield return new WaitForSecondsRealtime(delay);
        }
    }

    // PARA SOM QUANDO TERMINAR
    StopSound();

    // CURSOR PISCANDO INFINITO
    bool visible = true;

    while (true)
    {
        if (visible)
        {
            target.text = currentText + cursor;
        }
        else
        {
            target.text = currentText;
        }

        visible = !visible;

        yield return new WaitForSecondsRealtime(0.4f);
    }
}

    public void StopTyping(TMP_Text target)
    {
        if (target == null) return;

        if (typingCoroutines.TryGetValue(target, out Coroutine c))
        {
            StopCoroutine(c);
            typingCoroutines.Remove(target);
        }

        StopSound();
    }

    void StartSound()
    {
        if (audioSource && typingLoopSound)
        {
            audioSource.clip = typingLoopSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void StopSound()
    {
        if (audioSource && audioSource.isPlaying)
            audioSource.Stop();
    }
}