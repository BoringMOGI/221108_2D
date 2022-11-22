using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 실제로 콜라이더는 자식 오브젝트에게 있기 때문에
// 충돌이 일어나면 이벤트로 알려준다.
public class TriggerHandler : MonoBehaviour
{
    [SerializeField] UnityEvent onTriggerEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            onTriggerEnter?.Invoke();
    }
}
