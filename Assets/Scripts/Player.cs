using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Animator anim;

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");


        anim.SetBool("isRun", x != 0);      // 애니메이터의 bool 파라미터 isRun의 값을 갱신.
    }

}
