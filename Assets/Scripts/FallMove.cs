using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallMove : MonoBehaviour
{
    public static FallMove Instance { get; private set; }

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpPower;

    Rigidbody2D rigid;    // 참조 변수 rigid.
    bool isLock;          // 입력을 막는다.

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isLock)
            return;

        float x = Input.GetAxis("Horizontal");
        if (x != 0)
            rigid.velocity = new Vector2(moveSpeed * x, rigid.velocity.y);

        if (Input.GetKeyDown(KeyCode.C))
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 0f);
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    public void OnDead()
    {
        isLock = true;
        gameObject.SetActive(false);
    }
}
