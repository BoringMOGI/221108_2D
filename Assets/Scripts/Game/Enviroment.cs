using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 해당 컴포넌트를 쓰기 위해서는 Animation이 있어야한다고 강제.
[RequireComponent(typeof(Animation))]
public class Enviroment : MonoBehaviour
{
    Animation anim;

    private void Start()
    {
        anim = GetComponent<Animation>();
    }

    public void OnHit()
    {
        anim.Play("ObjectHit");
    }
    public void OnDead()
    {
        anim.Play("ObjectFade");
    }
}
