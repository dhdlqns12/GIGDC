using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item_Data", menuName = "Scriptable Object/item_Data", order = int.MaxValue)]
//생성메뉴바에 item_Data라는 scriptableobject를 생성하는 메뉴를 생성
public class item_Data : ScriptableObject
{
    [SerializeField]
    private int idx;//아이템의 인덱스
    public int IDX { get { return idx; } }
    [SerializeField]
    private string item_name;//아이템이름
    public string Item_name { get { return item_name; } }
    [SerializeField]
    private int price;//가격
    public int Price { get { return price; } }
    [SerializeField]
    private int hpregen;//체력
    public int HpRegen { get { return hpregen; } }
    [SerializeField]
    private int damage;//데미지
    public int Damage { get { return damage; } }
    [SerializeField]
    private float attackSpeed;//공격속도
    public float AttackSpeed { get { return attackSpeed; } }
}
