using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpPower;

    Rigidbody2D rigid;      // 리지드바디를 참조하는 변수.
    int jumpCount;          // 점프 가능 횟수.

    // 유니티는 실행 시 모든 컴포넌트의 이벤트 함수들을 델리게이트로 등록한다.
    // 이후, 순서에 따라 해당 함수들을 호출한다.
    // 모든 순서대로 호출이 끝난 상태를 one loop(=1FRAME)이라고 하며
    // 매 프레임마다 이 행동을 반복한다.
    
    // 게임 실행 시 최초에 1번만 불리는 초기화 함수.
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();        // 나의 Rigidbody2D를 검색해서 참조한다.
        jumpCount = 1;                              // 점프 횟수를 1로 초기화.

        // 게임 오브젝트에게서 Transform 컴포넌트를 검색해 참조한다.
        Transform transform = GetComponent<Transform>();

        // 현재 내가 0,0에 있고 우측으로 5만큼 움직여야하니까
        // (5,0)을 Transform의 position에 대입하자!!
        // 하지만 이러면 내 기준이 달라졌을 때에도 항상 (5,0)으로 간다.
        // transform.position = new Vector3(5, 0);     

        // 현재 위치에서 우측으로 5 위쪽으로 2만큼 움직이는 이동량이 생긴다.
        // Vector3 movment = (Vector3.right * 5f) + (Vector3.up * 2f);       // 방향 * 거리 = 이동량.
        // transform.position += movment;                                    // 현재 위치에서 우측으로 5만큼 움직여라.

        // World 좌표(=절대 좌표)
        // = 항상 움직이지 않고 회전하지 않는 월드를 기준으로 얼만큼 떨어져있으냐..
        //   Vector3.right => 항상 (1,0)의 값을 가진다.

        // Local 좌표(=상대 좌표)
        // = 나를 기준으로 얼만큼 덜어져있고 어떻게 회전해있느냐..
        //   transform.right => 내가 회전하는 것에 따라 방향 벡터가 바뀐다.

        //transform.position += transform.right * 3f;
    }

    // 매 프레임마다 불리는 함수.
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");  // 좌측:-1, 안누르면:0, 우측:1
        //float y = Input.GetAxisRaw("Vertical");  // 하단:-1, 안누르면:0, 상단:1
        rigid.velocity = new Vector2(x * moveSpeed, rigid.velocity.y);

        #region 좌표 이동 방식

        // 방향 벡터를 구할때는 항상 Normalize(=정규화)를 마지막에 수행해야한다.
        // Vector3 direction = (transform.right * x) + (transform.up * y);
        // direction.Normalized();

        // 예를 들어 우측으로 움직인 후 상단으로 움직였을 때에는 피타고라스의 법칙에 따라서 더 멀리 이동하게 된다.
        // 따라서 우측 상단이라는 방향 벡터를 구하고 그 곳으로 moveSpeed만큼 움직인다.

        // Time.deltaTime : 이전 프레임부터 현재 프레임 까지의 시간 차이. (약 0.000032f)
        // 해당 값을 곱하는 이유는 프레임 차이에 의한 속도 차이를 보장하기 위함이다.
        // transform.position += direction * moveSpeed * Time.deltaTime;

        #endregion

        // GetKeyDown   : 해당 키를 누른 그 순간 1회.
        // GetKeyUp     : 해당 키를 땐 그 순간 1회.
        // GetKey       : 해당 키를 누르고 있을 때 계속.
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            // ForceMode2D.Force    : 지속적인 힘.
            // ForceMode2D.Impulse  : 한번에 뿜어져 나오는 힘.

            // 점프하기 직전에 현재 y축 속도를 0으로 변경한다.
            rigid.velocity = new Vector2(rigid.velocity.x, 0f);
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

            jumpCount -= 1;
        }
    }
}
