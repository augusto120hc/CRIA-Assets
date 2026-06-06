using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneInicial : MonoBehaviour
{
    [Header("UI")]
    public GameObject painelCutscene;
    public TMP_Text nomeText;
    public TMP_Text dialogoText;
    public Image personagemImage;

    [Header("Sprites")]
    public Sprite sofiaSprite;
    public Sprite toySprite;

    [Header("Player")]
    public PlayerMovement movimentoPlayer;

    private IEnumerator Start()
    {
           if (GameState.introducaoConcluida)
            {
                painelCutscene.SetActive(false);
                gameObject.SetActive(false);
                yield break;
            }

    yield return null;

    movimentoPlayer = FindObjectOfType<PlayerMovement>();

    if (movimentoPlayer == null)
    {
        Debug.LogError("PlayerMovement não encontrado na cena.");
        yield break;
    }

    movimentoPlayer.podeMover = false;
    movimentoPlayer.PararImediatamente();

    painelCutscene.SetActive(true);


        yield return MostrarFala(
            "SOFIA",
            sofiaSprite,
            "Toy... você já percebeu que todo mundo parece ver uma internet diferente?");

        yield return MostrarFala(
            "TOY",
            toySprite,
            "Isso acontece porque as plataformas não mostram a mesma informação para todas as pessoas.");

        yield return MostrarFala(
            "SOFIA",
            sofiaSprite,
            "Mas a internet não deveria ser igual para todo mundo?");

        yield return MostrarFala(
            "TOY",
            toySprite,
            "Em teoria sim. Na prática, algoritmos escolhem o que cada pessoa vê.");

        yield return MostrarFala(
            "SOFIA",
            sofiaSprite,
            "Então o que eu vejo depende das minhas escolhas?");

        yield return MostrarFala(
            "TOY",
            toySprite,
            "Exatamente. Quanto mais você interage com determinados conteúdos, mais conteúdos parecidos recebe.");

        yield return MostrarFala(
            "SOFIA",
            sofiaSprite,
            "Parece conveniente.");

        yield return MostrarFala(
            "TOY",
            toySprite,
            "Às vezes é. Mas também pode limitar sua visão do mundo.");

        yield return MostrarFala(
            "SOFIA",
            sofiaSprite,
            "Então as plataformas acabam criando bolhas?");

        yield return MostrarFala(
            "TOY",
            toySprite,
            "Sim. Bolhas digitais são formadas quando os algoritmos filtram informações com base no comportamento dos usuários.");

        yield return MostrarFala(
            "SOFIA",
            sofiaSprite,
            "Quero entender como tudo isso funciona.");

        yield return MostrarFala(
            "TOY",
            toySprite,
            "Essa é a sua jornada, Sofia. Observe, explore e questione o que lhe é mostrado.");

        yield return MostrarFala(
            "SOFIA",
            sofiaSprite,
            "Então vamos começar.");

        painelCutscene.SetActive(false);

        movimentoPlayer.podeMover = true;

        GameState.introducaoConcluida = true;

        gameObject.SetActive(false);

        // Marca que a introdução já aconteceu
        GameState.introducaoConcluida = true;

        gameObject.SetActive(false);
    }

    IEnumerator MostrarFala(
        string nome,
        Sprite imagem,
        string fala)
    {
        nomeText.text = nome;
        personagemImage.sprite = imagem;

        if (TypewriterEffect.instance != null)
        {
            TypewriterEffect.instance.ShowText(
                dialogoText,
                fala
            );

            float tempo =
                fala.Length *
                TypewriterEffect.instance.delay +
                4f;

            yield return new WaitForSeconds(tempo);

            TypewriterEffect.instance.StopTyping(dialogoText);
        }
        else
        {
            dialogoText.text = fala;

            yield return new WaitForSeconds(
                Mathf.Max(3f, fala.Length * 0.05f)
            );
        }
    }
}