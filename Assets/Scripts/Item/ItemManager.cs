using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject[] Item;
    public enum Weopon_Type//enum���� ����� ���� ����
    {
        hand,    //�ָ�
        birdGun, //���Ÿ�����
        axe,     //���� ����
        potion   //����
    }

    public int attackPower = 0; //���� ���ݷ�
    public float attackDelay = 0; //���� ���ݼӵ�
    public float regenHp = 0; //HPȸ����

    public Weopon_Type wp_type = Weopon_Type.hand;//enum Weopon_Type wp_type

    void Start()
    {

    }


    void Update()
    {
        ItemSetting();
    }

    void ItemSetting()//������ ����
    {
     switch(wp_type)//enum wp_type���� ������ ���� ����
        {
            case Weopon_Type.hand:
                SetAttackSpeed(1);
                break;
            case Weopon_Type.birdGun:
                break;
            case Weopon_Type.axe:
                break;
            case Weopon_Type.potion:
                break;
        }
    }

    void SetAttackSpeed(float speed)//�Ű����� speed�� ���ݼӵ� ����
    {
        attackDelay = 1f/speed; //speed�� Ŀ�� ���� ���ݼӵ� ����
        attackDelay -= Time.deltaTime;
        if(attackDelay <= 0)//attacjspeed�� 1���� ũ�ٸ� ����
        {
            Attack(3);
        }
    }
    void Attack(int power)//�Ű����� power�� ���ݷ� ����
    {
        attackPower = power;
        return;
    }
}
