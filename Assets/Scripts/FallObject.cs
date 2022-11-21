using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObject : MonoBehaviour
{
    [SerializeField] FallManager fallManager;

    // 플레이어와 충돌 시 호출되는 이벤트 함수.
    public event System.Action onContactPlayer;
    public event System.Action onAddScore;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            // 매니저에게 알린다. (이벤트 델리게이트를 통해)
            onContactPlayer?.Invoke();
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(transform.position.y <= -6f)
        {
            // 사라진다...
            onAddScore?.Invoke();   // 스코어를 획득했다고 알린다.
            Destroy(gameObject);    // 나의 게임 오브젝를 삭제하겠다.
        }
    }

}
