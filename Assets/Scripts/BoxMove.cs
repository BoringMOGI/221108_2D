using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BoxMove : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;    // 리스폰되는 위치. (시작위치)
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] int point;                 // 포인트 수
    [SerializeField] int coin;                  // 코인 수

    Rigidbody2D rigid;    // 참조 변수 rigid.
    
    // 유니티가 부르는 이벤트 함수. (최초에 1회 불린다)
    void Start()
    {
        // 나에게서 Rigidbody2D컴포넌트를 검색해 rigid변수에 대입.
        // 만약 값을 찾지 못했다면 null을 반환.
        rigid = GetComponent<Rigidbody2D>();
        transform.position = respawnPoint.position;
        
        // nameof(대상) : 대상의 이름을 string으로 반환한다.
        Invoke(nameof(Talk), 1.0f);   // 1초 뒤에 Talk함수를 실행하라
    }
    private void Talk()
    {
        Debug.Log("외치기!!!");
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
        // 지연 실행 2가지 방법
        // 1. 코루틴 이용하기
        // 2. Invoke 이용하기
    }
    private void Respawn()
    {
        Debug.Log("플레이어가 리스폰 되었다");
        transform.position = respawnPoint.position;
        gameObject.SetActive(true);
    }
    

    void Update()
    {
        // rigid.velocity => 나의 현재 속도 : 실제로 물리력에 영향.
        // 우리는 x축 그러니까 좌,우 움직임을 제어하고 싶다.
        // y축은 건들지 않고 싶다. 그래서 값을 대입할 때 현재 자신의 y값을 대입했다.
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
