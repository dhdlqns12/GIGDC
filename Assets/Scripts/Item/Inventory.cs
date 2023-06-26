using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   public List<Item_Data> items; //아이템을 담을 리스트

    [SerializeField]
    private Transform slotParent; //슬롯의 부모가 되는 오브젝트를 담는곳
    [SerializeField]
    private ItemSlot[] slots;//하위에 등록된 slot을 담을곳

    private void OnValidate()//Onvalidate는 유니티 에디터에서 바로 작동을 하는 역활
    {
        slots = slotParent.GetComponentsInChildren<ItemSlot>();
    }

    private void Awake() //게임이 시작되면 items에 들어있는 것들을 인벤토리에 넣어줌
    {
        FreshSlot();
    }

    public void FreshSlot()//아이템 갱신함수
    {
        int i = 0;
        for(;i<items.Count&&i<slots.Length;i++)
        {
            slots[i].item = items[i];
        }
        for(;i<slots.Length;i++)
        {
            slots[i].item = null;
        }
    }

    public void AddItem(Item_Data _item)
    {
        if(items.Count<slots.Length)
        {
            items.Add(_item);
            FreshSlot();
        }
        else
        {
            print("슬롯이 가득 차 있습니다.");
        }
    }
}
