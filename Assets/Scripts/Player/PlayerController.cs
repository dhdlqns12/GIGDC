using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxShotDelay = 0.5f;  //���Ÿ� ����
    public float curShotDelay;
    public float curTime;      //�ٰŸ� ����
    public float coolTime = 0.5f;
    public float weapon;

    public float health = 10f;

    public Transform pos; //�������� ���� ���� ����1
    public Vector2 boxSize;  //�������� ���� ���� ����2
    public Transform target;
    public float speed;
    private Vector3 vector;

    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;

    public int walkCount;
    private int currentWalkCount;

    private bool canMove = true;

    void Update()
    {

        Attack();
        Reload();
        Die();

        // ���� ����Ű�� -1, ���� ����Ű�� 1, ���� ����Ű�� 1, ���� ����Ű�� -1
        // ��ư�� ������ �� ����

        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }

        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector3.zero;   //�浹�ÿ� ������ �и� ����
    }

    void Attack()
    {
        if (!Input.GetKey(KeyCode.Space))
            return;

        if (curShotDelay < maxShotDelay)
            return;

        switch (weapon)
        {
            case 1:

                HandAttack();


                break;

            case 2:



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

            if (Input.GetKey(KeyCode.Space))
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

    void Reload()
    {
        curShotDelay += Time.deltaTime;
        curTime -= Time.deltaTime;
    }

    public void OnHit(int dmg)
    {
        health -= dmg;
    }

    public void Die()
    {
        if (health <= 0)
            Destroy(gameObject);
    }

    IEnumerator MoveCoroutine()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            applyRunSpeed = runSpeed;
            applyRunFlag = true;
        }
        else
        {
            applyRunSpeed = 0;
            applyRunFlag = false;
        }
        vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

        while (currentWalkCount < walkCount)
        {
            if (vector.x != 0)
            {
                transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
            }
            else if (vector.y != 0)
            {
                transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
            }

            if (applyRunFlag)
            {
                currentWalkCount++;
            }
            currentWalkCount++;
            yield return new WaitForSeconds(0.05f);
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
    }
}
