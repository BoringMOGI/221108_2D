using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ITriggerEvent
{
    [SerializeField] float godModeTime;         // 무적 시간.
    [SerializeField] GameObject gameoverText;   // 게임 오버 텍스트.

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

    public void OnLockControl()
    {
        isLockControl = true;
    }
    public void OnHit()
    {
        anim.SetTrigger("onHit");
        movement.Throw(true);
        StartCoroutine(IEEndThrow());
        StartCoroutine(IEGodMode());
    }
    public void OnDead()
    {
        isLockControl = true;                               // 컨트롤러 막기.
        movement.enabled = false;                           // 이동 컴포넌트 끄기.
        rigid.bodyType = RigidbodyType2D.Static;            // Rigidbody를 정적으로 만들기. (멈춘다)
        gameObject.layer = LayerMask.NameToLayer("God");    // 죽었기 때문에 더이상 처리하지 않는다.
        anim.SetTrigger("onDead");                          // 데드 애니메이션 호출.
    }
    public void OnEndDead()
    {
        spriteRenderer.enabled = false;
        GameManager.Instance.OnGameOver();
    }
    public void OnGoal()
    {
        // 플레이어의 승리 포즈
        // 여러가지 플레이어의 연출..
        GameManager.Instance.OnGameClear();
    }

    IEnumerator IEGodMode()
    {
        bool isVisible = true;          // 보이는가?

        float time = godModeTime;       // 제한 시간.
        float flickTime = 0.0f;         // 깜빡이는 시간 (빈도)

        gameObject.layer = LayerMask.NameToLayer("God");        // 플레이어의 레이어를 God으로 변경.

        while((time -= Time.deltaTime) > 0.0f)                  // time을 시간의 흐름에 따라 뺀다.
        {
            if((flickTime -= Time.deltaTime) <= 0.0f)           // flick을 시간의 흐름에 따라 뺀다.
            {
                isVisible = !isVisible;                         // bool 값을 반대로 돌린다.
                spriteRenderer.enabled = isVisible;             // bool 값에 따라 스프라이트를 끄고 켠다.
                flickTime = 0.05f;                               // 다시 flick에 값을 대입한다.
            }

            yield return null;
        }

        spriteRenderer.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
    IEnumerator IEEndThrow()
    {
        isLockControl = true;

        // 플레이어가 던져지고 바닥에 착지할 때 까지 계속해서 도는 무한 루프.
        // 플레이어는 날아가는 동안 캐릭터를 제어할 수 없다.
        while (true)
        {
            if (movement.IsGrounded && rigid.velocity.y <= -0.1f)
                break;

            yield return null;
        }

        isLockControl = false;
    }

    public void OnEvent(string name)
    {
        switch(name)
        {
            // 골에 도착했다는 이벤트가 발생했을 때.
            case "Goal":
                GameManager.Instance.OnGameClear();
                movement.enabled = false;
                isLockControl = true;
                gameObject.layer = LayerMask.NameToLayer("God");
                break;
        }
    }
}
