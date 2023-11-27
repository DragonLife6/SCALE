using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Animator animator;
    private PlayerHealth playerHealth;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        // �������� ���� �� ���������
        if (playerHealth.isAlive)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // ���������� ������ ����
            Vector3 movement = new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime;

            if (movement != Vector3.zero)
            {
                animator.SetFloat("speed", moveSpeed);
            }
            else
            {
                animator.SetFloat("speed", 0);
            }

            // ����������� ��� �� ������
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
