using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{


    public int health = 1000;


    public GameObject bulletObjA;
    public GameObject vinespawnA;
    public GameObject vinespawnB;
    public GameObject waterwallA;
    public GameObject waterwallB;
    public GameObject waterwallC;
    public GameObject waterwallD;
    public GameObject vinespawnAwarn;
    public GameObject vinespawnBwarn;
    public GameObject waterwallAwarn;
    public GameObject waterwallBwarn;
    public GameObject waterwallCwarn;
    public GameObject waterwallDwarn;
    public GameObject effect;
    public GameObject poolmanager;

    bool closeattack = false;
    bool longattack = false;

    //public GameObject life;
    //public GameObject score;
    //public GameObject power;

    bool isThink;

    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;
    public GameObject magmas;
    public GameObject healthbar;


    //bgm 관리
    public GameObject Gbgm;
    public GameObject Bbgm;
    public GameObject BHbgm;


    SpriteRenderer renderer;

    //엔딩이동변수
    public GameObject player;
    public Transform teleport;
    public GameObject shadow;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        isThink = true;

        renderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Invoke("Think", 2f);

        magmas.SetActive(true);
        healthbar.SetActive(true);
        Gbgm.SetActive(false);
        Bbgm.SetActive(true);
    }

    private void OnEnable()
    {
        poolmanager.SetActive(true);
        health = 1000;

    }
    private void Update()
    {

    }

    void Returnhit()
    {
        renderer.color = new Color(1, 1, 1, 1);
    }

    public void HitEnemy(int dam)
    {

        health -= dam;
        Instantiate(effect, transform.position, transform.rotation);
        if (health <= 0)
        {
            Die();
        }
        

        renderer.color = new Color(1, 0, 0, 0.5f);
        Invoke("Returnhit", 0.6f);
    }

    void StopThink()
    {
        isThink = false;
    }

    void Think()
    {
            if (isThink)
            {
                patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
                curPatternCount = 0;

                switch (patternIndex)
                {
                    case 0:
                        vinespawnAwarn.SetActive(true);
                        LongAttack_A();
                        break;
                    case 1:
                        LongAttack_B();
                        break;
                    case 2:
                        vinespawnBwarn.SetActive(true);
                        CloseAttack_A();
                        break;
                    case 3:
                        CloseAttack_B();
                        break;
                }
            }
        
    }
    void LongAttack_A()
    {
        closeattack = false;
        longattack = true;

        Invoke("VineA", 3f);
        vinespawnBwarn.SetActive(false);
        vinespawnB.SetActive(false);
        waterwallA.SetActive(false);
        waterwallB.SetActive(false);
        waterwallD.SetActive(false);
        waterwallAwarn.SetActive(false);
        waterwallBwarn.SetActive(false);
        waterwallCwarn.SetActive(true);
        Invoke("WaterC", 1f);
        waterwallDwarn.SetActive(false);
        //GameObject bulletR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.3f, transform.rotation);

        //Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();

        ////Vector3 dicVecR = player.transform.position - transform.position;
        //rigidR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);


        curPatternCount++;

        Invoke("Think", 10f);


    }

    void LongAttack_B()
    {
        closeattack = false;
        longattack = false;

        vinespawnBwarn.SetActive(false);
        vinespawnB.SetActive(false);
        waterwallA.SetActive(false);
        waterwallB.SetActive(false);
        waterwallC.SetActive(false);
        waterwallD.SetActive(false);
        waterwallAwarn.SetActive(false);
        waterwallBwarn.SetActive(false);
        waterwallCwarn.SetActive(false);
        waterwallDwarn.SetActive(false);

        int roundNumA = 15;
       anim.SetTrigger("attack");
        for (int index = 0; index < roundNumA; index++)
        {

            //GameObject bullet = Instantiate(bulletObjA, transform.position, Quaternion.identity);
            GameObject bullet = GameManager.Instance.pool.Get(0);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dicVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNumA),
                Mathf.Sin(Mathf.PI * 2 * index / roundNumA));

            rigid.AddForce(dicVec.normalized * 6, ForceMode2D.Impulse);

            Vector3 rotVec = Vector3.forward * 360 * index / roundNumA + Vector3.forward * 90;
            bullet.transform.Rotate(rotVec);
        }

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("LongAttack_B", 0.3f);

        else
            Invoke("Think", 3);
    }
    void CloseAttack_A()
    {
        closeattack = true;
        longattack = false;

        vinespawnAwarn.SetActive(false);
        vinespawnA.SetActive(false);

        Invoke("VineB", 3f);
        waterwallAwarn.SetActive(true);
        Invoke("WaterA", 1f);

        waterwallB.SetActive(false);
        waterwallC.SetActive(false);
        waterwallD.SetActive(false);
        waterwallBwarn.SetActive(false);
        waterwallCwarn.SetActive(false);
        waterwallDwarn.SetActive(false);

        curPatternCount++;

        Invoke("Think", 10f);
    }

    void CloseAttack_B()
    {
        closeattack = false;
        longattack = false;
        vinespawnAwarn.SetActive(false);
        vinespawnA.SetActive(false);

        //Invoke("VineB", 3f);
        waterwallAwarn.SetActive(false);
        waterwallBwarn.SetActive(false);
        waterwallCwarn.SetActive(false);
        waterwallDwarn.SetActive(false);
        waterwallA.SetActive(false);
        waterwallB.SetActive(false);
        waterwallC.SetActive(false);
        waterwallD.SetActive(false);

        anim.SetTrigger("attack");
        int roundNumA = 15;
        for (int index = 0; index < roundNumA; index++)
        {

            //GameObject bullet = Instantiate(bulletObjA, transform.position, Quaternion.identity);
            GameObject bullet = GameManager.Instance.pool.Get(0);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dicVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNumA),
                Mathf.Sin(Mathf.PI * 2 * index / roundNumA));

            rigid.AddForce(dicVec.normalized * 6, ForceMode2D.Impulse);

            Vector3 rotVec = Vector3.forward * 360 * index / roundNumA + Vector3.forward * 90;
            bullet.transform.Rotate(rotVec);
        }

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("CloseAttack_B", 0.3f);

        else
            Invoke("Think", 3);
    }

    private void VineA()
    {
        vinespawnA.SetActive(true);
        vinespawnAwarn.SetActive(false);
    }
    private void VineB()
    {
        vinespawnB.SetActive(true);
        vinespawnBwarn.SetActive(false);
    }
    private void WaterA()
    {
        if (closeattack)
        {
            waterwallA.SetActive(true);
            waterwallAwarn.SetActive(false);
            waterwallB.SetActive(false);
            waterwallBwarn.SetActive(false);
            Invoke("WaterAA", 1f);
        }
    }

    private void WaterAA()
    {
        waterwallA.SetActive(false);
        waterwallBwarn.SetActive(true);
        Invoke("WaterB", 1f);
    }
    private void WaterB()
    {
        if (closeattack)
        {
            waterwallB.SetActive(true);
            waterwallBwarn.SetActive(false);
            waterwallA.SetActive(false);
            waterwallAwarn.SetActive(false);
            Invoke("WaterBB", 1f);
        }
    }

    private void WaterBB()
    {
        waterwallB.SetActive(false);
        waterwallAwarn.SetActive(true);
        Invoke("WaterA", 1f);

    }
    private void WaterC()
    {
        if (longattack)
        {
            waterwallC.SetActive(true);
            waterwallCwarn.SetActive(false);
            waterwallD.SetActive(false);
            waterwallDwarn.SetActive(false);
            Invoke("WaterCC", 1f);
        }
    }

    private void WaterCC()
    {
        if (longattack)
        {
            waterwallC.SetActive(false);
            waterwallDwarn.SetActive(true);
            Invoke("WaterD", 1f);
        }
    }
    private void WaterD()
    {
        if (longattack)
        {
            waterwallD.SetActive(true);
            waterwallDwarn.SetActive(false);
            waterwallC.SetActive(false);
            waterwallCwarn.SetActive(false);
            Invoke("WaterDD", 1f);
        }
    }

    private void WaterDD()
    {
        if (longattack)
        {
            waterwallD.SetActive(false);
            waterwallCwarn.SetActive(true);
            Invoke("WaterC", 1f);
        }
    }
    void Die()
    {
        vinespawnAwarn.SetActive(false);
        vinespawnBwarn.SetActive(false);
        vinespawnA.SetActive(false);
        vinespawnB.SetActive(false);
        waterwallA.SetActive(false);
        waterwallB.SetActive(false);
        waterwallC.SetActive(false);
        waterwallD.SetActive(false);
        waterwallAwarn.SetActive(false);
        waterwallBwarn.SetActive(false);
        waterwallCwarn.SetActive(false);
        waterwallDwarn.SetActive(false);
        magmas.SetActive(false);
        healthbar.SetActive(false);
        Bbgm.SetActive(false);
        BHbgm.SetActive(true);

        StartCoroutine(_Die());
    }
    IEnumerator _Die()
    {
        WaitForSeconds delay = new WaitForSeconds(0.5f);
        Debug.Log("DIE !");

        player.transform.position = teleport.position;
        GameManager.Instance.key = false;

        yield return delay;
        shadow.SetActive(true);
        yield return delay;
        shadow.SetActive(false);
        yield return delay;
        shadow.SetActive(true);
        yield return delay;
        shadow.SetActive(false);
        yield return delay;
        shadow.SetActive(true);
        yield return delay;
        shadow.SetActive(false);


        Destroy(gameObject);
    }

}
