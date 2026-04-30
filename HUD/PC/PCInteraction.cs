using UnityEngine;

using UnityEngine.Rendering.Universal;

public class PCInteraction : MonoBehaviour
{
    [SerializeField] private PC_UIController pcUI;
    [SerializeField] private GameObject botaoInteragir;

    [SerializeField] private GameObject setaPC;

    [SerializeField] private Light2D pcLight;// LUZ da TELA
    [SerializeField] private GameObject spriteFinalPC; //Tela Preta do PC

    

    private bool jaInteragiu = false; //Se ja interagiu com o botao pc

    void Start()
    {
        botaoInteragir.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!jaInteragiu)
            {
                botaoInteragir.SetActive(true);
                setaPC.SetActive(false);
            }

            Debug.Log("Player perto do PC");
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!jaInteragiu)
            {
                botaoInteragir.SetActive(false);
                setaPC.SetActive(true);
            }

            Debug.Log("Player saiu do PC");
        }
    }

    // 🔥 Esse método será chamado pelo botão UI
    public void AbrirPC()
    {
        jaInteragiu = true;

        botaoInteragir.SetActive(false);
        setaPC.SetActive(false);

        pcUI.AbrirPC();
        Time.timeScale = 0f;

        if (spriteFinalPC != null)
            spriteFinalPC.SetActive(true);

        if (pcLight != null)
            pcLight.enabled = false; // 💡 DESLIGA A LUZ AQUI
    }
}