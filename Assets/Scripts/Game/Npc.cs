using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    // 특정 오브젝트(=플레이어)가 나에게 다가오면 텍스트를 띄운다.
    [SerializeField] string talk;           // 무슨 말을 하는가?
    [SerializeField] TextMesh textMesh;     // 텍스트 모양을 그리는 컴포넌트.

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
