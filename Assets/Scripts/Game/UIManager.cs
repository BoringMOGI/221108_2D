using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;       // UGUI�� ���� ���� ���ӽ����̽�.

public class UIManager : MonoBehaviour
{
    [SerializeField] Text resultText;       // ��� �ؽ�Ʈ.
    [SerializeField] GameObject panel;      // �г�.

    private void Start()
    {
        panel.SetActive(false);     // �г��� ����.
    }
    public void ShowResult(string content)
    {
        resultText.text = content;  // �ؽ�Ʈ ������Ʈ�� text������ content ���ڿ� ����.
        panel.SetActive(true);
    }
}
