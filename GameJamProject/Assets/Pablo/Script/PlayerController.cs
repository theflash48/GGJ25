using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float inputX;
    public float inputZ;
    public float rotationSpeed;
    public float gravity = -9.8f;  // Gravedad
    public float verticalSpeed;  // Velocidad vertical (caída)
    public enum PlayerState { idle, walk };
    public PlayerState playerState;

    Animator animator;
    CharacterController controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();  
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine();
        ApplyGravity();
    }

    void StateMachine()
    {
        switch (playerState)
        {
            case PlayerState.idle:
                Idle();
                break;
            case PlayerState.walk:
                Walk();
                break;
        }
    }

    public void SetPlayerState(PlayerState s)
    {
        playerState = s;
    }

    void Idle()
    {

        if ((Input.GetAxisRaw("Horizontal") != 0) || (Input.GetAxisRaw("Vertical") != 0))
        {
            animator.SetBool("Idle", false);
            SetPlayerState(PlayerState.walk);
            Debug.Log("a");
        }
    }

    void Walk()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");

        // Si no hay input, cambia a estado de inactividad
        if (inputX == 0 && inputZ == 0)
        {
            SetPlayerState(PlayerState.idle);
            animator.SetBool("Idle", true);
        }

        Vector3 movementInput = new Vector3(inputX, 0, inputZ);

        Quaternion targetRotation = Quaternion.LookRotation(movementInput);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        Vector3 movement = movementInput.normalized * speed * Time.deltaTime;
        controller.Move(movement);
    }

    void ApplyGravity()
    {
        verticalSpeed += gravity * Time.deltaTime;  // Aumenta la velocidad vertical por la gravedad

        // Si el jugador está en el suelo, evita que siga cayendo
        if (controller.isGrounded)
        {
            if (verticalSpeed < 0)
            {
                verticalSpeed = -2f;  // Asegúrate de que no suba por error
            }
        }
    }
}
