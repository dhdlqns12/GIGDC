using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item_Data", menuName = "Scriptable Object/item_Data", order = int.MaxValue)]
//�����޴��ٿ� item_Data��� scriptableobject�� �����ϴ� �޴��� ����
public class item_Data : ScriptableObject
{
    [SerializeField]
    private int idx;//�������� �ε���
    public int IDX { get { return idx; } }
    [SerializeField]
    private string item_name;//�������̸�
    public string Item_name { get { return item_name; } }
    [SerializeField]
    private int price;//����
    public int Price { get { return price; } }
    [SerializeField]
    private int hpregen;//ü��
    public int HpRegen { get { return hpregen; } }
    [SerializeField]
    private int damage;//������
    public int Damage { get { return damage; } }
    [SerializeField]
    private float attackSpeed;//���ݼӵ�
    public float AttackSpeed { get { return attackSpeed; } }
}
