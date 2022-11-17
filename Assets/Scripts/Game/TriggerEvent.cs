using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerEvent
{
    void OnEvent(string name);
}

// ��� 2D �ݶ��̴��� �־���Ѵٰ� ����.
[RequireComponent(typeof(Collider2D))]
public class TriggerEvent : MonoBehaviour
{
    [SerializeField] string eventName;

    // TriggerEvent�� ����
    // => ���� �⵹�� ��ü �߿��� 'Ư�� ����'�� �����ϴ� ������Ʈ����
    //    �̺�Ʈ�� �߻������� �˸��� ���۳�Ʈ.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ����� �������̽��� �����ߴ��� Ȯ��.
        ITriggerEvent target = collision.GetComponent<ITriggerEvent>();
        if (target != null)
            target.OnEvent(eventName);
    }
}
