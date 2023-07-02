using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save4 : MonoBehaviour
{
    PlayerController playerController;

    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && GameManager.Instance.key == true && GameManager.Instance.isSave == true)
        {
            GameManager.Instance.GameSave();
            PlayerPrefs.SetInt("isBroom", System.Convert.ToInt32(playerController.isbroom));
            PlayerPrefs.SetInt("isSlingShot", System.Convert.ToInt32(playerController.isslingshot));
            PlayerPrefs.SetInt("isAxe", System.Convert.ToInt32(playerController.isaxe));
            GameManager.Instance.save4 = true;
            GameManager.Instance.isSave = false;
        }
    }
}
