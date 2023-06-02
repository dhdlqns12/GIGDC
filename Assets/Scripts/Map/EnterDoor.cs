using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDoor : MonoBehaviour
{
    public GameObject nextDoor;
    public GameObject mainCamera;
    public Transform cameraPos;
    public GameObject playerCamera;

    public int value = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && GameManager.Instance.key == true)
        {
            collision.gameObject.transform.position = nextDoor.transform.position;

            if (value == 0)
            {
                mainCamera.SetActive(false);
                playerCamera.SetActive(true);
            }
            else if(value == 1)
            {
                mainCamera.SetActive(true);
                playerCamera.SetActive(false);
            }

            Camera.main.transform.position = new Vector3(cameraPos.position.x, cameraPos.position.y, -10);


        }
    }

}
