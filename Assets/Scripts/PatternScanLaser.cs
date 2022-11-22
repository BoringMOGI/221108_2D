using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternScanLaser : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    private Animation anim;
    private bool isFire;

    private void Start()
    {
        anim = GetComponent<Animation>();
        spriteRenderer.enabled = false;
    }

    public void Fire()
    {
        if(!isFire)
            StartCoroutine(IELaser());
    }

    private IEnumerator IELaser()
    {
        isFire = true;
        spriteRenderer.enabled = true;

        Transform target = FallMove.Instance.transform;
        float scanTime = 1.0f;
        while ((scanTime -= Time.deltaTime) > 0.0f)
        {
            Vector3 position = transform.position;      // 나의 포지션을 가져온다.
            position.x = target.position.x;             // 타겟의 x축 위치를 대입한다.
            transform.position = position;              // 수정된 위치 값을 나의 위치로 대입한다.
            yield return null;
        }

        yield return new WaitForSeconds(0.4f);          // n초의 텀을 준다.

        // 실제로 레이저가 내려온다.
        // 애니메이션을 Play하고 다 끝날때까지 대기한다.
        anim.Play("VerticalLaser");
        while (anim.isPlaying)
            yield return null;

        isFire = false;
        spriteRenderer.enabled = false;
    }
}
