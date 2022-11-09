using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpPower;

    Rigidbody2D rigid;      // ������ٵ� �����ϴ� ����.
    int jumpCount;          // ���� ���� Ƚ��.

    // ����Ƽ�� ���� �� ��� ������Ʈ�� �̺�Ʈ �Լ����� ��������Ʈ�� ����Ѵ�.
    // ����, ������ ���� �ش� �Լ����� ȣ���Ѵ�.
    // ��� ������� ȣ���� ���� ���¸� one loop(=1FRAME)�̶�� �ϸ�
    // �� �����Ӹ��� �� �ൿ�� �ݺ��Ѵ�.
    
    // ���� ���� �� ���ʿ� 1���� �Ҹ��� �ʱ�ȭ �Լ�.
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();        // ���� Rigidbody2D�� �˻��ؼ� �����Ѵ�.
        jumpCount = 1;                              // ���� Ƚ���� 1�� �ʱ�ȭ.

        // ���� ������Ʈ���Լ� Transform ������Ʈ�� �˻��� �����Ѵ�.
        Transform transform = GetComponent<Transform>();

        // ���� ���� 0,0�� �ְ� �������� 5��ŭ ���������ϴϱ�
        // (5,0)�� Transform�� position�� ��������!!
        // ������ �̷��� �� ������ �޶����� ������ �׻� (5,0)���� ����.
        // transform.position = new Vector3(5, 0);     

        // ���� ��ġ���� �������� 5 �������� 2��ŭ �����̴� �̵����� �����.
        // Vector3 movment = (Vector3.right * 5f) + (Vector3.up * 2f);       // ���� * �Ÿ� = �̵���.
        // transform.position += movment;                                    // ���� ��ġ���� �������� 5��ŭ ��������.

        // World ��ǥ(=���� ��ǥ)
        // = �׻� �������� �ʰ� ȸ������ �ʴ� ���带 �������� ��ŭ ������������..
        //   Vector3.right => �׻� (1,0)�� ���� ������.

        // Local ��ǥ(=��� ��ǥ)
        // = ���� �������� ��ŭ �������ְ� ��� ȸ�����ִ���..
        //   transform.right => ���� ȸ���ϴ� �Ϳ� ���� ���� ���Ͱ� �ٲ��.

        //transform.position += transform.right * 3f;
    }

    // �� �����Ӹ��� �Ҹ��� �Լ�.
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");  // ����:-1, �ȴ�����:0, ����:1
        //float y = Input.GetAxisRaw("Vertical");  // �ϴ�:-1, �ȴ�����:0, ���:1
        rigid.velocity = new Vector2(x * moveSpeed, rigid.velocity.y);

        #region ��ǥ �̵� ���

        // ���� ���͸� ���Ҷ��� �׻� Normalize(=����ȭ)�� �������� �����ؾ��Ѵ�.
        // Vector3 direction = (transform.right * x) + (transform.up * y);
        // direction.Normalized();

        // ���� ��� �������� ������ �� ������� �������� ������ ��Ÿ������ ��Ģ�� ���� �� �ָ� �̵��ϰ� �ȴ�.
        // ���� ���� ����̶�� ���� ���͸� ���ϰ� �� ������ moveSpeed��ŭ �����δ�.

        // Time.deltaTime : ���� �����Ӻ��� ���� ������ ������ �ð� ����. (�� 0.000032f)
        // �ش� ���� ���ϴ� ������ ������ ���̿� ���� �ӵ� ���̸� �����ϱ� �����̴�.
        // transform.position += direction * moveSpeed * Time.deltaTime;

        #endregion

        // GetKeyDown   : �ش� Ű�� ���� �� ���� 1ȸ.
        // GetKeyUp     : �ش� Ű�� �� �� ���� 1ȸ.
        // GetKey       : �ش� Ű�� ������ ���� �� ���.
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            // ForceMode2D.Force    : �������� ��.
            // ForceMode2D.Impulse  : �ѹ��� �վ��� ������ ��.

            // �����ϱ� ������ ���� y�� �ӵ��� 0���� �����Ѵ�.
            rigid.velocity = new Vector2(rigid.velocity.x, 0f);
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

            jumpCount -= 1;
        }
    }
}