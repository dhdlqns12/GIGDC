using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static bool isChangeWeopon=false;//���� ���� ���� üũ

    public static GameObject[] weapons;//weapons ���ӿ�����Ʈ�迭


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isChangeWeopon)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                WeaponChainging("hand");
            }

            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                WeaponChainging("axe");
            }

            else if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                WeaponChainging("birdGun");
            }
        }
    }

    public IEnumerator WeaponChainging(string _type)
    {
        isChangeWeopon = true;

        //���� ��ü �ִϸ��̼� ���Ժκ�

        WeaponChange(_type);

        yield return isChangeWeopon=false;
    }

    void WeaponChange(string _type)
    {
        if(_type=="hand")
        {
            //hand��ũ��Ʈ �ҷ�����
        }
        else if(_type== "axe")
        {
            //axe��ũ��Ʈ �ҷ�����
        }
        else if(_type=="birdGun")
        {
            //birdGun��ũ��Ʈ �ҷ�����
        }
    }
}
