using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   
    public float bulletSpeed = 15f;
    // Start is called before the first frame update
    void Start()
    {

        Destroy(gameObject, 5);
    }
    void Update()
    {
        
        //if (transform.rotation.y == 0)
        //{
        //    transform.Translate(transform.right * bulletSpeed * Time.deltaTime);
        //}
        //else
        //{
        //    transform.Translate(transform.right * -1 * bulletSpeed * Time.deltaTime);

        //}
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
        {
            collision.GetComponent<MonsterController>().HitEnemy(1);
            Debug.Log("-1");
            Destroy(gameObject);
        }
    }
}
