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
            Vector3 position = transform.position;      // ���� �������� �����´�.
            position.x = target.position.x;             // Ÿ���� x�� ��ġ�� �����Ѵ�.
            transform.position = position;              // ������ ��ġ ���� ���� ��ġ�� �����Ѵ�.
            yield return null;
        }

        yield return new WaitForSeconds(0.4f);          // n���� ���� �ش�.

        // ������ �������� �����´�.
        // �ִϸ��̼��� Play�ϰ� �� ���������� ����Ѵ�.
        anim.Play("VerticalLaser");
        while (anim.isPlaying)
            yield return null;

        isFire = false;
        spriteRenderer.enabled = false;
    }
}
