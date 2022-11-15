using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour
{
    [SerializeField] Animator anim;         // �ִϸ�����.
    [SerializeField] string attackClipName; // ���� �ִϸ��̼� Ŭ���� �̸�.
    [SerializeField] float rate;            // ���� ������.
    [SerializeField] Vector3 offset;        // �⺻ ��ǥ ����.
    [SerializeField] float radius;          // ���� ����, ������.
    [SerializeField] LayerMask mask;        // ���� ��� ����ũ.

    bool isReady;
    bool isReverse;

    private void Start()
    {
        isReady = true;
    }

    public void Reverse(bool isReverse)
    {
        this.isReverse = isReverse;         // �ݴ� ��������?
    }
    public void Attack()
    {
        // �غ� ���� �ʾҴ�.
        if (!isReady)
            return;

        isReady = false;                    // �غ� ���� false.
        anim.SetTrigger("onAttack");        // �ִϸ����� trigger �۵�.
        StartCoroutine(IEEndAttack());      // ���� �ִϸ��̼��� ������ �� üũ.
    }

    // ������ ���� ������ üũ�ϴ� �Լ�. (�ִϸ��̼��� Ư�� �����ӿ� ������ �ϱ� ����)
    private void CheckAttackArea()
    {
        // �Ű������� �ݴ�� ������� ���� ���� x���� �ݴ�� ������.
        Vector3 pivot = Vector3.zero;
        pivot.x = transform.position.x + (offset.x * (isReverse ? -1f : 1f));
        pivot.y = transform.position.y + offset.y;
        pivot.z = 0f;

        // pivot ������ radius �������� ������ ���� ����� mask�� ���Ե� �ݶ��̴��� üũ�ض�.
        Collider2D[] targets = Physics2D.OverlapCircleAll(pivot, radius, mask);
        foreach (Collider2D target in targets)
        {
            // �ǰ� ����� Damageable�� ������ �ִ��� üũ.
            // Ŭ����?.�Լ�() : Ŭ������ null�� �ƴ϶�� ���ο� ����. (null�̸� ����)
            Damageable damageable = target.GetComponent<Damageable>();
            damageable?.OnDamage();
        }
    }
    IEnumerator IEEndAttack()
    {
        // ������ �����ӱ��� ����϶�.
        // ��? onAttack Ʈ���Ű� ������ �Ǿ����� "���� ������"�� clip�� ����Ǳ� �����̴�.

        yield return new WaitForEndOfFrame();
        //Debug.Log($"�������� ��� �� Ŭ���� �̸��� : {anim.GetCurrentAnimatorClipInfo(0)[0].clip.name}");

        // ���� ������� �ִϸ��̼� Ŭ���� �̸��� Attack�� �ƴ� ��쿡 isReady�� �����Ѵ�.
        while (true)
        {
            string current = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;       // ���� ������� Ŭ���� �̸�.
            if (current != attackClipName)                                          // ���� Ŭ�� �̸��� �ٸ� ��� (���� ���)
                break;


            yield return null;
        }
                
        yield return new WaitForSeconds(rate);  // ���� �ð��� Ŭ������ �ٸ��� ������ Ŭ���� ���̳� ���� rate��ŭ ��� ������ �ش�.
        isReady = true;                         // ���� ���� ���·� ����.
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 pivot = Vector3.zero;
        pivot.x = transform.position.x + (offset.x * (isReverse ? -1f : 1f));
        pivot.y = transform.position.y + offset.y;
        pivot.z = 0f;

        Gizmos.DrawWireSphere(pivot, radius);
    }
}
