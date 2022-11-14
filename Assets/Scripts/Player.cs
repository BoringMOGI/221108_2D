using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator anim;
    Movement movement;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    bool isLockControl;     // ĳ���� ��Ʈ�ѷ� ����.

    private void Start()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<Movement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float inputX = 0f;
        bool isCrouch = false;

        if (!isLockControl)
        {
            isCrouch = Crouch();
            if(!isCrouch)               // ��ũ���� ������ ������ �� ����.
                inputX = Movement();
            
            Jump();
        } 

        anim.SetFloat("velocityY", rigid.velocity.y);   // float �Ķ���� velocityY�� ����.
        anim.SetBool("isGround", movement.IsGrounded);  // bool �Ķ���� isGround�� ���� ����.
        anim.SetBool("isRun", inputX != 0);             // bool �Ķ���� isRun�� ���� ����.
        anim.SetBool("isCrouch", isCrouch);
    }
    private float Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        movement.Move(x);

        if (x <= -1)
            spriteRenderer.flipX = true;
        else if (x >= 1)
            spriteRenderer.flipX = false;

        return x;
    }
    private void Jump()
    {
        // ���� Ű�� ������, ������ Jump�� ���������� �̷������ ��.
        if (Input.GetKeyDown(KeyCode.Space) && movement.Jump())
        {
            // Ʈ���Ÿ� ������.
            anim.SetTrigger("onJump");
        }
    }
    private bool Crouch()
    {
        return Input.GetKey(KeyCode.DownArrow);
    }

}
