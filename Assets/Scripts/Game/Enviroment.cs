using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ش� ������Ʈ�� ���� ���ؼ��� Animation�� �־���Ѵٰ� ����.
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
