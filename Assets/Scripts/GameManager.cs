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
    public GameObject wolf1;
    public MonsterController_Wolf wolfController;
    public BossController bossController;
    public GameObject boss;
    public bool isActive19=false;
    public GameObject dialogCollider1;
    public GameObject dialogCollider2;
    public GameObject dialogCollider3;
    public GameObject dialogCollider4;
    public GameObject dialogCollider5;
    public GameObject dialogCollider7;
    public GameObject dialogCollider6;
    public GameObject dialogCollider8;
    public GameObject dialogCollider9;
    public GameObject batSpawn;
    public GameObject dialogCollider10;
    public GameObject dialogCollider11;
    public GameObject dialogCollider13;
    public GameObject dialogCollider14;
    public GameObject dialogCollider16;
    public GameObject dialogCollider19;
    public GameObject miniGame2Go;
    public GameObject wolf2;
    public GameObject neighborAEvent;
    public GameObject foxKey;
    public GameObject foxSpawn;
    //public Transform foxSpawnPosition1;
    //public Vector3 foxSpawnVector1;
    //public Transform foxSpawnPosition2;
    //public Vector3 foxSpawnVector2;
    //public Transform foxSpawnPosition3;
    //public Vector3 foxSpawnVector3;
    //public Transform foxSpawnPosition4;
    //public Vector3 foxSpawnVector4;
    //public Transform foxSpawnPosition5;
    //public Vector3 foxSpawnVector5;
    //public Transform foxSpawnPosition6;
    //public Vector3 foxSpawnVector6;
    //public Transform foxSpawnPosition7;
    //public Vector3 foxSpawnVector7;
    //public Transform foxSpawnPosition8;
    //public Vector3 foxSpawnVector8;
    //public GameObject fox1;
    //public GameObject fox2;
    //public GameObject fox3;
    //public GameObject fox4;
    //public GameObject fox5;
    //public GameObject fox6;
    //public GameObject fox7;
    //public GameObject fox8;
    public mini5 _mini5;
    public GameObject dialogCollider15;
    public GameObject dialogCollider18;

    public bool dead = false;

    public GameObject mousespawn;

    private void Awake()    //싱글턴
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        Application.targetFrameRate = 30;
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
        if(dialogCollider19.activeSelf==false&&isActive19==false)
        {
            boss.SetActive(true);
            isActive19 = true;
        }

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
        hpSlider.value = Mathf.Lerp(hpSlider.value, playerController.health / 100, Time.deltaTime * 100);
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


        GameManager.Instance.dead = false;
        player.transform.position = new Vector3(x, y, 0);
        playerCamera.SetActive(true);
        playerCamera.transform.position = new Vector3(0, 0, -10);
        dialogCollider1.SetActive(true);
        dialogCollider2.SetActive(true);
        dialogCollider3.SetActive(true);
        dialogCollider4.SetActive(true);
        dialogCollider5.SetActive(false);
        dialogCollider6.SetActive(true);
        dialogCollider7.SetActive(true);
        dialogCollider8.SetActive(true);
        //dialogCollider9.SetActive(true);
        dialogCollider11.SetActive(true);
        dialogCollider13.SetActive(true);
        dialogCollider16.SetActive(false);
        dialogCollider7.SetActive(true);
        playerController.isbroom = isBroom;
        playerController.isslingshot = isSlingShot;
        playerController.isaxe = isAxe;
        batSpawn.SetActive(false);
        dialogCollider18.SetActive(true);
        mousespawn.SetActive(false);
        foxSpawn.SetActive(false);
        dialogCollider19.SetActive(true);

        boss.SetActive(false);
        isActive19 = false;
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
        if (!playerController.isbroom)
        {
            broom.SetActive(false);
        }
        if (playerController.isslingshot)
        {
            slingshot.SetActive(true);
        }
        if (!playerController.isslingshot)
        {
            slingshot.SetActive(false);
        }
        if (playerController.isaxe)
        {
            axe.SetActive(true);
        }
        if (!playerController.isaxe)
        {
            axe.SetActive(false);
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
        plcr.health = 100f;
        plcr.anim.SetTrigger("Delay");
        Time.timeScale = 1;
        plcr.spriteRenderer.color = new Color(1, 1, 1, 1);
        gameOverUI.SetActive(false);
        key = true;
        fruitCount = 0;
        wolf1.SetActive(false);
        wolf2.SetActive(false);
        wolf1.transform.position = wolfController.originVector1;
        wolf2.transform.position = wolfController.originVector2;
        dialogCollider10.SetActive(false);
        miniGame2Go.SetActive(true);
        mazeCount = 0;
        neighborAEvent.SetActive(true);
        foxKill = 0;
        _mini5.isSet = false;
        foxKey.SetActive(false);
        foxKey.transform.GetChild(0).gameObject.SetActive(true);
        foxKey.transform.GetChild(1).gameObject.SetActive(true);
        foxKey.transform.GetChild(2).gameObject.SetActive(true);
        foxKey.transform.GetChild(3).gameObject.SetActive(false);
        foxKey.transform.GetChild(4).gameObject.SetActive(false);
        dialogCollider14.SetActive(true);
        mousespawn.SetActive(false);
        mousespawn.transform.GetChild(0).gameObject.SetActive(false);
        mousespawn.transform.GetChild(1).gameObject.SetActive(false);
        mousespawn.transform.GetChild(2).gameObject.SetActive(false);
        mousespawn.transform.GetChild(3).gameObject.SetActive(false);
        mousespawn.transform.GetChild(4).gameObject.SetActive(false);
        mousespawn.transform.GetChild(5).gameObject.SetActive(false);
        mousespawn.transform.GetChild(6).gameObject.SetActive(false);
        batSpawn.SetActive(false);
        batSpawn.transform.GetChild(0).gameObject.SetActive(true);
        batSpawn.transform.GetChild(1).gameObject.SetActive(true);
        batSpawn.transform.GetChild(2).gameObject.SetActive(true);
        batSpawn.transform.GetChild(3).gameObject.SetActive(true);
        batSpawn.transform.GetChild(4).gameObject.SetActive(true);
        batSpawn.transform.GetChild(5).gameObject.SetActive(true);
        batSpawn.transform.GetChild(6).gameObject.SetActive(true);
        batSpawn.transform.GetChild(7).gameObject.SetActive(true);
        batSpawn.transform.GetChild(8).gameObject.SetActive(true);
        batSpawn.transform.GetChild(9).gameObject.SetActive(true);
        batSpawn.transform.GetChild(10).gameObject.SetActive(true);
        batSpawn.transform.GetChild(11).gameObject.SetActive(true);

        foxSpawn.SetActive(false);
        foxSpawn.transform.GetChild(0).gameObject.SetActive(true);
        foxSpawn.transform.GetChild(1).gameObject.SetActive(true);
        foxSpawn.transform.GetChild(2).gameObject.SetActive(true);
        foxSpawn.transform.GetChild(3).gameObject.SetActive(true);
        foxSpawn.transform.GetChild(4).gameObject.SetActive(true);
        foxSpawn.transform.GetChild(5).gameObject.SetActive(true);
        foxSpawn.transform.GetChild(6).gameObject.SetActive(true);
        foxSpawn.transform.GetChild(7).gameObject.SetActive(true);
    }

    public void GameOver_Menu()
    {
        SceneManager.LoadScene("GameMain");
        Time.timeScale = 1;
    }
}
