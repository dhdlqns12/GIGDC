using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public TextMeshProUGUI got_gold;
    public TextMeshProUGUI sel_item_price;
    public Image sel_item_image;
    [SerializeField]
    private List<item_Data> item_data_list; //아이템 데이터 생성한것을 받아옴
    public GameObject shopPanel; // 아이템목록을 보여주는 패널

    void Start()//씬이 시작될떄 호출
    {
        int j = 0;//가로줄
        int k = 0;//세로줄
        int l = 5;//가로줄
        //아이템 리스트 만큼 객체생성
        for (int i = 0; 2 * k < item_data_list.Count; i++)//아이템갯수만큼 생성  //item_data_list.Count=3임 (현재) 아이템 카운트가 만약 10개라면 그리고 가로줄에 2개 한다면  1 2 1 2 1 2 1 2 총 5줄
        {
            for (j = 0; j < l; j++)//가로줄
            {
                GameObject item = Instantiate(Resources.Load<GameObject>("item"));//리소스폴더에 아이템이미지(리소스)넣어주면 해결됨(이름 같게해야됨)
                item.GetComponent<item_data_update>().I_D = item_data_list[j + k * l];
                item.transform.SetParent(shopPanel.transform);
                item.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                item.transform.localPosition = new Vector3(-130 + 60 * j, -60 * k, 0);//간격 생성
                item.GetComponent<item_data_update>().item_data = item_data_list[j + k * l];
            }
            k++;
        }
        this.gameObject.SetActive(false);//씬이호출될때 표시되지않게함
    }

    public void CloseTab() //X버튼 눌럿을때 닫는 메소드
    {
        GameObject parent1 = transform.parent.gameObject;

        parent1.SetActive(false);
    }
}
