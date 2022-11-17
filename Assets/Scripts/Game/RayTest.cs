using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    [SerializeField] LayerMask rayMask;     // �ش� ���̾ ���� �ݶ��̴��� Ray�� üũ�Ѵ�.
    float rayDistance = 10f;

    void Update()
    {
        // ���� �� ��ġ�κ��� ���������� 2�� ���̸� ������ ������ �߻�.
        // RaycastHit2D : ������ �浹�� ��ü�� ���� ������ ����ִ�.
        // Raycast �Ű�����
        // - Vector2 origin     : ����
        // - Vector2 direction  : ����
        // - float distance     : �Ÿ�
        // - int layerMask      : ���̾� ����ŷ
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, rayDistance, rayMask);
        if (hit.collider != null)
        {
            Debug.Log($"������ ���� �浹�ߴ� : {hit.collider.name}");
            contactPoint = hit.point;       // ������ �浹ü�� �浹�� ����(point)
        }
        else
        {
            contactPoint = null;            // �ƹ��͵� �浹���� �ʾҴ�.
        }
    }

    // ����� �׷��ִ� �̺�Ʈ �Լ�.
    // = �� â�� �׷����� ������, �� ���...
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
