using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public string enemyName;
    int damage;
    void Start()
    {

    }


    void Update()
    {
        //Invoke("Destroy", 0.7f);
        if (enemyName == "Mouse")
        {
            Invoke("off", 0.7f);
        }
        else if (enemyName == "Bat")
        {
            Invoke("off", 1f);
        }
        else if (enemyName == "Fox")
        {
            Invoke("off", 0.4f);
        }

        else if (enemyName == "Boss")
        {
            Invoke("off", 1.5f);
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
    public void off()
    {
        gameObject.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (enemyName == "Mouse")
            {
                damage = 1;
                other.GetComponent<PlayerController>().OnHit(damage);

            }
            else if (enemyName == "Bat")
            {
                damage = 2;
                other.GetComponent<PlayerController>().OnHit(damage);
            }
            else if (enemyName == "Fox")
            {
                damage = 3;
                other.GetComponent<PlayerController>().OnHit(damage);
            }

            else if (enemyName == "Boss")
            {
                damage = 5;
                other.GetComponent<PlayerController>().OnHit(damage);
            }
            gameObject.SetActive(false);
        }
    }
}
