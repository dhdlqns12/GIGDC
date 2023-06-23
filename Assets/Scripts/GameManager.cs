using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;   //플레이어 연결
    public GameObject miniG;    //미니게임 이동 오브젝트
    public Transform cameraPos; //카메라 시점 변경
    public bool cameraFix = false;
    public bool cameraFollow = false;

    public GameObject mainCamera;
    public GameObject playerCamera;


    private void Awake()    //싱글턴
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

    }

    public bool key = true;     // 함정발동 변수

    public int fruitCount = 0;  // 미니게임 변수
    public bool goMini = false; // 미니게임 이동변수

    private void Update()
    {

        //if (fruitCount == 4)    //미니게임에서 메인게임으로
        //{
        //    player.SetActive(true);
        //    miniG.SetActive(false);
        //    mainCamera.SetActive(false);
        //    playerCamera.SetActive(true);

        //    Camera.main.transform.position = new Vector3(cameraPos.position.x, cameraPos.position.y, -10);

        //}

    }

}
