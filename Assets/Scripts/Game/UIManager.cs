using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;       // UGUI를 쓰기 위한 네임스페이스.

public class UIManager : MonoBehaviour
{
    [SerializeField] Text resultText;       // 결과 텍스트.
    [SerializeField] GameObject panel;      // 패널.

    private void Start()
    {
        panel.SetActive(false);     // 패널을 끈다.
    }
    public void ShowResult(string content)
    {
        resultText.text = content;  // 텍스트 컴포넌트의 text변수에 content 문자열 대입.
        panel.SetActive(true);
    }
}
