using UnityEngine;
using static PlayerController;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float inputForce;
    public float inputX;
    public float inputZ;
    public float rotation;

    public enum PlayerState { idle, walk };
    public PlayerState playerState;

    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
        animator.Play("Idle");

        if ((Input.GetAxis("Horizontal") != 0) && (Input.GetAxis("Vertical") != 0))
        {
            SetPlayerState(PlayerState.walk);
        }
    }

    void Walk()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");
        rotation = Mathf.Atan2(inputX, inputZ) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

        animator.Play("Walk");

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if ((Input.GetAxis("Horizontal") == 0) && (Input.GetAxis("Vertical") == 0))
        {
            SetPlayerState(PlayerState.idle);
        }
    }
}
