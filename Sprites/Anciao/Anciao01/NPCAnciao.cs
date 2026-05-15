using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnciaoPerfil : MonoBehaviour
{
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;

    public GameObject botaoInteragir;


    private bool playerPerto = false;

  void Start()
    {
        if (dialogueUI != null)
            dialogueUI.SetActive(false);

        if (botaoInteragir != null)
            botaoInteragir.SetActive(false);
    }

    

    void Update()
    {
        if (playerPerto)
        {
            AtualizarPosicaoBotao();
        }
    }

    // =========================
    //  POSICIONA O BOTÃO ACIMA DO NPC
    void AtualizarPosicaoBotao()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.5f);
        botaoInteragir.transform.position = pos;
    }

    // =========================
    // 🧠 FUNÇÃO CHAMADA PELO BOTÃO
    public void FalarComSofia()
    {
        if (GameData.instance == null) return;

        Perfil perfil = GameData.instance.perfilSofia;

        dialogueUI.SetActive(true);

        switch (perfil)
        {
            case Perfil.Emocional:
                TypewriterEffect.instance.ShowText(dialogueText,
                "Eu vejo em você...\n" +
                "um coração que reage antes de entender.\n" +
                "O mundo vai tentar usar isso contra você.");
                break;

            case Perfil.Racional:
                TypewriterEffect.instance.ShowText(dialogueText,
                "Sua mente é afiada...\n" +
                "você analisa tudo.\n" +
                "Mas cuidado... nem tudo pode ser resolvido com lógica.");
                break;

            case Perfil.Curioso:
                TypewriterEffect.instance.ShowText(dialogueText,
                "Você busca respostas...\n" +
                "mesmo quando elas se escondem.\n" +
                "Mas alguns caminhos... mudam quem percorre.");
                break;
        }
    }

    // =========================
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerPerto = true;
            botaoInteragir.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerPerto = false;

                if (botaoInteragir != null)
                    botaoInteragir.SetActive(false);

                if (dialogueUI != null)
                    dialogueUI.SetActive(false);

                //  Para o typewriter
                if (TypewriterEffect.instance != null)
                {
                    TypewriterEffect.instance.StopTyping(dialogueText);
                }

                //  LIMPA o texto (ESSENCIAL)
                if (dialogueText != null)
                {
                    dialogueText.text = "";
                }
            }
        }
}