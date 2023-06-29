using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxShotDelay = 0.5f;  //���Ÿ� ����
    public float curShotDelay;
    public float curTime;      //�ٰŸ� ����
    public float coolTime = 0.5f;
    public float curPTime;
    public float maxPTime = 5f;

    public float weapon;
    public GameObject bullet;
    public GameObject bulletpos;
    public Vector2 PlayerPos;
    Vector2 Mouseposition;
    float bulletSpeed = 100f;

    float health = 10f;

    public Transform pos; //�������� ���� ���� ����1
    public Vector2 boxSize;  //�������� ���� ���� ����2
    public Transform target;
    public float speed = 40f;
    private Vector3 vector;
    public float runSpeed;
    public int walkCount;
    private int currentWalkCount;
    public bool canMove = true;





    Camera cam;
    Bullet BBullet;

    //�ִϸ��̼� ������
    Animator anim;
    public Vector2 moveDiriection = new Vector2(1, 0);  //�ִϸ��̼ǿ� ����� ����

    private void OnEnable()
    {
        canMove = true;
    }

    private void Start()
    {
        cam = Camera.main;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        //if(health <= 0)
        //{
        //    Die();
        //    return;
        //}
        Attack();
        Reload();
        Die();
        Potion();
        WeaponChange();

        // ���� ����Ű�� -1, ���� ����Ű�� 1, ���� ����Ű�� 1, ���� ����Ű�� -1
        // ��ư�� ������ �� ����

        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }

            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.velocity = Vector3.zero;   //�浹�ÿ� ������ �и� ����

        }


        Mouseposition = Input.mousePosition;
        Mouseposition = cam.ScreenToWorldPoint(Mouseposition);


        //�÷��̾� �ִϸ��̼� �Լ�
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);


        if (!Mathf.Approximately(move.x, 0) || !Mathf.Approximately(move.y, 0))
        {
            moveDiriection.Set(move.x, move.y);
            moveDiriection.Normalize();
        }

        anim.SetFloat("xDir", moveDiriection.x);
        anim.SetFloat("yDir", moveDiriection.y);
        anim.SetFloat("speed", move.magnitude);
        // ������
    }

    void Attack()
    {
        //if (!Input.GetKey(KeyCode.Space))
        //     return;

        if (curShotDelay < maxShotDelay)
            return;

        switch (weapon)
        {
            case 1:

                HandAttack();


                break;

            case 2:

                SlingShotAttack();

                break;
            case 3:


                break;

        }

        curShotDelay = 0;
    }

    void HandAttack()    //���� ���� �Լ�,, ��Ʃ�� https://youtu.be/_tSxQ9f6tX0 ����
    {
        if (curTime <= 0)
        {

            if (Input.GetMouseButtonDown(0)) //(Input.GetKey(KeyCode.Space))
            {
                Collider2D[] collider2ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in collider2ds)
                {
                    if (collider.tag == "Monster")
                    {
                        collider.GetComponent<MonsterController>().HitEnemy(1);
                        Debug.Log("-1");
                    }
                }
                //����
                curTime = coolTime;

            }

        }

    }

    void SlingShotAttack()
    {
        if (curTime <= 0)
        {

            if (Input.GetMouseButton(0))
            {

                Vector2 Dir = (Mouseposition - PlayerPos);
                GameObject MakeBullet = Instantiate(bullet, transform.position, Quaternion.Euler(0.0f, 0.0f, 0.0f));

                BBullet = MakeBullet.GetComponent<Bullet>();
                BBullet.Launch(Dir.normalized, bulletSpeed * Time.deltaTime * 100f);
                //GameObject Bullet = Instantiate(bullet);
                //Bullet.transform.position = bulletpos.transform.position;
                //Bullet.gameObject.GetComponent<Rigidbody2D>().AddForce(Mouseposition * bulletSpeed*Time.deltaTime, ForceMode2D.Impulse);


                curTime = coolTime;

            }
        }
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
        curTime -= Time.deltaTime;
        curPTime += Time.deltaTime;
    }

    public void OnHit(int dmg)
    {
        health -= dmg;
    }



    public void Die()
    {
        if (health <= 0)
        {
            //Destroy(gameObject);
            anim.SetTrigger("Die");
            Invoke("DelayDie", 3f);
        }
    }

    void DelayDie()
    {
        Destroy(gameObject);
    }

    public void Potion()
    {
        if (curPTime < maxPTime)
            return;
        if (Input.GetKeyDown(KeyCode.R))
        {
            health = 300f;

            curPTime = 0;
        }
    }
    IEnumerator MoveCoroutine()
    {

        vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

        while (currentWalkCount < walkCount)
        {
            if (vector.x != 0)
            {
                transform.Translate(vector.x * (speed) * Time.deltaTime, 0, 0);
            }
            else if (vector.y != 0)
            {
                transform.Translate(0, vector.y * (speed) * Time.deltaTime, 0);
            }


            currentWalkCount++;
            yield return new WaitForSeconds(0.01f);
        }
        currentWalkCount = 0;
        canMove = true;
    }

    private void OnDrawGizmos()   //���� �ݶ��̴� �׸��� �Լ�
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Key")
        {
            GameManager.Instance.key = true;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "trap")
        {
            Trap();
        }
    }

    public void Trap()
    {
        speed = 0;
        Invoke("TrapEnd", 2f);
    }

    void TrapEnd()
    {
        speed = 5f;
    }

    public void WeaponChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapon = 2;
        }
    }
}
