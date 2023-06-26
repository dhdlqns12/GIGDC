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
        get { return _item; } //������ ������ �ҷ�����
        set
        {
            _item = value;
            if (_item != null) //�������� �������� �ʴ´ٸ�
            {
                image.sprite = item.itemImage;
                image.color = new Color(1, 1, 1, 1);//������ �̹��� �ҷ����� ǥ���ϱ�
            }
            else//�����Ѵٸ�
            {
                image.color = new Color(1, 1, 1, 0);//����ȭ
            }
        }
    }
}
