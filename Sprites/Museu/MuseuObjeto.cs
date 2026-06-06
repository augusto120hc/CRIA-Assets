using UnityEngine;

public class MuseuObjeto : MonoBehaviour
{
    public int idObjeto;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer =
            GetComponent<SpriteRenderer>();

        AtualizarObjeto();
    }

    public void AtualizarObjeto()
    {
        bool mostrar = false;

        switch (idObjeto)
        {
            case 1:
                mostrar = GameManager.instance.objeto1Exposto;
                break;

            case 2:
                mostrar = GameManager.instance.objeto2Exposto;
                break;

            case 3:
                mostrar = GameManager.instance.objeto3Exposto;
                break;

            case 4:
                mostrar = GameManager.instance.objeto4Exposto;
                break;
        }

        spriteRenderer.enabled = mostrar;

        // Debug.Log(
        //     "Objeto Museu ID "
        //     + idObjeto
        //     + " | Mostrar: "
        //     + mostrar
        // );
    }
}