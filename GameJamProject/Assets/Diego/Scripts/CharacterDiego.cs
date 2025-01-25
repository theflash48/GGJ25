using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterDiego : MonoBehaviour
{
    public float Maxspeed, speed, JumpHeight, RotationSmoothTime = 0.1f, mVelocidadVertical, rbForce, interactDistance;
    public bool mIsJumping2, mIsGrounded = true, canRotate = true, isDragging, isPickingUp, isPickingFinished;
    public int isDraggingWhere;
    private Vector2 mMovementVector;
    private PlayerInput mPlayerInput;
    private CharacterController mController;
    private float _rotationVelocity, _targetRotation = 0.0f, mVelocidadTerminal = 53.0f, slopeLimit;
    private bool mIsJumpPressed, mIsJumping, mIsFalling, mIsAgacharse, sliding, mCanLevantarse, mIsActivating, mIsDead, pullPushSound;
    private GameObject _mainCamera;
    private Vector3 slopeSlideVelocity;
    public Animator mCharacterAnimator;


    void Start()
    {
        speed = Maxspeed;
        mIsJumping2 = false;

        mPlayerInput = GetComponent<PlayerInput>();
        mController = GetComponent<CharacterController>();


        // Mira si en esta escena queremos el script de Push/Pull, si se encuentra en la escena lo añade

        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");


    }

    void Update()
    {
        // Parametro aterrizaje
        mIsJumping = mCharacterAnimator.GetBool("IsJumping");
        // Parametro caida
        mIsFalling = mCharacterAnimator.GetBool("IsFalling");
        if (!mIsActivating && !mIsDead)
        {

            // Control de estados
            mMovimientoPersonaje();
          

        }
    }

  

    // Funcion que comprueba si estamos tocando el suelo con raycast


    private void mMovimientoPersonaje()
    {

        if (slopeSlideVelocity == Vector3.zero) sliding = false;
        else sliding = true;

        if (sliding && mController.isGrounded && !mIsGrounded)
        {

            Vector3 velocity = slopeSlideVelocity;
            velocity.y = mVelocidadVertical;

            mController.Move(velocity * Time.deltaTime);

        }
        else
        {

            // Direccion del input de movimiento
            mMovementVector = mPlayerInput.actions["Move"].ReadValue<Vector2>();
            Vector3 inputDirection = new Vector3(mMovementVector.x, 0.0f, mMovementVector.y).normalized;

            // Calculamos la rotacion del personaje segun el input
            if (mMovementVector != Vector2.zero)
            {

                mCharacterAnimator.SetBool("IsMoving", true);
                if (mIsGrounded && !mIsFalling && !isDragging && !mIsAgacharse && !isPickingUp) mCharacterAnimator.Play("Walk");
                // Se controla si puede rotar el personaje por si esta haciendo un pull push donde no puede rotar
                if (canRotate)
                {

                    _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
                    float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);

                    transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
                }

            }
            else
            {
                mCharacterAnimator.SetBool("IsMoving", false);
            }


            // Si no estamos cogiendo o arrastrando un objeto nos moveremos normalmente
            if (!isPickingUp && !isDragging)
            {
                mController.Move(inputDirection * (speed * Time.deltaTime) + new Vector3(0.0f, mVelocidadVertical, 0.0f) * Time.deltaTime);
            }
        }

    }
}
