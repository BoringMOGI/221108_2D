using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public void OnDamage()              // ������ �޴´�.
    {
        gameObject.SetActive(false);    // (�ӽ�) ������Ʈ�� ����.
    }
}
