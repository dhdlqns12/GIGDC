using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mini5 : MonoBehaviour
{
    public GameObject keys;
    public GameObject dialogCollider15;
    public bool isOpen = false;
    public bool isSet = false;

    // Update is called once per frame
    void Update()
    {
        DialogActive();

        if (isOpen==true&&isSet==false)
        {
            dialogCollider15.SetActive(true);
            isSet = true;
        }
        if(dialogCollider15.activeSelf==false&&isSet==true)
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

    public void DialogActive()
    {
        if(GameManager.Instance.foxKill==8)
        {
            isOpen = true;
        }
    }

}
