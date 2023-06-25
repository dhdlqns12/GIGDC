using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController_Wolf : MonoBehaviour
{
    public Transform player;
    float speed = 5f;
    public float range = 10f;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= range)
        {
            //transform.LookAt(player);

            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}
