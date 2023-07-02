using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint4 : MonoBehaviour
{
    public GameObject saveUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" && GameManager.Instance.key == true&&GameManager.Instance.save4==false)
        {
            saveUI.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
