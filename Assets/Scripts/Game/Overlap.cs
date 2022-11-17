using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlap : MonoBehaviour
{
    [SerializeField] float circleRadius;                // 원의 반지름.
    [SerializeField] Collider2D contactCollider;        // 충돌 중인 콜라이더.
    [SerializeField] Collider2D[] contactColliders;     // 충돌 중인 "모든" 콜라이더.

    void Update()
    {
        // circleRaduis 반지름을 가지는 원을 만들어 그 내부에 들어온 콜라이더를 1개 반환한다.
        // 아무것도 들어오지 않으면 null을 반환한다. 따라서 null체크가 "꼭" 필요하다.
        contactCollider = Physics2D.OverlapCircle(transform.position, circleRadius);        // 충돌체 중 1개만 반환.
        contactColliders = Physics2D.OverlapCircleAll(transform.position, circleRadius);    // 충돌체 전부를 반환.
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }
}
