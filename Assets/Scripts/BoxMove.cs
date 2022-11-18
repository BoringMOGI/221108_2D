using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BoxMove : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;    // �������Ǵ� ��ġ. (������ġ)
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] int point;                 // ����Ʈ ��
    [SerializeField] int coin;                  // ���� ��

    Rigidbody2D rigid;    // ���� ���� rigid.
    
    // ����Ƽ�� �θ��� �̺�Ʈ �Լ�. (���ʿ� 1ȸ �Ҹ���)
    void Start()
    {
        // �����Լ� Rigidbody2D������Ʈ�� �˻��� rigid������ ����.
        // ���� ���� ã�� ���ߴٸ� null�� ��ȯ.
        rigid = GetComponent<Rigidbody2D>();
        transform.position = respawnPoint.position;
        
        // nameof(���) : ����� �̸��� string���� ��ȯ�Ѵ�.
        Invoke(nameof(Talk), 1.0f);   // 1�� �ڿ� Talk�Լ��� �����϶�
    }
    private void Talk()
    {
        Debug.Log("��ġ��!!!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Item":
                Item item = collision.GetComponent<Item>();
                OnGetItem(item);
                collision.gameObject.SetActive(false);
                break;
            case "Trap":
                OnDead();
                break;
        }
    }
    private void OnGetItem(Item item)
    {
        switch(item.Type)
        {
            case ITEM.Coin:
                coin += 1;

                break;
            case ITEM.Life:

                break;
            case ITEM.Point:
                point += 1;

                break;
        }
    }
    private void OnDead()
    {
        gameObject.SetActive(false);
        Invoke(nameof(Respawn), 1.0f);
        // ���� ���� 2���� ���
        // 1. �ڷ�ƾ �̿��ϱ�
        // 2. Invoke �̿��ϱ�
    }
    private void Respawn()
    {
        Debug.Log("�÷��̾ ������ �Ǿ���");
        transform.position = respawnPoint.position;
        gameObject.SetActive(true);
    }
    

    void Update()
    {
        // rigid.velocity => ���� ���� �ӵ� : ������ �����¿� ����.
        // �츮�� x�� �׷��ϱ� ��,�� �������� �����ϰ� �ʹ�.
        // y���� �ǵ��� �ʰ� �ʹ�. �׷��� ���� ������ �� ���� �ڽ��� y���� �����ߴ�.
        float x = Input.GetAxis("Horizontal");
        if(x != 0)
            rigid.velocity = new Vector2(moveSpeed * x, rigid.velocity.y);

        if (Input.GetKeyDown(KeyCode.C))
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 0f);
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }
}
