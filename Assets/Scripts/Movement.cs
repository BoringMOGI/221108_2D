using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    // 유니티는 실행 시 모든 컴포넌트의 이벤트 함수들을 델리게이트로 등록한다.
    // 이후, 순서에 따라 해당 함수들을 호출한다.
    // 모든 순서대로 호출이 끝난 상태를 one loop(=1FRAME)이라고 하며
    // 매 프레임마다 이 행동을 반복한다.
    
    // 게임 실행 시 최초에 1번만 불리는 초기화 함수.
    void Start()
    {
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
        float y = Input.GetAxisRaw("Vertical");    // 하단:-1, 안누르면:0, 상단:1
        transform.position += transform.right * moveSpeed * x * Time.deltaTime;
        transform.position += transform.up * moveSpeed * y * Time.deltaTime;
    }
}
