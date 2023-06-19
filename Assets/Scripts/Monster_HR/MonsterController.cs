using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//몬스터 공격을 어떻게 할 것인지 고민중. 현재 근접 공격, 몬스터 밀림, 플레이어 밀림 및 떨림 현상은 해결했으나
//몬스터가 콜라이더를 무시하고 자꾸 플레이어의 중심으로 향하는 문제,
//몬스터들이 모여들면 하나로 겹쳐(보이)지는 문제도 발생
public class MonsterController : MonoBehaviour
{
    enum EnemyState
    {
        // 대기 상태 , 이동, 공격, 피격, 복귀, 죽음
        Idle, Move, Attack, Damaged, Die
    }

    EnemyState m_State;

    public string enemyName;  //몬스터 종류 구분 변수
    public float speed;   //몬스터 속도 변수
    public int health;
    public float maxShotDelay; //총알 지연변수들
    public float curShotDelay;


    public Transform target;  //몬스터가 추적하는 플레이어 위치값 변수
    public Vector3 direction;
    public GameObject player;
    public GameObject bulletObjA;
    public GameObject tornado;
    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;

    public int walkCount;
    private int currentWalkCount;

    private bool canMove = true;
    private Vector3 vector;


    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;



    void Awake()
    {
        m_State = EnemyState.Idle;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }



    // Update is called once per frame
    void Update()
    {
        switch (m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Damaged:
                //Damaged();
                break;
            case EnemyState.Die:
                //Die();
                break;
        }
        
        Reload();
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector3.zero; //충돌시에 떨림과 밀림 방지

    }
    void Idle()
    {
        // Player의 현재 위치를 받아오는 Object
        target = GameObject.Find("Player").transform;
        // Player의 위치와 이 객체의 위치를 빼고 단위 벡터화 한다.
        direction = (target.position - transform.position).normalized;
        // Player와 객체 간의 거리 계산
        float distance = Vector3.Distance(target.position, transform.position);
        // 일정거리 안에 있을 시, 해당 방향으로 무빙
        if (enemyName == "Mouse")
        {
            if (distance < 10.0f)
            {
                m_State = EnemyState.Move;
                Debug.Log("상태전환 : Idle -> Move");


            }
        }
        if (enemyName == "Bat")
        {
            if (distance < 10.0f)
            {
                m_State = EnemyState.Move;
                Debug.Log("상태전환 : Idle -> Move");


            }
        }
    }
    public void Move()   //플레이어가 일정 범위 안에 들어오면 따라오게 만드는 함수
    {

        target = GameObject.Find("Player").transform;

        direction = (target.position - transform.position).normalized;

        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= 30.0f)
        {
            if (enemyName == "Mouse")
            {
                if (distance <= 10.0f && distance > 1.5f)
                {
                    StartCoroutine(MoveCoroutine());
                    m_State = EnemyState.Move;
                }
                else if (distance <= 1.5f)
                {
                    m_State = EnemyState.Attack;
                    Debug.Log("상태전환 : Move -> Attack");
                }
            }
            else if (enemyName == "Bat")
            {
                if (distance <= 10.0f && distance > 3.0f)
                {
                    StartCoroutine(MoveCoroutine());
                    m_State = EnemyState.Move;
                }
                else if (distance <= 3.0f)
                {
                    m_State = EnemyState.Attack;
                    Debug.Log("상태전환 : Move -> Attack");
                }
            }
        }

    }
    void Attack()
    {

        target = GameObject.Find("Player").transform;

        direction = (target.position - transform.position).normalized;

        float distance = Vector3.Distance(target.position, transform.position);

        if (curShotDelay < maxShotDelay)
            return;

        if (enemyName == "Mouse")
        {
            if (distance <= 1.5f)
            {
                target = GameObject.Find("Player").transform;
                GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                Vector3 dicVec = target.transform.position - transform.position;
                rigid.AddForce(dicVec * 1f, ForceMode2D.Impulse);
            }
            else
            {
                m_State = EnemyState.Move;
                Debug.Log("상태전환 : Attack -> Move");
            }
        }
        else if (enemyName == "Bat")
        {
            if (distance <= 3.0f)
            {
                target = GameObject.Find("Player").transform;
                GameObject bullet = Instantiate(tornado, transform.position, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                Vector3 dicVec = target.transform.position - transform.position;
                rigid.AddForce(dicVec * 1f, ForceMode2D.Impulse);
               
            }
            else
            {
                m_State = EnemyState.Move;
                Debug.Log("상태전환 : Attack -> Move");
            }
        }
       
        curShotDelay = 0;
    }
    public void HitEnemy(int dam)
    {
        if (m_State == EnemyState.Damaged || m_State == EnemyState.Die)
        {
            return;
        }

        health -= dam;

        if (health > 0)
        {
            m_State = EnemyState.Damaged;
            Debug.Log("상태 전환 : Any Satte -> Damaged");


            Damaged();
        }
        else
        {
            m_State = EnemyState.Die;
            Debug.Log("상태 전환 : Any Satte -> Die");


            Die();
        }
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void Damaged()
    {
        StartCoroutine(Damage());
    }
    IEnumerator Damage()
    {

        // 피격 애니메이션 실행
        // 피격 애니메이션 동작시간만큼 딜레이(0.5 초 동작시간으로 변경해야함)
        yield return new WaitForSeconds(0.5f);

        m_State = EnemyState.Move;
        Debug.Log("상태 전환 : Damaged -> Move");


    }
    void Die()
    {
        StopAllCoroutines();

        StartCoroutine(_Die());
    }

    IEnumerator MoveCoroutine()
    {
        target = GameObject.Find("Player").transform;

        direction = (target.position - transform.position).normalized;

        float distance = Vector3.Distance(target.position, transform.position);

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        yield return new WaitForSeconds(0.2f);

    }
    IEnumerator _Die()
    {
        // 죽는 애니메이션 실행
        // 죽는 애니메이션 동작시간 대기
        yield return new WaitForSeconds(1f);
        Debug.Log("DIE !");
        //GameManager.instance.coin += 10;
        Destroy(gameObject);
    }

    
  
}
