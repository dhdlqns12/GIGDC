using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save1 : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && GameManager.Instance.key == true&&GameManager.Instance.isSave==true)
        {
            GameManager.Instance.GameSave();
            GameManager.Instance.isSave =false;
        }
    }
}
