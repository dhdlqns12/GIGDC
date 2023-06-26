using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Image image;

    private Item_Data _item;
    public Item_Data item
    {
        get { return _item; } //아이템 데이터 불러오기
        set
        {
            _item = value;
            if (_item != null) //아이템이 존재하지 않는다면
            {
                image.sprite = item.itemImage;
                image.color = new Color(1, 1, 1, 1);//아이템 이미지 불러오고 표시하기
            }
            else//존재한다면
            {
                image.color = new Color(1, 1, 1, 0);//투명화
            }
        }
    }
}
