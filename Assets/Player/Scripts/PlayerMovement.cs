using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] bool keyboardControl;
    [SerializeField] MovementJoystick movementJoystick;

    public float moveSpeed = 5.0f;
    private Animator animator;
    private PlayerHealth playerHealth;
    public AudioClip footstepsSound;
    private AudioSource audioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // ќтримуЇмо вх≥д в≥д клав≥атури
        if (playerHealth.isAlive)
        {
            Vector3 movement;
            float horizontalInput;
            if (keyboardControl)
            {
                horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");

                // ќбчислюЇмо вектор руху
                movement = new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime;
            }
            else
            {
                movement = new Vector3(movementJoystick.joystickVec.x, movementJoystick.joystickVec.y, 0) * moveSpeed * Time.deltaTime;
                horizontalInput = movementJoystick.joystickVec.x;
            }

            if (movement != Vector3.zero)
            {
                animator.SetFloat("speed", moveSpeed);
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(footstepsSound);
                }
            }
            else
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
                animator.SetFloat("speed", 0);
            }

            // «астосовуЇмо рух до гравц€
            transform.Translate(movement);

            if (horizontalInput > 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-0.1f, 0.1f, 1f);
            }
            else if (horizontalInput < 0 && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(0.1f, 0.1f, 1f);
            }
        }
    }

    public void AddMoveSpeed(float coef)
    {
        moveSpeed += coef;
    }
}
