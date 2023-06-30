using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PoolManager pool;
    public GameObject player;   //�÷��̾� ����
    public GameObject miniG;    //�̴ϰ��� �̵� ������Ʈ
    public Transform cameraPos; //ī�޶� ���� ����
    public bool cameraFix = false;
    public bool cameraFollow = false;

    public GameObject mainCamera;
    public GameObject playerCamera;

    public int mazeCount = 0;
    public int foxKill = 0;

    public GameObject IMG;
    public Button BT;
    PlayerController playerController;
    public float hp;
    public Slider hpSlider;


    private void Awake()    //�̱���
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public bool key = true;     // �����ߵ� ����

    public int fruitCount = 0;  // �̴ϰ��� ����
    public bool goMini = false; // �̴ϰ��� �̵�����

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IMG.SetActive(true);

            if (IMG == true)
            {
                Time.timeScale = 0f;
            }
        }

        Hp_Bar();
        //if (fruitCount == 4)    //�̴ϰ��ӿ��� ���ΰ�������
        //{
        //    player.SetActive(true);
        //    miniG.SetActive(false);
        //    mainCamera.SetActive(false);
        //    playerCamera.SetActive(true);

        //    Camera.main.transform.position = new Vector3(cameraPos.position.x, cameraPos.position.y, -10);

        //}

    }
    private void Hp_Bar()
    {
        hpSlider.value = Mathf.Lerp(hpSlider.value, playerController.health / 10, Time.deltaTime * 100);
    }

}
