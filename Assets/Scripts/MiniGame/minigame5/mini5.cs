using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mini5 : MonoBehaviour
{
    public GameObject keys;
    

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.foxKill == 8)
        {
            keys.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="Player")
        {
            GameManager.Instance.key = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && GameManager.Instance.key == true)
        {
            Destroy(gameObject);
        }
    }

}
