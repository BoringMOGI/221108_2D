using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    // ����Ƽ�� ���� �� ��� ������Ʈ�� �̺�Ʈ �Լ����� ��������Ʈ�� ����Ѵ�.
    // ����, ������ ���� �ش� �Լ����� ȣ���Ѵ�.
    // ��� ������� ȣ���� ���� ���¸� one loop(=1FRAME)�̶�� �ϸ�
    // �� �����Ӹ��� �� �ൿ�� �ݺ��Ѵ�.
    
    // ���� ���� �� ���ʿ� 1���� �Ҹ��� �ʱ�ȭ �Լ�.
    void Start()
    {
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
        float y = Input.GetAxisRaw("Vertical");    // �ϴ�:-1, �ȴ�����:0, ���:1
        transform.position += transform.right * moveSpeed * x * Time.deltaTime;
        transform.position += transform.up * moveSpeed * y * Time.deltaTime;
    }
}
