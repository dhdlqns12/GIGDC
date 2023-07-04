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
    public int maxhealth;
    public float maxShotDelay; //�Ѿ� ����������
    public float curShotDelay;

    public Transform InitialPos;
    Transform spawnPos;

    public Transform target;  //���Ͱ� �����ϴ� �÷��̾� ��ġ�� ����
    public Vector3 direction;
    public GameObject player;
    public GameObject effect;


    public int walkCount;
    private int currentWalkCount;


    private Vector3 vector;


    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    public Animator anim;
    public Vector2 moveDiriection = new Vector2(1, 0);

    AudioSource audiosource;
    public AudioClip m_hit;
    public AudioClip b_hit;
    public AudioClip f_hit;


    void Awake()
    {
        m_State = EnemyState.Idle;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audiosource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        spawnPos = InitialPos;
        transform.position = spawnPos.transform.position;
    }
    private void OnEnable()
    {
        m_State = EnemyState.Idle;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = maxhealth;
        anim = GetComponent<Animator>();
        transform.position = spawnPos.transform.position ;
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
        if (enemyName == "Mouse" || enemyName == "Fox")
        {
            if (distance < 10.0f)
            {
                m_State = EnemyState.Move;
                Debug.Log("������ȯ : Idle -> Move");


            }
        }
        else if (enemyName == "Bat")
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
            else if (enemyName == "Fox")
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
                GameObject bullet = GameManager.Instance.pool.Get(1);
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
                //GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
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
                GameObject bullet = GameManager.Instance.pool.Get(4);
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
                //GameObject bullet = Instantiate(tornado, transform.position, transform.rotation);
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
        else if (enemyName == "Fox")
        {
            if (distance <= 1.5f)
            {
                target = GameObject.Find("Player").transform;
                //GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
                GameObject bullet = GameManager.Instance.pool.Get(3);
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
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
        if (enemyName == "Mouse")
        {
            audiosource.clip = m_hit;
            audiosource.Play();
        }
        else if(enemyName == "Bat")
        {
            audiosource.clip = b_hit;
            audiosource.Play();
        }
        else if(enemyName =="Fox")
        {
            audiosource.clip = f_hit;
            audiosource.Play();
        }

            if (m_State == EnemyState.Damaged || m_State == EnemyState.Die)
        {
            return;
        }

        health -= dam;
        Instantiate(effect, transform.position, transform.rotation);


        if (health > 0)
        {
            m_State = EnemyState.Damaged;
            Debug.Log("���� ��ȯ : Any Satte -> Damaged");


            Damaged();
            Invoke("returnhit", 0.7f);
        }
        else
        {
            m_State = EnemyState.Die;
            Debug.Log("���� ��ȯ : Any Satte -> Die");


            Die();
        }
    }

    void returnhit()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
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
        spriteRenderer.color = new Color(1, 0, 0, 0.7f);
        // �ǰ� �ִϸ��̼� ����
        // �ǰ� �ִϸ��̼� ���۽ð���ŭ ������(0.5 �� ���۽ð����� �����ؾ���)
        yield return new WaitForSeconds(0.7f);

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

        if (!Mathf.Approximately(direction.x, 0) || !Mathf.Approximately(direction.y, 0))
        {
            moveDiriection.Set(direction.x, direction.y);
            moveDiriection.Normalize();
        }

        anim.SetFloat("xDir", moveDiriection.x);
        anim.SetFloat("yDir", moveDiriection.y);
        anim.SetFloat("speed", direction.magnitude);


        yield return new WaitForSeconds(0.2f);

    }
    IEnumerator _Die()
    {
        // �״� �ִϸ��̼� ����
        // �״� �ִϸ��̼� ���۽ð� ���
        yield return new WaitForSeconds(1f);
        Debug.Log("DIE !");
        //GameManager.instance.coin += 10;

        if (enemyName == "Fox")
            GameManager.Instance.foxKill++;

        gameObject.SetActive(false);
    }



}
