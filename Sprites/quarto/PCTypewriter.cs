using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class PCTypewriter : MonoBehaviour
{
    [Header("Configuração")]
    public float delay = 0.03f;

    [Header("Som")]
    public AudioSource audioSource;
    public AudioClip typingLoopSound;

    public System.Action OnFinishTyping;

    private Dictionary<TMP_Text, Coroutine> typingCoroutines = new();

    public void ShowText(TMP_Text target, string fullText)
    {
        if (target == null) return;

        if (typingCoroutines.TryGetValue(target, out Coroutine old))
        {
            StopCoroutine(old);
            typingCoroutines.Remove(target);
        }

        Coroutine c = StartCoroutine(TypeText(target, fullText));
        typingCoroutines[target] = c;
    }

    IEnumerator TypeText(TMP_Text target, string text)
    {
        target.text = "";

        StartSound();

        foreach (char c in text)
        {
            if (target == null) yield break;

            target.text += c;
            yield return new WaitForSecondsRealtime(delay);
        }

        StopSound();

        typingCoroutines.Remove(target);

        // 💥 evento correto (UMA SÓ VEZ)
        OnFinishTyping?.Invoke();
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
        if (audioSource == null || typingLoopSound == null) return;

        audioSource.clip = typingLoopSound;
        audioSource.loop = true;

        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    void StopSound()
    {
        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();
    }
}