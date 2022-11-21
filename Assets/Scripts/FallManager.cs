using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FallManager : MonoBehaviour
{
    [Header("Transform")]
    [SerializeField] Transform createMin;       // ���� �ּ� ��ġ.
    [SerializeField] Transform createMax;       // ���� �ִ� ��ġ.
    [SerializeField] Transform fallParenet;     // �� ������Ʈ�� �θ� ������Ʈ.

    [Header("Fall Object")]
    [SerializeField] FallObject fallPrefab;     // ������ ������.
    [SerializeField] float createRate;          // ���� �ð�(����)

    [Header("UI")]
    [SerializeField] Text scoreText;            // ���� �ؽ�Ʈ UI.

    [Header("Event")]
    [SerializeField] UnityEvent onGameOver;     // ���� ������ ȣ���ϴ� �̺�Ʈ.

    LineRenderer lineRenderer;
    bool isGameOver;            // ������ �����°�?
    float nextCreateTime;       // ���� ���� �ð�.
    int score;                  // ����.

    private void Start()
    {
        if (false)
        {
            // ��Ÿ�ӿ� ���ӿ�����Ʈ�� �ν��Ͻ�(=����)�ϰ� ������Ʈ�� ���� �� �ִ�.
            // ������ �� ������� �ϼ�ǰ�� �������� �̿��� �����ϴ� ���� ���ϴ�.
            GameObject my = new GameObject("My Object");
            my.AddComponent<SpriteRenderer>();
            my.AddComponent<BoxCollider2D>();
            my.AddComponent<Rigidbody2D>();
            Destroy(my);                              // ������Ʈ�� �����Ѵ�.

            // ���ӿ�����Ʈ ����(=Clone)
            Instantiate(fallPrefab);                  // ������Ʈ�� �����Ѵ�.
            Instantiate(fallPrefab, transform);       // ������Ʈ�� �����ϵ� transform�� �ڽ����� �����.
            Instantiate(fallPrefab, new Vector3(10, 10, 0), Quaternion.identity);   // ������Ʈ�� Ư�� ��ġ, ȸ�� ���� �ָ鼭 �����Ѵ�.

            // ���� ������Ʈ�� ���� �����ϰ� �θ� ������Ʈ�� ���� �����Ѵ�.
            GameObject obj = new GameObject("������Ʈ");
            obj.transform.SetParent(transform);

            // �θ� ������Ʈ�� �����Ѵ�. (�ֻ��� ������Ʈ�� �ȴ�.)
            transform.SetParent(null);
            transform.parent = null;
        }

        lineRenderer = GetComponent<LineRenderer>();

        score = 0;
        scoreText.text = $"SCORE : {score}";
    }

    void Update()
    {
        if (!isGameOver)
        {
            CreateFall();
            DrawCreateLine();
        }
    }

    private void CreateFall()
    {
        // Time.time : ���� ���� �� ���ݱ��� ��ŭ �����°�?
        if (Time.time >= nextCreateTime)
        {
            nextCreateTime = Time.time + createRate;

            // Random.Range(min, max) : �ּ� �̻� �ִ� "�̸�"�� ���� ��ȯ�Ѵ�.
            float maxDistance = Vector3.Distance(createMin.position, createMax.position);
            Vector3 direction = (createMax.position - createMin.position).normalized;
            Vector3 position = createMin.position + (direction * Random.Range(0, maxDistance));

            // �������� �̿��� �ν��Ͻ�(=��ü) ����.
            FallObject newObject = Instantiate(fallPrefab, position, Quaternion.identity, fallParenet);
            newObject.onContactPlayer += OnContactPlayer;
            newObject.onAddScore += OnAddScore;
        }
    }
    private void DrawCreateLine()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, createMin.position);
        lineRenderer.SetPosition(1, createMax.position);
    }

    private void OnContactPlayer()
    {
        // �̹� ���� ������ �Ǿ��� ������ �ߺ� ȣ���� ���Ѵ�.
        if (isGameOver)
            return;

        isGameOver = true;          // ������ �������� �˸���.
        onGameOver?.Invoke();       // ��ϵ� �̺�Ʈ �Լ� ȣ��.
        Debug.Log("������ ������");
    }
    private void OnAddScore()
    {
        // ���� ������ ���¿����� ������ ȹ���� �� ����.
        if (isGameOver)
            return;

        score += 1;
        scoreText.text = $"SCORE : {score}";
    }

}
