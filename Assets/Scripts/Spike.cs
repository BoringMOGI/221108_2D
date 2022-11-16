using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    // ���� Ʈ������ �� : �ݸ��� üũ�� �Ұ����ϱ� ������ Ʈ���� üũ�ۿ� ���Ѵ�.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable target = collision.GetComponent<Damageable>();
        if (target != null)
            target.OnDamage();
    }
}
