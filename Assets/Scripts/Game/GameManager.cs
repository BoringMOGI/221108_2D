using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // GameManager�ڷ��� static ���� Instance.
    // �ڵ� ������Ƽ�� �б�� public, ����� private���� ����.
    public static GameManager Instance { get; private set; }

    [SerializeField] UIManager uiManager;

    private void Awake()
    {
        Instance = this;    // �� �ڽ��� ������Ų��.
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
        // ���� ���� �ε��϶�.
        // ���� �ε��Ҷ��� ������ ���� ��ε��Ѵ�.
        SceneManager.LoadScene("Game");
    }
}
