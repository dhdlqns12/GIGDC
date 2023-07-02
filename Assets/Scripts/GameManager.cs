using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PoolManager pool;
    public GameObject player;   //플레이어 연결
    public GameObject miniG;    //미니게임 이동 오브젝트
    public Transform cameraPos; //카메라 시점 변경
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

    //아이템 표시 UI
    public GameObject broom;
    public GameObject slingshot;
    public GameObject axe;
    public GameObject potioncool;

    public bool isSave = false;
    public GameObject saveUI;
    public PlayerController plcr;
    public GameObject gameOverUI;
    public bool save4 = false;

    private void Awake()    //싱글턴
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        GameLoad();
    }

    public bool key = true;     // 함정발동 변수

    public int fruitCount = 0;  // 미니게임 변수
    public bool goMini = false; // 미니게임 이동변수

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
        ItemShow();
        //if (fruitCount == 4)    //미니게임에서 메인게임으로
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

    public void GameSave()
    {
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.Save();
        PlayerPrefs.SetFloat("PlayerCameraPosX", Camera.main.transform.position.x);
        PlayerPrefs.SetFloat("PlayerCameraPosY", Camera.main.transform.position.y);
        //PlayerPrefs.SetInt("isBroom", System.Convert.ToInt32(playerController.isbroom));
        //PlayerPrefs.SetInt("isSlingShot", System.Convert.ToInt32(playerController.isslingshot));
        //PlayerPrefs.SetInt("isAxe", System.Convert.ToInt32(playerController.isaxe));
    }

    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
            return;

        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        float cameraX = PlayerPrefs.GetFloat("PlayerCameraPosX");
        float CameraY = PlayerPrefs.GetFloat("PlayerCameraPosY");
        bool isBroom = System.Convert.ToBoolean(PlayerPrefs.GetInt("isBroom"));
        bool isSlingShot = System.Convert.ToBoolean(PlayerPrefs.GetInt("isSlingShot"));
        bool isAxe = System.Convert.ToBoolean(PlayerPrefs.GetInt("isAxe"));



        player.transform.position = new Vector3(x, y, 0);
        playerCamera.SetActive(true);
        playerCamera.transform.position = new Vector3(0, 0, -10);
        playerController.isbroom = isBroom;
        playerController.isslingshot = isSlingShot;
        playerController.isaxe = isAxe;
    }

    public void SaveYes()
    {
        isSave = true;
        saveUI.SetActive(false);
        Time.timeScale = 1;
    }
    public void SaveNo()
    {
        isSave = false;
        saveUI.SetActive(false);
        Time.timeScale = 1;
    }

    private void ItemShow()
    {
        if (playerController.isbroom)
        {
            broom.SetActive(true);
        }
        if (playerController.isslingshot)
        {
            slingshot.SetActive(true);
        }
        if (playerController.isaxe)
        {
            axe.SetActive(true);
        }
        if (!playerController.ispotion)
        {
            potioncool.SetActive(true);
        }
        if (playerController.ispotion)
        {
            potioncool.SetActive(false);
        }

    }

    public void GameOver_Load()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
        {
            SceneManager.LoadScene("SampleScene");
        }
        else if (PlayerPrefs.HasKey("PlayerX"))
        {
            GameLoad();
        }
        plcr.health = 10f;
        plcr.anim.SetTrigger("Delay");
        Time.timeScale = 1;
        plcr.spriteRenderer.color = new Color(1, 1, 1, 1);
        gameOverUI.SetActive(false);

    }

    public void GameOver_Menu()
    {
        SceneManager.LoadScene("GameMain");
        Time.timeScale = 1;
    }
}
