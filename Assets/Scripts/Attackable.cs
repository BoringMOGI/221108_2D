using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour
{
    [SerializeField] Animator anim;         // 애니메이터.
    [SerializeField] string attackClipName; // 공격 애니메이션 클립의 이름.
    [SerializeField] float rate;            // 공격 딜레이.
    [SerializeField] Vector3 offset;        // 기본 좌표 간격.
    [SerializeField] float radius;          // 공격 범위, 반지름.
    [SerializeField] LayerMask mask;        // 공격 대상 마스크.

    bool isReady;
    bool isReverse;

    private void Start()
    {
        isReady = true;
    }

    public void Reverse(bool isReverse)
    {
        this.isReverse = isReverse;         // 반대 방향인지?
    }
    public void Attack()
    {
        // 준비가 되지 않았다.
        if (!isReady)
            return;

        isReady = false;                    // 준비 상태 false.
        anim.SetTrigger("onAttack");        // 애니메이터 trigger 작동.
        StartCoroutine(IEEndAttack());      // 공격 애니메이션이 끝났는 지 체크.
    }

    // 실제로 공격 범위를 체크하는 함수. (애니메이션의 특정 프레임에 공격을 하기 위함)
    private void CheckAttackArea()
    {
        // 매개변수로 반대로 돌려라는 값이 오면 x축을 반대로 돌린다.
        Vector3 pivot = Vector3.zero;
        pivot.x = transform.position.x + (offset.x * (isReverse ? -1f : 1f));
        pivot.y = transform.position.y + offset.y;
        pivot.z = 0f;

        // pivot 지점에 radius 반지름을 가지는 원을 만들고 mask에 포함된 콜라이더만 체크해라.
        Collider2D[] targets = Physics2D.OverlapCircleAll(pivot, radius, mask);
        foreach (Collider2D target in targets)
        {
            // 피격 대상이 Damageable을 가지고 있는지 체크.
            // 클래스?.함수() : 클래스가 null이 아니라면 내부에 들어간다. (null이면 무시)
            Damageable damageable = target.GetComponent<Damageable>();
            damageable?.OnDamage();
        }
    }
    IEnumerator IEEndAttack()
    {
        // 마지막 프레임까지 대기하라.
        // 왜? onAttack 트리거가 실행이 되었지만 "다음 프레임"에 clip이 변경되기 때문이다.

        yield return new WaitForEndOfFrame();
        //Debug.Log($"한프레임 대기 후 클립의 이름은 : {anim.GetCurrentAnimatorClipInfo(0)[0].clip.name}");

        // 현재 재생중인 애니메이션 클립의 이름이 Attack이 아닐 경우에 isReady를 갱신한다.
        while (true)
        {
            string current = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;       // 현재 재생중인 클립의 이름.
            if (current != attackClipName)                                          // 공격 클립 이름과 다를 경우 (끝난 경우)
                break;


            yield return null;
        }
                
        yield return new WaitForSeconds(rate);  // 공격 시간은 클립마다 다르기 때문에 클립이 끝이난 이후 rate만큼 대기 시작을 준다.
        isReady = true;                         // 공격 가능 상태로 변경.
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
