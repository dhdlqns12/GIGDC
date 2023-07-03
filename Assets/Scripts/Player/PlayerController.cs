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

    public Transform pos1; //근접공격 범위 지정 변수1
    public Vector2 boxSize1;  //근접공격 범위 지정 변수2
    public Transform pos2; //근접공격 범위 지정 변수1
    public Vector2 boxSize2;  //근접공격 범위 지정 변수2
    public Transform target;
    public float speed = 40f;
    public float Maxspeed = 40f;
    private Vector3 vector;
    public bool canMove = true;


    public SpriteRenderer spriteRenderer;
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
    public GameObject gameOverUI;
    public bool wolfDead=false;
    public bool trapDead = false;
    public GameObject mouseSpawn;
    public GameObject batSpawn;
    public NeighborA neighborA;
    public GameObject foxSpawn;
    public GameObject dialogCollider14;
    public GameObject dialogCollider16;
    public GameObject dialogCollider20;
    public GameObject dialogCollider21;
    public GameObject dialogCollider25;
    public GameObject dialogCollider26;
    public GameObject dialogCollider27;
    public GameObject dialogCollider28;
    public bool isActive16 = false;
    public bool isSet16 = false;
    public bool isActive21 = false;
    public bool isActive26 = false;
    public bool isActive27 = false;
    public bool isActive28 = false;
    public GameObject fadeOut;


    //애니메이션 변수들
    float horizontal;
    float vertical;
    Vector2 move;

    public Animator anim;
    public bool dead = false;
    public Vector2 moveDiriection; //애니메이션에 사용할 방향

    AudioSource audiosource;
    public AudioClip broombgm;
    public AudioClip slingshotbgm;
    public AudioClip axebgm;

    private void OnEnable()
    {
        canMove = true;
        StartCoroutine(deadCheck());
    }

    private void Start()
    {
        moveDiriection = new Vector2(0, -1);
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        cam = mainCamera;
    }
    void Update()
    {
        Vector3 worldPos = cam.WorldToScreenPoint(Input.mousePosition);
        Mouseposition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.farClipPlane));


        if(myGameManager.isAction==true&&neighborA.isEffect==true)
        {
            anim.SetTrigger("Idle");
        }

        if (health <= 0)
        {
            return;
        }

        CheckCam();
        Attack();
        Reload();
        //Die();
        Potion();
        WeaponChange();
        MouseSpawn();
        BatSpawn();
        FoxSpawn();
        Active_DialogCollider16();
        Active_DialogCollider21();
        Active_DialogCollider27();
        Active_DialogCollider28();
        Active_FadeOut();

        // 좌측 방향키면 -1, 우측 방향키면 1, 상측 방향키면 1, 하측 방향키면 -1
        // 버튼을 눌렀을 때 실행


        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        //플레이어 애니메이션 함수
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        move = new Vector2(horizontal, vertical);


        if (!Mathf.Approximately(move.x, 0) || !Mathf.Approximately(move.y, 0))
        {
            moveDiriection.Set(move.x, move.y);
            moveDiriection.Normalize();
        }
        if (myGameManager.isAction == false&&neighborA.isEffect==false)
        {
            anim.SetFloat("xDir", moveDiriection.x);
            anim.SetFloat("yDir", moveDiriection.y);
            anim.SetFloat("speed", move.magnitude);
        }
        // 여까지

        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        if (vDown && v == 1)
        {
            if(myGameManager.isAction==false)
            dirVec = Vector3.up;
        }
        else if (vDown && v == -1)
        {
            if (myGameManager.isAction == false)
                dirVec = Vector3.down;
        }
        if (hDown && h == -1)
        {
            if (myGameManager.isAction == false)
                dirVec = Vector3.left;
        }
        if (hDown && h == 1)
        {
            if (myGameManager.isAction == false)
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
                AxeAttack();

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
        if (!isbroom)
            return;
        if (curTime <= 0)
        {

            if (Input.GetMouseButtonDown(0)) //(Input.GetKey(KeyCode.Space))
            {
                audiosource.clip = broombgm;
                audiosource.Play();
                Collider2D[] collider2ds = Physics2D.OverlapBoxAll(pos1.position, boxSize1, 0);
                foreach (Collider2D collider in collider2ds)
                {
                    if (collider.tag == "Monster")
                    {
                        collider.GetComponent<MonsterController>().HitEnemy(1);
                        Debug.Log("-1");
                    }
                    if (collider.tag == "Boss")
                    {
                        collider.GetComponent<BossController>().HitEnemy(1);
                        Debug.Log("-1");
                    }
                }
                //공격
                curTime = coolTime;
                if (myGameManager.isAction == false && neighborA.isEffect == false)
                {
                    anim.SetFloat("xDir", moveDiriection.x);
                    anim.SetFloat("yDir", moveDiriection.y);
                    anim.SetTrigger("attack");
                }
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
                audiosource.clip = slingshotbgm;
                audiosource.Play();
                Vector3 direction = (Mouseposition - PlayerPos);

                GameObject MakeBullet = GameManager.Instance.pool.Get(2);
                float z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                MakeBullet.transform.position = transform.position;
                MakeBullet.transform.rotation = Quaternion.Euler(0, 0, z);


                curTime = coolTime;
                if (myGameManager.isAction == false && neighborA.isEffect == false)
                {
                    anim.SetFloat("xMouseDir", direction.x);
                    anim.SetFloat("yMouseDir", direction.y);
                    anim.SetTrigger("attack2");
                }

            }
        }
    }

    void AxeAttack()    //근접 공격 함수,, 유튜브 https://youtu.be/_tSxQ9f6tX0 참고
    {
        if (!isaxe)
            return;
        if (curTime <= 0)
        {

            if (Input.GetMouseButtonDown(0)) //(Input.GetKey(KeyCode.Space))
            {
                audiosource.clip = axebgm;
                audiosource.Play();
                GameObject aEffect = Instantiate(axeEffect, transform.position, transform.rotation);

                Destroy(aEffect, 0.5f);
                Collider2D[] collider2ds = Physics2D.OverlapBoxAll(pos2.position, boxSize2, 0);
                foreach (Collider2D collider in collider2ds)
                {
                    if (collider.tag == "Monster")
                    {
                        collider.GetComponent<MonsterController>().HitEnemy(10);
                        Debug.Log("-10");
                    }
                    if (collider.tag == "Boss")
                    {
                        collider.GetComponent<BossController>().HitEnemy(10);
                        Debug.Log("-10");
                    }
                }
                //공격
                curTime = coolTime;
                if (myGameManager.isAction == false && neighborA.isEffect == false)
                {
                    anim.SetFloat("xDir", moveDiriection.x);
                    anim.SetFloat("yDir", moveDiriection.y);
                    anim.SetTrigger("attack3");
                }


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

    IEnumerator deadCheck()
    {
        while (true)
        {
            if (health <= 0)
            {
                anim.SetTrigger("Die");
                //dead = true;
                yield return new WaitForSeconds(2f);
                dead = false;
                Delay();
                //Invoke("Delay", 1f);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void Die()
    {
        if (dead)
        {
            anim.SetTrigger("Die");
            dead = false;
            Invoke("Delay", 2f);
        }
    }

    void Delay()
    {
        if (health <= 0||wolfDead==true||trapDead==true)
        {
            wolfDead = false;
            gameOverUI.SetActive(true);
            Time.timeScale = 0;
        }
        
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

        if(myGameManager.isAction==false && neighborA.isEffect == false)
            rigid.MovePosition(transform.position + movement);


        yield return new WaitForSeconds(0.03f);
        canMove = true;

    }

    private void OnDrawGizmos()   //가상 콜라이더 그리는 함수
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
            collision.gameObject.SetActive(false);
        }
        if (collision.tag == "FalseKey")
        {
            Destroy(collision.gameObject);
        }
        if (collision.tag == "trap")
        {
            Trap();
        }
        if(collision.tag == "TrabObject")
        {
            dead = true;
            trapDead = true;
            Invoke("Die", 0.5f);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (myGameManager.isAction == true && collision.tag == "broom")
        {
            isbroom = true;
            weapon = 1;
        }

        if (myGameManager.isAction == true && collision.tag == "slingshot")
        {
            isslingshot = true;
            weapon = 2;
        }
        if (myGameManager.isAction == true && collision.tag == "axe")
        {
            isaxe = true;
            weapon = 3;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wolf")
        {
            dead = true;
            wolfDead = true;
            Die();
        }
    }

    public void Trap()
    {
        speed = 0;
        Invoke("TrapEnd", 2f);
    }
    public void TrapA()
    {
        speed = 0;
        Invoke("TrapEnd", 5f);
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
            if(myGameManager.isAction==false)
                weapon = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!isslingshot)
                return;
            if (myGameManager.isAction == false)
                weapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!isaxe)
                return;
            if (myGameManager.isAction == false)
                weapon = 3;
        }
    }

    public void MouseSpawn()
    {
        if(isbroom==true&&myGameManager.isAction==false)
        {
            mouseSpawn.SetActive(true);
        }
    }

    public void BatSpawn()
    {
        if(isslingshot==true&&myGameManager.isAction==false)
        {
            batSpawn.SetActive(true);
        }
    }

    public void FoxSpawn()
    {
        if(dialogCollider14.activeSelf==false)
        {
            foxSpawn.SetActive(true);
        }
    }

    public void Active_DialogCollider16()
    {
        if (GameManager.Instance.foxKill == 8)
        {
            isActive16 = true;
            if (isActive16 == true && isSet16 == false)
            {
                dialogCollider16.SetActive(true);
                isSet16 = true;
            }
        }
        if (GameManager.Instance.foxKill == 0)
        {
            isActive16 = false;
            dialogCollider16.SetActive(false);
        }
    }

    public void Active_DialogCollider21()
    {
        if(dialogCollider20.activeSelf==false&&isActive21==false)
        {
            dialogCollider21.SetActive(true);
            isActive21 = true;
        }
    }
    public void Active_DialogCollider27()
    {
        if (dialogCollider26.activeSelf == false && isActive26 == false)
        {
            dialogCollider27.SetActive(true);
            isActive27 = true;
            isActive26 = true;
        }
    }

    public void Active_DialogCollider28()
    {
        if (isActive27 == true && dialogCollider27.activeSelf == false)
        {
            dialogCollider28.SetActive(true);
            isActive27 = false;
            isActive28 = true;
        }
    }

    public void Active_FadeOut()
    {
        if (isActive28 == true && dialogCollider28.activeSelf == false)
        {
            fadeOut.SetActive(true);
            isActive28 = false;
        }
    }
}
