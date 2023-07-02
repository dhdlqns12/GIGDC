using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save1 : MonoBehaviour
{
    PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && GameManager.Instance.key == true&&GameManager.Instance.isSave==true)
        {
            GameManager.Instance.GameSave();
            PlayerPrefs.SetInt("isBroom", System.Convert.ToInt32(playerController.isbroom));
            GameManager.Instance.isSave =false;
        }
    }
}
