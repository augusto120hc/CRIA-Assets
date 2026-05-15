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

            target.text = "";

            bool insideTag = false;

            foreach (char c in text)
            {
                target.text += c;

                if (c == '<')
                    insideTag = true;

                if (c == '>')
                    insideTag = false;

                // delay só em letras reais
                if (!insideTag)
                {
                    yield return new WaitForSecondsRealtime(delay);
                }
            }

            StopSound();

            typingCoroutines.Remove(target);
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