using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public void OnDamage()              // 공격을 받는다.
    {
        gameObject.SetActive(false);    // (임시) 오브젝트를 끈다.
    }
}
