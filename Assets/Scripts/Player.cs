using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator anim;
    Movement movement;
    Attackable attackable;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    bool isLockControl;     // ĳ���� ��Ʈ�ѷ� ����.

    private void Start()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<Movement>();
        attackable = GetComponent<Attackable>();
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
            Attack();
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

        // �������� �� �� (�÷��̾� ���忡���� �ݴ�� ���� ���̴�.)
        if (x <= -1)
        {
            spriteRenderer.flipX = true;
            attackable.Reverse(true);
        }
        // ���������� �� �� (�÷��̾� ���忡���� �� �������� ���� ���̴�.)
        else if (x >= 1)
        {
            spriteRenderer.flipX = false;
            attackable.Reverse(false);
        }

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
    private void Attack()
    {
        // Ư�� Ű�� ������ ���� ��û�� �Ѵ�. (���� �� �־��� �Ѵ�.)
        if (Input.GetKeyDown(KeyCode.LeftControl) && movement.IsGrounded)
            attackable.Attack();
    }

}
