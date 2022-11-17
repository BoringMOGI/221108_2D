using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlap : MonoBehaviour
{
    [SerializeField] float circleRadius;                // ���� ������.
    [SerializeField] Collider2D contactCollider;        // �浹 ���� �ݶ��̴�.
    [SerializeField] Collider2D[] contactColliders;     // �浹 ���� "���" �ݶ��̴�.

    void Update()
    {
        // circleRaduis �������� ������ ���� ����� �� ���ο� ���� �ݶ��̴��� 1�� ��ȯ�Ѵ�.
        // �ƹ��͵� ������ ������ null�� ��ȯ�Ѵ�. ���� nullüũ�� "��" �ʿ��ϴ�.
        contactCollider = Physics2D.OverlapCircle(transform.position, circleRadius);        // �浹ü �� 1���� ��ȯ.
        contactColliders = Physics2D.OverlapCircleAll(transform.position, circleRadius);    // �浹ü ���θ� ��ȯ.
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }
}
