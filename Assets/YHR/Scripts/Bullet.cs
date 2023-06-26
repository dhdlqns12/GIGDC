using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {

        //Destroy(gameObject, 3f);
    }
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 Direction, float Speed)
    {
        rigidbody2D.AddForce(Direction * Speed);
    }
    void Update()
    {


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            collision.GetComponent<MonsterController>().HitEnemy(1);
            Debug.Log("-1");
            Destroy(gameObject);
        }
        if (collision.tag == "Boss")
        {
            collision.GetComponent<BossController>().HitEnemy(1);
            Debug.Log("-1");
            Destroy(gameObject);
        }
    }
}
