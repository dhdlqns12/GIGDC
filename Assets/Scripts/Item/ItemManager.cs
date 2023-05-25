using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject[] Item;
    public enum Weopon_Type//enum으로 선언된 무기 정보
    {
        hand,    //주먹
        birdGun, //원거리무기
        axe,     //근접 무기
        potion   //물약
    }

    public int attackPower = 0; //무기 공격력
    public float attackDelay = 0; //무기 공격속도
    public float regenHp = 0; //HP회복량

    public Weopon_Type wp_type = Weopon_Type.hand;//enum Weopon_Type wp_type

    void Start()
    {

    }


    void Update()
    {
        ItemSetting();
    }

    void ItemSetting()//아이템 세팅
    {
     switch(wp_type)//enum wp_type으로 아이템 정보 설정
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

    void SetAttackSpeed(float speed)//매개변수 speed로 공격속도 설정
    {
        attackDelay = 1f/speed; //speed가 커질 수록 공격속도 증가
        attackDelay -= Time.deltaTime;
        if(attackDelay <= 0)//attacjspeed가 1보다 크다면 공격
        {
            Attack(3);
        }
    }
    void Attack(int power)//매개변수 power로 공격력 설정
    {
        attackPower = power;
        return;
    }
}
