using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint1 : MonoBehaviour
{
    public GameObject saveUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" && GameManager.Instance.key == true)
        {
            saveUI.SetActive(true);
            Time.timeScale = 0;
        }
    }

}
