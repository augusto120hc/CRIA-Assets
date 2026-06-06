using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArmarioMuseu : MonoBehaviour
{
    [Header("UI")]
    public GameObject painelDialogo;

    public TextMeshProUGUI textoPergunta;

    public Button btnSim;
    public Button btnNao;

    private bool playerPerto;

    [Header("Audio")]
    public AudioSource audioSource;

    public AudioClip somDevolver;

    public Button btnFechar;

    void Start()
    {
        painelDialogo.SetActive(false);

        textoPergunta.color = Color.white;

        textoPergunta.text =
            "Deseja devolver os objetos ao museu?";

        btnSim.onClick.AddListener(DevolverObjetos);

        btnNao.onClick.AddListener(FecharPainel);

        btnFechar.gameObject.SetActive(false);
btnFechar.onClick.AddListener(FecharFinal);
    }

    void Update()
    {
        if (playerPerto)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                painelDialogo.SetActive(true);

                // RESETA TEXTO
                textoPergunta.color = Color.white;

                textoPergunta.text =
                    "Deseja devolver os objetos ao museu?";

                // REATIVA BOTÕES
                btnSim.gameObject.SetActive(true);

                btnNao.gameObject.SetActive(true);
            }
        }
    }

    public void DevolverObjetos()
    {
        bool possuiObjetos =
            GameManager.instance.coletouObjeto1 ||
            GameManager.instance.coletouObjeto2 ||
            GameManager.instance.coletouObjeto3 ||
            GameManager.instance.coletouObjeto4;

        // NÃO POSSUI OBJETOS
        if (!possuiObjetos)
        {
            // textoPergunta.color = Color.red;

            textoPergunta.text =
                "<color=#df3e23>Você não tem os objetos que representam a Memória Subversiva.</color>\nEncontre-os e traga-os para futuras gerações.";

            // 👇 mostra o botão de fechar
            btnFechar.gameObject.SetActive(true);

            // 👇 esconde os outros botões
            btnSim.gameObject.SetActive(false);
            btnNao.gameObject.SetActive(false);

            // GameManager.instance.memoriasSubversivasDevolvidas = true;

            return;
        }

        // OBJETO 1
        if (GameManager.instance.coletouObjeto1)
        {
            GameManager.instance.objeto1Exposto = true;
        }

        // OBJETO 2
        if (GameManager.instance.coletouObjeto2)
        {
            GameManager.instance.objeto2Exposto = true;
        }

        // OBJETO 3
        if (GameManager.instance.coletouObjeto3)
        {
            GameManager.instance.objeto3Exposto = true;
        }

        // OBJETO 4
        if (GameManager.instance.coletouObjeto4)
        {
            GameManager.instance.objeto4Exposto = true;
        }

        GameManager.instance.memoriasSubversivasDevolvidas = true;

        Debug.Log("✓ Memórias Subversivas devolvidas ao museu!");

        // SOM
        if (audioSource != null && somDevolver != null)
        {
            audioSource.PlayOneShot(somDevolver);
        }

        // ATUALIZA OBJETOS DO MUSEU
        MuseuObjeto[] objetos =
            FindObjectsOfType<MuseuObjeto>();

        foreach (MuseuObjeto obj in objetos)
        {
            obj.AtualizarObjeto();
        }

        // ESCONDE BOTÕES
        btnSim.gameObject.SetActive(false);

        btnNao.gameObject.SetActive(false);

        // TEXTO FINAL
        textoPergunta.color = Color.white;

        textoPergunta.text =
            "Algoritmos favorecem tendências globais e enterram culturas pequenas.\n\n" +
            "As Memórias Subversivas guardam aquilo que os algoritmos não conseguem copiar.\n"+
            "elas representam as menorias.";

        // MOSTRA BOTÃO FINAL
        btnFechar.gameObject.SetActive(true);
                
    }

    public void FecharPainel()
    {
        painelDialogo.SetActive(false);

        // REATIVA BOTÕES
        btnSim.gameObject.SetActive(true);

        btnNao.gameObject.SetActive(true);

        // RESETA TEXTO
        // textoPergunta.color = Color.white;

        textoPergunta.text =
"Deseja devolver os objetos que representam as Memórias Subversivas ao museu?\n\n" +
"<color=#f9a31b>valorização de culturas locais e periféricas e resistência cultural de personagens invisibilizados</color>";
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerPerto = true;

            // ABRE AUTOMATICAMENTE
            painelDialogo.SetActive(true);

            // textoPergunta.color = Color.white;

            textoPergunta.text =
                "Deseja devolver os objetos que representam as Memórias Subversivas ao museu?\n\n" +
"<color=#f9a31b>valorização de culturas locais e periféricas e resistência cultural de personagens invisibilizados</color>";
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerPerto = false;

            // FECHA O PAINEL
            painelDialogo.SetActive(false);

            // RESETA BOTÕES
            btnSim.gameObject.SetActive(true);
            btnNao.gameObject.SetActive(true);

            // ESCONDE BOTÃO FINAL
            btnFechar.gameObject.SetActive(false);

            // RESETA TEXTO
            // textoPergunta.color = Color.white;

            textoPergunta.text =
                "Deseja devolver os objetos que representam as Memórias Subversivas ao museu?\n\n" +
"<color=#f9a31b>valorização de culturas locais e periféricas e resistência cultural de personagens invisibilizados</color>";
        }
    }

    public void FecharFinal()
    {
        painelDialogo.SetActive(false);

        btnFechar.gameObject.SetActive(false);

        btnSim.gameObject.SetActive(true);
        btnNao.gameObject.SetActive(true);

        // textoPergunta.color = Color.white;
        textoPergunta.text = "Deseja devolver os objetos que representam as Memórias Subversivas ao museu?\n\n" +
"<color=#f9a31b>valorização de culturas locais e periféricas e resistência cultural de personagens invisibilizados</color>";
    }
}