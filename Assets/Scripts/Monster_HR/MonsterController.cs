using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public string enemyName;
    public const float moveSpeed = 0.8f;
    public int health;
    public float maxShotDelay;
    public float curShotDelay;
    public GameObject player;
    public GameObject bulletObjA;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        Reload();
    }

    void Fire()
    {

        if (curShotDelay < maxShotDelay)
            return;

        if (enemyName == "A")
        {
            GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dicVec = player.transform.position - transform.position;
            rigid.AddForce(dicVec * 0.3f, ForceMode2D.Impulse);
        }

        curShotDelay = 0;
    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
}
