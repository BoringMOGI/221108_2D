using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    [SerializeField] LayerMask rayMask;     // 해당 레이어를 가진 콜라이더만 Ray가 체크한다.
    float rayDistance = 10f;

    void Update()
    {
        // 현재 내 위치로부터 오른쪽으로 2의 길이를 가지는 광선을 발사.
        // RaycastHit2D : 광선에 충돌한 물체에 대한 정보가 들어있다.
        // Raycast 매개변수
        // - Vector2 origin     : 원점
        // - Vector2 direction  : 방향
        // - float distance     : 거리
        // - int layerMask      : 레이어 마스킹
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, rayDistance, rayMask);
        if (hit.collider != null)
        {
            Debug.Log($"광선에 무언가 충돌했다 : {hit.collider.name}");
            contactPoint = hit.point;       // 광선과 충돌체가 충돌한 지점(point)
        }
        else
        {
            contactPoint = null;            // 아무것도 충돌하지 않았다.
        }
    }

    // 기즈모를 그려주는 이벤트 함수.
    // = 씬 창에 그려지는 아이콘, 선 등등...
    Vector3? contactPoint = null;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if (contactPoint == null)
        {
            Gizmos.DrawRay(transform.position, Vector3.right * rayDistance);
            Gizmos.DrawWireSphere(transform.position + Vector3.right * rayDistance, 0.05f);
        }
        else
        {
            Gizmos.DrawLine(transform.position, (Vector3)contactPoint);
            Gizmos.DrawWireSphere((Vector3)contactPoint, 0.05f);
        }
    }
}
