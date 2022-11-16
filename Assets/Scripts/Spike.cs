using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    // 내가 트리거일 때 : 콜리즌 체크가 불가능하기 때문에 트리거 체크밖에 못한다.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable target = collision.GetComponent<Damageable>();
        if (target != null)
            target.OnDamage();
    }
}
