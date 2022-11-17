using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ITriggerEvent
{
    [SerializeField] float godModeTime;         // ���� �ð�.
    [SerializeField] GameObject gameoverText;   // ���� ���� �ؽ�Ʈ.

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
        isLockControl = true;                               // ��Ʈ�ѷ� ����.
        movement.enabled = false;                           // �̵� ������Ʈ ����.
        rigid.bodyType = RigidbodyType2D.Static;            // Rigidbody�� �������� �����. (�����)
        gameObject.layer = LayerMask.NameToLayer("God");    // �׾��� ������ ���̻� ó������ �ʴ´�.
        anim.SetTrigger("onDead");                          // ���� �ִϸ��̼� ȣ��.
    }
    public void OnEndDead()
    {
        spriteRenderer.enabled = false;
        GameManager.Instance.OnGameOver();
    }
    public void OnGoal()
    {
        // �÷��̾��� �¸� ����
        // �������� �÷��̾��� ����..
        GameManager.Instance.OnGameClear();
    }

    IEnumerator IEGodMode()
    {
        bool isVisible = true;          // ���̴°�?

        float time = godModeTime;       // ���� �ð�.
        float flickTime = 0.0f;         // �����̴� �ð� (��)

        gameObject.layer = LayerMask.NameToLayer("God");        // �÷��̾��� ���̾ God���� ����.

        while((time -= Time.deltaTime) > 0.0f)                  // time�� �ð��� �帧�� ���� ����.
        {
            if((flickTime -= Time.deltaTime) <= 0.0f)           // flick�� �ð��� �帧�� ���� ����.
            {
                isVisible = !isVisible;                         // bool ���� �ݴ�� ������.
                spriteRenderer.enabled = isVisible;             // bool ���� ���� ��������Ʈ�� ���� �Ҵ�.
                flickTime = 0.05f;                               // �ٽ� flick�� ���� �����Ѵ�.
            }

            yield return null;
        }

        spriteRenderer.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
    IEnumerator IEEndThrow()
    {
        isLockControl = true;

        // �÷��̾ �������� �ٴڿ� ������ �� ���� ����ؼ� ���� ���� ����.
        // �÷��̾�� ���ư��� ���� ĳ���͸� ������ �� ����.
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
            // �� �����ߴٴ� �̺�Ʈ�� �߻����� ��.
            case "Goal":
                GameManager.Instance.OnGameClear();
                movement.enabled = false;
                isLockControl = true;
                gameObject.layer = LayerMask.NameToLayer("God");
                break;
        }
    }
}
