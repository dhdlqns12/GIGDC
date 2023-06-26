using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static bool isChangeWeopon=false;//무기 변경 여부 체크

    public static GameObject[] weapons;//weapons 게임오브젝트배열


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

        //무기 교체 애니메이션 삽입부분

        WeaponChange(_type);

        yield return isChangeWeopon=false;
    }

    void WeaponChange(string _type)
    {
        if(_type=="hand")
        {
            //hand스크립트 불러오기
        }
        else if(_type== "axe")
        {
            //axe스크립트 불러오기
        }
        else if(_type=="birdGun")
        {
            //birdGun스크립트 불러오기
        }
    }
}
