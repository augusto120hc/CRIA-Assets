using System.Collections;
using UnityEngine;

public class ScreenGlitch : MonoBehaviour
{
    public RectTransform panel;
    public float intensity = 10f;
    public float duration = 0.2f;

    public IEnumerator Glitch()
    {
        Vector3 original = panel.localPosition;
        float t = 0;

        while (t < duration)
        {
            float x = Random.Range(-intensity, intensity);
            float y = Random.Range(-intensity, intensity);

            panel.localPosition = original + new Vector3(x, y, 0);

            t += Time.deltaTime;
            yield return null;
        }

        panel.localPosition = original;
    }
}