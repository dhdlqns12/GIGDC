using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.5f);
    }
    private void OnEnable()
    {
        Destroy(gameObject, 1f);
    }
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            collision.GetComponent<MonsterController>().HitEnemy(5);
            Debug.Log("-1");
            gameObject.SetActive(false);
        }
        if (collision.tag == "Boss")
        {
            collision.GetComponent<BossController>().HitEnemy(5);
            Debug.Log("-1");
            gameObject.SetActive(false);
        }
    }

    void bulletoff()
    {
        gameObject.SetActive(false);
    }
}
