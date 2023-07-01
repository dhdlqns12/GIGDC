using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdGun_mini : MonoBehaviour
{
    float speed = 2.0f;

    public GameObject bullet;

    public GameObject mainCamera;
    public GameObject playerCamera;
    public GameObject player;   //�÷��̾� ����
    public GameObject miniG;    //�̴ϰ��� �̵� ������Ʈ
    public GameObject dialogCollider10;

    public GameObject wolf1;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");

        Vector3 pos = transform.position;
        pos.x += h * Time.deltaTime * speed;

        transform.position = pos;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bulletPrefab = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + 1.5f, 0), Quaternion.identity);
            Rigidbody2D bulletShot = bulletPrefab.GetComponent<Rigidbody2D>();
            bulletShot.AddForce(Vector2.up * 2, ForceMode2D.Impulse);
        }

        if (GameManager.Instance.fruitCount == 4)    //�̴ϰ��ӿ��� ���ΰ�������
        {
            player.SetActive(true);
            miniG.SetActive(false);
            mainCamera.SetActive(false);
            playerCamera.SetActive(true);
            dialogCollider10.SetActive(true);
            wolf1.SetActive(true);
            //Camera.main.transform.position = new Vector3(cameraPos.position.x, cameraPos.position.y, -10);

        }

    }
}
