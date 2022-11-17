using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRayTest : MonoBehaviour
{
    float rayRadius = 0.5f;     // 원형 레이의 반지름.
    float rayDistance = 5f;     // 레이의 길이.

    void Update()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, rayRadius, Vector2.right, rayDistance);
        if(hit.collider != null)
        {
            contactPoint = hit.point;
        }
        else
        {
            contactPoint = null;
        }
    }

    Vector3? contactPoint;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if(contactPoint == null)
        {
            Gizmos.DrawRay(transform.position, Vector3.right * rayDistance);
            Gizmos.DrawWireSphere(transform.position + Vector3.right * rayDistance, rayRadius);
        }
        else
        {
            // 원의 기준점 위치
            Vector3 point = (Vector3)contactPoint;
            point.x -= rayRadius;
            point.y = transform.position.y;

            Gizmos.DrawLine(transform.position, point);
            Gizmos.DrawWireSphere(point, rayRadius);
        }
    }
}
