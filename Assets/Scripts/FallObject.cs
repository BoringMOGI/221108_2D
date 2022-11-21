using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObject : MonoBehaviour
{
    [SerializeField] FallManager fallManager;

    // �÷��̾�� �浹 �� ȣ��Ǵ� �̺�Ʈ �Լ�.
    public event System.Action onContactPlayer;
    public event System.Action onAddScore;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            // �Ŵ������� �˸���. (�̺�Ʈ ��������Ʈ�� ����)
            onContactPlayer?.Invoke();
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(transform.position.y <= -6f)
        {
            // �������...
            onAddScore?.Invoke();   // ���ھ ȹ���ߴٰ� �˸���.
            Destroy(gameObject);    // ���� ���� �������� �����ϰڴ�.
        }
    }

}
