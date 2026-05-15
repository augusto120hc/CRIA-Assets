using UnityEngine;

public class HairColor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cabeloRenderer;

    public void MudarCor(Color cor)
    {
        cabeloRenderer.color = cor;
    }
}