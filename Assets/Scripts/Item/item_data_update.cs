using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class item_data_update : MonoBehaviour
{
    [SerializeField]
    public GameObject shop;//상위부모 오브젝트
    public item_Data I_D;
    public Button button;
    public TextMeshProUGUI text;
    private float x;
    private float y;
    private float z;


    public item_Data item_data { set { I_D = value; } }
    void Start()
    {
        shop = this.transform.parent.parent.parent.parent.gameObject;
        this.GetComponent<Image>().sprite = Resources.Load<Sprite>(I_D.Item_name);
        this.GetComponent<Button>().onClick.AddListener(printitem_info);
        this.GetComponentInChildren<Text>().text = I_D.Item_name;
        x = this.gameObject.transform.position.x;
        y = this.gameObject.transform.position.y;
        z = this.gameObject.transform.position.z;
        this.transform.position = new Vector3(x, y - 30, z);//아이템의 위치가 콘텐츠의 천장에 닿은부분을 뺴주기위해 넣음
    }
    public void printitem_info()
    {
        shop.GetComponent<Shop>().sel_item_image.sprite = Resources.Load<Sprite>(I_D.Item_name);//아이템 이미지를 출력
        shop.GetComponent<Shop>().sel_item_price.text = "price:" + I_D.Price.ToString();//아이템의 가격을 출력
    }
}
