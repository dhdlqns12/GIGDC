using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public string enemyName;
    public int damage;
    void Start()
    {
        
    }

    
    void Update()
    {
        Invoke("Destroy", 0.7f);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().OnHit(damage);
        }
    }
}
