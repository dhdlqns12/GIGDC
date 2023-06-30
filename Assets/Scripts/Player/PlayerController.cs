using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxShotDelay = 0.5f;  //원거리 공격
    public float curShotDelay;
    public float curTime;      //근거리 공격
    public float coolTime = 0.5f;
    public float curPTime;
    public float maxPTime = 5f;

    public float weapon;
    public GameObject bullet;
    public GameObject bulletpos;
    public Vector3 PlayerPos;
    Vector3 Mouseposition;
    float bulletSpeed = 100f;

    public float health = 300f;

    public Transform pos; //근접공격 범위 지정 변수1
    public Vector2 boxSize;  //근접공격 범위 지정 변수2
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


    //애니메이션 변수들
    Animator anim;
    public bool dead = false;
    public Vector2 moveDiriection = new Vector2(1, 0);  //애니메이션에 사용할 방향

    private void OnEnable()
    {
        canMove = true;
    }

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        cam = mainCamera;
    }
    void Update()
    {
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

        // 좌측 방향키면 -1, 우측 방향키면 1, 상측 방향키면 1, 하측 방향키면 -1
        // 버튼을 눌렀을 때 실행


        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        //플레이어 애니메이션 함수
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
        // 여까지

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
        Mouseposition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.farClipPlane));
        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
            //Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            //rigidbody.velocity = Vector3.zero;   //충돌시에 떨림과 밀림 방지
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


                break;

        }

        curShotDelay = 0;
    }
    void Returnhit()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
    void HandAttack()    //근접 공격 함수,, 유튜브 https://youtu.be/_tSxQ9f6tX0 참고
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
                //공격
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

                Vector3 direction = (Mouseposition - PlayerPos);

                GameObject MakeBullet = GameManager.Instance.pool.Get(2);
                float z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                MakeBullet.transform.position = transform.position;
                MakeBullet.transform.rotation = Quaternion.Euler(0, 0, z);
                

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
        spriteRenderer.color = new Color(1, 0, 1, 0.4f);
        Invoke("Returnhit", 0.6f);
    }



    public void Die()
    {
        if(dead)
        {
            anim.SetTrigger("Die");
            dead = false;
            health = 10f;
        }
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

        vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();


        Vector3 movement = new Vector3(vector.x, vector.y, 0);
        movement = movement.normalized * speed * Time.deltaTime;


        rigid.MovePosition(transform.position + movement);


        yield return new WaitForSeconds(0.03f);
        canMove = true;

    }

    private void OnDrawGizmos()   //가상 콜라이더 그리는 함수
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
