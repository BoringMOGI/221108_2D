using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Animator anim;

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");


        anim.SetBool("isRun", x != 0);      // �ִϸ������� bool �Ķ���� isRun�� ���� ����.
    }

}
