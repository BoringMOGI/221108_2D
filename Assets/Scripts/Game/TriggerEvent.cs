using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerEvent
{
    void OnEvent(string name);
}

// 어떠한 2D 콜라이더가 있어야한다고 제한.
[RequireComponent(typeof(Collider2D))]
public class TriggerEvent : MonoBehaviour
{
    [SerializeField] string eventName;

    // TriggerEvent의 역할
    // => 나와 출돌한 물체 중에서 '특정 조건'을 만족하는 오브젝트에게
    //    이벤트가 발생했음을 알리는 컴퍼넌트.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 대상이 인터페이스를 구현했는지 확인.
        ITriggerEvent target = collision.GetComponent<ITriggerEvent>();
        if (target != null)
            target.OnEvent(eventName);
    }
}
