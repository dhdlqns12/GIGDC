using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterwall : MonoBehaviour
{
    AudioSource audiosource;
    int damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && GameManager.Instance.dead == false)
        {
            other.GetComponent<PlayerController>().OnHit(damage);
        }
    }
}
