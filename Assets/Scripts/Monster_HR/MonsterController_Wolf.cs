using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController_Wolf : MonoBehaviour
{
    public Transform player;
    float speed = 2f;
    public float range = 10f;

    Animator anim;
    public Vector2 moveDiriection = new Vector2(1, 0);

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
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
