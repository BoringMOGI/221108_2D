using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] int hp;            // 체력.
    [SerializeField] UnityEvent onHit;  // 피격 당했을 때 불리는 이벤트 함수.
    [SerializeField] UnityEvent onDead; // 죽었을 때 불리는 이벤트 함수.

    public void OnDamage(int power = 1) // 공격을 받는다.
    {
        // 공격력이 없거나, 나의 체력이 0일 때 (=죽음)
        if (power < 1 || hp <= 0)
            return;

        // Mahtf.Clamp(값, 최소값, 최대값)
        // = 해당 값을 최소, 최대값 사이로 고정해주는 함수.
        hp = Mathf.Clamp(hp - power, 0, 99);
        
        // 생존 여부에 따라 이벤트 호출.
        // 클래스객체?.Invoke() 해당 객체가 null이 아닐 때 내부에 들어가 Invoke를 호출한다.
        if(hp <= 0)
            onDead?.Invoke();
        else
            onHit?.Invoke();
    }
}
