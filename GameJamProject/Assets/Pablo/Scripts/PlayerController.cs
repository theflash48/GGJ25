using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float currentVelocity;
    public Vector2 movementVector2;
    public float rotationSpeed;
    public float rotationSmoothTime;
    public float targetRotation;
    public bool movementActive,DentroCirculo;
    public float gravity;
    public enum PlayerState { idle, walk };
    public PlayerState playerState;

    Animator animator;
    CharacterController controller;
    PlayerInput playerInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        movementActive = true;
        DentroCirculo = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (movementActive) Movement();
    }

    private void Movement()
    {
        movementVector2 = playerInput.actions["Move"].ReadValue<Vector2>();
        

        
        if (!DentroCirculo) 
        {
            if (movementVector2 != Vector2.zero)
            {
                animator.SetBool("Running", true);
                targetRotation = Mathf.Atan2(movementVector2.x, movementVector2.y) * Mathf.Rad2Deg - 90;
            }
            else
            {
                animator.SetBool("Running", false);
            }
            // Si no hay input, cambia a estado de inactividad


            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationSpeed, rotationSmoothTime);

            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            Vector3 movement = new Vector3(-movementVector2.y, -2, movementVector2.x).normalized;

            controller.Move(speed * Time.deltaTime * movement);
        }
        else { animator.Play("ANI_Robot_01_Idle");Debug.Log("Anim"); }
        
    }


    public void SetPlayerState(PlayerState s)
    {
        playerState = s;
    }


}