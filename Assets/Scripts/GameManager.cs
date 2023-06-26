using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;   //�÷��̾� ����
    public GameObject miniG;    //�̴ϰ��� �̵� ������Ʈ
    public Transform cameraPos; //ī�޶� ���� ����
    public bool cameraFix = false;
    public bool cameraFollow = false;

    public GameObject mainCamera;
    public GameObject playerCamera;

    public int mazeCount = 0;


    private void Awake()    //�̱���
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

    }

    public bool key = true;     // �����ߵ� ����

    public int fruitCount = 0;  // �̴ϰ��� ����
    public bool goMini = false; // �̴ϰ��� �̵�����

    private void Update()
    {

        //if (fruitCount == 4)    //�̴ϰ��ӿ��� ���ΰ�������
        //{
        //    player.SetActive(true);
        //    miniG.SetActive(false);
        //    mainCamera.SetActive(false);
        //    playerCamera.SetActive(true);

        //    Camera.main.transform.position = new Vector3(cameraPos.position.x, cameraPos.position.y, -10);

        //}

    }

}
