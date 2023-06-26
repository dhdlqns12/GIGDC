using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   public List<Item_Data> items; //�������� ���� ����Ʈ

    [SerializeField]
    private Transform slotParent; //������ �θ� �Ǵ� ������Ʈ�� ��°�
    [SerializeField]
    private ItemSlot[] slots;//������ ��ϵ� slot�� ������

    private void OnValidate()//Onvalidate�� ����Ƽ �����Ϳ��� �ٷ� �۵��� �ϴ� ��Ȱ
    {
        slots = slotParent.GetComponentsInChildren<ItemSlot>();
    }

    private void Awake() //������ ���۵Ǹ� items�� ����ִ� �͵��� �κ��丮�� �־���
    {
        FreshSlot();
    }

    public void FreshSlot()//������ �����Լ�
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
            print("������ ���� �� �ֽ��ϴ�.");
        }
    }
}
