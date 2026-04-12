using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour
{
    [Header("Player e Controle")]
    private PlayerMovement playerScript;
    private bool playerNaArea = false;

    [Header("Som")]
    public AudioClip somAbrir;
    private AudioSource audioSource;

    [Header("Animação do Baú")]
    public Animator animator;

    [Header("Interação")]
    public float tempoPuxando = 1.0f;
    public float forcaRecuo = 5f; // 🔥 mais simples e consistente
    public float tempoRecuo = 0.3f;

    [Header("Moedas")]
    public GameObject moedaPrefab;
    public int qtdMoedas = 20;
    public float espalhamento = 2f;

    private bool bauAberto = false;
    private Coroutine puxarCoroutine = null;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        if (!playerNaArea || playerScript == null || bauAberto)
            return;

        playerScript.estaPuxando = playerScript.apertouInteragir;

        if (playerScript.apertouInteragir && puxarCoroutine == null)
            puxarCoroutine = StartCoroutine(PuxarEabrir());

        if (!playerScript.apertouInteragir && puxarCoroutine != null)
        {
            StopCoroutine(puxarCoroutine);
            puxarCoroutine = null;
            playerScript.estaPuxando = false;
        }
    }

    private IEnumerator PuxarEabrir()
    {
        float tempoSegurando = 0f;

        while (tempoSegurando < tempoPuxando)
        {
            if (!playerScript.apertouInteragir)
                yield break;

            tempoSegurando += Time.deltaTime;
            yield return null;
        }

        AbrirBau();
        puxarCoroutine = null;
    }

    private void AbrirBau()
        {
            if (bauAberto) return;

            bauAberto = true;

            if (somAbrir != null)
                audioSource.PlayOneShot(somAbrir);

            if (playerScript != null)
            {
                playerScript.estaPuxando = false;

                Rigidbody2D rb = playerScript.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    // 🔥 EMPURRÃO FIXO (SEM DIREÇÃO DO PLAYER)
                    Vector2 direcao = (playerScript.transform.position - transform.position).normalized;

                    if (direcao == Vector2.zero)
                        direcao = Vector2.up;

                    rb.velocity = direcao * forcaRecuo;

                    playerScript.podeMover = false;
                    StartCoroutine(ReativarMovimento(playerScript, tempoRecuo));
                }
            }

            if (animator != null)
                animator.SetTrigger("Abrir");

            StartCoroutine(SpawnMoedas());

            Debug.Log("Baú aberto!");
}
    private IEnumerator ReativarMovimento(PlayerMovement p, float delay)
    {
        yield return new WaitForSeconds(delay);
        p.podeMover = true;
    }

    private IEnumerator SpawnMoedas()
    {
        for (int i = 0; i < qtdMoedas; i++)
        {
            if (moedaPrefab == null) break;

            GameObject moeda = Instantiate(moedaPrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb = moeda.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0f;

                float velX = Random.Range(-espalhamento, espalhamento);
                float velY = Random.Range(0.5f, espalhamento);

                rb.velocity = new Vector2(velX, velY);
                StartCoroutine(DesacelerarMoeda(rb, 0.5f));
            }

            yield return new WaitForSeconds(0.02f);
        }
    }

    private IEnumerator DesacelerarMoeda(Rigidbody2D rb, float tempo)
    {
        float elapsed = 0f;
        Vector2 startVel = rb.velocity;

        while (elapsed < tempo)
        {
            elapsed += Time.deltaTime;
            rb.velocity = Vector2.Lerp(startVel, Vector2.zero, elapsed / tempo);
            yield return null;
        }

        rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNaArea = true;
            playerScript = other.GetComponent<PlayerMovement>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNaArea = false;

            if (playerScript != null)
                playerScript.estaPuxando = false;

            if (puxarCoroutine != null)
                StopCoroutine(puxarCoroutine);

            puxarCoroutine = null;
            playerScript = null;
        }
    }
}