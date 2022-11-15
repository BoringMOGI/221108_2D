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

    bool isLockControl;     // 캐릭터 컨트롤러 막기.

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
            if(!isCrouch)               // 웅크리고 있으면 움직일 수 없다.
                inputX = Movement();
            
            Jump();
            Attack();
        } 

        anim.SetFloat("velocityY", rigid.velocity.y);   // float 파라미터 velocityY를 갱신.
        anim.SetBool("isGround", movement.IsGrounded);  // bool 파라미터 isGround의 값을 갱신.
        anim.SetBool("isRun", inputX != 0);             // bool 파라미터 isRun의 값을 갱신.
        anim.SetBool("isCrouch", isCrouch);
    }
   
    private float Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        movement.Move(x);

        // 왼쪽으로 볼 때 (플레이어 입장에서는 반대로 보는 것이다.)
        if (x <= -1)
        {
            spriteRenderer.flipX = true;
            attackable.Reverse(true);
        }
        // 오른족으로 볼 때 (플레이어 입장에서는 정 방향으로 보는 것이다.)
        else if (x >= 1)
        {
            spriteRenderer.flipX = false;
            attackable.Reverse(false);
        }

        return x;
    }
    private void Jump()
    {
        // 점프 키를 눌렀고, 실제로 Jump가 성공적으로 이루어졌을 때.
        if (Input.GetKeyDown(KeyCode.Space) && movement.Jump())
        {
            // 트리거를 누른다.
            anim.SetTrigger("onJump");
        }
    }
    private bool Crouch()
    {
        return Input.GetKey(KeyCode.DownArrow);
    }
    private void Attack()
    {
        // 특정 키를 누르면 공격 요청을 한다. (땅에 서 있어햐 한다.)
        if (Input.GetKeyDown(KeyCode.LeftControl) && movement.IsGrounded)
            attackable.Attack();
    }

}
