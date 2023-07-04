using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController_Wolf : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public float range = 20f;
    public Transform originTransform1;
    public Vector3 originVector1;
    public Transform originTransform2;
    public Vector3 originVector2;
    public GameObject wolf1;
    public GameObject wolf2;

    Animator anim;
    public Vector2 moveDiriection = new Vector2(1, 0);

    private void Start()
    {
        originTransform1.position = wolf1.transform.position;
        anim = GetComponent<Animator>();
        originVector1 = new Vector3(originTransform1.position.x, originTransform1.position.y, 0);
        originTransform2.position = wolf2.transform.position;
        originVector2 = new Vector3(originTransform2.position.x, originTransform2.position.y, 0);
    }

    void Update()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector3.zero; 

        Invoke("Follow", 3f);

    }

    void Follow()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= range)
        {
            //transform.LookAt(player);

            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }

        Vector3 dir = player.position - transform.position;

        if (!Mathf.Approximately(dir.x, 0) || !Mathf.Approximately(dir.y, 0))
        {
            moveDiriection.Set(dir.x, dir.y);
            moveDiriection.Normalize();
        }

        anim.SetFloat("xDir", moveDiriection.x);
        anim.SetFloat("yDir", moveDiriection.y);
        anim.SetFloat("speed", dir.magnitude);


    }
}
