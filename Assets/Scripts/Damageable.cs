using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] int hp;            // ü��.
    [SerializeField] UnityEvent onHit;  // �ǰ� ������ �� �Ҹ��� �̺�Ʈ �Լ�.
    [SerializeField] UnityEvent onDead; // �׾��� �� �Ҹ��� �̺�Ʈ �Լ�.

    public void OnDamage(int power = 1) // ������ �޴´�.
    {
        // ���ݷ��� ���ų�, ���� ü���� 0�� �� (=����)
        if (power < 1 || hp <= 0)
            return;

        // Mahtf.Clamp(��, �ּҰ�, �ִ밪)
        // = �ش� ���� �ּ�, �ִ밪 ���̷� �������ִ� �Լ�.
        hp = Mathf.Clamp(hp - power, 0, 99);
        
        // ���� ���ο� ���� �̺�Ʈ ȣ��.
        // Ŭ������ü?.Invoke() �ش� ��ü�� null�� �ƴ� �� ���ο� �� Invoke�� ȣ���Ѵ�.
        if(hp <= 0)
            onDead?.Invoke();
        else
            onHit?.Invoke();
    }
}
