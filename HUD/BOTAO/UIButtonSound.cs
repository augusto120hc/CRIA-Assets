using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour,
    IPointerEnterHandler,
    IPointerClickHandler
{
    public AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    bool CanPlay()
    {
        return audioSource != null &&
               audioSource.isActiveAndEnabled &&
               audioSource.gameObject.activeInHierarchy;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!CanPlay()) return;

        if (hoverSound != null)
            audioSource.PlayOneShot(hoverSound);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!CanPlay()) return;


        audioSource.PlayOneShot(clickSound);
    }
}