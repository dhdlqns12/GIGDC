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
    public bool isbroom = false;
    public bool isslingshot = false;
    public bool isaxe = false;
    public bool ispotion = true;
    public GameObject bullet;
    public GameObject bulletpos;
    public GameObject axeEffect;
    public Vector3 PlayerPos;
    Vector3 Mouseposition;
    float bulletSpeed = 100f;

    public float health = 300f;

    public Transform pos1; //�������� ���� ���� ����1
    public Vector2 boxSize1;  //�������� ���� ���� ����2
    public Transform pos2; //�������� ���� ���� ����1
    public Vector2 boxSize2;  //�������� ���� ���� ����2
    public Transform target;
    public float speed = 40f;
    public float Maxspeed = 40f;
    private Vector3 vector;
    public bool canMove = true;


    SpriteRenderer spriteRenderer;
    public Camera cam;
    public Camera mainCamera;
    public Camera playerCamera;
    public bool ismaincam = true;

    public MyGameManager myGameManager;
    Vector3 dirVec;
    float h;
    float v;
    Rigidbody2D rigid;
    GameObject scanObject;


    //�ִϸ��̼� ������
    float horizontal;
    float vertical;
    Vector2 move;

    Animator anim;
    public bool dead = false;
    public Vector2 moveDiriection; //�ִϸ��̼ǿ� ����� ����

    private void OnEnable()
    {
        canMove = true;
    }

    private void Start()
    {
        moveDiriection = new Vector2(0, -1);
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        cam = mainCamera;
    }
    void Update()
    {
        Vector3 worldPos = cam.WorldToScreenPoint(Input.mousePosition);
        Mouseposition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.farClipPlane));

        if (health <= 0)
        {
            dead = true;
        }

        CheckCam();
        Attack();
        Reload();
        Die();
        Potion();
        WeaponChange();

        // ���� ����Ű�� -1, ���� ����Ű�� 1, ���� ����Ű�� 1, ���� ����Ű�� -1
        // ��ư�� ������ �� ����


        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        //�÷��̾� �ִϸ��̼� �Լ�
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        move = new Vector2(horizontal, vertical);


        if (!Mathf.Approximately(move.x, 0) || !Mathf.Approximately(move.y, 0))
        {
            moveDiriection.Set(move.x, move.y);
            moveDiriection.Normalize();
        }

        anim.SetFloat("xDir", moveDiriection.x);
        anim.SetFloat("yDir", moveDiriection.y);
        anim.SetFloat("speed", move.magnitude);
        // ������

        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        if (vDown && v == 1)
        {
            dirVec = Vector3.up;
        }
        else if (vDown && v == -1)
        {
            dirVec = Vector3.down;
        }
        if (hDown && h == -1)
        {
            dirVec = Vector3.left;
        }
        if (hDown && h == 1)
        {
            dirVec = Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.Space) && scanObject != null)
        {
            myGameManager.Action(scanObject);
        }
    }

    void CheckCam()
    {
        if (ismaincam)
        {
            mainCamera = Camera.main;
            cam = mainCamera;

        }
        else
        {
            playerCamera = Camera.main;
            cam = playerCamera;
        }
    }

    private void FixedUpdate()
    {

        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
            //Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            //rigidbody.velocity = Vector3.zero;   //�浹�ÿ� ������ �и� ����
        }


        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
            scanObject = null;
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
                AxeAttack();

                break;

        }

        curShotDelay = 0;
    }
    void Returnhit()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
    void HandAttack()    //���� ���� �Լ�,, ��Ʃ�� https://youtu.be/_tSxQ9f6tX0 ����
    {
        if (!isbroom)
            return;
        if (curTime <= 0)
        {

            if (Input.GetMouseButtonDown(0)) //(Input.GetKey(KeyCode.Space))
            {
                Collider2D[] collider2ds = Physics2D.OverlapBoxAll(pos1.position, boxSize1, 0);
                foreach (Collider2D collider in collider2ds)
                {
                    if (collider.tag == "Monster" || collider.tag == "Boss")
                    {
                        collider.GetComponent<MonsterController>().HitEnemy(1);
                        Debug.Log("-1");
                    }
                }
                //����
                curTime = coolTime;

                anim.SetFloat("xDir", moveDiriection.x);
                anim.SetFloat("yDir", moveDiriection.y);
                anim.SetTrigger("attack");
            }

        }

    }

    void SlingShotAttack()
    {
        if (!isslingshot)
            return;
        if (curTime <= 0)
        {

            if (Input.GetMouseButton(0))
            {

                Vector3 direction = (Mouseposition - PlayerPos);

                GameObject MakeBullet = GameManager.Instance.pool.Get(2);
                float z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                MakeBullet.transform.position = transform.position;
                MakeBullet.transform.rotation = Quaternion.Euler(0, 0, z);


                curTime = coolTime;

                anim.SetFloat("xMouseDir", direction.x);
                anim.SetFloat("yMouseDir", direction.y);
                anim.SetTrigger("attack2");

            }
        }
    }

    void AxeAttack()    //���� ���� �Լ�,, ��Ʃ�� https://youtu.be/_tSxQ9f6tX0 ����
    {
        if (!isaxe)
            return;
        if (curTime <= 0)
        {

            if (Input.GetMouseButtonDown(0)) //(Input.GetKey(KeyCode.Space))
            {
                GameObject aEffect = Instantiate(axeEffect, transform.position, transform.rotation);

                Destroy(aEffect, 0.5f);
                Collider2D[] collider2ds = Physics2D.OverlapBoxAll(pos2.position, boxSize2, 0);
                foreach (Collider2D collider in collider2ds)
                {
                    if (collider.tag == "Monster" || collider.tag == "Boss")
                    {
                        collider.GetComponent<BossController>().HitEnemy(10);
                        Debug.Log("-10");

                    }
                }
                //����
                curTime = coolTime;

                anim.SetFloat("xDir", moveDiriection.x);
                anim.SetFloat("yDir", moveDiriection.y);
                anim.SetTrigger("attack3");


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
        spriteRenderer.color = new Color(1, 0, 1, 0.4f);
        Invoke("Returnhit", 0.6f);
    }



    public void Die()
    {
        if (dead)
        {
            anim.SetTrigger("Die");
            dead = false;            
            health = 10f;
            Invoke("Delay", 5f);
        }
    }

    void Delay()
    {
        anim.SetTrigger("Delay");
    }

    public void Potion()
    {
        if (curPTime < maxPTime)
            return;
        if (Input.GetKeyDown(KeyCode.R))
        {
            ispotion = false;
            health = 300f;

            curPTime = 0;
        }
    }
    IEnumerator MoveCoroutine()
    {

        vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();


        Vector3 movement = new Vector3(vector.x, vector.y, 0);
        movement = movement.normalized * speed * Time.deltaTime;


        rigid.MovePosition(transform.position + movement);


        yield return new WaitForSeconds(0.03f);
        canMove = true;

    }

    private void OnDrawGizmos()   //���� �ݶ��̴� �׸��� �Լ�
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos1.position, boxSize1);
        Gizmos.DrawWireCube(pos2.position, boxSize2);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Key")
        {
            GameManager.Instance.key = true;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "FalseKey")
        {
            Destroy(collision.gameObject);
        }
        if (collision.tag == "trap")
        {
            Trap();
        }
        if (collision.tag == "broom")
        {
            isbroom = true;
            weapon = 1;
        }
        if (collision.tag == "slingshot")
        {
            isslingshot = true;
            weapon = 2;
        }
        if (collision.tag == "axe")
        {
            isaxe = true;
            weapon = 3;
        }
        if(collision.tag == "TrabObject")
        {
            dead = true;
            Invoke("Die", 2f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wolf")
        {
            dead = true;
            Die();
        }
    }

    public void Trap()
    {
        speed = 0;
        Invoke("TrapEnd", 2f);
    }

    void TrapEnd()
    {
        speed = Maxspeed;
    }

    public void WeaponChange()
    {
        if (!isbroom)
            return;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapon = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!isslingshot)
                return;
            weapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!isaxe)
                return;
            weapon = 3;
        }
    }
}
