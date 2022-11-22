using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FallManager : MonoBehaviour
{
    [Header("Transform")]
    [SerializeField] Transform createMin;       // 생성 최소 위치.
    [SerializeField] Transform createMax;       // 생성 최대 위치.
    [SerializeField] Transform fallParenet;     // 공 오브젝트의 부모 오브젝트.

    [Header("Fall Object")]
    [SerializeField] FallObject fallPrefab;     // 생성할 프리팹.
    [SerializeField] float createRate;          // 생성 시간(간격)

    [SerializeField] PatternScanLaser laser;    // 임시.
    [SerializeField] float patternRate;         // 패턴 시간.

    [Header("UI")]
    [SerializeField] Text scoreText;            // 점수 텍스트 UI.

    [Header("Event")]
    [SerializeField] UnityEvent onGameOver;     // 게임 오버시 호출하는 이벤트.

    LineRenderer lineRenderer;
    bool isGameOver;            // 게임이 끝났는가?
    float nextCreateTime;       // 다음 생성 시간.
    float nextPatternTime;      // 다음 패턴 생성 시간.
    int score;                  // 점수.

    private void Start()
    {
        if (false)
        {
            // 런타임에 게임오브젝트를 인스턴스(=생성)하고 컴포넌트를 붙일 수 있다.
            // 하지만 이 방법보다 완성품인 프리팹을 이용해 복제하는 것이 편하다.
            GameObject my = new GameObject("My Object");
            my.AddComponent<SpriteRenderer>();
            my.AddComponent<BoxCollider2D>();
            my.AddComponent<Rigidbody2D>();
            Destroy(my);                              // 오브젝트를 삭제한다.

            // 게임오브젝트 복제(=Clone)
            Instantiate(fallPrefab);                  // 오브젝트를 복제한다.
            Instantiate(fallPrefab, transform);       // 오브젝트를 복제하되 transform의 자식으로 만든다.
            Instantiate(fallPrefab, new Vector3(10, 10, 0), Quaternion.identity);   // 오브젝트에 특정 위치, 회전 값을 주면서 복제한다.

            // 게임 오브젝트를 새로 생성하고 부모 오브젝트를 나로 지정한다.
            GameObject obj = new GameObject("오브젝트");
            obj.transform.SetParent(transform);

            // 부모 오브젝트를 제거한다. (최상위 오브젝트가 된다.)
            transform.SetParent(null);
            transform.parent = null;
        }

        lineRenderer = GetComponent<LineRenderer>();

        nextCreateTime = createRate;
        nextPatternTime = patternRate;
        score = 0;
        scoreText.text = $"SCORE : {score}";
    }

    void Update()
    {
        if (!isGameOver)
        {
            CreateFall();
            CreatePattern();
            DrawCreateLine();
        }
    }

    private void CreateFall()
    {
        // Time.time : 게임 시작 후 지금까지 얼만큼 지났는가?
        if (Time.time >= nextCreateTime)
        {
            nextCreateTime = Time.time + createRate;

            // Random.Range(min, max) : 최소 이상 최대 "미만"의 값을 반환한다.
            float maxDistance = Vector3.Distance(createMin.position, createMax.position);
            Vector3 direction = (createMax.position - createMin.position).normalized;
            Vector3 position = createMin.position + (direction * Random.Range(0, maxDistance));

            // 프리팹을 이용해 인스턴스(=객체) 생성.
            FallObject newObject = Instantiate(fallPrefab, position, Quaternion.identity, fallParenet);
            newObject.onContactPlayer += OnContactPlayer;
            newObject.onAddScore += OnAddScore;
        }
    }
    private void CreatePattern()
    {
        if(Time.time >= nextPatternTime)
        {
            nextPatternTime = Time.time + patternRate;
            laser.Fire();
        }
    }
    private void DrawCreateLine()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, createMin.position);
        lineRenderer.SetPosition(1, createMax.position);
    }

    public void OnContactPlayer()
    {
        // 이미 게임 오버가 되었기 때문에 중복 호출을 피한다.
        if (isGameOver)
            return;

        isGameOver = true;          // 게임이 끝났음을 알린다.
        onGameOver?.Invoke();       // 등록된 이벤트 함수 호출.
        Debug.Log("게임이 끝났다");
    }
    private void OnAddScore()
    {
        // 게임 오버인 상태에서는 점수를 획득할 수 없다.
        if (isGameOver)
            return;

        score += 1;
        scoreText.text = $"SCORE : {score}";
    }

}
