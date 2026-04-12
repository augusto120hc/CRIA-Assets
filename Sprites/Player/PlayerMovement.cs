using UnityEngine; 
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 move;

    public Vector2 direcaoOlhar = Vector2.down;

    public bool podeMover = true;
    public bool emRecuo = false;

    private PlayerInputActions controls;

    public bool estaPuxando = false;
    public bool apertouInteragir = false;

    // Última direção válida
    private float lastMoveX = 0;
    private float lastMoveY = -1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        controls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        controls.Player.Enable();

        controls.Player.Move.performed += OnMove;
        controls.Player.Move.canceled += OnMoveCancel;

        controls.Player.Submit.performed += OnInteract;
        controls.Player.Submit.canceled += OnInteractCancel;
    }

    private void OnDisable()
    {
        controls.Player.Move.performed -= OnMove;
        controls.Player.Move.canceled -= OnMoveCancel;

        controls.Player.Submit.performed -= OnInteract;
        controls.Player.Submit.canceled -= OnInteractCancel;

        controls.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        move = ctx.ReadValue<Vector2>();
    }

    private void OnMoveCancel(InputAction.CallbackContext ctx)
    {
        move = Vector2.zero;
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        apertouInteragir = true;
    }

    private void OnInteractCancel(InputAction.CallbackContext ctx)
    {
        apertouInteragir = false;
    }

    void Update()
{
    // 🔥 garante leitura imediata do input
    move = controls.Player.Move.ReadValue<Vector2>();

    bool estaMovendo = move.sqrMagnitude > 0.01f;

    if (estaMovendo)
    {
        direcaoOlhar = move.normalized;

        float absX = Mathf.Abs(move.x);
        float absY = Mathf.Abs(move.y);

        if (absX > absY)
        {
            lastMoveX = move.x > 0 ? 1 : -1;
            lastMoveY = 0;
        }
        else
        {
            lastMoveX = 0;
            lastMoveY = move.y > 0 ? 1 : -1;
        }

        animator.SetFloat("MoveX", move.x);
        animator.SetFloat("MoveY", move.y);
    }
    else
    {
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", 0);
    }

    animator.SetFloat("LastMoveX", lastMoveX);
    animator.SetFloat("LastMoveY", lastMoveY);

    animator.SetBool("IsMoving", estaMovendo);
    animator.SetBool("Puxando", estaPuxando);
}
    private void FixedUpdate()
    {
        if (!podeMover || emRecuo)
            return;

        Vector2 normalizedMove = move.normalized;

        rb.MovePosition(rb.position + normalizedMove * moveSpeed * Time.fixedDeltaTime);

        animator.SetFloat("MoveX", move.x);
        animator.SetFloat("MoveY", move.y);
        animator.SetBool("IsMoving", move.sqrMagnitude > 0.01f);
    }
}