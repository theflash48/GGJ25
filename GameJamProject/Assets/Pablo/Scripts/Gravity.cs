using UnityEngine;
using UnityEngine.InputSystem;

public class Gravity : MonoBehaviour
{
    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //public float Maxspeed, speed, JumpHeight, RotationSmoothTime = 0.1f, Gravity = -15.0f, GroundedRadius = 0.28f, GroundedOffset = -1f, mVelocidadVertical, rbForce, interactDistance;
    //public LayerMask GroundLayers;
    //public bool mIsJumping2, mIsGrounded = true, canRotate = true, isDragging, isPickingUp, isPickingFinished;
    //public int isDraggingWhere;
    //private DragObjects rbCajaPullPush;
    //private Vector2 mMovementVector;
    //private PlayerInput mPlayerInput;
    //private CharacterController mController;
    //private float _rotationVelocity, _targetRotation = 0.0f, mVelocidadTerminal = 53.0f, slopeLimit;
    //private bool mIsJumpPressed, mIsJumping, mIsFalling, mIsAgacharse, sliding, mCanLevantarse, mIsActivating, mIsDead, pullPushSound;
    //private GameObject _mainCamera;
    //private Vector3 slopeSlideVelocity;
    //public Animator mCharacterAnimator;
    //public bool startDialogue, intheText;
    //private Dialogue dialogueManager;
    //public Lever lever;
    //private Vector3 slidingDirection, currentMovement;


    //void Start()
    //{
    //    speed = Maxspeed;
    //    mIsJumping2 = false;

    //    mPlayerInput = GetComponent<PlayerInput>();
    //    mController = GetComponent<CharacterController>();

    //    dialogueManager = FindObjectOfType<Dialogue>();
    //    lever = FindObjectOfType<Lever>();

    //    startDialogue = false;
    //    intheText = false;

    //    // Mira si en esta escena queremos el script de Push/Pull, si se encuentra en la escena lo añade

    //    _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

    //    if (GetComponent<DragObjects>() != null) rbCajaPullPush = GetComponent<DragObjects>();


    //}

    //void Update()
    //{
    //    // Parametro aterrizaje
    //    mIsJumping = mCharacterAnimator.GetBool("IsJumping");
    //    // Parametro caida
    //    mIsFalling = mCharacterAnimator.GetBool("IsFalling");
    //    if (!mIsActivating && !mIsDead)
    //    {

    //        // Control de estados
    //        mMovimientoPersonaje();
    //        mSaltoGravedad();
    //        mGroundCheck();
    //        mInteractuar();

    //    }
    //}

    //private void mSaltoGravedad()
    //{

    //    mIsJumpPressed = mPlayerInput.actions["Jump"].WasPressedThisFrame();

    //    // Salta si esta tocando el suelo y hemos pulsado la tecla
    //    if (mIsJumpPressed && mIsGrounded && !mIsFalling && !mIsAgacharse && !isPickingUp && !isDragging && !sliding)
    //    {
    //        Debug.Log("Entro");
    //        mCharacterAnimator.Play("Jump");
    //        mIsJumping2 = true;
    //        mCharacterAnimator.SetBool("IsFalling", true);
    //        mCharacterAnimator.SetBool("IsGrounding", true);
    //    }

    //    // Si estamos callendo sin saltar
    //    if (!mIsFalling && mVelocidadVertical < 0.0f) mVelocidadVertical = -3f;

    //    // Se calcula la velocidad de caida
    //    if (mVelocidadVertical < mVelocidadTerminal) mVelocidadVertical += Gravity * Time.deltaTime;
    //}

    //// Funcion que comprueba si estamos tocando el suelo con raycast
    //private void mGroundCheck()
    //{

    //    Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
    //    mIsGrounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    //    mCharacterAnimator.SetBool("IsGrounded", mIsGrounded);

    //    // Si estamos callendo que se active la animacion levitacion si no es un salto
    //    if (!mIsGrounded && !mIsJumping2) mCharacterAnimator.Play("Levitacion");

    //}
    //private void OnDrawGizmosSelected()
    //{

    //    Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
    //    Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

    //    if (mIsGrounded) Gizmos.color = transparentGreen;
    //    else Gizmos.color = transparentRed;

    //    Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
    //}
    //private void mMovimientoPersonaje()
    //{

    //    if (slopeSlideVelocity == Vector3.zero) sliding = false;
    //    else sliding = true;

    //    if (sliding && mController.isGrounded && !mIsGrounded)
    //    {

    //        Vector3 velocity = slopeSlideVelocity;
    //        velocity.y = mVelocidadVertical;

    //        mController.Move(velocity * Time.deltaTime);

    //    }
    //    else
    //    {

    //        // Direccion del input de movimiento
    //        mMovementVector = mPlayerInput.actions["Move"].ReadValue<Vector2>();
    //        Vector3 inputDirection = new Vector3(mMovementVector.x, 0.0f, mMovementVector.y).normalized;

    //        // Calculamos la rotacion del personaje segun el input
    //        if (mMovementVector != Vector2.zero)
    //        {

    //            mCharacterAnimator.SetBool("IsMoving", true);
    //            if (mIsGrounded && !mIsFalling && !isDragging && !mIsAgacharse && !isPickingUp) mCharacterAnimator.Play("Walk");
    //            // Se controla si puede rotar el personaje por si esta haciendo un pull push donde no puede rotar
    //            if (canRotate)
    //            {

    //                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
    //                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);

    //                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
    //            }

    //        }
    //        else
    //        {
    //            mCharacterAnimator.SetBool("IsMoving", false);
    //        }

    //        // Movimiento si no esta aterrizando
    //        if (!mIsJumping)
    //        {

    //            // Controlamos si estamos arrastrando un objeto o no
    //            if (isDragging)
    //            {

    //                pullPushRb(rbCajaPullPush.rb);

    //                // Si lo estamos arrastrando comprobamos desde donde
    //                switch (isDraggingWhere)
    //                {

    //                    // Dependiendo del caso ejerceremos una animacion u otra hacia una direccion u otra
    //                    case 1:
    //                        currentMovement = new Vector3(0.0f, 0.0f, inputDirection.z);
    //                        mController.Move(new Vector3(0.0f, 0.0f, inputDirection.z) * (speed * Time.deltaTime) + new Vector3(0.0f, mVelocidadVertical, 0.0f) * Time.deltaTime);
    //                        animPullPush(mMovementVector.y);
    //                        break;
    //                    case 2:
    //                        currentMovement = new Vector3(0.0f, 0.0f, inputDirection.z);
    //                        mController.Move(new Vector3(0.0f, 0.0f, inputDirection.z) * (speed * Time.deltaTime) + new Vector3(0.0f, mVelocidadVertical, 0.0f) * Time.deltaTime);
    //                        animPullPush(-mMovementVector.y);
    //                        break;
    //                    case 3:
    //                        currentMovement = new Vector3(inputDirection.x, 0.0f, 0.0f);
    //                        mController.Move(new Vector3(inputDirection.x, 0.0f, 0.0f) * (speed * Time.deltaTime) + new Vector3(0.0f, mVelocidadVertical, 0.0f) * Time.deltaTime);
    //                        animPullPush(mMovementVector.x);
    //                        break;
    //                    case 4:
    //                        currentMovement = new Vector3(inputDirection.x, 0.0f, 0.0f);
    //                        mController.Move(new Vector3(inputDirection.x, 0.0f, 0.0f) * (speed * Time.deltaTime) + new Vector3(0.0f, mVelocidadVertical, 0.0f) * Time.deltaTime);
    //                        animPullPush(-mMovementVector.x);
    //                        break;
    //                }
    //            }

    //            // Comprobamos si estamos cogiendo un objeto
    //            if (isPickingUp)
    //            {

    //                // Cuando terminemos la animación podremos movernos
    //                if (isPickingFinished) mController.Move(inputDirection * (speed * Time.deltaTime) + new Vector3(0.0f, mVelocidadVertical, 0.0f) * Time.deltaTime);

    //            }

    //            // Si no estamos cogiendo o arrastrando un objeto nos moveremos normalmente
    //            if (!isPickingUp && !isDragging)
    //            {
    //                mController.Move(inputDirection * (speed * Time.deltaTime) + new Vector3(0.0f, mVelocidadVertical, 0.0f) * Time.deltaTime);
    //            }

    //        }
    //    }

    //}
}
