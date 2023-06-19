using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//���� ������ ��� �� ������ �����. ���� ���� ����, ���� �и�, �÷��̾� �и� �� ���� ������ �ذ�������
//���Ͱ� �ݶ��̴��� �����ϰ� �ڲ� �÷��̾��� �߽����� ���ϴ� ����,
//���͵��� �𿩵�� �ϳ��� ����(����)���� ������ �߻�
public class MonsterController : MonoBehaviour
{
    enum EnemyState
    {
        // ��� ���� , �̵�, ����, �ǰ�, ����, ����
        Idle, Move, Attack, Damaged, Die
    }

    EnemyState m_State;

    public string enemyName;  //���� ���� ���� ����
    public float speed;   //���� �ӵ� ����
    public int health;
    public float maxShotDelay; //�Ѿ� ����������
    public float curShotDelay;


    public Transform target;  //���Ͱ� �����ϴ� �÷��̾� ��ġ�� ����
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
        rigidbody.velocity = Vector3.zero; //�浹�ÿ� ������ �и� ����

    }
    void Idle()
    {
        // Player�� ���� ��ġ�� �޾ƿ��� Object
        target = GameObject.Find("Player").transform;
        // Player�� ��ġ�� �� ��ü�� ��ġ�� ���� ���� ����ȭ �Ѵ�.
        direction = (target.position - transform.position).normalized;
        // Player�� ��ü ���� �Ÿ� ���
        float distance = Vector3.Distance(target.position, transform.position);
        // �����Ÿ� �ȿ� ���� ��, �ش� �������� ����
        if (enemyName == "Mouse")
        {
            if (distance < 10.0f)
            {
                m_State = EnemyState.Move;
                Debug.Log("������ȯ : Idle -> Move");


            }
        }
        if (enemyName == "Bat")
        {
            if (distance < 10.0f)
            {
                m_State = EnemyState.Move;
                Debug.Log("������ȯ : Idle -> Move");


            }
        }
    }
    public void Move()   //�÷��̾ ���� ���� �ȿ� ������ ������� ����� �Լ�
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
                    Debug.Log("������ȯ : Move -> Attack");
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
                    Debug.Log("������ȯ : Move -> Attack");
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
                Debug.Log("������ȯ : Attack -> Move");
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
                Debug.Log("������ȯ : Attack -> Move");
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
            Debug.Log("���� ��ȯ : Any Satte -> Damaged");


            Damaged();
        }
        else
        {
            m_State = EnemyState.Die;
            Debug.Log("���� ��ȯ : Any Satte -> Die");


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

        // �ǰ� �ִϸ��̼� ����
        // �ǰ� �ִϸ��̼� ���۽ð���ŭ ������(0.5 �� ���۽ð����� �����ؾ���)
        yield return new WaitForSeconds(0.5f);

        m_State = EnemyState.Move;
        Debug.Log("���� ��ȯ : Damaged -> Move");


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
        // �״� �ִϸ��̼� ����
        // �״� �ִϸ��̼� ���۽ð� ���
        yield return new WaitForSeconds(1f);
        Debug.Log("DIE !");
        //GameManager.instance.coin += 10;
        Destroy(gameObject);
    }

    
  
}
