using UnityEngine;

public class DirecaoToy : MonoBehaviour
{
    private Transform player;

    private Vector3 escalaOriginal;

    void Start()
    {
        escalaOriginal = transform.localScale;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null)
            return;

        // Player está à direita
        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(escalaOriginal.x),
                escalaOriginal.y,
                escalaOriginal.z
            );
        }
        // Player está à esquerda
        else
        {
            transform.localScale = new Vector3(
                -Mathf.Abs(escalaOriginal.x),
                escalaOriginal.y,
                escalaOriginal.z
            );
        }
    }
}