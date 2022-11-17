using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    // Ư�� ������Ʈ(=�÷��̾�)�� ������ �ٰ����� �ؽ�Ʈ�� ����.
    [SerializeField] string talk;           // ���� ���� �ϴ°�?
    [SerializeField] TextMesh textMesh;     // �ؽ�Ʈ ����� �׸��� ������Ʈ.

    private void Start()
    {
        textMesh.text = string.Empty;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Trigger Enter : {collision.name}");

        if (collision.gameObject.tag == "Player")
            textMesh.text = talk;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            textMesh.text = string.Empty;
    }
}
