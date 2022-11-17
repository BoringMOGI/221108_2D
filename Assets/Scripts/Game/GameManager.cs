using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // GameManager자료형 static 변수 Instance.
    // 자동 프로퍼티로 읽기는 public, 쓰기는 private으로 제한.
    public static GameManager Instance { get; private set; }

    [SerializeField] UIManager uiManager;

    private void Awake()
    {
        Instance = this;    // 나 자신을 참조시킨다.
    }

    public void OnGameClear()
    {
        uiManager.ShowResult("GAME CLEAR");
    }
    public void OnGameOver()
    {
        uiManager.ShowResult("GAME OVER");
    }
    public void OnRetry()
    {
        // 게임 씬을 로드하라.
        // 씬을 로드할때는 현재의 씬을 언로드한다.
        SceneManager.LoadScene("Game");
    }
}
